import type { Produto } from '../types/Produto'

interface Props {
  produtos: Produto[]
  loading: boolean
}

function formatarPreco(valor: number) {
  return valor.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  })
}

function ProdutoList({ produtos, loading }: Props) {
  if (loading)
    return <p className="vazio">Carregando...</p>

  if (produtos.length === 0)
    return <p className="vazio">Nenhum produto encontrado. Cadastre o primeiro ao lado.</p>

  return (
    <ul className="lista">
      {produtos.map(p => (
        <li key={p.id} className="produto-item">
          <span className="produto-inicial">{p.nome.charAt(0).toUpperCase()}</span>
          <span className="produto-nome">{p.nome}</span>
          <span className="produto-preco">{formatarPreco(p.preco)}</span>
        </li>
      ))}
    </ul>
  )
}

export default ProdutoList