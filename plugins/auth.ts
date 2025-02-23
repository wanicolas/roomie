export default defineNuxtPlugin(() => {
    if (process.client) {
      const token = localStorage.getItem("token");
      const role = localStorage.getItem("role"); // ✅ Récupérer le rôle
  
      if (token) {
        const originalFetch = window.fetch;
  
        window.fetch = async (input: RequestInfo | URL, init?: RequestInit) => {
          init = init || {}; // S'assurer que `init` est défini
          init.headers = {
            ...init.headers,
            Authorization: `Bearer ${token}`,
          };
          return originalFetch(input, init);
        };
      }
  
      return {
        provide: {
          auth: {
            token,
            role, // ✅ Fournir le rôle globalement
            isAdmin: role === "Admin", // ✅ Vérifier si admin
          },
        },
      };
    }
  });
  