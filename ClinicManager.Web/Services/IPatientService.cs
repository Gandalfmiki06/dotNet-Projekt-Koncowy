public interface IPatientService
{
    Task<List<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(int id);
    Task CreateAsync(Patient patient);
}