using backend_web_dev_assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend_web_dev_assignment3.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes Or Classes/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Classes/List
        public ActionResult List()
        {
            ClassesDataController controller = new ClassesDataController();
            List<Classes> classList = controller.GetAllClasses();

            return View(classList);
        }

        // GET: Classes/Show/{id}
        public ActionResult Show(int id)
        {
            ClassesDataController controller = new ClassesDataController();
            Classes classObj = controller.GetClass(id);

            return View(classObj);
        }
    }
}