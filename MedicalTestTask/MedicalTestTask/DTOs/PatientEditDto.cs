namespace MedicalTestTask.DTOs;

public record PatientEditDto(
    int Id,
    string LastName,
    string FirstName,
    string MiddleName,
    string Address,
    DateTime BirthDate,
    string Gender,
    int AreaId);