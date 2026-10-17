import { useEffect, useState } from 'react'
import type { Produto } from '../types/Produto'
import { produtoService } from '../services/ProdutoService'
import ProdutoForm from '../components/ProdutoForm'
import ProdutoList from '../components/ProdutoList'
import '../assets/css/Produto.css'

function ProdutosPage() {
  const [produtos, setProdutos] = useState<Produto[]>([])
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)
  const [produtoEdicao, setProdutoEdicao] = useState<Produto | null>(null)

  const carregarProdutos = async () => {
    try {
      setLoading(true)
      setErro(null)
      setProdutos(await produtoService.listar())
    } catch {
      setErro('Erro ao carregar produtos.')
    } finally {
      setLoading(false)
    }
  }

  const aoSalvar = async () => {
    setProdutoEdicao(null)
    await carregarProdutos()
  }

  const editarProduto = (produto: Produto) => {
    setProdutoEdicao(produto)
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  const excluirProduto = async (id: number) => {
    setErro(null)

    try {
      await produtoService.excluir(id)
      if (produtoEdicao?.id === id) setProdutoEdicao(null)
      await carregarProdutos()
    } catch {
      setErro('Não foi possível excluir o produto. Ele pode ter vendas registradas.')
    }
  }

  useEffect(() => {
    carregarProdutos()
  }, [])

  return (
    <div>
      <h1 className="pagina-titulo">Gestão de Produtos</h1>

      <div className="produtos-grid">
        <ProdutoForm
          onSalvo={aoSalvar}
          produtoEdicao={produtoEdicao}
          onCancelar={() => setProdutoEdicao(null)}
        />

        <section>
          <div className="produtos-topo">
            <h2>Produtos cadastrados</h2>
          </div>

          {erro && <p className="erro">{erro}</p>}

          <ProdutoList
            produtos={produtos}
            loading={loading}
            editandoId={produtoEdicao?.id}
            onEditar={editarProduto}
            onExcluir={excluirProduto}
          />
        </section>
      </div>
    </div>
  )
}

export default ProdutosPage