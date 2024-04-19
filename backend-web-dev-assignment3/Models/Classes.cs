using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend_web_dev_assignment3.Models
{
    public class Classes
    {
        public int classid;
        public string classcode { get; set; }
        public long teacherid { get; set; }
        public DateTime startdate { get; set; }
        public DateTime finishdate { get; set; }
        public string classname { get; set; }
    }
}