using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;
using api.Models.Dtos;
using api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController: ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeQualificationService _employeeQualificationService;
        private readonly IMapper _mapper;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService, IEmployeeQualificationService employeeQualificationService, IMapper mapper)
        {
            _logger = logger;
            _employeeService = employeeService;
            _employeeQualificationService = employeeQualificationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string txtFirstOrLastName, string orderByField,
            string sortOrder, int page, int pageSize = 10)
        {
            var obj = await _employeeService.GetAllPaging(txtFirstOrLastName, orderByField, sortOrder, page, pageSize);
            var itemsToReturn = _mapper.Map<List<EmployeeDto>>(obj.Items);
            _logger.LogInformation(itemsToReturn.ToString());
            return Ok(new {
                total = obj.Total,
                items = itemsToReturn
            });
        }

        [Route("getbyid/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _employeeService.GetById(id);
            var employeeQualifications = obj.EmployeeQualification;
            var itemToReturn = _mapper.Map<EmployeeDto>(obj);

            var objEmployeeQualification = new EmployeeQualificationDto();
            itemToReturn.EmployeeQualification = new List<EmployeeQualificationDto>();
            foreach (var item in employeeQualifications)
            {
                objEmployeeQualification = _mapper.Map<EmployeeQualificationDto>(item);

                var valid = false;
                if (item.ValidTo == null) {
                    valid = true;
                } else {
                    if (item.ValidTo >= (DateTime?)DateTime.Now) {
                        valid = true;
                    }
                }
                objEmployeeQualification.IsValid = valid;
                objEmployeeQualification.Name = item.Qualification.Name;

                itemToReturn.EmployeeQualification.Add(objEmployeeQualification);
            }

            return Ok(itemToReturn);
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add([FromBody]EmployeeDto employee)
        {
            var newItem = new Employee();
            newItem.UpdateEmployee(employee);
            _employeeService.Add(newItem);
            _employeeService.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [Route("edit")]
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody]EmployeeDto employee)
        {
            var newItem = await _employeeService.GetById(employee.Id);
            if (newItem == null) {
                return NotFound();
            }
            newItem.UpdateEmployee(employee);
            _employeeService.Update(newItem);
            _employeeService.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var employee = await _employeeService.GetById(id);
            
            if (employee == null) {
                return NotFound();
            }
            var qualifications = employee.EmployeeQualification;
            foreach(var qualification in qualifications) {
              _employeeQualificationService.Delete(qualification);
            }
            _employeeService.Delete(employee);
            _employeeService.SaveChanges();
            return Ok();
        }
    }
}