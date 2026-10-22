
import { useEffect, useState } from 'react'
import type { Cliente } from '../types/Cliente'
import { ClienteService } from '../services/ClienteService'
import ClienteForm from '../components/ClienteForm'
import ClienteList from '../components/ClienteList'
import '../assets/css/Cliente.css'

function ClientesPage() {
  const [clientes, setClientes] = useState<Cliente[]>([])
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)
  const [clienteEditando, setClienteEditando] = useState<Cliente | null>(null)

  const carregarClientes = async () => {
    setLoading(true)
    setErro(null)

    try {
      setClientes(await ClienteService.listar())
    } catch {
      setErro('Erro ao carregar clientes.')
    } finally {
      setLoading(false)
    }
  }

  const excluirCliente = async (id: number) => {
    setErro(null)

    try {
      await ClienteService.excluir(id)

      if (clienteEditando?.id === id) {
        setClienteEditando(null)
      }

      await carregarClientes()
    } catch {
      setErro('Não foi possível excluir o cliente. Ele pode ter vendas registradas.')
    }
  }

  const editarCliente = (cliente: Cliente) => {
    setClienteEditando(cliente)
    setErro(null)
  }

  const cancelarEdicao = () => {
    setClienteEditando(null)
  }

  const salvarEdicao = async () => {
    setClienteEditando(null)
    await carregarClientes()
  }

  useEffect(() => {
    carregarClientes()
  }, [])

  return (
    <div>
      <h1 className="cliente-titulo">Gestão de Clientes</h1>

      <ClienteForm
        onClienteCriado={carregarClientes}
        clienteEditando={clienteEditando}
        onClienteAtualizado={salvarEdicao}
        onCancelarEdicao={cancelarEdicao}
      />

      <h2 className="cliente-subtitulo">Clientes cadastrados</h2>

      {erro && <p className="cliente-erro">{erro}</p>}

      <ClienteList
        clientes={clientes}
        loading={loading}
        onExcluir={excluirCliente}
        onEditar={editarCliente}
      />
    </div>
  )
}

export default ClientesPage
