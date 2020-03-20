using System;
using System.Collections.Generic;
using DepartmentsEmployees.Data;
using DepartmentsEmployees.Models;

namespace DepartmentsEmployees
{
    class Program
    {
        static void Main(string[] args)
        {
            DepartmentRepository departmentRepo = new DepartmentRepository();

            Console.WriteLine("Getting All Departments:");
            Console.WriteLine();

            List<Department> allDepartments = departmentRepo.GetAllDepartments();

            foreach (Department dept in allDepartments)
            {
                Console.WriteLine($"{dept.Id} {dept.DeptName}");
            }
        }
    }
}
