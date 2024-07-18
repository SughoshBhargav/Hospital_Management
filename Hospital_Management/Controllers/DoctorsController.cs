using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Management.Data;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hospital_Management.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly HealthCareDbContext _context;

        public DoctorsController(HealthCareDbContext context)
        {
            _context = context;
        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
            var healthCareDbContext = _context.Doctors
                .Include(d => d.User)
                .Include(d => d.DoctorSpecialties)
                .ThenInclude(ds => ds.Specialty);
            return View(await healthCareDbContext.ToListAsync());
        }
        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DoctorID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
       

        public IActionResult Create()
        {
            // Fetch users who have the role "Doctor"
            var doctorUsers = _context.Users.Where(u => u.UserType == "Doctor").ToList();

            ViewData["UserID"] = new SelectList(doctorUsers, "UserID", "UserID");
            ViewBag.DoctorName = string.Empty;
            return View();
        }


        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorID,UserID,Name,Gender,Experience,AboutDoctor")] Doctor doctor)
        {
            // Fetch the user with the selected UserID and role "Doctor"
            doctor.User = await _context.Users.FirstOrDefaultAsync(u => u.UserID == doctor.UserID && u.UserType == "Doctor");

            if (doctor.User == null)
            {
                ModelState.AddModelError("UserID", "Selected user is not a doctor.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(doctor);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Doctor Created Successfully";
                    return RedirectToAction(nameof(Index));
                }catch(Exception ex)
                {
                    TempData["error"] = "Something went wrong !!";
                }
            }

            // Fetch users who have the role "Doctor" again to re-populate the dropdown in case of an error
            var doctorUsers = _context.Users.Where(u => u.UserType == "Doctor").ToList();
            ViewData["UserID"] = new SelectList(doctorUsers, "UserID", "Email", doctor.UserID);
            
            return View(doctor);
        }




        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", doctor.UserID);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorID,UserID,Name,Gender,Experience,AboutDoctor")] Doctor doctor)
        {
            if (id != doctor.DoctorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Doctor details edited Successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["error"] = "Something went wrong!!";
                    if (!DoctorExists(doctor.DoctorID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", doctor.UserID);
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        /* public async Task<IActionResult> Delete(int? id)
         {
             if (id == null || _context.Doctors == null)
             {
                 return NotFound();
             }

             var doctor = await _context.Doctors
                 .Include(d => d.User)
                 .FirstOrDefaultAsync(m => m.DoctorID == id);
             if (doctor == null)
             {
                 return NotFound();
             }

             return View(doctor);
         }

         // POST: Doctors/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             if (_context.Doctors == null)
             {
                 return Problem("Entity set 'HealthCareDbContext.Doctors'  is null.");
             }
             var doctor = await _context.Doctors.FindAsync(id);
             if (doctor != null)
             {
                 _context.Doctors.Remove(doctor);
             }

             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }*/

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.UserID == id);
            doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            try
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Doctor deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Failed to delete Doctor: {ex.Message}" });
            }


        }

        private bool DoctorExists(int id)
        {
          return (_context.Doctors?.Any(e => e.DoctorID == id)).GetValueOrDefault();
        }

        // GET: Doctors/GetDoctorName/5
        [HttpGet]
        public async Task<IActionResult> GetDoctorName(int id)
        {
            var doctorName = await _context.Users
                .Where(u => u.UserID == id)
                .Select(u => u.Username)
                .FirstOrDefaultAsync();

            return Json(doctorName);
        }

    }
}
