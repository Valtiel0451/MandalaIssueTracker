using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MandalaIssueTrackerMVC.Models;

namespace MandalaIssueTrackerMVC.Controllers
{
    public class ProjectUsersController : Controller
    {
        private readonly IssueTrackerContext _context;

        public ProjectUsersController(IssueTrackerContext context)
        {
            _context = context;
        }

        // GET: ProjectUsers
        public async Task<IActionResult> Index()
        {
            var issueTrackerContext = _context.ProjectUsers.Include(p => p.Proj).Include(p => p.User);
            return View(await issueTrackerContext.ToListAsync());
        }

        // GET: ProjectUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectUser = await _context.ProjectUsers
                .Include(p => p.Proj)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProjId == id);
            if (projectUser == null)
            {
                return NotFound();
            }

            return View(projectUser);
        }

        // GET: ProjectUsers/Create
        public IActionResult Create()
        {
            ViewData["ProjId"] = new SelectList(_context.Projects, "ProjId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name");
            return View();
        }

        // POST: ProjectUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjId,UserId")] ProjectUser projectUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjId"] = new SelectList(_context.Projects, "ProjId", "Name", projectUser.ProjId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name", projectUser.UserId);
            return View(projectUser);
        }

        // GET: ProjectUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectUser = await _context.ProjectUsers.FindAsync(id);
            if (projectUser == null)
            {
                return NotFound();
            }
            ViewData["ProjId"] = new SelectList(_context.Projects, "ProjId", "Name", projectUser.ProjId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name", projectUser.UserId);
            return View(projectUser);
        }

        // POST: ProjectUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjId,UserId")] ProjectUser projectUser)
        {
            if (id != projectUser.ProjId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectUserExists(projectUser.ProjId))
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
            ViewData["ProjId"] = new SelectList(_context.Projects, "ProjId", "Name", projectUser.ProjId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name", projectUser.UserId);
            return View(projectUser);
        }

        // GET: ProjectUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectUser = await _context.ProjectUsers
                .Include(p => p.Proj)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProjId == id);
            if (projectUser == null)
            {
                return NotFound();
            }

            return View(projectUser);
        }

        // POST: ProjectUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectUser = await _context.ProjectUsers.FindAsync(id);
            _context.ProjectUsers.Remove(projectUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectUserExists(int id)
        {
            return _context.ProjectUsers.Any(e => e.ProjId == id);
        }
    }
}
