
import api from './api'
import type { Produto, NovoProduto } from '../types/Produto'

export const produtoService = {

  listar: async (): Promise<Produto[]> => {
    const { data } = await api.get('/produto')
    return data
  },

  buscarPorId: async (id: number): Promise<Produto> => {
    const { data } = await api.get(`/produto/${id}`)
    return data
  },

  criar: async (p: NovoProduto): Promise<Produto> => {
    const { data } = await api.post('/produto', p)
    return data
  },

  atualizar: async (p: Produto): Promise<Produto> => {
    const { data } = await api.put('/produto/' + p.id, p)
    return data
  },

  excluir: async (id: number): Promise<void> => {
    await api.delete(`/produto/${id}`)
  },

  uploadFoto: async (id: number, arquivo: File): Promise<string> => {
    const formData = new FormData()
    formData.append('arquivo', arquivo)

    const { data } = await api.post(
      `/produtos/${id}/foto`,
      formData,
      { headers: { 'Content-Type': 'multipart/form-data' } }
    )

    return data.fotoBase64
  },

  removerFoto: async (id: number): Promise<void> => {
    await api.delete(`/produtos/${id}/foto`)
  }

}
