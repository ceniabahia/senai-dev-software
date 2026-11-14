
using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutoController(IProdutoService service)
        => _service = service;

    // LISTAR TODOS OS PRODUTOS
    [HttpGet]
    public IActionResult GetAll()
    {
        var produtos = _service.GetAll();
        return Ok(produtos);
    }

    // BUSCAR PRODUTO POR ID
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var produto = _service.GetById(id);

        if (produto == null)
            return NotFound();

        return Ok(produto);
    }

    // CADASTRAR PRODUTO
    [HttpPost]
    public IActionResult Create([FromBody] Produto produto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var criado = _service.Create(produto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = criado.Id },
            criado);
    }

    // ATUALIZAR PRODUTO
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Produto produto)
    {
        var atualizado = _service.Update(id, produto);

        if (atualizado == null)
            return NotFound();

        return Ok(atualizado);
    }

    // EXCLUIR PRODUTO
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deletado = _service.Delete(id);

        if (!deletado)
            return NotFound();

        return NoContent();
    }

    // ENVIAR OU ATUALIZAR FOTO DO PRODUTO
    [HttpPost("{id}/foto")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFoto(
        int id,
        IFormFile arquivo)
    {
        if (arquivo == null || arquivo.Length == 0)
        {
            return BadRequest(new
            {
                message = "Nenhum arquivo enviado."
            });
        }

        try
        {
            var fotoBase64 = await _service.SalvarFoto(id, arquivo);

            return Ok(new
            {
                message = "Foto atualizada com sucesso.",
                fotoBase64
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // REMOVER FOTO DO PRODUTO
    [HttpDelete("{id}/foto")]
    public IActionResult RemoverFoto(int id)
    {
        try
        {
            _service.RemoverFoto(id);

            return Ok(new
            {
                message = "Foto removida com sucesso."
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
