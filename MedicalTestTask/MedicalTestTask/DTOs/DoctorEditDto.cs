namespace MedicalTestTask.DTOs;

public record DoctorEditDto(int Id, string FullName, int CabinetId, int SpecializationId, int? AreaId);