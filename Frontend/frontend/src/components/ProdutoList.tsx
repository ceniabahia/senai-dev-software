
import { useState } from 'react'
import type { Produto } from '../types/Produto'
import ModalFoto from '../components/produtos/ModalFoto'

interface Props {
  produtos: Produto[]
  loading: boolean
  editandoId?: number | null
  onEditar: (produto: Produto) => void
  onExcluir: (id: number) => void
}

function formatarPreco(valor: number) {
  return valor.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  })
}

function textoEstoque(estoque: number) {
  if (estoque <= 0) return 'Sem estoque'
  return `${estoque} em estoque`
}

function classeEstoque(estoque: number) {
  if (estoque <= 0) return 'estoque-zerado'
  if (estoque <= 5) return 'estoque-baixo'
  return 'estoque-ok'
}

function ProdutoList({
  produtos,
  loading,
  editandoId,
  onEditar,
  onExcluir
}: Props) {

  // Controla o produto selecionado para abrir a foto
  const [produtoModal, setProdutoModal] =
    useState<Produto | null>(null)

  if (loading)
    return <p className="vazio">Carregando...</p>

  if (produtos.length === 0)
    return <p className="vazio">Nenhum produto cadastrado ainda.</p>

  const confirmarExclusao = (produto: Produto) => {
    if (window.confirm(`Excluir o produto "${produto.nome}"?`)) {
      onExcluir(produto.id)
    }
  }

  return (
    <>
      <ul className="lista">
        {produtos.map(p => (
          <li
            key={p.id}
            className={`produto-item ${editandoId === p.id ? 'produto-item-editando' : ''}`}
          >

            {/* FOTO DO PRODUTO */}
            <div
              onClick={() =>
                p.fotoBase64 && setProdutoModal(p)
              }
              style={{
                cursor: p.fotoBase64 ? 'pointer' : 'default',
                position: 'relative',
                overflow: 'hidden',
                borderRadius: '8px',
                width: 100,
                minWidth: 100,
                height: 100
              }}
            >
              {p.fotoBase64 ? (
                <>
                  <img
                    src={p.fotoBase64}
                    alt={p.nome}
                    style={{
                      width: '100%',
                      height: '100%',
                      objectFit: 'cover',
                      display: 'block'
                    }}
                  />

                  <div
                    style={{
                      position: 'absolute',
                      top: 5,
                      right: 5,
                      background: 'rgba(0,0,0,0.5)',
                      color: 'white',
                      borderRadius: '50%',
                      width: 25,
                      height: 25,
                      display: 'flex',
                      alignItems: 'center',
                      justifyContent: 'center',
                      fontSize: 12
                    }}
                  >
                    🔍
                  </div>
                </>
              ) : (
                <div
                  style={{
                    width: '100%',
                    height: '100%',
                    background: '#f0f0f0',
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    justifyContent: 'center',
                    color: '#bbb'
                  }}
                >
                  <span style={{ fontSize: 28 }}>📷</span>
                  <span style={{ fontSize: 10, marginTop: 4 }}>
                    sem foto
                  </span>
                </div>
              )}
            </div>

            {/* INFORMAÇÕES DO PRODUTO */}
            <div className="produto-info">
              <span className="produto-nome">{p.nome}</span>

              <span className={`produto-estoque ${classeEstoque(p.estoque)}`}>
                {textoEstoque(p.estoque)}
              </span>
            </div>

            <span className="produto-preco">
              {formatarPreco(p.preco)}
            </span>

            {/* BOTÕES DE AÇÃO */}
            <div className="produto-acoes">
              <button
                type="button"
                className="produto-editar"
                aria-label={`Editar ${p.nome}`}
                onClick={() => onEditar(p)}
              >
                Editar
              </button>

              <button
                type="button"
                className="produto-excluir"
                aria-label={`Excluir ${p.nome}`}
                onClick={() => confirmarExclusao(p)}
              >
                Excluir
              </button>
            </div>

          </li>
        ))}
      </ul>

      {/* MODAL DA FOTO - FORA DO MAP */}
      {produtoModal && (
        <ModalFoto
          produto={produtoModal}
          onClose={() => setProdutoModal(null)}
        />
      )}
    </>
  )
}

export default ProdutoList
