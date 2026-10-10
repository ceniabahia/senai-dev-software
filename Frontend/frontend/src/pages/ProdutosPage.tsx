import { useEffect, useState } from 'react'
import type { Produto } from '../types/Produto'
import { produtoService } from '../services/produtoService'
import ProdutoForm from '../components/ProdutoForm'
import ProdutoList from '../components/ProdutoList'
import '../assets/css/Produto.css'

function ProdutosPage() {
  const [produtos, setProdutos] = useState<Produto[]>([])
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

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

  useEffect(() => {
    carregarProdutos()
  }, [])

  return (
    <div>
      <h1 className="pagina-titulo">Gestão de Produtos</h1>

      <div className="produtos-grid">
        <ProdutoForm onProdutoCriado={carregarProdutos} />

        <section>
          <div className="produtos-topo">
            <h2>Produtos cadastrados</h2>
          </div>

          {erro && <p className="erro">{erro}</p>}

          <ProdutoList produtos={produtos} loading={loading} />
        </section>
      </div>
    </div>
  )
}

export default ProdutosPage