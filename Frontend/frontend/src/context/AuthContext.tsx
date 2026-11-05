
import { createContext, useContext, useState, type ReactNode } from 'react'
import { type AuthUser } from '../types/Auth'

interface AuthContextType {
  user: AuthUser | null
  isAuthenticated: boolean
  login: (u: AuthUser) => void
  logout: () => void
}

const AuthContext = createContext<AuthContextType | null>(null)

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<AuthUser | null>(() => {
    const saved = localStorage.getItem('auth')

    if (!saved) return null

    try {
      return JSON.parse(saved)
    } catch {
      localStorage.removeItem('auth')
      return null
    }
  })

  const login = (u: AuthUser) => {
    setUser(u)
    localStorage.setItem('auth', JSON.stringify(u))

    if (u.token) {
      localStorage.setItem('token', u.token)
    }
  }

  const logout = () => {
    setUser(null)
    localStorage.removeItem('auth')
    localStorage.removeItem('token')
  }

  return (
    <AuthContext.Provider value={{
      user,
      isAuthenticated: user !== null,
      login,
      logout
    }}>
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth() {
  const ctx = useContext(AuthContext)

  if (!ctx) {
    throw new Error('useAuth deve ser usado dentro de AuthProvider')
  }

  return ctx
}
