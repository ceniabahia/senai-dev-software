import { useState } from 'react'
import { produtoService } from '../services/produtoService'

interface Props {
  onProdutoCriado: () => void
}

function ProdutoForm({ onProdutoCriado }: Props) {
  const [nome, setNome] = useState('')
  const [preco, setPreco] = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setErro(null)
    try {
      setLoading(true)
      await produtoService.criar({
        nome,
        preco: Number(preco)
      })
      setNome('')
      setPreco('')
      onProdutoCriado()
    } catch {
      setErro('Erro ao cadastrar. Tente novamente.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <h2 className="form-titulo">Cadastrar produto</h2>

      {erro && <p className="erro-form">{erro}</p>}

      <div className="campo">
        <label htmlFor="nome">Nome</label>
        <input
          id="nome"
          type="text"
          placeholder="Ex.: Teclado mecânico"
          value={nome}
          onChange={e => setNome(e.target.value)}
          required
        />
      </div>

      <div className="campo">
        <label htmlFor="preco">Preço</label>
        <input
          id="preco"
          type="number"
          step="0.01"
          min="0"
          placeholder="0,00"
          value={preco}
          onChange={e => setPreco(e.target.value)}
          required
        />
      </div>

      <button className="botao" type="submit" disabled={loading}>
        {loading ? 'Salvando...' : 'Cadastrar'}
      </button>
    </form>
  )
}

export default ProdutoForm