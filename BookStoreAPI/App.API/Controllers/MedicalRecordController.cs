using App.Application.Features.MedicalRecord.Create;
using App.Application.Features.MedicalRecord.Update;
using App.Application.Interfaces.MedicalRecord.Service;
using App.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class MedicalRecordController : CustomBaseController
    {
        private readonly IMedicalService _medicalService;
        public MedicalRecordController(IMedicalService medicalService)
        {
            _medicalService = medicalService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllMedicalRecordAsync()
        => CreateActionResult(await _medicalService.GetAllMedicalRecordsAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMedicalRecordByIdAsync(int id)
            =>CreateActionResult(await _medicalService.GetMedicalRecordByIdAsync(id));

        [HttpGet("{diagnosis}")]
        public async Task<IActionResult> GetMedicalRecordByDiagnosisAsync(string diagnosis)
            =>CreateActionResult(await _medicalService.GetMedicalRecordByDiagnosisAsync(diagnosis));

        [HttpPost]
        public async Task<IActionResult> AddMedicalRecordAsync(CreateMedicalRecordRequest createMedicalRecordRequest)
        =>CreateActionResult(await _medicalService.CreateMedicalRecordAsync(createMedicalRecordRequest));

        [HttpPut]
        public async Task<IActionResult> UpdateMedicalRecordAsyn(UpdateMedicalRecordRequest updateMedicalRecordRequest)
            => CreateActionResult(await _medicalService.UpdateMedicalRecordAsync(updateMedicalRecordRequest));
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMedicalRecordAsync(int id)
            =>CreateActionResult(await _medicalService.DeleteMedicalRecordAsync(id));


    }
}
