import api from './api'
import type { Cliente, NovoCliente } from '../types/Cliente'

export const ClienteService = {
  listar: async (): Promise<Cliente[]> => {
    const { data } = await api.get('/cliente')
    return data
  },
  criar: async (c: NovoCliente): Promise<Cliente> => {
    const { data } = await api.post('/cliente', c)
    return data
  },

atualizar: async (c: Cliente): Promise<Cliente> => {
  const { data } = await api.put('/cliente/' + c.id, c)
  return data
},

  

    excluir: async (id: number): Promise<void> => {
    await api.delete(`/cliente/${id}`)
  

  }
}