using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWebProject.Models.DB;

namespace MVCWebProject.Controllers
{
    public class StudentGradesController : Controller
    {
        private readonly CC21_Team4_Sem1Context _context;

        public StudentGradesController(CC21_Team4_Sem1Context context)
        {
            _context = context;
        }

        // GET: StudentGrades
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentGrades.ToListAsync());
        }

        // GET: StudentGrades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrades
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentGrade == null)
            {
                return NotFound();
            }

            return View(studentGrade);
        }

        // GET: StudentGrades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentGrades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentName,M1,M2,M3,M4")] StudentGrade studentGrade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentGrade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentGrade);
        }

        // GET: StudentGrades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrades.FindAsync(id);
            if (studentGrade == null)
            {
                return NotFound();
            }
            return View(studentGrade);
        }

        // POST: StudentGrades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentName,M1,M2,M3,M4")] StudentGrade studentGrade)
        {
            if (id != studentGrade.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentGrade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentGradeExists(studentGrade.StudentId))
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
            return View(studentGrade);
        }

        // GET: StudentGrades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrades
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentGrade == null)
            {
                return NotFound();
            }

            return View(studentGrade);
        }

        // POST: StudentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentGrade = await _context.StudentGrades.FindAsync(id);
            _context.StudentGrades.Remove(studentGrade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentGradeExists(int id)
        {
            return _context.StudentGrades.Any(e => e.StudentId == id);
        }
    }
}
