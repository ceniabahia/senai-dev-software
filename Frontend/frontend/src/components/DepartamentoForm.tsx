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
  const [funcionario, setFuncionario] = useState('')
  const [email, setEmail] = useState('')
  const [telefone, setTelefone] = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const editando = !!deptoEdicao

  useEffect(() => {
    setErro(null)
    setNome(deptoEdicao?.nome ?? '')
    setFuncionario(deptoEdicao?.funcionario ?? '')
    setEmail(deptoEdicao?.email ?? '')
    setTelefone(deptoEdicao?.telefone ?? '')
  }, [deptoEdicao])

  const limpar = () => {
    setNome('')
    setFuncionario('')
    setEmail('')
    setTelefone('')
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setErro(null)
    try {
      setLoading(true)

      if (deptoEdicao) {
        await departamentoService.atualizar({
          ...deptoEdicao,
          nome,
          funcionario,
          email,
          telefone
        })
      } else {
        await departamentoService.criar({
          nome,
          funcionario,
          email,
          telefone,
          ativo: true
        })
        limpar()
      }

      onSalvo()
    } catch (err: any) {
      console.error('Resposta da API:', err?.response?.data)
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
        <label htmlFor="depto-nome">Nome do departamento</label>
        <input
          id="depto-nome"
          type="text"
          placeholder="Ex.: Recursos Humanos"
          value={nome}
          onChange={e => setNome(e.target.value)}
          required
        />
      </div>

      <div className="depto-campo">
        <label htmlFor="depto-funcionario">Funcionário responsável</label>
        <input
          id="depto-funcionario"
          type="text"
          placeholder="Nome do responsável"
          value={funcionario}
          onChange={e => setFuncionario(e.target.value)}
          required
        />
      </div>

      <div className="depto-campo">
        <label htmlFor="depto-email">E-mail</label>
        <input
          id="depto-email"
          type="email"
          placeholder="nome@email.com"
          value={email}
          onChange={e => setEmail(e.target.value)}
          required
        />
      </div>

      <div className="depto-campo">
        <label htmlFor="depto-telefone">Telefone</label>
        <input
          id="depto-telefone"
          type="tel"
          placeholder="(00) 00000-0000"
          value={telefone}
          onChange={e => setTelefone(e.target.value)}
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