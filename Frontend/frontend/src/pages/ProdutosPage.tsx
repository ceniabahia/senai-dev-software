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
      setProdutos(await produtoService.listar())
    } catch { setErro('Erro ao carregar produtos.') }
    finally { setLoading(false) }
  }

  useEffect(() => { carregarProdutos() }, [])

  return (<div>
    <h1>Gestão de Produtos</h1>
    <ProdutoForm onProdutoCriado={carregarProdutos} />
    {erro && <p>{erro}</p>}
    <ProdutoList produtos={produtos} loading={loading} />
  </div>)
}
export default ProdutosPage