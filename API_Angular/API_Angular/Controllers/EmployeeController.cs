using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Angular.Models;
using System.Data.Entity;

namespace API_Angular.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {

        MyDataBaseEntities db = new MyDataBaseEntities();

        [HttpGet]
        [Route("getAllEmployees")]
        public IHttpActionResult getAll()
        {
            return Ok(db.Employees.ToList());
        }

        [HttpGet]
        [Route("getEmployeeByID/{employeeId}")]
        public IHttpActionResult getEmployee(string employeeId)
        {
            int ID = Convert.ToInt32(employeeId);
            Employee emp = db.Employees.Find(ID);
            if (emp == null) 
            {
                return NotFound();
            }
            else
            {
            return Ok(emp);
            }
        }

        [HttpPost]
        [Route("addEmployee")]
        public IHttpActionResult postEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return Ok(employee);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPut]
        [Route("updateEmployee")]
        public IHttpActionResult putEmployee(Employee employee)
        {
            Employee emp = db.Employees.Find(employee.EmpID);
            if (emp == null)
            {
                return NotFound();
            }
            else
            {
                emp.EmpName = employee.EmpName;
                emp.Gender = employee.Gender;
                emp.DateOfBirth = employee.DateOfBirth;
                emp.Address = employee.Address;
                db.SaveChanges();
                return Ok(employee);
            }
        }

        [HttpDelete]
        [Route("deleteEmployee")]
        public IHttpActionResult deleteEmployee(int id)
        {
            Employee emp = db.Employees.Find(id);
            if (emp == null) 
            {
                return NotFound();
            }
            else
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Ok(emp);
            }
        }

    }
}
