using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Wep API: Angular, IOS, Android gibi UI'ların (client'ların) projemizi kullanabilmesi (anlaması) için bu standart yapıyı kullanırız. Wbp API Restful service'dir. Genellikle JSON formatıyla çalışır. Wep API HTTP istekleri üzerinden yapılır. Kullanıcılar bize Request (istekler)'de bulunurlar. Örneğin Ürünleri listele gibi. Bizim ona verdiğimiz yanıt Response (cevap)'dur.

// ASP.NET .NET projelerindeki web projelerine verilen isim. 

// Resftful yapılar karşımıza HTTP protokolü ile geliyor. Controller sitemimizi kullancak olan client'ların (tarayıcı, mobil uygulama, angular gibi) bize hangi operasyonlar için ve nasıl isteklerde bulunabileceklerini oluşturduğumuz yer.

namespace WepAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
