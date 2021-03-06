﻿using System;
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
            EmployeeRepository employeeRepo = new EmployeeRepository();

            Console.WriteLine("Getting All Departments:");
            Console.WriteLine();

            List<Department> allDepartments = departmentRepo.GetAllDepartments();
            List<Employee> allEmployees = employeeRepo.GetAllEmployees();

            foreach (Department dept in allDepartments)
            {
                Console.WriteLine($"{dept.Id}.) {dept.DeptName}");
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Department with Id 1");

            Department singleDepartment = departmentRepo.GetDepartmentById(1);
            Console.WriteLine($"{singleDepartment.Id}.) {singleDepartment.DeptName}");

            Console.WriteLine("CREATE");
            Console.WriteLine("What's the name of the new Department?");
            var newDeptName = Console.ReadLine();

            Department newDept = new Department
            {
                DeptName = newDeptName
            };

            departmentRepo.AddDepartment(newDept);

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Added the new {newDept.DeptName} Department!");

            Console.WriteLine("----------------------------");
            Console.WriteLine("UPDATE");
            Console.WriteLine("which Department (use number)?");
            var updatedDepartments = departmentRepo.GetAllDepartments();

            foreach (Department dept in updatedDepartments)
            {
                Console.WriteLine($"{dept.Id}.) {dept.DeptName}");
            }
            var updatedDepartmentId = int.Parse(Console.ReadLine());
            Department departmentToUpdate = departmentRepo.GetDepartmentById(updatedDepartmentId);
            Console.WriteLine($"Update {departmentToUpdate.DeptName} department Name.");
            departmentToUpdate.DeptName = Console.ReadLine();
            

            departmentRepo.UpdateDepartment(updatedDepartmentId, departmentToUpdate);

            Console.WriteLine("----------------------------");
            Console.WriteLine("DELETE");
            Console.WriteLine("which Department (use number)?");
            foreach (Department dept in updatedDepartments)
            {
                Console.WriteLine($"{dept.Id}.) {dept.DeptName}");
            }
            var deleteDepartmentId = int.Parse(Console.ReadLine());
            Department departmentToDelete = departmentRepo.GetDepartmentById(deleteDepartmentId);
            Console.WriteLine($"Deleting {departmentToDelete.DeptName}");
            departmentRepo.DeleteDepartment(deleteDepartmentId);

            Console.WriteLine("Getting all Employees");
            foreach (Employee emp in allEmployees)
            {
                Console.WriteLine($"{emp.Id}.) {emp.FirstName} {emp.LastName}");
            }

            Console.WriteLine("CREATE");
            Console.WriteLine("What is their First Name?");
            var newEmployeeFirstName = Console.ReadLine();
            Console.WriteLine("what is their Last Name?");

            var newEmployeeLastName = Console.ReadLine();
            Console.WriteLine("Which department do they work in?");
            foreach (Department dept in allDepartments)
            {
                Console.WriteLine($"{dept.Id}.) {dept.DeptName}");
            }
            var newEmployeeDepartmentId = int.Parse(Console.ReadLine());

            Employee newEmployee = new Employee
            {
                FirstName = newEmployeeFirstName,
                LastName = newEmployeeLastName,
                DepartmentId = newEmployeeDepartmentId
            };

            employeeRepo.AddEmployee(newEmployee);

            Console.WriteLine("----------------------------");
            Console.WriteLine("UPDATE");
            Console.WriteLine("which Employee (use number)?");
            foreach (Employee emp in allEmployees)
            {
                Console.WriteLine($"{emp.Id}.) {emp.FirstName} {emp.LastName}");
            }
            var updatedEmployeeId = int.Parse(Console.ReadLine());
            Employee employeeToUpdate = employeeRepo.GetEmployeeById(updatedEmployeeId);
            Console.WriteLine($"Update {employeeToUpdate.FirstName} {employeeToUpdate.LastName} first name.");
            employeeToUpdate.FirstName = Console.ReadLine();
            Console.WriteLine($"Update {employeeToUpdate.FirstName} {employeeToUpdate.LastName} last name.");
            employeeToUpdate.LastName = Console.ReadLine();
            Console.WriteLine($"Update {employeeToUpdate.FirstName} {employeeToUpdate.LastName}'s department.");
            employeeToUpdate.DepartmentId = int.Parse(Console.ReadLine());

            employeeRepo.UpdateEmployee(updatedEmployeeId, employeeToUpdate);

            Console.WriteLine("DELETE");
            Console.WriteLine("which employee (use number)?");
            
            List<Employee> updatedAllEmployees = employeeRepo.GetAllEmployees();

            foreach (Employee emp in updatedAllEmployees)
            {
                Console.WriteLine($"{emp.Id}.) {emp.FirstName} {emp.LastName}");
            }
            var deleteEmployeeId = int.Parse(Console.ReadLine());
            Employee employeeToDelete = employeeRepo.GetEmployeeById(deleteEmployeeId);

            Console.WriteLine($"Deleting {employeeToDelete.FirstName} {employeeToDelete.LastName}");
            employeeRepo.DeleteEmployee(deleteEmployeeId);

        }
    }
}
