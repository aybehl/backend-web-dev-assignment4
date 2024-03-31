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
            List<Teacher> teachersList = controller.getAllTeachers(name);

            return View(teachersList);
        }

        // GET: Teacher/Show/{id}
        public ActionResult Show(int id) {
            TeachersDataController controller = new TeachersDataController();
            Teacher teacher = controller.getTeacher(id);

            return View(teacher);
        }

        // GET: Teacher/Search
        public ActionResult Search(string name = null, DateTime? hireDate = null, decimal? salary = null) {
            TeachersDataController controller = new TeachersDataController();
            List<Teacher> teachers = controller.searchTeachers(name, hireDate, salary);

            return View(teachers);
        }

        // GET: Teacher/New
        public ActionResult New() {
            return View();
        }

        // POST: Teacher/Create
        [HttpPost]
        public ActionResult Create(string firstName, string lastName, string employeeNumber, DateTime hireDate, decimal salary)
        {
            Teacher newTeacher = new Teacher();
            newTeacher.teacherfname = firstName;
            newTeacher.teacherlname = lastName;
            newTeacher.employeenumber = employeeNumber;
            newTeacher.hiredate = hireDate;
            newTeacher.salary = salary;

            TeachersDataController controller = new TeachersDataController();
            controller.addNewTeacher(newTeacher);

            return RedirectToAction("List");
        }

        //GET: /Teacher/ConfirmDelete/{id}
        public ActionResult ConfirmDelete(int id) {
            TeachersDataController controller = new TeachersDataController();
            Teacher teacherToDelete = controller.getTeacher(id);
            return View(teacherToDelete);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeachersDataController controller = new TeachersDataController();
            controller.deleteTeacher(id);

            return RedirectToAction("List");
        }

    }
}