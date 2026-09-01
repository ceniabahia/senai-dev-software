/*using Microsoft.AspNetCore.Mvc;
using MinhaApi.Models;
using MinhaApi.Services;

[ApiController]
[Route("api/[controller]")]
public class TipoController
    : ControllerBase
{
    private readonly TipoController _service;

    public TipoController(
        TipoController service)
        => _service = service;

    // GET /api/tipo
    [HttpGet]
    public IActionResult GetAll()
    {
        var tipos = _service.GetAll();
        return Ok(tipos);
    }

    // GET /api/tipo/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var tipo = _service.GetById(id);
        if (tipo == null)
            return NotFound();
        return Ok(tipo);
    }
}*/