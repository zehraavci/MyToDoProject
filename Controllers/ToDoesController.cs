using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyToDoProject.Data;
using MyToDoProject.Models;

namespace MyToDoProject.Controllers
{
    [Authorize]
    public class ToDoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CetUser> _userManager;

        public ToDoesController(ApplicationDbContext context, UserManager<CetUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: ToDoes
        public async Task<IActionResult> Index(SearchViewModel searchModel)
        {
            var cetUser = await _userManager.GetUserAsync(HttpContext.User);
            var query = _context.Todos.Include(t => t.Category).Where(t => t.CetUserId == cetUser.Id);

            if (searchModel.CategoryId != 0)
            {
                query = query.Where(t => t.CategoryId == searchModel.CategoryId);
            }
            if (searchModel.InDescription)
            {
                query = query.Where(t => t.Description.Contains(searchModel.SearchTitle));
            
            }else if (!String.IsNullOrWhiteSpace(searchModel.SearchTitle))
            {
                query = query.Where(t => t.Title.Contains(searchModel.SearchTitle));
            }
            query = query.OrderBy(w => w.DueDate);
            searchModel.Result = await query.ToListAsync();
            return View(searchModel);
        }

        // GET: ToDoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.Todos
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            var cetUser = await _userManager.GetUserAsync(HttpContext.User);
            if (toDo.CetUserId != cetUser.Id)
            {
                return Unauthorized();
            }
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // GET: ToDoes/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: ToDoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,IsCompleted,DueDate,CategoryId")] ToDo toDo)
        {
            var cetUser =await _userManager.GetUserAsync(HttpContext.User);
            toDo.CetUserId = cetUser.Id;
            if (ModelState.IsValid)
            {
                _context.Add(toDo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", toDo.CategoryId);
            return View(toDo);
        }

        // GET: ToDoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.Todos.FindAsync(id);
            var cetUser = await _userManager.GetUserAsync(HttpContext.User);
            if (toDo.CetUserId != cetUser.Id)
            {
                return Unauthorized();
            }
            if (toDo == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", toDo.CategoryId);
            return View(toDo);
        }

        // POST: ToDoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IsCompleted,DueDate,CategoryId,CreatedDate,CetUserId")] ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldTodo = await _context.Todos.FindAsync(id);
                    var cetUser = await _userManager.GetUserAsync(HttpContext.User);
                    if(oldTodo.CetUserId != cetUser.Id)
                    {
                        return Unauthorized();
                    }
                    oldTodo.Title = toDo.Title;
                    oldTodo.CompletedDate = toDo.CompletedDate;
                    oldTodo.CategoryId = toDo.CategoryId;
                    oldTodo.IsCompleted = toDo.IsCompleted;
                    oldTodo.Description = toDo.Description;
                    oldTodo.DueDate = toDo.DueDate;
                    _context.Update(oldTodo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoExists(toDo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", toDo.CategoryId);
            return View(toDo);
        }

        // GET: ToDoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.Todos
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDo = await _context.Todos.FindAsync(id);
            _context.Todos.Remove(toDo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}
