using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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