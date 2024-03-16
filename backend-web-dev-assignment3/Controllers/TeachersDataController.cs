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

        [HttpGet]
        [Route("api/TeachersDataController/searchTeachers")]
        public List<Teacher> searchTeachers(string name = null, string hireDate = null, string salary = null)
        {
            MySqlConnection conn = schoolDbContext.AccessDatabase();
            conn.Open();
            MySqlCommand command = conn.CreateCommand();
            string query = "SELECT * FROM teachers WHERE 1=1";
            if (!string.IsNullOrEmpty(name)) {
                query += $" AND teacherfname LIKE '%{name}%'";
            }

            if (!string.IsNullOrEmpty(hireDate))
            {
                //DateTime parsedHireDate = DateTime.Parse(hireDate + " 00:00:00");

                query += $" AND hiredate = {hireDate} 00:00:00";
            }

            if (!string.IsNullOrEmpty(salary))
            {
                query += $" AND salary = {salary}";
            }

            command.CommandText = query;

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
        [Route("api/TeachersDataController/search/{name?}/{hireDate?}/{salary?}")]
        public string search(string name = null, string hireDate = null, string salary = null)
        {
            return $"{name} {hireDate} {salary}";
        }


    }
}
