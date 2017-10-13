using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SystemetSida.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using SystemetSida.Models;
using unirest_net.http;

namespace SystemetSida.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Search(string beverage_name)
        {

            string getStr = "https://karlroos-systemet.p.mashape.com/product?name=" + beverage_name;
            
            HttpResponse<string> response = Unirest.get(getStr)
            .header("X-Mashape-Key", "KlCvBh2xRUmshTOZHDrBJFDe410Pp1b8YU8jsnscTwJ8nGcDqL")
            .header("Accept", "application/json")
            .asJson<string>();
            var json = response.Body;
            //var serializer = new JavaScriptSerializer();
            Beverage[] beverages = JsonConvert.DeserializeObject<Beverage[]>(json);
            ViewModel vm = new ViewModel();
            vm.Beverages = beverages;
            return View("Beverages", vm);
        }

    }
}
