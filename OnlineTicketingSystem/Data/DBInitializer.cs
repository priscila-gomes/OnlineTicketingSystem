using OnlineTicketingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineTicketingSystem.Data
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<OnlineTicketingSystemContext>
    {
        protected override void Seed(OnlineTicketingSystemContext context)
        {
            // Project
            var projects = new List<Project>
            {
                new Project{ProjectTitle="Project One"},
                new Project{ProjectTitle="Project Two"},
                new Project{ProjectTitle="Project Three"},
                new Project{ProjectTitle="Project Four"},
                new Project{ProjectTitle="Project Five"}
            };

            projects.ForEach(s => context.Projects.Add(s));
            context.SaveChanges();

            // Department
            var departments = new List<Department>
            {
                new Department{DeptId=101, DeptName="Division of Telecommunications Extranet Development"},
                new Department{DeptId=102, DeptName="Extranet Multimedia Connectivity and Security Division"},
                new Department{DeptId=103, DeptName="Branch of Extranet Implementation"},
                new Department{DeptId=104, DeptName="Branch of Intranet Computer Maintenance and E-Commerce PC Programming"},
                new Department{DeptId=105, DeptName="Wireless Extranet Backup Team"},
                new Department{DeptId=106, DeptName="Database Programming Branch"},
                new Department{DeptId=107, DeptName="Hardware Backup Department"},
                new Department{DeptId=108, DeptName="Multimedia Troubleshooting and Maintenance Team"},
                new Department{DeptId=109, DeptName="Office of Statistical Data Connectivity"},
                new Department{DeptId=110, DeptName="Division of Application Security"},
                new Department{DeptId=111, DeptName="Network Maintenance and Multimedia Implementation Committee"},
                new Department{DeptId=112, DeptName="Mainframe PC Development and Practical Source Code Acquisition Team"},
                new Department{DeptId=113, DeptName="PC Maintenance Department"},
                new Department{DeptId=114, DeptName="Bureau of Business-Oriented PC Backup and Wireless Telecommunications Control"},
                new Department{DeptId=115, DeptName="Software Technology and Networking Department"}
            };

            departments.ForEach(s => context.Departments.Add(s));
            context.SaveChanges();

            // Employee
            var employees = new List<Employee>
            {
                new Employee{DeptRefId=101, EmpName="Roma Marcell"},
                new Employee{DeptRefId=102, EmpName="Hugo Wess"},
                new Employee{DeptRefId=103, EmpName="Kelvin Lahr"},
                new Employee{DeptRefId=101, EmpName="Janelle Newberg"},
                new Employee{DeptRefId=104, EmpName="Mellie Lombard"},
                new Employee{DeptRefId=105, EmpName="Reita Abshire"},
                new Employee{DeptRefId=106, EmpName="Dalila Vickrey"},
                new Employee{DeptRefId=103, EmpName="Idella Dallman"},
                new Employee{DeptRefId=107, EmpName="Farah Eldridge"},
                new Employee{DeptRefId=107, EmpName="Lana Montes"},
                new Employee{DeptRefId=102, EmpName="Leola Thornburg"},
                new Employee{DeptRefId=106, EmpName="Marcelo Paris"},
                new Employee{DeptRefId=108, EmpName="Ione Tomlin"},
                new Employee{DeptRefId=108, EmpName="Hilario Masterson"},
                new Employee{DeptRefId=109, EmpName="Keren Gillespi"},
                new Employee{DeptRefId=101, EmpName="Linh Leitzel"},
                new Employee{DeptRefId=110, EmpName="Rosalia Ayoub"},
                new Employee{DeptRefId=104, EmpName="Shawna Hood"},
                new Employee{DeptRefId=111, EmpName="Wilfredo Stumpf"},
                new Employee{DeptRefId=109, EmpName="Alexandra Brendle"},
                new Employee{DeptRefId=112, EmpName="Luciano Riddell"},
                new Employee{DeptRefId=111, EmpName="Boyce Perales"},
                new Employee{DeptRefId=101, EmpName="Alissa Perlman"},
                new Employee{DeptRefId=111, EmpName="Latoyia Kremer"},
                new Employee{DeptRefId=105, EmpName="Tawna Blackmore"},
                new Employee{DeptRefId=107, EmpName="Claudine Valderrama"},
                new Employee{DeptRefId=111, EmpName="Katheryn Lepak"},
                new Employee{DeptRefId=108, EmpName="Sage Bow"},
                new Employee{DeptRefId=107, EmpName="Altha Rudisill"},
                new Employee{DeptRefId=110, EmpName="Olympia Vien"},
                new Employee{DeptRefId=113, EmpName="Olene Pyron"},
                new Employee{DeptRefId=102, EmpName="Delorse Searle"},
                new Employee{DeptRefId=114, EmpName="Greta Quigg"},
                new Employee{DeptRefId=104, EmpName="Kenneth Bowie"},
                new Employee{DeptRefId=107, EmpName="Colton Kranz"},
                new Employee{DeptRefId=108, EmpName="Kasie Barclay"},
                new Employee{DeptRefId=102, EmpName="Thresa Levins"},
                new Employee{DeptRefId=115, EmpName="Diego Hasbrouck"},
                new Employee{DeptRefId=106, EmpName="Tomoko Gale"}
            };

            employees.ForEach(s => context.Employees.Add(s));
            context.SaveChanges();

            /*var tickets = new List<Ticket>
            {
            new Ticket { Description = "Meredith", ProjectRefId = 1, DeptRefId=115, EmpRefId=38, SubmitDate = DateTime.Parse("2020-05-01 07:20") }
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();*/

            base.Seed(context);
        }
    }
}