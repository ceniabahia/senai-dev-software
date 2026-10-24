import { useEffect, useState } from 'react'
import type { Venda } from '../types/Vendas'
import type { Cliente } from '../types/Cliente'
import type { Produto } from '../types/Produto'
import { vendasService } from '../services/VendasService'
import { ClienteService } from '../services/ClienteService'
import { produtoService } from '../services/ProdutoService'
import VendasForm from '../components/VendasForm'
import VendasList from '../components/VendasList'
import '../assets/css/Venda.css'

function VendasPage() {
  const [vendas, setVendas] = useState<Venda[]>([])
  const [clientes, setClientes] = useState<Cliente[]>([])
  const [produtos, setProdutos] = useState<Produto[]>([])
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const carregarVendas = async () => {
    setLoading(true)
    setErro(null)

    try {
      const [listaVendas, listaClientes, listaProdutos] = await Promise.all([
        vendasService.listar(),
        ClienteService.listar(),
        produtoService.listar()
      ])
      setVendas(listaVendas)
      setClientes(listaClientes)
      setProdutos(listaProdutos)
    } catch {
      setErro('Erro ao carregar vendas.')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    carregarVendas()
  }, [])

  return (
    <div>
      <h1 className="venda-titulo">Gestão de Vendas</h1>

      <VendasForm onVendaCriada={carregarVendas} />

      <h2 className="venda-subtitulo">Vendas registradas</h2>

      {erro && <p className="venda-erro">{erro}</p>}

      <VendasList
        vendas={vendas}
        clientes={clientes}
        produtos={produtos}
        loading={loading}
      />
    </div>
  )
}

export default VendasPage