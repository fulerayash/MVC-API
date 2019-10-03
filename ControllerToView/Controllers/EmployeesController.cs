using ControllerToView.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ControllerToView.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Test
        FuleraDBEntities db = new FuleraDBEntities();


        //List<EmployeeData> EmployeeList= new List<EmployeeData>()
        //    {
        //        new EmployeeData{
        //            EmployeeID = 14412,
        //            Name = "Yash Fulera",
        //            Functionality = "Delivery",
        //            Grade = "TR"
        //        },
        //        new EmployeeData{
        //            EmployeeID = 14260,
        //            Name = "Vishal Yadav",
        //            Functionality = "Delivery",
        //            Grade = "EX3"
        //        },
        //        new EmployeeData{
        //            EmployeeID = 10249,
        //            Name = "Manish Tiwari",
        //            Functionality = "Delivery",
        //            Grade = "MM3"
        //        }
        //    };

        [HttpGet]
        public ActionResult Details()
        {
            ViewBag.Branch = "Digital";
            ViewData["CompanyName"] = "Compunnel";
            TempData["Functionality"] = 0;
            ViewData["Location"] = "Noida, India";

            //return View(EmployeeList);
            return View(db.EmployeeDatas);
        }
        
        
        public ActionResult EditEmployee(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmployeeData EditEmp = db.EmployeeDatas.Find(ID);
            if (EditEmp == null)
            {
                return HttpNotFound();
            }
            return View(EditEmp);
        }


        [HttpPut, ActionName("EditEmployee")]
        public ActionResult EditEmployeeCnf([Bind(Include = "EmployeeID, Name, Functionality, Grade")] EmployeeData EditEmp)
        {
            if (ModelState.IsValid) {
                db.Entry(EditEmp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(EditEmp);
        }

        public ActionResult DeleteEmployee(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            EmployeeData DeleteEmp = db.EmployeeDatas.Find(ID);
            if (DeleteEmp == null)
            {
                return HttpNotFound();
            }
            return View(DeleteEmp);
        }


        [HttpDelete, ActionName("DeleteEmployee")]
        public ActionResult DeleteConfirmed(int ID)
        {
            EmployeeData DeleteEmp = db.EmployeeDatas.Find(ID);
            db.EmployeeDatas.Remove(DeleteEmp);
            db.SaveChanges();
            return RedirectToAction("Details");
        }


        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee([Bind(Include = "EmployeeID, Name, Functionality, Grade")] EmployeeData AddEmp)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeDatas.Add(AddEmp);
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(AddEmp);
        }
    }
}