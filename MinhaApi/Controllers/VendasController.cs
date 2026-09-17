using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VendasController : ControllerBase
{
    private readonly IVendasService _service;

    public VendasController(IVendasService service)
        => _service = service;

    [HttpGet]
    public IActionResult GetAll()
    {
        var vendas = _service.GetAll();
        return Ok(vendas);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var venda = _service.GetById(id);

        if (venda == null)
            return NotFound();

        return Ok(venda);
    }

[HttpPost]
public IActionResult Create([FromBody] VendasRequest request)
{
    var venda = _service.Create(request);

    return CreatedAtAction(
        nameof(GetById),
        new { id = venda.Id },
        venda);
}
}