
using MinhaApi.Models;
using MinhaApi.Repositories;
using Microsoft.AspNetCore.Http;

namespace MinhaApi.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repo;
        private readonly IFotoService _fotoService;

        public ProdutoService(
            IProdutoRepository repo,
            IFotoService fotoService)
        {
            _repo = repo;
            _fotoService = fotoService;
        }

        // LISTAR TODOS OS PRODUTOS
        public IEnumerable<Produto> GetAll()
        {
            var produtos = _repo.GetAll();

            foreach (var produto in produtos)
            {
                produto.FotoBase64 =
                    _fotoService.LerBase64(produto.Foto);
            }

            return produtos;
        }

        // BUSCAR PRODUTO POR ID
        public Produto? GetById(int id)
        {
            var produto = _repo.GetById(id);

            if (produto == null)
                return null;

            produto.FotoBase64 =
                _fotoService.LerBase64(produto.Foto);

            return produto;
        }

        // CRIAR PRODUTO
        public Produto Create(Produto produto)
        {
            if (produto.Preco < 0)
                throw new ArgumentException("Preço inválido.");

            _repo.Add(produto);

            return produto;
        }

        // ATUALIZAR PRODUTO
        public Produto? Update(int id, Produto p)
        {
            var produtoExistente = _repo.GetById(id);

            if (produtoExistente == null)
                return null;

            if (p.Preco < 0)
                throw new ArgumentException("Preço inválido.");

            p.Id = id;

            // Preserva a foto existente.
            p.Foto = produtoExistente.Foto;

            _repo.Update(p);

            return p;
        }

        // EXCLUIR PRODUTO
        public bool Delete(int id)
        {
            var produto = _repo.GetById(id);

            if (produto == null)
                return false;

            var fotoAntiga = produto.Foto;

            // Primeiro exclui o produto do banco.
            _repo.Delete(id);

            // Depois exclui a foto física.
            if (!string.IsNullOrEmpty(fotoAntiga))
            {
                _fotoService.Deletar(fotoAntiga);
            }

            return true;
        }

        // SALVAR OU SUBSTITUIR FOTO DO PRODUTO
        public async Task<string> SalvarFoto(
            int id,
            IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                throw new ArgumentException(
                    "Nenhum arquivo enviado.");
            }

            var produto = _repo.GetById(id);

            if (produto == null)
            {
                throw new KeyNotFoundException(
                    "Produto não encontrado.");
            }

            var fotoAntiga = produto.Foto;

            // Salva a nova imagem.
            var nomeArquivo =
                await _fotoService.SalvarAsync(arquivo);

            try
            {
                // Atualiza o banco com o nome da nova foto.
                _repo.AtualizarFoto(id, nomeArquivo);
            }
            catch
            {
                // Remove a nova imagem se o banco falhar.
                _fotoService.Deletar(nomeArquivo);
                throw;
            }

            // Remove a foto anterior após atualizar o banco.
            if (!string.IsNullOrEmpty(fotoAntiga))
            {
                _fotoService.Deletar(fotoAntiga);
            }

            // Retorna a imagem em Base64.
            var fotoBase64 =
                _fotoService.LerBase64(nomeArquivo);

            if (fotoBase64 == null)
            {
                throw new InvalidOperationException(
                    "Não foi possível carregar a foto.");
            }

            return fotoBase64;
        }

        // REMOVER FOTO DO PRODUTO
        public void RemoverFoto(int id)
        {
            var produto = _repo.GetById(id);

            if (produto == null)
            {
                throw new KeyNotFoundException(
                    "Produto não encontrado.");
            }

            if (string.IsNullOrEmpty(produto.Foto))
                return;

            var fotoAntiga = produto.Foto;

            // Primeiro remove a referência no banco.
            _repo.AtualizarFoto(id, null);

            // Depois remove o arquivo físico.
            _fotoService.Deletar(fotoAntiga);
        }
    }
}
