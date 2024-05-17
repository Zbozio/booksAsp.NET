using Bazowane.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bazowane.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

        private readonly BazowaneContext bazowaneContext;

        public HomeController(ILogger<HomeController> logger, BazowaneContext bazowaneContext)
		{
			_logger = logger;
			this.bazowaneContext= bazowaneContext;
		}

		public IActionResult Index()
		{
			var ksiazki = bazowaneContext.Ksiazkas.ToList();
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
