using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Repository;
using MyProject.Repository.Interface;
using System.Collections.Generic;
using System.Net;

namespace MyProject.Controllers
{
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentRepository departmentRepository;

        public DepartmentsController(DepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        [HttpGet]
        public  ActionResult Get()
        {
            var get =  departmentRepository.Get();

            if (get.Count() == 0 || get == null)
            {
                return StatusCode(204, new { status = HttpStatusCode.NoContent, message = "Data tidak ada yang ditampilakan", Data = get });
            }
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data ditemukan", Data = get });
        }

        [HttpGet("{Id}")]
        public ActionResult Get(int Id)
        {
            var get = departmentRepository.Get(Id);

            if (get == null)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan", Data = get });
            }

            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data ditemukan", Data = get });
        }

        [HttpPost]
        public ActionResult Insert(Department department)
        {
            var getId = departmentRepository.Get(department.Id);

            if (string.IsNullOrWhiteSpace(department.Name))
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Name Department cannot be null" });
            }
            else if(getId == null)
            {
                var insert = departmentRepository.Insert(department);
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Success Create", Data = insert });
            }
            return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Failed Create" });

        }

        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            var get = departmentRepository.Get(Id);

            if (get != null)
            {
                var del = departmentRepository.Delete(Id);
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Success delete", Data = del });
            }
            return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Request Delete failed"});
            
        }

        [HttpPut]
        public ActionResult Update(Department department)
        {
            var getId = departmentRepository.Get(department.Id);

            if (string.IsNullOrWhiteSpace(department.Name))
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Name Cannot be Null" });
            }
            else if (getId != null)
            {
                getId.Name = department.Name;

                var updateEmp = departmentRepository.Update(getId);

                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data success update", Data = updateEmp });
            }
            return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Id Not Found" });            
        }
    }
}
