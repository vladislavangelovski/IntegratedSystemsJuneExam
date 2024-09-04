using Domain.Domain_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IPolyclinicService _polyclinicService;
        private readonly IExaminationService _examinationService;

        public AdminController(IPolyclinicService polyclinicService, IExaminationService examinationService)
        {
            _polyclinicService = polyclinicService;
            _examinationService = examinationService;
        }

        [HttpGet("[action]")]
        public List<Polyclinic> GetAllPolyclinics()
        {
            return _polyclinicService.GetAllPolyclinics();
        }
        [HttpGet("[action]")]
        public List<HealthExamination> GetAllExaminations()
        {
            //return _healthExaminationRepository.GetAll().ToList();
            return null;
        }
        [HttpPost("[action]")]
        public HealthExamination GetDetailsExamination(BaseEntity id)
        {
            return _examinationService.GetDetailsForExamination(id);
        }
    }
}
