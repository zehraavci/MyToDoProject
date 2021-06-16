using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyToDoProject.Data;
using MyToDoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDoProject.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<CetUser> _userManager;

        public CategoryMenuViewComponent(ApplicationDbContext dbContext, UserManager<CetUser> userManager)
        {
            this.dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cetUser = await _userManager.GetUserAsync(HttpContext.User);
            var categoryList = await dbContext.Categories.Where(t => t.CetUserId == cetUser.Id || t.CetUserId == null).ToListAsync();
            return View(categoryList);
        }
    }
}
