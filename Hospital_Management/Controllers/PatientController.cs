using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class PatientController : Controller
    {
        // GET: PatirntController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PatirntController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PatirntController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatirntController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatirntController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatirntController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatirntController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatirntController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
