using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FornecedorController : ControllerBase
{
    private readonly IFornecedorService _service;

    public FornecedorController(IFornecedorService service)
        => _service = service;

    [HttpGet]
    public IActionResult GetAll()
    {
        var fornecedores = _service.GetAll();

        return Ok(fornecedores);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var fornecedor = _service.GetById(id);

        if (fornecedor == null)
            return NotFound();

        return Ok(fornecedor);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Fornecedor fornecedor)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var criado = _service.Create(fornecedor);

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
        [FromBody] Fornecedor fornecedor)
    {
        var atualizado = _service.Update(id, fornecedor);

        if (atualizado == null)
            return NotFound();

        return Ok(atualizado);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deletado = _service.Delete(id);

        if (!deletado)
            return NotFound();

        return NoContent();
    }
}