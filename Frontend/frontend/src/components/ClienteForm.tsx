import { useState } from 'react'
import { ClienteService } from '../services/clienteService'
import '../assets/css/Cliente.css'

interface Props {
  onClienteCriado: () => void
}

function ClienteForm({ onClienteCriado }: Props) {
  const [nome, setNome] = useState('')
  const [email, setEmail] = useState('')
  const [cpf, setCpf] = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setErro(null)
    try {
      setLoading(true)
      await ClienteService.criar({ nome, email, cpf })
      setNome('')
      setEmail('')
      setCpf('')
      onClienteCriado()
    } catch {
      setErro('Erro ao cadastrar. Tente novamente.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <form className="cliente-form" onSubmit={handleSubmit}>
      <h2 className="cliente-form-titulo">Cadastrar cliente</h2>

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

      <button className="cliente-botao" type="submit" disabled={loading}>
        {loading ? 'Salvando...' : 'Cadastrar'}
      </button>
    </form>
  )
}

export default ClienteForm