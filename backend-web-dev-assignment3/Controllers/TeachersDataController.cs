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
    public class TeachersDataController : ApiController
    {
        private SchoolDbContext schoolDbContext = new SchoolDbContext();

        [HttpGet]
        [Route("api/TeachersDataController/getAllTeachers")]
        public List<Teacher> getAllTeachers() {
            MySqlConnection conn = schoolDbContext.AccessDatabase();
            conn.Open();
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from Teachers";

            MySqlDataReader reader = command.ExecuteReader();

            List<Teacher> teachersList = new List<Teacher>();
            while (reader.Read())
            {
                Teacher teacher = new Teacher();
                teacher.teacherid = (int)reader["teacherid"];
                teacher.teacherfname = (string)reader["teacherfname"];
                teacher.teacherlname = (string)reader["teacherlname"];
                teacher.employeenumber = (string)reader["employeenumber"];
                teacher.salary = (decimal)reader["salary"];
                teacher.hiredate = (DateTime)reader["hiredate"];
                teachersList.Add(teacher);
            }

            conn.Close();

            return teachersList;
        }

        [HttpGet]
        [Route("api/TeachersDataController/getTeacher/{teacherid}")]
        public Teacher getTeacher(int teacherid)
        {
            MySqlConnection conn = schoolDbContext.AccessDatabase();
            conn.Open();
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Teachers WHERE teacherid = " + teacherid;

            MySqlDataReader reader = command.ExecuteReader();

            Teacher teacher = new Teacher();
            while (reader.Read())
            { 
                teacher.teacherid = (int)reader["teacherid"];
                teacher.teacherfname = (string)reader["teacherfname"];
                teacher.teacherlname = (string)reader["teacherlname"];
                teacher.employeenumber = (string)reader["employeenumber"];
                teacher.salary = (decimal)reader["salary"];
                teacher.hiredate = (DateTime)reader["hiredate"];
            }

            conn.Close();

            return teacher;
        }
    }
}
