import { useEffect, useState } from 'react'
import type { Cliente } from '../types/Cliente'
import { clienteService } from '../services/clienteService'
import ClienteForm from '../components/ClienteForm'
import ClienteList from '../components/ClienteList'
import '../assets/css/Cliente.css'

function ClientesPage() {
  const [clientes, setClientes] = useState<Cliente[]>([])
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const carregarClientes = async () => {
    setLoading(true)
    setErro(null)

    try {
      setClientes(await clienteService.listar())
    } catch {
      setErro('Erro ao carregar clientes.')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    carregarClientes()
  }, [])

  return (
    <div>
      <h1 className="cliente-titulo">Gestão de Clientes</h1>

      <ClienteForm onClienteCriado={carregarClientes} />

      <h2 className="cliente-subtitulo">Clientes cadastrados</h2>

      {erro && <p className="cliente-erro">{erro}</p>}

      <ClienteList clientes={clientes} loading={loading} />
    </div>
  )
}

export default ClientesPage