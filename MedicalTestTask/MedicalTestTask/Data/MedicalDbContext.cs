using MedicalTestTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalTestTask.Data;

public class MedicalDbContext : DbContext
{
    public DbSet<Area> Areas { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Cabinet> Cabinets { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }

    public MedicalDbContext(DbContextOptions<MedicalDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
}