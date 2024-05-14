using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityLibrary.Data;
using UniversityLibrary.Models;

namespace UniversityLibrary.Controllers
{
    public class BookLoansController(UniversityLibraryContext context) : Controller
    {
        private readonly UniversityLibraryContext _context = context;

        // GET: BookLoans
        public async Task<IActionResult> Index()
        {
            var universityLibraryContext = _context.BookLoan.Include(b => b.Student).Include(b => b.Books).AsNoTracking();
            return View(await universityLibraryContext.ToListAsync());
        }

        // GET: BookLoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await _context.BookLoan
                .Include(b => b.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            return View(bookLoan);
        }

        // GET: BookLoans/Create
        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_context.Student, "RegistrationID", "FirstName");
            return View();
        }

        // POST: BookLoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StudentID,StartDate,EndDate")] BookLoan bookLoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookLoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_context.Student, "RegistrationID", "FirstName", bookLoan.StudentID);
            return View(bookLoan);
        }

        // GET: BookLoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await _context.BookLoan.FindAsync(id);
            if (bookLoan == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(_context.Student, "RegistrationID", "FirstName", bookLoan.StudentID);
            return View(bookLoan);
        }

        // POST: BookLoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StudentID,StartDate,EndDate")] BookLoan bookLoan)
        {
            if (id != bookLoan.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookLoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookLoanExists(bookLoan.ID))
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
            ViewData["StudentID"] = new SelectList(_context.Student, "RegistrationID", "FirstName", bookLoan.StudentID);
            return View(bookLoan);
        }

        // GET: BookLoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLoan = await _context.BookLoan
                .Include(b => b.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            return View(bookLoan);
        }

        // POST: BookLoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookLoan = await _context.BookLoan.FindAsync(id);
            if (bookLoan != null)
            {
                _context.BookLoan.Remove(bookLoan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookLoanExists(int id)
        {
            return _context.BookLoan.Any(e => e.ID == id);
        }
    }
}
