using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class PatientsController : Controller
{
    private readonly IPatientService _service;

    public PatientsController(IPatientService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var patients = await _service.GetAllAsync();
        return View(patients);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Patient patient)
    {
        if (!ModelState.IsValid)
            return View(patient);

        await _service.CreateAsync(patient);

        return RedirectToAction(nameof(Index));
    }
}