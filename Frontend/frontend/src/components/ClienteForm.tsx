
import { useEffect, useState } from 'react'
import type { FormEvent } from 'react'
import type { Cliente, NovoCliente } from '../types/Cliente'
import { ClienteService } from '../services/ClienteService'
import '../assets/css/Cliente.css'

interface Props {
  onClienteCriado: () => void | Promise<void>
  clienteEditando: Cliente | null
  onClienteAtualizado: () => void | Promise<void>
  onCancelarEdicao: () => void
}

function ClienteForm({
  onClienteCriado,
  clienteEditando,
  onClienteAtualizado,
  onCancelarEdicao
}: Props) {
  const [nome, setNome] = useState('')
  const [email, setEmail] = useState('')
  const [cpf, setCpf] = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  useEffect(() => {
    if (clienteEditando) {
      setNome(clienteEditando.nome)
      setEmail(clienteEditando.email)
      setCpf(clienteEditando.cpf)
      setErro(null)
    } else {
      setNome('')
      setEmail('')
      setCpf('')
    }
  }, [clienteEditando])

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    setErro(null)

    try {
      setLoading(true)

      if (clienteEditando) {
        const cliente: Cliente = {
          ...clienteEditando,
          nome,
          email,
          cpf
        }

        await ClienteService.atualizar(cliente)
        await onClienteAtualizado()
      } else {
        const novoCliente: NovoCliente = {
          nome,
          email,
          cpf
        }

        await ClienteService.criar(novoCliente)
        setNome('')
        setEmail('')
        setCpf('')
        await onClienteCriado()
      }
    } catch {
      setErro(
        clienteEditando
          ? 'Erro ao atualizar cliente. Tente novamente.'
          : 'Erro ao cadastrar. Tente novamente.'
      )
    } finally {
      setLoading(false)
    }
  }

  const cancelarEdicao = () => {
    setNome('')
    setEmail('')
    setCpf('')
    setErro(null)
    onCancelarEdicao()
  }

  return (
    <form className="cliente-form" onSubmit={handleSubmit}>
      <h2 className="cliente-form-titulo">
        {clienteEditando ? 'Editar cliente' : 'Cadastrar cliente'}
      </h2>

      {erro && <p className="cliente-erro">{erro}</p>}

      <div className="cliente-form-campos">
        <div className="cliente-campo">
          <label htmlFor="nome">Nome</label>
          <input
            id="nome"
            type="text"
            placeholder="Nome completo"
            value={nome}
            onChange={e => setNome(e.target.value)}
            required
          />
        </div>

        <div className="cliente-campo">
          <label htmlFor="email">E-mail</label>
          <input
            id="email"
            type="email"
            placeholder="nome@email.com"
            value={email}
            onChange={e => setEmail(e.target.value)}
            required
          />
        </div>

        <div className="cliente-campo">
          <label htmlFor="cpf">CPF</label>
          <input
            id="cpf"
            type="text"
            placeholder="000.000.000-00"
            value={cpf}
            onChange={e => setCpf(e.target.value)}
            required
          />
        </div>
      </div>

      <button
        className="cliente-botao"
        type="submit"
        disabled={loading}
      >
        {loading
          ? 'Salvando...'
          : clienteEditando
            ? 'Salvar alterações'
            : 'Cadastrar'}
      </button>

      {clienteEditando && (
        <button
          className="cliente-cancelar"
          type="button"
          onClick={cancelarEdicao}
          disabled={loading}
        >
          Cancelar
        </button>
      )}
    </form>
  )
}

export default ClienteForm
