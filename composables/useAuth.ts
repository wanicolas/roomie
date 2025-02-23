import { ref } from 'vue'
import { useRouter } from 'vue-router'

export function useAuth() {
  const token = ref<string | null>(process.client ? localStorage.getItem('token') : null)
  const router = useRouter()
  const errorMessage = ref<string | null>(null)

  const login = async (email: string, password: string) => {
    try {
      const response = await fetch('http://localhost:5184/api/Auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password })
      })

      if (!response.ok) {
        throw new Error('Erreur de connexion')
      }

      const data = await response.json()
      token.value = data.token
      localStorage.setItem('token', data.token)

      router.push('/')
    } catch (error) {
      errorMessage.value = 'Email ou mot de passe incorrect'
    }
  }

  const logout = () => {
    token.value = null
    localStorage.removeItem('token')
    router.push('/login')
  }

  return { token, login, logout, errorMessage }
}
