export interface Venda {
  id: number
  clienteId: number
  produtoId: number
  quantidade: number
  total: number
  data: string
}

export interface VendaCriar {
  clienteId: number
  produtoId: number
  quantidade: number
}