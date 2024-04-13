using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace backend_web_dev_assignment3.ViewModels
{
    public class TeacherViewModel
    {
        public int teacherid;

        [Required(ErrorMessage = "First name is required")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string lastName { get; set; }

        [RegularExpression(@"^T\d{3}$", ErrorMessage = "Employee number must be in the format T followed by 3 digits. For example, T123.")]
        public string employeeNumber { get; set; }

        [RegularExpression(@"^\d+(\.\d{0,2})?$", ErrorMessage = "Please enter a valid salary amount. Example: 50 or 50.12")]
        public decimal? salary { get; set; }

        public DateTime? hireDate { get; set; }
    }
}