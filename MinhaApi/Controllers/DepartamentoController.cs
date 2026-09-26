using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DepartamentoController : ControllerBase
{
    private readonly IDepartamentoService _service;

    public DepartamentoController(IDepartamentoService service)
        => _service = service;

    [HttpGet]
    public IActionResult GetAll()
    {
        var departamento = _service.GetAll();

        return Ok(departamento);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var departamento = _service.GetById(id);

        if (departamento == null)
            return NotFound();

        return Ok(departamento);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Departamento departamento)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var criado = _service.Create(departamento);

            return CreatedAtAction(
                nameof(GetById),
                new { id = criado.id },
                criado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(
        int id,
        [FromBody] Departamento departamento)
    {
        var atualizado = _service.Update(id, departamento);

        if (atualizado == null)
            return NotFound();

        return Ok(atualizado);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int Id)
    {
        var deletado = _service.Delete(Id);

        if (!deletado)
            return NotFound();

        return NoContent();
    }
}