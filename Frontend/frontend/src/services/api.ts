
import axios from 'axios'

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
})

api.interceptors.request.use(config => {
  const saved = localStorage.getItem('auth')

  if (saved) {
    try {
      const { token } = JSON.parse(saved)

      if (token) {
        config.headers.Authorization = `Bearer ${token}`
      }
    } catch {
      localStorage.removeItem('auth')
    }
  }

  return config
})

export default api
