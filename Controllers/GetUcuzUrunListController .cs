using EnUcuzUrun.Marketler;
using EnUcuzUrun.Models;
using EnUcuzUrun.Net5.Res.Api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EnUcuzUrun.Net5.Res.Api.Controllers
{
    public class GetUcuzUrunListController : ControllerBase
    {

        private IWebHostEnvironment _hostingEnvironment;

        public GetUcuzUrunListController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [Route("GetProductBySearch/{AranacakUrun}")]
        public async  Task<Response<Product>> GetProductBySearch(string AranacakUrun)
        {

            var model = new HomeViewModel()
            {
                LogoUrls = Utilities.MarketLogoUrls,
                Products = new List<Product>()
            };

            List<Market> markets = new()
            {
                new MarketA101 { SearchText = AranacakUrun },
              //  new MarketBizim { SearchText = product }, // Hata veriyor
                new MarketCagri { SearchText = AranacakUrun },
                new MarketHakmar { SearchText = AranacakUrun },
                new MarketMopas { SearchText = AranacakUrun  },
                new MarketHappyCenter { SearchText = AranacakUrun }
            };
            List<Task> taskList = markets.Select(market => new Task(market.LoadProducts)).ToList();
            taskList.ForEach(t => t.Start());
            await Task.WhenAll(taskList);
            markets.ForEach(market => model.Products.AddRange(market.Products));


            if (AranacakUrun != null)
            {
                return new Success<Product>(model.Products, "İşlem Başarılı", model.Products.Count + " Ürün Bulundu");
            }
            else
            {
                return new Failure<Product>("Veriye Ulaşılamadı.");
            }

        }

        private List<T> ReadAll<T>(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                var data = JsonConvert.DeserializeObject<List<T>>(sr.ReadToEnd());

                return data;
            }

        }

        private List<T> Read<T>(string path, Func<T, bool> f)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                var data = JsonConvert.DeserializeObject<List<T>>(sr.ReadToEnd()).Where(f).ToList();

                return data;
            }

        }

    }
}
