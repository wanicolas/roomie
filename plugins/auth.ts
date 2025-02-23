export default defineNuxtPlugin(() => {
    const token = process.client ? localStorage.getItem('token') : null
  
    if (token) {
      const originalFetch = window.fetch
  
      window.fetch = async (input: RequestInfo | URL, init?: RequestInit) => {
        init = init || {} // S'assurer que `init` est défini
        init.headers = {
          ...init.headers,
          Authorization: `Bearer ${token}`
        }
        return originalFetch(input, init)
      }
    }
  })
  