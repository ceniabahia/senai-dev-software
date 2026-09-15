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

    // GET /api/vendas
    [HttpGet]
    public IActionResult GetAll()
    {
        var vendas = _service.GetAll();
        return Ok(vendas);
    }

    // GET /api/vendas/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var venda = _service.GetById(id);

        if (venda == null)
            return NotFound();

        return Ok(venda);
    }

    // POST /api/vendas
    [HttpPost]
    public IActionResult Create([FromBody] Vendas vendas)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var criado = _service.Create(vendas);

            return CreatedAtAction(
                nameof(GetById),
                new { id = criado.Id },
                criado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}