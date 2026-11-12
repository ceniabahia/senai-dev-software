
import { useEffect, useState } from 'react'
import type { Produto } from '../types/Produto'
import { produtoService } from '../services/ProdutoService'
import FotoUpload from '../components/produtos/FotoUpload'

interface Props {
  onSalvo: () => void
  produtoEdicao?: Produto | null
  onCancelar?: () => void
}

function ProdutoForm({ onSalvo, produtoEdicao, onCancelar }: Props) {
  const [nome, setNome] = useState('')
  const [preco, setPreco] = useState('')
  const [estoque, setEstoque] = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)
  const [fotoBase64Atual, setFotoBase64Atual] = useState<string | null>(null)

  const editando = !!produtoEdicao

  async function carregarProduto() {
    if (!produtoEdicao?.id) return

    try {
      const produto = await produtoService.buscarPorId(produtoEdicao.id)

      setNome(produto.nome)
      setPreco(String(produto.preco))
      setEstoque(String(produto.estoque))
      setFotoBase64Atual(produto.fotoBase64 ?? null)
    } catch {
      setErro('Erro ao carregar os dados do produto.')
    }
  }

  useEffect(() => {
    setErro(null)

    if (produtoEdicao) {
      carregarProduto()
    } else {
      setNome('')
      setPreco('')
      setEstoque('')
      setFotoBase64Atual(null)
    }
  }, [produtoEdicao])

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setErro(null)

    try {
      setLoading(true)

      if (produtoEdicao) {
        await produtoService.atualizar({
          ...produtoEdicao,
          nome,
          preco: Number(preco),
          estoque: Number(estoque)
        })
      } else {
        await produtoService.criar({
          nome,
          preco: Number(preco),
          estoque: Number(estoque),
          ativo: true
        })

        setNome('')
        setPreco('')
        setEstoque('')
        setFotoBase64Atual(null)
      }

      onSalvo()
    } catch {
      setErro(
        editando
          ? 'Erro ao salvar as alterações. Tente novamente.'
          : 'Erro ao cadastrar. Tente novamente.'
      )
    } finally {
      setLoading(false)
    }
  }

  return (
    <form className="form-card" onSubmit={handleSubmit}>
      <h2 className="form-titulo">
        {editando ? 'Editar produto' : 'Cadastrar produto'}
      </h2>

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

      <div className="campo-linha">
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

        <div className="campo">
          <label htmlFor="estoque">Estoque</label>
          <input
            id="estoque"
            type="number"
            step="1"
            min="0"
            placeholder="0"
            value={estoque}
            onChange={e => setEstoque(e.target.value)}
            required
          />
        </div>
      </div>
      <div className="form-acoes">
        <button className="botao" type="submit" disabled={loading}>
          {loading
            ? 'Salvando...'
            : editando
              ? 'Salvar alterações'
              : 'Cadastrar'}
        </button>

        {editando && (
          <button
            className="botao-secundario"
            type="button"
            onClick={onCancelar}
            disabled={loading}
          >
            Cancelar
          </button>
        )}
      </div>

      {editando && produtoEdicao && (
        <FotoUpload
          produtoId={produtoEdicao.id}
          fotoBase64Atual={fotoBase64Atual}
          onAtualizar={carregarProduto}
        />
      )}
    </form>
  )
}

export default ProdutoForm
