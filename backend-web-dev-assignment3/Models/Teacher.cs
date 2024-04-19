using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace backend_web_dev_assignment3.Models
{
    public class Teacher
    {
        public int teacherid;

        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[a-zA-Z]+(?:\s[a-zA-Z]+)*$", ErrorMessage = "First name must contain letters only")]
        public string teacherfname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^[a-zA-Z]+(?:\s[a-zA-Z]+)*$", ErrorMessage = "Last name must contain letters only")]
        public string teacherlname { get; set; }

        [Required]
        [RegularExpression(@"^T\d{3}$", ErrorMessage = "Employee number must be in the format T followed by 3 digits. For example, T123.")]
        public string employeenumber { get; set; }

        [RegularExpression(@"^\d+(\.\d{0,2})?$", ErrorMessage = "Please enter a valid salary amount. Example: 50 or 50.12")]
        public decimal? salary { get; set; }

        public DateTime? hiredate { get; set; }
    }
}