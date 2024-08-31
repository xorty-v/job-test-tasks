namespace MedicalTestTask.DTOs;

public record DoctorListDto(
    int Id,
    string FullName,
    string CabinetNumber,
    string SpecializationName,
    string AreaNumber);