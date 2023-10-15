using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthorizeLibrary.Data;
using DBModels.AppModels;

namespace MqareElsfrh.web.Controllers
{
    public class StudentDutiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentDutiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentDuties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentDuties.Include(s => s.group).Include(s => s.task);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentDuties/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.StudentDuties == null)
            {
                return NotFound();
            }

            var studentDuty = await _context.StudentDuties
                .Include(s => s.group)
                .Include(s => s.task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentDuty == null)
            {
                return NotFound();
            }

            return View(studentDuty);
        }

        // GET: StudentDuties/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            ViewData["TaskId"] = new SelectList(_context.StudentTasks, "Id", "Name");
            return View();
        }

        // POST: StudentDuties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DutyStatus,UserId,TaskId,GroupId,Id")] StudentDuty studentDuty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentDuty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", studentDuty.GroupId);
            ViewData["TaskId"] = new SelectList(_context.StudentTasks, "Id", "Name", studentDuty.TaskId);
            return View(studentDuty);
        }

        // GET: StudentDuties/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.StudentDuties == null)
            {
                return NotFound();
            }

            var studentDuty = await _context.StudentDuties.FindAsync(id);
            if (studentDuty == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", studentDuty.GroupId);
            ViewData["TaskId"] = new SelectList(_context.StudentTasks, "Id", "Name", studentDuty.TaskId);
            return View(studentDuty);
        }

        // POST: StudentDuties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("DutyStatus,UserId,TaskId,GroupId,Id")] StudentDuty studentDuty)
        {
            if (id != studentDuty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentDuty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentDutyExists(studentDuty.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", studentDuty.GroupId);
            ViewData["TaskId"] = new SelectList(_context.StudentTasks, "Id", "Name", studentDuty.TaskId);
            return View(studentDuty);
        }

        // GET: StudentDuties/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.StudentDuties == null)
            {
                return NotFound();
            }

            var studentDuty = await _context.StudentDuties
                .Include(s => s.group)
                .Include(s => s.task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentDuty == null)
            {
                return NotFound();
            }

            return View(studentDuty);
        }

        // POST: StudentDuties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.StudentDuties == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StudentDuties'  is null.");
            }
            var studentDuty = await _context.StudentDuties.FindAsync(id);
            if (studentDuty != null)
            {
                _context.StudentDuties.Remove(studentDuty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentDutyExists(long id)
        {
          return (_context.StudentDuties?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
