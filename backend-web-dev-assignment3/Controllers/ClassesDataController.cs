using backend_web_dev_assignment3.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace backend_web_dev_assignment3.Controllers
{
    public class ClassesDataController : ApiController
    {
        private SchoolDbContext schoolDbContext = new SchoolDbContext();

        /// <summary>
        /// This method will access the School database to get all the class details from the Classes table
        /// </summary>
        /// <example>
        /// GET api/ClassesData/getAllClasses -> [{}]
        /// </example>
        /// <returns>A list of Classes Objects</returns>
        [HttpGet]
        [Route("api/ClassesData/getAllClasses")]
        public List<Classes> GetAllClasses()
        {
            MySqlConnection conn = schoolDbContext.AccessDatabase();
            conn.Open();
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from Classes";
            MySqlDataReader reader = command.ExecuteReader();

            List<Classes> classList = new List<Classes>();
            while (reader.Read())
            {
                Classes classObj = new Classes();
                classObj.teacherid = (long)reader["teacherid"];
                classObj.classid = (int)reader["classid"];
                classObj.classcode = (string)reader["classcode"];
                classObj.classname = (string)reader["classname"];
                classObj.startdate = (DateTime)reader["startdate"];
                classObj.finishdate = (DateTime)reader["finishdate"];
                classList.Add(classObj);
            }

            conn.Close();

            return classList;
        }

        /// <summary>
        /// This method will find class given a class id from the Classes table
        /// </summary>
        /// <param name="classid">The classid primary key</param>
        /// <example>
        /// GET api/ClassesData/getClass/1 -> returns Class with classid 1
        /// </example>
        /// <returns>A Class Object</returns>
        [HttpGet]
        [Route("api/ClassesData/getClass/{classid}")]
        public Classes GetClass(int classid)
        {
            MySqlConnection conn = schoolDbContext.AccessDatabase();
            conn.Open();
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Classes WHERE classid = " + classid;

            MySqlDataReader reader = command.ExecuteReader();

            Classes classObj = new Classes();
            while (reader.Read())
            {
                classObj.teacherid = (long)reader["teacherid"];
                classObj.classid = (int)reader["classid"];
                classObj.classcode = (string)reader["classcode"];
                classObj.classname = (string)reader["classname"];
                classObj.startdate = (DateTime)reader["startdate"];
                classObj.finishdate = (DateTime)reader["finishdate"];
            }

            conn.Close();

            return classObj;
        }
    }
}
