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
using System.Web.Http.Cors;
using System.Diagnostics;

namespace backend_web_dev_assignment3.Controllers
{
    public class TeachersDataController : ApiController
    {
        private SchoolDbContext schoolDbContext = new SchoolDbContext();

        /// <summary>
        /// This method will access the School database to get all the teachers details from the Teachers table
        /// An optional string 'name' can be used to filter the search result
        /// </summary>
        /// <example>
        /// GET api/TeachersData/getAllTeachers -> [{"employeenumber": "T378", "hiredate": "2016-08-05T00:00:00", "salary": "55.30", "teacherfname": "Alexander", "teacherid": 1, "teacherlname": "Bennett"}]
        /// </example>
        /// <returns>A list of teacher Objects</returns>
        [HttpGet]
        [Route("api/TeachersData/getAllTeachers/{name?}")]
        public List<Teacher> GetAllTeachers(string name = null) {
            MySqlConnection conn = schoolDbContext.AccessDatabase();
            conn.Open();
            MySqlCommand command = conn.CreateCommand();
            //command.CommandText = "Select * from Teachers";

            //SQL QUERY
            command.CommandText = "SELECT * FROM Teachers WHERE lower(teacherfname) LIKE lower(@key) OR lower(teacherlname) LIKE lower(@key) OR lower(concat(teacherfname, ' ', teacherlname)) LIKE lower(@key)";

            command.Parameters.AddWithValue("@key", "%" + name + "%");
            MySqlDataReader reader = command.ExecuteReader();

            List<Teacher> teachersList = new List<Teacher>();
            while (reader.Read())
            {
                Teacher teacher = new Teacher();
                teacher.teacherid = (int)reader["teacherid"];
                teacher.teacherfname = (string)reader["teacherfname"];
                teacher.teacherlname = (string)reader["teacherlname"];
                teacher.employeenumber = (string)reader["employeenumber"];
                teacher.salary = Convert.IsDBNull(reader["salary"]) ? (decimal?)null : Convert.ToDecimal(reader["salary"]);
                teacher.hiredate = (DateTime)reader["hiredate"];
                teachersList.Add(teacher);
            }

            conn.Close();

            return teachersList;
        }

        /// <summary>
        /// This method will find a teacher given a teacher id from the Teachers table
        /// </summary>
        /// <param name="teacherid">The teacher primary key</param>
        /// <example>
        /// GET /api/TeachersData/getTeacher/1 -> returns Teacher with teacherid 1
        /// </example>
        /// <returns>A Teacher Object</returns>
        [HttpGet]
        [Route("api/TeachersData/getTeacher/{teacherid}")]
        public Teacher GetTeacher(int teacherid)
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
                teacher.salary = Convert.IsDBNull(reader["salary"]) ? (decimal?)null : Convert.ToDecimal(reader["salary"]);
                teacher.hiredate = (DateTime)reader["hiredate"];
            }

            conn.Close();

            return teacher;
        }

        // GET: api/teachers/search?name={name}&hireDate={hireDate}&salary={salary}
        /// <summary>
        /// This method will search for teachers that match the parameters - 
        /// name, hire date greater than the entered hire date and salary greater than the entered salary
        /// If all of these parameters are null, then no results are found.
        /// If any of these parameters are not null, then the DB is queried to find a match
        /// with that parameter
        /// </summary>
        /// <param name="name">The name of teacher</param>
        /// <param name="hireDate">Hire Date</param>
        /// <param name="salary">Salary</param>
        /// <example>
        /// GET api/teachers/search?name={name}&hireDate={hireDate}&salary={salary} -> returns all teachers that match the parameters
        /// </example>
        /// <returns>List of teacher objects</returns>
        [HttpGet]
        [Route("api/teachersData/search")]
        public List<Teacher> SearchTeachers(string name = null, DateTime? hireDate = null, decimal? salary = null)
        {
            // Initialize a list to store the search results
            List<Teacher> teachers = new List<Teacher>();

            if ((name == null || name == "") && hireDate == null && salary == null)
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
                query += " AND lower(teacherfname) LIKE lower(@Name) OR lower(teacherlname) LIKE lower(@Name) OR lower(concat(teacherfname, ' ', teacherlname)) LIKE lower(@Name)";

                // Create a parameter for name to prevent SQL injection
                command.Parameters.AddWithValue("@Name", "%" + name + "%");
            }

            // Check if hireDate parameter is provided
            if (hireDate.HasValue)
            {
                // Format hireDate parameter to match SQL datetime format
                string formattedHireDate = hireDate.Value.ToString("yyyy-MM-dd HH:mm:ss");

                // Append hireDate condition to the query
                query += " AND hiredate >= @HireDate";

                // Create a parameter for hireDate to prevent SQL injection
                command.Parameters.AddWithValue("@HireDate", formattedHireDate);
            }

            // Check if salary parameter is provided
            if (salary.HasValue)
            {
                // Append salary condition to the query
                query += " AND salary >= @Salary";

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
                teacher.salary = Convert.IsDBNull(reader["salary"]) ? (decimal?)null : Convert.ToDecimal(reader["salary"]);
                teacher.hiredate = (DateTime)reader["hiredate"];
                teachers.Add(teacher);
            }

            connection.Close();

            // Return the list of teachers
            return teachers;
        }

        /// <summary>
        /// This method will add a new teacher given as parameter the Teacher object
        /// </summary>
        /// <param name="newTeacher">Teacher Object containing all attributes in the Teacher model</param>
        /// <example>
        /// POST /api/TeachersData/addNewTeacher/ [Body - {"employeenumber": "T378", "hiredate": "2016-08-05T00:00:00", 
        /// "salary": "55.30", "teacherfname": "Alexander", "teacherid": 1, "teacherlname": "Bennett"}] -> This adds a new 
        /// teacher with the data from the body of the request to the Teachers Table
        /// </example>
        /// <returns>Status 200, if action is successful otherwise Internal Server Error - 500</returns>
        [HttpPost]
        [Route("api/teachersData/addNewTeacher")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public IHttpActionResult AddNewTeacher([FromBody]Teacher newTeacher) {
            if (newTeacher == null) {
                return BadRequest("Invalid input in the body for post request");
            }

            try
            {
                MySqlConnection Conn = schoolDbContext.AccessDatabase();
                Conn.Open();

                MySqlCommand cmd = Conn.CreateCommand();
                cmd.CommandText = "INSERT into Teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) VALUES (@firstName, @lastName, @employeeNumber, @hireDate, @salary)";
                cmd.Parameters.AddWithValue("@firstName", newTeacher.teacherfname);
                cmd.Parameters.AddWithValue("@lastName", newTeacher.teacherlname);
                cmd.Parameters.AddWithValue("@employeeNumber", newTeacher.employeenumber);
                cmd.Parameters.AddWithValue("@hireDate", newTeacher.hiredate);
                cmd.Parameters.AddWithValue("@salary", newTeacher.salary);

                cmd.ExecuteNonQuery();
                Conn.Close();

                return Ok(new { message = "Teacher added successfully" });
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// This method will delete a teacher from the Teachers Table, given a teacher id
        /// Additionally, it makes the teacherid as NULL for all classes that have this teacher as a reference
        /// </summary>
        /// <param name="id">Teacher id</param>
        /// <example>
        /// POST /api/TeachersData/deleteTeacher/ Body - [1] -> Deletes teacher data for teacherid 1 and updates the classes table with teacherid as NULL, which contains this teacherid
        /// </example>
        /// <returns>void</returns>
        [HttpPost]
        [Route("api/teachersData/deleteTeacher")]
        public IHttpActionResult DeleteTeacher(int id)
        {
            MySqlConnection Conn = schoolDbContext.AccessDatabase();
            Conn.Open();

            var transaction = Conn.BeginTransaction();
            try
            {
                MySqlCommand updateCmd = Conn.CreateCommand();
                updateCmd.CommandText = "UPDATE classes SET teacherid = NULL WHERE teacherid = @id";
                updateCmd.Parameters.AddWithValue("@id", id);
                updateCmd.ExecuteNonQuery();

                MySqlCommand deleteCmd = Conn.CreateCommand();
                deleteCmd.CommandText = "DELETE from Teachers where teacherid=@id";
                deleteCmd.Parameters.AddWithValue("@id", id);
                deleteCmd.ExecuteNonQuery();
               
                transaction.Commit();
                Conn.Close();
                return Ok(new { message = "Teacher deleted successfully" });
            }
            catch(Exception ex) {
                transaction.Rollback();
                return InternalServerError(ex);
            }
            
        }

        /// <summary>
        /// This method will update an existing teacher given as parameter the teacher id. 
        /// The body of the request consisting of the updated teacher object
        /// </summary>
        /// <param name="id">teacher id to uniquely identify a teacher</param>
        /// <example>
        /// POST /api/TeachersData/updateTeacher/ [Body - {"employeenumber": "T378", "hiredate": "2016-08-05T00:00:00", 
        /// "salary": "55.30", "teacherfname": "Alexander", "teacherid": 1, "teacherlname": "Bennett"}] -> This updates an existing
        /// teacher with the data from the body of the request to the Teachers Table
        /// </example>
        /// <returns>Status 200, if action is successful otherwise Internal Server Error - 500</returns>
        [HttpPost]
        [Route("api/teachersData/updateTeacher/{id}")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public IHttpActionResult UpdateTeacher(int id, [FromBody] Teacher teacherToUpdate)
        {
            if (teacherToUpdate == null)
            {
                return BadRequest("Invalid input in the body for post request");
            }

            if (id != teacherToUpdate.teacherid)
            {
                return BadRequest("Mismatched ID in the request and model data.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                return BadRequest("Validation error: " + string.Join(", ", errors));
            }

            try
            {
                MySqlConnection Conn = schoolDbContext.AccessDatabase();
                Conn.Open();

                MySqlCommand cmd = Conn.CreateCommand();
                cmd.CommandText = "UPDATE Teachers SET teacherfname = @firstName, teacherlname = @lastName, employeenumber = @employeeNumber, hiredate = @hireDate, salary = @salary WHERE teacherid = @id";
                cmd.Parameters.AddWithValue("@firstName", teacherToUpdate.teacherfname);
                cmd.Parameters.AddWithValue("@lastName", teacherToUpdate.teacherlname);
                cmd.Parameters.AddWithValue("@employeeNumber", teacherToUpdate.employeenumber);
                cmd.Parameters.AddWithValue("@hireDate", teacherToUpdate.hiredate);
                cmd.Parameters.AddWithValue("@salary", teacherToUpdate.salary);
                cmd.Parameters.AddWithValue("@id", teacherToUpdate.teacherid);

                int rowsAffected = cmd.ExecuteNonQuery();
                Conn.Close();

                if (rowsAffected > 0)
                {
                    return Ok(new { message = "Teacher updated successfully" });
                }
                else
                {
                    var message = "Teacher with the specified ID was not found.";
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
