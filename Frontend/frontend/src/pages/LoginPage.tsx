import { useState } from 'react'
import { useNavigate, Navigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'
import { authService } from '../services/authService'
import '../assets/css/Login.css'
function LoginPage() {
  const [email, setEmail]     = useState('')
  const [senha, setSenha]     = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro]       = useState<string | null>(null)
  const { login, isAuthenticated } = useAuth()
  const navigate = useNavigate()

  // Já logado? Não precisa ver o login
  if (isAuthenticated) return <Navigate to="/produtos" replace />

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setErro(null)
    try {
      setLoading(true)
      const resp = await authService.login({ email, senha })
      login(resp)
      navigate('/produtos')
    } catch {
      setErro('E-mail ou senha incorretos.')
    } finally {
      setLoading(false)
    }
  }

  return (
  <div className="login-pagina">
    <form className="login-card" onSubmit={handleSubmit}>
      <h1 className="login-titulo">Entrar</h1>
      <p className="login-sub">Acesse o painel de gestão.</p>

      {erro && <p className="login-erro">{erro}</p>}

      <div className="login-campo">
        <label htmlFor="email">E-mail</label>
        <input
          id="email"
          type="email"
          autoComplete="username"
          placeholder="nome@email.com"
          value={email}
          onChange={e => setEmail(e.target.value)}
          required
        />
      </div>

      <div className="login-campo">
        <label htmlFor="senha">Senha</label>
        <input
          id="senha"
          type="password"
          autoComplete="current-password"
          placeholder="Sua senha"
          value={senha}
          onChange={e => setSenha(e.target.value)}
          required
        />
      </div>

      <button className="login-botao" type="submit" disabled={loading}>
        {loading ? 'Entrando...' : 'Entrar'}
      </button>
    </form>
  </div>
)
}
export default LoginPage