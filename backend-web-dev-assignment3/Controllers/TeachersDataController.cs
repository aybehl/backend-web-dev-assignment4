using backend_web_dev_assignment3.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
        [Route("api/TeachersData/getAllTeachers")]
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

        // GET: api/teachers/search?name={name}&hireDate={hireDate}&salary={salary}
        [HttpGet]
        [Route("api/teachersData/search")]
        public List<Teacher> Search(string name = null, DateTime? hireDate = null, decimal? salary = null)
        {
            // Initialize a list to store the search results
            List<Teacher> teachers = new List<Teacher>();

            if (name == null && hireDate == null && salary == null)
            {
                return teachers;
            }

            // Construct the SQL query
            string query = "SELECT * FROM Teachers WHERE 1 = 1";

            // Create a MySqlCommand object
            MySqlConnection connection = schoolDbContext.AccessDatabase();
            
            MySqlCommand command = new MySqlCommand(query, connection);

            // Check if name parameter is provided
            if (!string.IsNullOrEmpty(name))
            {
                // Append name condition to the query
                query += " AND teacherfname LIKE @Name";

                // Create a parameter for name to prevent SQL injection
                command.Parameters.AddWithValue("@Name", "%" + name + "%");
            }

            // Check if hireDate parameter is provided
            if (hireDate.HasValue)
            {
                // Format hireDate parameter to match SQL datetime format
                string formattedHireDate = hireDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

                // Append hireDate condition to the query
                query += " AND hiredate = @HireDate";

                // Create a parameter for hireDate to prevent SQL injection
                command.Parameters.AddWithValue("@HireDate", formattedHireDate);
            }

            // Check if salary parameter is provided
            if (salary.HasValue)
            {
                // Append salary condition to the query
                query += " AND salary = @Salary";

                // Create a parameter for salary to prevent SQL injection
                command.Parameters.AddWithValue("@Salary", salary.Value);
            }

            // Set the updated query to the MySqlCommand object
            command.CommandText = query;

            // Execute the query and retrieve the results
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Teacher teacher = new Teacher();
                teacher.teacherid = (int)reader["teacherid"];
                teacher.teacherfname = (string)reader["teacherfname"];
                teacher.teacherlname = (string)reader["teacherlname"];
                teacher.employeenumber = (string)reader["employeenumber"];
                teacher.salary = (decimal)reader["salary"];
                teacher.hiredate = (DateTime)reader["hiredate"];
                teachers.Add(teacher);
            }

            connection.Close();
            // Return the list of teachers
            return teachers;
        }
    }
}
