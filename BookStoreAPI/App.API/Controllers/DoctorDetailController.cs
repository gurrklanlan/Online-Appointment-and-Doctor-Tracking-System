using System.Net;
using App.Application.Features.DoctorDetail.Create;
using App.Application.Features.DoctorDetail.Update;
using App.Application.Interfaces.DoctorDetail.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
   
    public class DoctorDetailController : CustomBaseController
    {
        private readonly IDoctorDetailService _doctordetailService;
        public DoctorDetailController(IDoctorDetailService doctordetailService)
        {
            _doctordetailService = doctordetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctorDetails()
       =>CreateActionResult(await _doctordetailService.GetAllDoctorDetailsAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDoctorDetailByIdAsync(int id)
            =>CreateActionResult(await _doctordetailService.GetDoctorDetailByIdAsync(id));

        [HttpGet("by-specialty/{specialty}")]
        public async Task<IActionResult> GetDoctorDetailBySpecialtyAsync(string specialty)
        {
            if (string.IsNullOrWhiteSpace(specialty))
                return BadRequest("Uzmanlık alanı boş olamaz.");

            var result = await _doctordetailService.GetDoctorDetailBySpecialtyAsync(specialty);
            return CreateActionResult(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateDoctrolDetailAsync(CreateDoctorDetailRequest request)
            =>CreateActionResult(await _doctordetailService.CreateDoctorDetailAsync(request));

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateDoctorDetailAsync(UpdateDoctorDetailRequest update)
            => CreateActionResult(await _doctordetailService.UpdateDoctorDetailAsync(update));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDoctorDetailAsync(int id)
            =>CreateActionResult(await _doctordetailService.DeleteDoctorDetailAsync(id));

    }
}
