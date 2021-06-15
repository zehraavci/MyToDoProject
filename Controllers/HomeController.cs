using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyToDoProject.Data;
using MyToDoProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDoProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<CetUser> _userManager;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext dbContext, UserManager<CetUser> userManager)
        {
            _logger = logger;
            this.dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<ToDo> result;
            if (User.Identity.IsAuthenticated)
            {
                var cetUser = await _userManager.GetUserAsync(HttpContext.User);
                var queryDb = dbContext.Todos.Include(t => t.Category).Where(t =>t.CetUserId == cetUser.Id && !t.IsCompleted).OrderBy(t => t.DueDate).Take(5);
                result = await queryDb.ToListAsync();
            }
            else
            {
                result = new List<ToDo>();
            }
            return View(result);

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
