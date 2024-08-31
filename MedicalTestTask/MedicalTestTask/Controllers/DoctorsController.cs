using MedicalTestTask.Data;
using MedicalTestTask.DTOs;
using MedicalTestTask.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalTestTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController
{
    private readonly MedicalDbContext _dbContext;

    public DoctorsController(MedicalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IResult> GetDoctors(string sortField, int pageNumber = 1, int pageSize = 10)
    {
        var query = _dbContext.Doctors
            .Include(d => d.Cabinet)
            .Include(d => d.Specialization)
            .Include(d => d.Area)
            .AsQueryable();

        if (!string.IsNullOrEmpty(sortField))
        {
            query = sortField switch
            {
                "FullName" => query.OrderBy(d => d.FullName),
                "Specialization" => query.OrderBy(d => d.Specialization.Name),
                _ => query.OrderBy(d => d.Id)
            };
        }

        var doctors = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(d => new DoctorListDto(d.Id, d.FullName, d.Cabinet.Number, d.Specialization.Name,
                d.Area != null ? d.Area.Number : string.Empty))
            .ToListAsync();

        return Results.Ok(doctors);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetDoctor(int id)
    {
        var doctor = await _dbContext.Doctors.SingleOrDefaultAsync(d => d.Id == id);

        if (doctor == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(new DoctorEditDto(
            doctor.Id,
            doctor.FullName,
            doctor.CabinetId,
            doctor.SpecializationId,
            doctor.AreaId));
    }

    [HttpPost]
    public async Task<IResult> CreateDoctor(DoctorEditDto doctorDto)
    {
        var doctor = new Doctor
        {
            FullName = doctorDto.FullName,
            CabinetId = doctorDto.CabinetId,
            SpecializationId = doctorDto.SpecializationId,
            AreaId = doctorDto.AreaId
        };

        _dbContext.Doctors.Add(doctor);
        await _dbContext.SaveChangesAsync();

        return Results.NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateDoctor(int id, DoctorEditDto doctorDto)
    {
        var doctor = await _dbContext.Doctors.SingleOrDefaultAsync(d => d.Id == id);
        if (doctor == null)
        {
            return Results.NotFound();
        }

        doctor.FullName = doctorDto.FullName;
        doctor.CabinetId = doctorDto.CabinetId;
        doctor.SpecializationId = doctorDto.SpecializationId;
        doctor.AreaId = doctorDto.AreaId;

        await _dbContext.SaveChangesAsync();

        return Results.NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteDoctor(int id)
    {
        var doctor = await _dbContext.Doctors.SingleOrDefaultAsync(d => d.Id == id);
        if (doctor == null)
        {
            return Results.NotFound();
        }

        _dbContext.Doctors.Remove(doctor);
        await _dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}