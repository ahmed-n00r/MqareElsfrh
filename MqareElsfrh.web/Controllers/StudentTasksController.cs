using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthorizeLibrary.Data;
using DBModels.AppModels;
using DBModels.AppConstants;

namespace MqareElsfrh.web.Controllers
{
    public class StudentTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentTasks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentTasks.Include(s => s.parentTask);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentTasks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.StudentTasks == null)
            {
                return NotFound();
            }

            var studentTask = await _context.StudentTasks
                .Include(s => s.parentTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentTask == null)
            {
                return NotFound();
            }

            return View(studentTask);
        }

        // GET: StudentTasks/Create
        public IActionResult Create()
        {
            ViewData["TaskId"] = new SelectList(_context.StudentTasks, "Id", "Name");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: StudentTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TaskStatus,TaskType,EndDate,RoleId,TaskId,Id")] StudentTask studentTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskId"] = new SelectList(_context.StudentTasks, "Id", "Name", studentTask.TaskId);
            return View(studentTask);
        }

        // GET: StudentTasks/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.StudentTasks == null)
            {
                return NotFound();
            }

            var studentTask = await _context.StudentTasks.FindAsync(id);
            if (studentTask == null)
            {
                return NotFound();
            }
            var statusList = Enum.GetValues(typeof(TaskStatusEnum))
                .Cast<TaskStatusEnum>()
                .Select(e => new SelectListItem() { Text = e.ToString(), Value = ((int)e).ToString() })
                .ToList()
                .OrderBy(e => e.Text);
            ViewData["TaskStatus"] = new SelectList(statusList, "Value", "Text", studentTask.TaskStatus);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", studentTask.Id);
            ViewData["TaskId"] = new SelectList(_context.StudentTasks, "Id", "Name", studentTask.TaskId);
            return View(studentTask);
        }

        // POST: StudentTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,TaskStatus,TaskType,EndDate,RoleId,TaskId,Id")] StudentTask studentTask)
        {
            if (id != studentTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentTaskExists(studentTask.Id))
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
            var statusList = Enum.GetValues(typeof(TaskStatusEnum))
                .Cast<TaskStatusEnum>()
                .Select(e => new SelectListItem() { Text = e.ToString(), Value = ((int)e).ToString() })
                .ToList()
                .OrderBy(e => e.Text);
            ViewData["TaskStatus"] = new SelectList(statusList, "Value", "Text", studentTask.TaskStatus);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", studentTask.Id);
            ViewData["TaskId"] = new SelectList(_context.StudentTasks, "Id", "Name", studentTask.TaskId);
            return View(studentTask);
        }

        // GET: StudentTasks/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.StudentTasks == null)
            {
                return NotFound();
            }

            var studentTask = await _context.StudentTasks
                .Include(s => s.parentTask)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentTask == null)
            {
                return NotFound();
            }

            return View(studentTask);
        }

        // POST: StudentTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.StudentTasks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StudentTasks'  is null.");
            }
            var studentTask = await _context.StudentTasks.FindAsync(id);
            if (studentTask != null)
            {
                _context.StudentTasks.Remove(studentTask);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentTaskExists(long id)
        {
          return (_context.StudentTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
