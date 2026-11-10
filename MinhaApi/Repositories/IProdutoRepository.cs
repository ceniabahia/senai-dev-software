
using MinhaApi.Models;

namespace MinhaApi.Repositories
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> GetAll();

        Produto? GetById(int id);

        void Add(Produto produto);

        void Update(Produto produto);

        void Delete(int id);

        void AtualizarEstoque(int id, int estoque);
        // Adicionar ao final da interface existente:
void AtualizarFoto(int id, string? nomeArquivo);
    }
}
