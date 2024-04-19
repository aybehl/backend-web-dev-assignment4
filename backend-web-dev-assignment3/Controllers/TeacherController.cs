using backend_web_dev_assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Diagnostics;
using backend_web_dev_assignment3.ViewModels;
//using System.Web.Http;

namespace backend_web_dev_assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher Or Teacher/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List
        public ActionResult List(string name = null) {
            TeachersDataController controller = new TeachersDataController();
            List<Teacher> teachersList = controller.GetAllTeachers(name);

            return View(teachersList);
        }

        // GET: Teacher/Show/{id}
        public ActionResult Show(int id) {
            TeachersDataController controller = new TeachersDataController();
            Teacher teacher = controller.GetTeacher(id);

            return View(teacher);
        }

        // GET: Teacher/Search
        public ActionResult Search(string name = null, DateTime? hireDate = null, decimal? salary = null) {
            TeachersDataController controller = new TeachersDataController();
            List<Teacher> teachers = controller.SearchTeachers(name, hireDate, salary);

            return View(teachers);
        }

        // GET: Teacher/New
        public ActionResult New() {
            return View();
        }

        // POST: Teacher/Create
        [HttpPost]
        public ActionResult Create(TeacherViewModel model)
        {
            // Check if ModelState is valid
            if (!ModelState.IsValid)
            {
                return View("New", model);
            }

            Teacher newTeacher = new Teacher();
            newTeacher.teacherfname = model.firstName;
            newTeacher.teacherlname = model.lastName;
            newTeacher.employeenumber = model.employeeNumber;
            newTeacher.hiredate = model.hireDate ?? DateTime.Now;
            newTeacher.salary = model.salary;

            TeachersDataController controller = new TeachersDataController();
            controller.AddNewTeacher(newTeacher);

            return RedirectToAction("List");
        }

        //GET: /Teacher/ConfirmDelete/{id}
        public ActionResult ConfirmDelete(int id) {
            TeachersDataController controller = new TeachersDataController();
            Teacher teacherToDelete = controller.GetTeacher(id);
            return View(teacherToDelete);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeachersDataController controller = new TeachersDataController();
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        //GET: /Teacher/Update/{id}
        public ActionResult Update(int id) {
            TeachersDataController controller = new TeachersDataController();
            Teacher teacher = controller.GetTeacher(id);

            return View(teacher);
        }

        //POST: /Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, Teacher model)
        {
            // Check if ModelState is invalid
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TeachersDataController controller = new TeachersDataController();
            Teacher teacherToUpdate = new Teacher();
            teacherToUpdate.teacherfname = model.teacherfname;
            teacherToUpdate.teacherlname = model.teacherlname;
            teacherToUpdate.employeenumber = model.employeenumber;
            teacherToUpdate.hiredate = model.hiredate;
            teacherToUpdate.salary = model.salary;
            teacherToUpdate.teacherid = id;

            controller.UpdateTeacher(id, teacherToUpdate);
            return RedirectToAction("Show/" + id);
        }

    }
}