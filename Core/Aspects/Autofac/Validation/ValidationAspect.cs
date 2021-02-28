using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception // Aspect -- Method'un başında sonunda veya hata verdiğinde çalışacak yapı
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            // defensive coding -- savunma odaklı kodlama
            // gönderdiğimiz veri IValidator değilse hata verecek kodu yazdık.
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil!");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            // çalışma anında _validatorType'ın (yani gönderdiğimiz verininin -- ProductValidator) instance'ını alır.
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            // invocation = method
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
