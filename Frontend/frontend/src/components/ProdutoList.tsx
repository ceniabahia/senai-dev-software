import type { Produto } from '../types/Produto'

interface Props {
  produtos: Produto[]
  loading: boolean
  editandoId?: number | null
  onEditar: (produto: Produto) => void
  onExcluir: (id: number) => void
}

function formatarPreco(valor: number) {
  return valor.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  })
}

function textoEstoque(estoque: number) {
  if (estoque <= 0) return 'Sem estoque'
  return `${estoque} em estoque`
}

function classeEstoque(estoque: number) {
  if (estoque <= 0) return 'estoque-zerado'
  if (estoque <= 5) return 'estoque-baixo'
  return 'estoque-ok'
}

function ProdutoList({ produtos, loading, editandoId, onEditar, onExcluir }: Props) {
  if (loading)
    return <p className="vazio">Carregando...</p>

  if (produtos.length === 0)
    return <p className="vazio">Nenhum produto cadastrado ainda.</p>

  const confirmarExclusao = (produto: Produto) => {
    if (window.confirm(`Excluir o produto "${produto.nome}"?`)) {
      onExcluir(produto.id)
    }
  }

  return (
    <ul className="lista">
      {produtos.map(p => (
        <li
          key={p.id}
          className={`produto-item ${editandoId === p.id ? 'produto-item-editando' : ''}`}
        >
          <span className="produto-inicial">{p.nome.charAt(0).toUpperCase()}</span>

          <div className="produto-info">
            <span className="produto-nome">{p.nome}</span>
            <span className={`produto-estoque ${classeEstoque(p.estoque)}`}>
              {textoEstoque(p.estoque)}
            </span>
          </div>

          <span className="produto-preco">{formatarPreco(p.preco)}</span>

          <div className="produto-acoes">
            <button
              type="button"
              className="produto-editar"
              aria-label={`Editar ${p.nome}`}
              onClick={() => onEditar(p)}
            >
              Editar
            </button>

            <button
              type="button"
              className="produto-excluir"
              aria-label={`Excluir ${p.nome}`}
              onClick={() => confirmarExclusao(p)}
            >
              Excluir
            </button>
          </div>
        </li>
      ))}
    </ul>
  )
}

export default ProdutoList