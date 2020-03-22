﻿using System.Collections.Generic;
using System.Data.SqlClient;
using DepartmentsEmployees.Models;

namespace DepartmentsEmployees.Data

{
    public class DepartmentRepository
    {

         public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=DepartmentsEmployees; Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }
       
        /// <summary>
        ///  Returns a single department with the given id.
        /// </summary>
        public Department GetDepartmentById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT DeptName FROM Department WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Department department = null;

                    // If we only expect a single row back from the database, we don't need a while loop.
                    if (reader.Read())
                    {
                        department = new Department
                        {
                            Id = id,
                            DeptName = reader.GetString(reader.GetOrdinal("DeptName"))
                        };
                    }

                    reader.Close();

                    return department;
                }
            }
        }

        /// <summary>
        ///  Returns a list of all departments in the database
        /// </summary>
        public List<Department> GetAllDepartments()
    {

        //  We must "use" the database connection.
        //  Because a database is a shared resource (other applications may be using it too) we must
        //  be careful about how we interact with it. Specifically, we Open() connections when we need to
        //  interact with the database and we Close() them when we're finished.
        //  In C#, a "using" block ensures we correctly disconnect from a resource even if there is an error.
        //  For database connections, this means the connection will be properly closed.
        using (SqlConnection conn = Connection)
        {
            // Note, we must Open() the connection, the "using" block   doesn't do that for us.
            conn.Open();

            // We must "use" commands too.
            using (SqlCommand cmd = conn.CreateCommand())
            {
                // Here we setup the command with the SQL we want to execute before we execute it.
                cmd.CommandText = "SELECT Id, DeptName FROM Department";

                // Execute the SQL in the database and get a "reader" that will give us access to the data.
                SqlDataReader reader = cmd.ExecuteReader();

                // A list to hold the departments we retrieve from the database.
                List<Department> departments = new List<Department>();

                // Read() will return true if there's more data to read
                while (reader.Read())
                {
                    // The "ordinal" is the numeric position of the column in the query results.
                    //  For our query, "Id" has an ordinal value of 0 and "DeptName" is 1.
                    int idColumnPosition = reader.GetOrdinal("Id");

                    // We user the reader's GetXXX methods to get the value for a particular ordinal.
                    int idValue = reader.GetInt32(idColumnPosition);

                    int deptNameColumnPosition = reader.GetOrdinal("DeptName");
                    string deptNameValue = reader.GetString(deptNameColumnPosition);

                    // Now let's create a new department object using the data from the database.
                    Department department = new Department
                    {
                        Id = idValue,
                        DeptName = deptNameValue
                    };

                    // ...and add that department object to our list.
                    departments.Add(department);
                }

                // We should Close() the reader. Unfortunately, a "using" block won't work here.
                reader.Close();

                // Return the list of departments who whomever called this method.
                return departments;
            }
        }
    }
    }
}