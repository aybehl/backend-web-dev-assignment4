using backend_web_dev_assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Diagnostics;

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
        public ActionResult Create(string firstName, string lastName, string employeeNumber, decimal salary, DateTime? hireDate = null)
        {
            Teacher newTeacher = new Teacher();
            newTeacher.teacherfname = firstName;
            newTeacher.teacherlname = lastName;
            newTeacher.employeenumber = employeeNumber;
            newTeacher.hiredate = hireDate ?? DateTime.Now;
            newTeacher.salary = salary;

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

    }
}