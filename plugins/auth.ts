import { defineNuxtPlugin } from '#app'
import { useAuthStore } from '@/store/auth'
import { onMounted } from 'vue'

export default defineNuxtPlugin(() => {
  if (process.client) {
    onMounted(() => {
      const authStore = useAuthStore()

      // Vérifie l'authentification au chargement
      authStore.checkAuth()

      if (authStore.isLoggedIn) {
        const originalFetch = window.fetch

        window.fetch = async (input: RequestInfo | URL, init?: RequestInit) => {
          init = init || {}
          init.headers = {
            ...init.headers,
            Authorization: `Bearer ${authStore.token}`
          }
          return originalFetch(input, init)
        }
      }
    })
  }
})
