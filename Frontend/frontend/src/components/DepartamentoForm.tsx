import { useEffect, useState } from 'react'
import type { Departamento } from '../types/Departamento'
import { departamentoService } from '../services/DepartamentoService'

interface Props {
  onSalvo: () => void
  deptoEdicao?: Departamento | null
  onCancelar?: () => void
}

function DepartamentoForm({ onSalvo, deptoEdicao, onCancelar }: Props) {
  const [nome, setNome] = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const editando = !!deptoEdicao

  useEffect(() => {
    setErro(null)
    setNome(deptoEdicao ? deptoEdicao.nome : '')
  }, [deptoEdicao])

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setErro(null)
    try {
      setLoading(true)

      if (deptoEdicao) {
        await departamentoService.atualizar(deptoEdicao.id, { ...deptoEdicao, nome })
      } else {
        await departamentoService.criar({ nome })
        setNome('')
      }

      onSalvo()
    } catch {
      setErro(editando ? 'Erro ao salvar as alterações.' : 'Erro ao cadastrar. Tente novamente.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <form className="depto-form" onSubmit={handleSubmit}>
      <h2 className="depto-form-titulo">
        {editando ? 'Editar departamento' : 'Cadastrar departamento'}
      </h2>

      {erro && <p className="depto-erro">{erro}</p>}

      <div className="depto-campo">
        <label htmlFor="depto-nome">Nome</label>
        <input
          id="depto-nome"
          type="text"
          placeholder="Ex.: Recursos Humanos"
          value={nome}
          onChange={e => setNome(e.target.value)}
          required
        />
      </div>

      <div className="depto-acoes-form">
        <button className="depto-botao" type="submit" disabled={loading}>
          {loading ? 'Salvando...' : editando ? 'Salvar alterações' : 'Cadastrar'}
        </button>

        {editando && (
          <button
            className="depto-botao-secundario"
            type="button"
            onClick={onCancelar}
            disabled={loading}
          >
            Cancelar
          </button>
        )}
      </div>
    </form>
  )
}

export default DepartamentoForm