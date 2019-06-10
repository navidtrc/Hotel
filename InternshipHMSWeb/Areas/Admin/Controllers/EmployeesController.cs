//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using InternshipHMSDataAccess;
//using InternshipHMSModels.Models;

//namespace InternshipHMSWeb.Areas.Admin.Controllers
//{
//    public class EmployeesController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: Admin/Employees
//        public ActionResult Index()
//        {
//            var people = db.People.Include(e => e.Person_ApplicationUser);
//            return View(people.ToList());
//        }

//        // GET: Admin/Employees/Details/5
//        public ActionResult Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Employee employee = db.People.Find(id);
//            if (employee == null)
//            {
//                return HttpNotFound();
//            }
//            return View(employee);
//        }

//        // GET: Admin/Employees/Create
//        public ActionResult Create()
//        {
//            ViewBag.ID = new SelectList(db.ApplicationUsers, "Id", "SerialNumber");
//            return View();
//        }

//        // POST: Admin/Employees/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,NationalCode,Birthdate,Nationality,LivingPlace,CreateDate,EmployeeCode")] Employee employee)
//        {
//            if (ModelState.IsValid)
//            {
//                employee.ID = Guid.NewGuid();
//                db.People.Add(employee);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ID = new SelectList(db.ApplicationUsers, "Id", "SerialNumber", employee.ID);
//            return View(employee);
//        }

//        // GET: Admin/Employees/Edit/5
        

//        // GET: Admin/Employees/Delete/5
//        public ActionResult Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Employee employee = db.People.Find(id);
//            if (employee == null)
//            {
//                return HttpNotFound();
//            }
//            return View(employee);
//        }

//        // POST: Admin/Employees/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(Guid id)
//        {
//            Employee employee = db.People.Find(id);
//            db.People.Remove(employee);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
