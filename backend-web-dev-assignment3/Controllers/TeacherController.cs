using backend_web_dev_assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace backend_web_dev_assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List() {
            TeachersDataController controller = new TeachersDataController();
            List<Teacher> teachersList = controller.getAllTeachers();

            return View(teachersList);
        }

        public ActionResult Show(int id) {
            TeachersDataController controller = new TeachersDataController();
            Teacher teacher = controller.getTeacher(id);

            return View(teacher);
        }
    }
}