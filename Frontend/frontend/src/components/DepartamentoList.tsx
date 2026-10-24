import type { Departamento } from '../types/Departamento'

interface Props {
  departamentos: Departamento[]
  loading: boolean
  editandoId?: number | null
  onEditar: (d: Departamento) => void
  onExcluir: (id: number) => void
}

function DepartamentoList({ departamentos, loading, editandoId, onEditar, onExcluir }: Props) {
  if (loading)
    return <p className="depto-vazio">Carregando...</p>

  if (departamentos.length === 0)
    return <p className="depto-vazio">Nenhum departamento cadastrado ainda.</p>

  const confirmarExclusao = (d: Departamento) => {
    if (window.confirm(`Excluir o departamento "${d.nome}"?`)) {
      onExcluir(d.id)
    }
  }

  return (
    <ul className="depto-lista">
      {departamentos.map(d => (
        <li
          key={d.id}
          className={`depto-item ${editandoId === d.id ? 'depto-item-editando' : ''}`}
        >
          <span className="depto-inicial">{d.nome.charAt(0).toUpperCase()}</span>
          <span className="depto-nome">{d.nome}</span>

          <div className="depto-acoes">
            <button
              type="button"
              className="depto-editar"
              aria-label={`Editar ${d.nome}`}
              onClick={() => onEditar(d)}
            >
              Editar
            </button>
            <button
              type="button"
              className="depto-excluir"
              aria-label={`Excluir ${d.nome}`}
              onClick={() => confirmarExclusao(d)}
            >
              Excluir
            </button>
          </div>
        </li>
      ))}
    </ul>
  )
}

export default DepartamentoList