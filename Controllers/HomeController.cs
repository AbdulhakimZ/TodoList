using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TodoListContext _context;

        public HomeController(TodoListContext context,ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var items = _context.TodoItems.ToList();
            return View(items);
        }

        [HttpPost]
        public IActionResult Create(string description)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                var todo = new TodoItem();
                todo.Description = description;
                todo.IsComplete = false;
                _context.TodoItems.Add(todo);
                _context.SaveChanges();
                return Json(new { success = true, id = todo.Id });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Update(int id, bool IsComplete)
        {
            var todo = _context.TodoItems.Find(id);

            if (todo == null)
            {
                return Json(new { success = false });
            }

            todo.IsComplete = IsComplete;
            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return Json(new { success = false });
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();

            return Json(new { success = true });
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