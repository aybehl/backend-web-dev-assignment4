using backend_web_dev_assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;


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
        public ActionResult List() {
            TeachersDataController controller = new TeachersDataController();
            List<Teacher> teachersList = controller.getAllTeachers();

            return View(teachersList);
        }

        // GET: Teacher/Show/{id}
        public ActionResult Show(int id) {
            TeachersDataController controller = new TeachersDataController();
            Teacher teacher = controller.getTeacher(id);

            return View(teacher);
        }

        // GET: Teacher/Search
        [HttpPost]
        public ActionResult Search(string name = null, DateTime? hireDate = null, decimal? salary = null) {
            TeachersDataController controller = new TeachersDataController();
            List<Teacher> teachers = controller.searchTeachers(name, hireDate, salary);

            return View(teachers);
        }
    }
}