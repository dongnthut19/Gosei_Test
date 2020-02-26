using System.Collections.Generic;
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
    public class QualificationController: ControllerBase
    {
        private readonly ILogger<QualificationController> _logger;
        private readonly IQualificationService _qualificationService;
        private readonly IMapper _mapper;

        public QualificationController(ILogger<QualificationController> logger, IEmployeeService employeeService, IQualificationService qualificationService, IMapper mapper)
        {
            _logger = logger;
            _qualificationService = qualificationService;
            _mapper = mapper;
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add([FromBody]EmployeeQualificationDto qualification)
        {
            var newItem = new Qualification() {
                Name = qualification.Name,
                EmployeeQualification = new List<EmployeeQualification>() {
                    new EmployeeQualification() {
                        Institution = qualification.Institution,
                        EmployeeId = qualification.EmployeeId,
                        Note = qualification.Note,
                        City = qualification.City,
                        ValidTo = qualification.ValidTo,
                        ValidFrom = qualification.ValidFrom,
                    }
                }
            };

            _qualificationService.Add(newItem);
            _qualificationService.SaveChanges();
            return Ok();
        }
    }
}