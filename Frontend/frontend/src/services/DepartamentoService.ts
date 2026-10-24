// src/services/DepartamentoService.ts
import api from './api'
import type { Departamento, NovoDepartamento } from '../types/Departamento'

export const departamentoService = {
  listar: async (): Promise<Departamento[]> => {
    const { data } = await api.get('/departamento')
    return data
  },

  criar: async (d: NovoDepartamento): Promise<Departamento> => {
    const { data } = await api.post('/departamento', d)
    return data
  },

  atualizar: async (_id: number, d: Departamento): Promise<Departamento> => {
    const { data } = await api.put(`/departamento/${d.id}`, d)
    return data
  },

  excluir: async (id: number): Promise<void> => {
    await api.delete(`/departamento/${id}`)
  }
}

// Mantém o nome com maiúscula, para quem ainda importar assim
export const DepartamentoService = departamentoService