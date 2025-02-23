export function useAuth() {
  async function login(email: string, password: string) {
      const response = await fetch("http://localhost:5184/api/Account/login", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ email, password }),
      });

      if (!response.ok) {
          console.error("Erreur de connexion");
          return null;
      }

      const data = await response.json();
      localStorage.setItem("token", data.Token); // Stocker le token
      return data.Token;
  }

  function logout() {
      localStorage.removeItem("token"); // Supprimer le token
  }

  function getToken() {
      return localStorage.getItem("token");
  }

  return { login, logout, getToken };
}
