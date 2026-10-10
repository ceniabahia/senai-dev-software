import type { Venda } from '../types/Vendas'
import type { Cliente } from '../types/Cliente'
import type { Produto } from '../types/Produto'
import '../assets/css/Venda.css'

interface Props {
  vendas: Venda[]
  clientes: Cliente[]
  produtos: Produto[]
  loading: boolean
}

function formatarPreco(valor?: number) {
  return (valor ?? 0).toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  })
}

function formatarData(data?: string) {
  if (!data) return ''
  const d = new Date(data)
  return isNaN(d.getTime()) ? '' : d.toLocaleDateString('pt-BR')
}

function VendasList({ vendas, clientes, produtos, loading }: Props) {
  if (loading)
    return <p className="venda-vazio">Carregando...</p>

  if (vendas.length === 0)
    return <p className="venda-vazio">Nenhuma venda registrada ainda.</p>

  return (
    <ul className="venda-lista">
      {vendas.map(v => {
        const cliente = clientes.find(c => c.id === v.clienteId)
        const produto = produtos.find(p => p.id === v.produtoId)
        const total = v.total ?? (produto ? produto.preco * v.quantidade : 0)

        return (
          <li key={v.id} className="venda-item">
            <div className="venda-info">
              <span className="venda-principal">
                {produto?.nome ?? 'Produto removido'} × {v.quantidade}
              </span>
              <span className="venda-detalhe">
                {cliente?.nome ?? 'Cliente removido'}
                {v.data && ` • ${formatarData(v.data)}`}
              </span>
            </div>
            <span className="venda-total">{formatarPreco(total)}</span>
          </li>
        )
      })}
    </ul>
  )
}

export default VendasList