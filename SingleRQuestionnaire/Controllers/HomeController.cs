using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SingleRQuestionnaire.Models;

namespace SingleRQuestionnaire.Controllers
{
    public class HomeController : Controller
    {   

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
