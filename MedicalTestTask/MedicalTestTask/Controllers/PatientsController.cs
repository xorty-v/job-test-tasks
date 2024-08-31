using MedicalTestTask.Data;
using MedicalTestTask.DTOs;
using MedicalTestTask.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalTestTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController
{
    private readonly MedicalDbContext _dbContext;

    public PatientsController(MedicalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IResult> GetPatients(string sortField, int pageNumber = 1,
        int pageSize = 10)
    {
        var query = _dbContext.Patients.Include(p => p.Area).AsQueryable();

        if (!string.IsNullOrEmpty(sortField))
        {
            query = sortField switch
            {
                "LastName" => query.OrderBy(p => p.LastName),
                "FirstName" => query.OrderBy(p => p.FirstName),
                _ => query.OrderBy(p => p.Id)
            };
        }

        var patients = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p =>
                new PatientListDto(p.Id, $"{p.LastName} {p.FirstName} {p.MiddleName}", p.Address, p.Area.Number))
            .ToListAsync();

        return Results.Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetPatient(int id)
    {
        var patient = await _dbContext.Patients.SingleOrDefaultAsync(p => p.Id == id);

        if (patient == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(new PatientEditDto(
            patient.Id,
            patient.LastName,
            patient.FirstName,
            patient.MiddleName,
            patient.Address,
            patient.BirthDate,
            patient.Gender,
            patient.AreaId
        ));
    }

    [HttpPost]
    public async Task<IResult> CreatePatient(PatientEditDto patientDto)
    {
        var patient = new Patient
        {
            LastName = patientDto.LastName,
            FirstName = patientDto.FirstName,
            MiddleName = patientDto.MiddleName,
            Address = patientDto.Address,
            BirthDate = patientDto.BirthDate,
            Gender = patientDto.Gender,
            AreaId = patientDto.AreaId
        };

        _dbContext.Patients.Add(patient);
        await _dbContext.SaveChangesAsync();

        return Results.NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdatePatient(int id, PatientEditDto patientDto)
    {
        var patient = await _dbContext.Patients.SingleOrDefaultAsync(p => p.Id == id);

        if (patient == null)
        {
            return Results.NotFound();
        }

        patient.LastName = patientDto.LastName;
        patient.FirstName = patientDto.FirstName;
        patient.MiddleName = patientDto.MiddleName;
        patient.Address = patientDto.Address;
        patient.BirthDate = patientDto.BirthDate;
        patient.Gender = patientDto.Gender;
        patient.AreaId = patientDto.AreaId;

        await _dbContext.SaveChangesAsync();

        return Results.NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeletePatient(int id)
    {
        var patient = await _dbContext.Patients.SingleOrDefaultAsync(p => p.Id == id);

        if (patient == null)
        {
            return Results.NotFound();
        }

        _dbContext.Patients.Remove(patient);
        await _dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}