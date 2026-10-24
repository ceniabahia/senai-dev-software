
import { useEffect, useState } from 'react'
import type { Cliente } from '../types/Cliente'
import type { Produto } from '../types/Produto'

import { produtoService } from '../services/ProdutoService'
import { vendasService } from '../services/VendasService'
import '../assets/css/Venda.css'
import { ClienteService } from '../services/ClienteService'

interface Props {
  onVendaCriada: () => void
}

function VendasForm({ onVendaCriada }: Props) {
  const [clientes, setClientes] = useState<Cliente[]>([])
  const [produtos, setProdutos] = useState<Produto[]>([])

  const [clienteId, setClienteId] = useState('')
  const [produtoId, setProdutoId] = useState('')
  const [quantidade, setQuantidade] = useState('1')

  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  useEffect(() => {
    const carregarOpcoes = async () => {
      try {
        const [listaClientes, listaProdutos] = await Promise.all([
          ClienteService.listar(),
          produtoService.listar()
        ])
        setClientes(listaClientes)
        setProdutos(listaProdutos)
      } catch {
        setErro('Erro ao carregar clientes e produtos.')
      }
    }
    carregarOpcoes()
  }, [])

  const produtoSelecionado = produtos.find(p => p.id === Number(produtoId))
  const total = produtoSelecionado
    ? produtoSelecionado.preco * Number(quantidade || 0)
    : 0

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setErro(null)
    try {
      setLoading(true)
      await vendasService.criar({
        clienteId: Number(clienteId),
        produtoId: Number(produtoId),
        quantidade: Number(quantidade)
      })
      setClienteId('')
      setProdutoId('')
      setQuantidade('1')
      onVendaCriada()
    } catch {
      setErro('Erro ao registrar a venda. Tente novamente.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <form className="venda-form" onSubmit={handleSubmit}>
      <h2 className="venda-form-titulo">Registrar venda</h2>

      {erro && <p className="venda-erro">{erro}</p>}

      <div className="venda-form-campos">
        <div className="venda-campo">
          <label htmlFor="cliente">Cliente</label>
          <select
            id="cliente"
            value={clienteId}
            onChange={e => setClienteId(e.target.value)}
            required
          >
            <option value="">Selecione...</option>
            {clientes.map(c => (
              <option key={c.id} value={c.id}>{c.nome}</option>
            ))}
          </select>
        </div>

        <div className="venda-campo">
          <label htmlFor="produto">Produto</label>
          <select
            id="produto"
            value={produtoId}
            onChange={e => setProdutoId(e.target.value)}
            required
          >
            <option value="">Selecione...</option>
            {produtos.map(p => (
              <option key={p.id} value={p.id}>{p.nome}</option>
            ))}
          </select>
        </div>

        <div className="venda-campo">
          <label htmlFor="quantidade">Quantidade</label>
          <input
            id="quantidade"
            type="number"
            min="1"
            step="1"
            value={quantidade}
            onChange={e => setQuantidade(e.target.value)}
            required
          />
        </div>

        <div className="venda-campo">
          <label>Total</label>
          <span className="venda-total-previa">
            {total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
          </span>
        </div>
      </div>

      <button className="venda-botao" type="submit" disabled={loading}>
        {loading ? 'Salvando...' : 'Registrar venda'}
      </button>
    </form>
  )
}

export default VendasForm