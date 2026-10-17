import type { Cliente } from '../types/Cliente'
import '../assets/css/Cliente.css'

interface Props {
  clientes: Cliente[]
  loading: boolean
  onExcluir: (id: number) => void
}

function ClienteList({ clientes, loading, onExcluir }: Props) {
  if (loading)
    return <p className="cliente-vazio">Carregando...</p>

  if (clientes.length === 0)
    return <p className="cliente-vazio">Nenhum cliente cadastrado ainda.</p>

  const confirmarExclusao = (cliente: Cliente) => {
    if (window.confirm(`Excluir o cliente "${cliente.nome}"?`)) {
      onExcluir(cliente.id)
    }
  }

  return (
    <ul className="cliente-lista">
      {clientes.map(c => (
        <li key={c.id} className="cliente-item">
          <span className="cliente-inicial">{c.nome.charAt(0).toUpperCase()}</span>

          <div className="cliente-dados">
            <span className="cliente-nome">{c.nome}</span>
            <span className="cliente-email">{c.email}</span>
          </div>

          <span className="cliente-cpf">{c.cpf}</span>

          <button
            type="button"
            className="cliente-excluir"
            aria-label={`Excluir ${c.nome}`}
            onClick={() => confirmarExclusao(c)}
          >
            Excluir
          </button>
        </li>
      ))}
    </ul>
  )
}

export default ClienteList