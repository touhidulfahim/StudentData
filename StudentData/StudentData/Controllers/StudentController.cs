using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentData;

namespace StudentData.Controllers
{
    public class StudentController : Controller
    {
        private StudentDataEntities db = new StudentDataEntities();

        // GET: /Student/
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Country).Include(s => s.Department).Include(s => s.District).Include(s => s.Gender).Include(s => s.PoliceStation).Include(s => s.State);
            return View(students.ToList());
        }

        // GET: /Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: /Student/Create
        public ActionResult Create()
        {
            List<Country> countrys = new List<Country>();
            List<State> states = new List<State>();
            List<District> districts = new List<District>();
            List<PoliceStation> policeStations = new List<PoliceStation>();
            List<Gender> genders = new List<Gender>();
            List<Department> departments = new List<Department>();
            using (StudentDataEntities stu = new StudentDataEntities())
            {
                countrys = stu.Countries.OrderBy(a => a.CuntryName).ToList();
                genders = stu.Genders.OrderBy(a => a.GenderName).ToList();
                departments = stu.Departments.OrderBy(a => a.DepartmentName).ToList();
            }
            ViewBag.Country = new SelectList(countrys, "Id", "CuntryName");
            ViewBag.State = new SelectList(states, "Id", "StateName");
            ViewBag.District = new SelectList(districts, "Id", "DistrictName");
            ViewBag.PoliceStation = new SelectList(policeStations, "Id", "PoliceStName");
            ViewBag.Gender = new SelectList(genders, "Id", "GenderName");
            ViewBag.Department = new SelectList(departments, "Id", "DepartmentName");
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student, HttpPostedFileBase file)
        {
            List<Country> countrys = new List<Country>();
            List<State> states = new List<State>();
            List<District> districts = new List<District>();
            List<PoliceStation> policeStations = new List<PoliceStation>();
            List<Gender> genders = new List<Gender>();
            List<Department> departments = new List<Department>();
            using (StudentDataEntities stu = new StudentDataEntities())
            {
                countrys = stu.Countries.OrderBy(a => a.CuntryName).ToList();
                genders = stu.Genders.OrderBy(a => a.GenderName).ToList();
                departments = stu.Departments.OrderBy(a => a.DepartmentName).ToList();
            }
            ViewBag.Country = new SelectList(countrys, "Id", "CuntryName", student.CountryId);
            ViewBag.State = new SelectList(states, "Id", "StateName", student.StateId);
            ViewBag.District = new SelectList(districts, "Id", "DistrictName", student.DistrictId);
            ViewBag.PoliceStation = new SelectList(policeStations, "Id", "PoliceStName", student.PoliceStationId);
            ViewBag.Gender = new SelectList(genders, "Id", "GenderName", student.GenderId);
            ViewBag.Department = new SelectList(departments, "Id", "DepartmentName", student.DepartmentId);

            //PHOTO Section..................//
            #region
            if (file != null)
            {
                if (file.ContentLength > (512 * 1000)) // 512 KB
                {
                    ModelState.AddModelError("FileErrorMessage", "File size must be within 512 KB");
                }
                string[] allowedType = new string[] { "image/png", "image/gif", "image/jpeg", "image/jpg" };
                bool isFileTypeValid = false;
                foreach (var i in allowedType)
                {
                    if (file.ContentType == i.ToString())
                    {
                        isFileTypeValid = true;
                        break;
                    }
                }
                if (!isFileTypeValid)
                {
                    ModelState.AddModelError("FileErrorMessage", "Only .png, .gif and .jpg file type allowed");
                }
            }
            #endregion
            #region// Validate Model & Save to Database
            if (ModelState.IsValid)
            {
                //Save here
                if (file != null)
                {
                    string savePath = Server.MapPath("~/Image");
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    file.SaveAs(Path.Combine(savePath, fileName));
                    student.ImagePath = fileName;
                }
                using (StudentDataEntities dc = new StudentDataEntities())
                {
                    student.StudentRegNo = GetStudentRegNo(student);
                    dc.Students.Add(student);
                    dc.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }
            #endregion


        }

        private string GetStudentRegNo(Student student)
        {
            using (StudentDataEntities st = new StudentDataEntities())
            {
                var cnt =
               st.Students.Count(m => m.DepartmentId == student.DepartmentId) + 1;

                var aDepartment = st.Departments.FirstOrDefault(m => m.Id == student.DepartmentId);

                string leadingZero = "";
                int length = 3 - cnt.ToString().Length;
                for (int i = 0; i < length; i++)
                {
                    leadingZero += "0";
                }

                string studentRegNo = aDepartment.DepartmentName + "-" + leadingZero + cnt;
                return studentRegNo;
            }

        }

        public JsonResult GetState(int countryId)
        {
            using (StudentDataEntities db = new StudentDataEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                List<State> states = db.States.Where(a => a.CountryId == countryId).ToList();
                return Json(states, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDistrict(int stateId)
        {
            using (StudentDataEntities db = new StudentDataEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                List<District> districts = db.Districts.Where(a => a.StateId == stateId).ToList();
                return Json(districts, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPoliceStation(int districtId)
        {
            using (StudentDataEntities db = new StudentDataEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                List<PoliceStation> policeStations = db.PoliceStations.Where(a => a.DistrictId == districtId).ToList();
                return Json(policeStations, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetStudentByDepartmenttId(int departmentId)
        {
            var students = db.Students.Where(m => m.DepartmentId == departmentId).ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }





        // GET: /Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "CuntryName", student.CountryId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", student.DepartmentId);
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "DistrictName", student.DistrictId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "GenderName", student.GenderId);
            ViewBag.PoliceStationId = new SelectList(db.PoliceStations, "Id", "PoliceStName", student.PoliceStationId);
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName", student.StateId);
            return View(student);
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,FirstName,LastName,FathersName,MothersName,DateOfBirth,GenderId,MobileNo,EmailID,CountryId,StateId,DistrictId,PoliceStationId,Address,DepartmentId,ImagePath,StudentRegNo")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "CuntryName", student.CountryId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", student.DepartmentId);
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "DistrictName", student.DistrictId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "GenderName", student.GenderId);
            ViewBag.PoliceStationId = new SelectList(db.PoliceStations, "Id", "PoliceStName", student.PoliceStationId);
            ViewBag.StateId = new SelectList(db.States, "Id", "StateName", student.StateId);
            return View(student);
        }

        // GET: /Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
