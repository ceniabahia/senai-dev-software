import  api  from './api'
import type { Venda, VendaCriar } from '../types/Vendas'

export const vendasService = {
  async listar(): Promise<Venda[]> {
    const { data } = await api.get<Venda[]>('/vendas')
    return data
  },

  async criar(venda: VendaCriar): Promise<Venda> {
    const { data } = await api.post<Venda>('/vendas', venda)
    return data
  }
}