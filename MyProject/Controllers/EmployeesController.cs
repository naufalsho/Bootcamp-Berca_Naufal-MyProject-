using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Context;
using MyProject.Models;
using MyProject.Repository;
using MyProject.ViewModel;
using System.Net;

namespace MyProject.Controllers
{
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly DepartmentRepository departmentRepository;

        public EmployeesController(EmployeeRepository employeeRepository, DepartmentRepository departmentRepository)
        {
            this.employeeRepository = employeeRepository;
            this.departmentRepository = departmentRepository;
        }

        //[HttpPost]
        //public ActionResult Insert(Employee employee)
        //{

        //    var nik = employeeRepository.Get(employee.NIK);


        //    if (string.IsNullOrWhiteSpace(employee.NIK))
        //    {
        //        return BadRequest("");
        //    }

        //    employeeRepository.Insert(employee);

        //    //action nya adalah GET, dan modelnya dari Employees
        //    var url = Url.Action("GET", "Employees", employee);
        //    return Created(url, employee);
        //    //return Ok();

        //}
        [HttpPost]
        public ActionResult Insert(Employee employee)
        {
            var phone = employeeRepository.IsExistsPhone(employee.Phone);
            var email = employeeRepository.IsExistsEmail(employee.Email);
            
            if (phone)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Phone already exists"});
            } else if (email)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email already exists"});
            }

            var getDept =  departmentRepository.Get().Any(d => d.Id == employee.Department.Id);
            if (getDept == false)
            {

                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "ID Department not exists" });
            }

            // Generate new NIK
            string newNik = employeeRepository.GenerateNewNik();

            employee.NIK = newNik;

            var insert = employeeRepository.Insert(employee);

            return StatusCode(201, new { status = HttpStatusCode.Created, message = "Success Create", Data = insert });
        }

        [HttpGet]
        public ActionResult Get()
        {
            var get = employeeRepository.Get();

            if (get.Count() == 0 || get == null)
            {
                return StatusCode(204, new { status = HttpStatusCode.NoContent, message = "Data No Content", Data = get });
            }
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data ditemukan", Data = get });
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {

            var get = employeeRepository.Get(NIK);

            if (get == null)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan", Data = get });
            }

            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data ditemukan", Data = get });
        }

        //[HttpPut]
        //public ActionResult Update(Employee employee)
        //{
        //    var put = employeeRepository.Get(employee.NIK);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Not a valid model");
        //    }
        //    else if (put != null)
        //    {
        //        put.FirstName = employee.FirstName;
        //        put.LastName = employee.LastName;
        //        put.Phone = employee.Phone;
        //        put.BirthDate = employee.BirthDate;
        //        put.Salary = employee.Salary;
        //        put.Email = employee.Email;
        //        put.Gender = employee.Gender;

        //        employeeRepository.Update(put);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //    return Ok();
        //}

        [HttpPut]
        public ActionResult Update(Employee employee)
        {
            var phone = employeeRepository.IsExistsPhone(employee.Phone);
            var email = employeeRepository.IsExistsEmail(employee.Email);

            if (string.IsNullOrWhiteSpace(employee.NIK)) 
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "NIK cannot be empty" });
            }

                var updateEmp = employeeRepository.Update(employee);

            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data success update", Data = updateEmp });

            ////action nya adalah GET, dan modelnya dari Employees
            //var url = Url.Action("GET", "Employees", employee);
            //return Created(url, employee);

        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            var get = employeeRepository.Get(NIK);

            if (get != null)
            {
                employeeRepository.Delete(NIK);
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Success delete", Data = get });
            }
            return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Request Delete failed", Data = get });
        }

        [HttpGet("/GetAllEmployeeDept")]
        public ActionResult GetAllEmployeeDept()
        {
            var employees = employeeRepository.GetAllEmployeeDept().ToList();
            

            var response = employees.Select(e => new
            {
                NIK = e.NIK,
                FullName = $"{e.FirstName} {e.LastName}",
                NamaDepartment = e.Department?.Name
            });

            if (employees.Count() == 0)
            {
                return StatusCode(204, new { status = HttpStatusCode.NoContent, message = "Data No Content!", Data = response });
            }

            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Found!", Data = response });
        }

        [HttpPost("/Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var validId = employeeRepository.IsIdValid(registerVM.DepartmentId);
            var validPhone = employeeRepository.IsExistsPhone(registerVM.Phone);
            var validEmail = employeeRepository.IsExistsEmail(registerVM.Email);

            if(registerVM == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Register Failed! Data is Null", Data = registerVM });
            }
            else if(!validId)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Register Failed! Id Department is Invalid!", Data = registerVM });
            } else if (validPhone)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Register Failed! Phone is Already Exists!", Data = registerVM });
            }else if (validEmail)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Register Failed! Email is Already Exists!", Data = registerVM });
            }
            var regis = employeeRepository.Register(registerVM);
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Register Success!", Data = regis });
        }

        [HttpGet("TestCors")]
        public ActionResult TestCors()
        {
            var response = new { message = "Test CORS Berhasil" };
            return Ok(response);
        }
    }
}
