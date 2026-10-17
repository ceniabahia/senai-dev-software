// src/services/ProdutoService.ts
import api from './api'
import type { Produto, NovoProduto } from '../types/Produto'

export const produtoService = {

  listar: async (): Promise<Produto[]> => {
    const { data } = await api.get('/produto')
    return data
  },

  criar: async (p: NovoProduto): Promise<Produto> => {
    const { data } = await api.post('/produto', p)
    return data
  },
  atualizar: async ( p: Produto): Promise<Produto> => {
    const { data } = await api.put('/produto/'+ p.id, p)
    return data
  },


  excluir: async (id: number): Promise<void> => {
    await api.delete(`/produto/${id}`)

    
  }
}