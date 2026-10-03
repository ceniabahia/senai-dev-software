import { useEffect, useState } from 'react'
import { type Produto } from './types/Produto'
import { produtoService } from './services/produtoService'
import ProdutoForm from './components/ProdutoForm'
import ProdutoList from './components/ProdutoList'
import './App.css'

function App() {
  const [produtos, setProdutos] = useState<Produto[]>([])
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)
  const [busca, setBusca] = useState('')

  const carregarProdutos = async () => {
    try {
      setLoading(true)
      setErro(null)
      const dados = await produtoService.listar()
      setProdutos(dados)
    } catch {
      setErro('Erro ao carregar produtos.')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    carregarProdutos()
  }, [])

  const total = produtos.reduce((soma, p) => soma + p.preco, 0)

  const produtosFiltrados = produtos.filter(p =>
    p.nome.toLowerCase().includes(busca.trim().toLowerCase())
  )

  return (
    <div className="container">
      <h1 className="titulo">Gestão de Produtos</h1>

      <div className="grid">
        <aside className="painel">
          <p className="total-label">Valor total cadastrado</p>
          <p className="total">
            {total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
          </p>
          <p className="contagem">
            {produtos.length} {produtos.length === 1 ? 'produto' : 'produtos'}
          </p>

          <ProdutoForm onProdutoCriado={carregarProdutos} />
        </aside>

        <main>
          <div className="topo-lista">
            <h2 className="subtitulo">Produtos cadastrados</h2>
            <input
              className="busca"
              type="search"
              placeholder="Buscar produto"
              aria-label="Buscar produto"
              value={busca}
              onChange={e => setBusca(e.target.value)}
            />
          </div>

          {erro && <p className="erro">{erro}</p>}
          <ProdutoList produtos={produtosFiltrados} loading={loading} />
        </main>
      </div>
    </div>
  )
}

export default App