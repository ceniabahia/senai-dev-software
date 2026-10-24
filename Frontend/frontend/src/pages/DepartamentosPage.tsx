import { useEffect, useState } from 'react'
import type { Departamento } from '../types/Departamento'
import { departamentoService } from '../services/DepartamentoService'
import DepartamentoForm from '../components/DepartamentoForm'
import DepartamentoList from '../components/DepartamentoList'
import '../assets/css/Departamento.css'

function DepartamentosPage() {
  const [departamentos, setDepartamentos] = useState<Departamento[]>([])
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)
  const [deptoEdicao, setDeptoEdicao] = useState<Departamento | null>(null)

  const carregar = async () => {
    try {
      setLoading(true)
      setErro(null)
      setDepartamentos(await departamentoService.listar())
    } catch {
      setErro('Erro ao carregar departamentos.')
    } finally {
      setLoading(false)
    }
  }

  const aoSalvar = async () => {
    setDeptoEdicao(null)
    await carregar()
  }

  const excluir = async (id: number) => {
    setErro(null)
    try {
      await departamentoService.excluir(id)
      if (deptoEdicao?.id === id) setDeptoEdicao(null)
      await carregar()
    } catch {
      setErro('Não foi possível excluir o departamento. Ele pode estar em uso.')
    }
  }

  useEffect(() => {
    carregar()
  }, [])

  return (
    <div>
      <h1 className="depto-titulo">Gestão de Departamentos</h1>

      <div className="depto-grid">
        <DepartamentoForm
          onSalvo={aoSalvar}
          deptoEdicao={deptoEdicao}
          onCancelar={() => setDeptoEdicao(null)}
        />

        <section>
          <h2 className="depto-subtitulo">Departamentos cadastrados</h2>

          {erro && <p className="depto-erro">{erro}</p>}

          <DepartamentoList
            departamentos={departamentos}
            loading={loading}
            editandoId={deptoEdicao?.id}
            onEditar={setDeptoEdicao}
            onExcluir={excluir}
          />
        </section>
      </div>
    </div>
  )
}

export default DepartamentosPage