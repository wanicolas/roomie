<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'

const email = ref('')
const password = ref('')
const errorMessage = ref(null)
const router = useRouter()

const isLoggedIn = ref(false);

// Vérifie si l'utilisateur est connecté au chargement de la page
onMounted(() => {
  if (process.client) { // Vérifie si l'on est côté client
    const token = localStorage.getItem('token');
    isLoggedIn.value = !!token; // Si un token est présent, l'utilisateur est connecté
  }
});

const login = async () => {
  errorMessage.value = ""

  try {
    const response = await fetch("http://localhost:5184/api/Auth/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        email: email.value,
        password: password.value,
      }),
    })

    if (!response.ok) {
      const errorData = await response.json()
      throw new Error(errorData.message || "Échec de la connexion.")
    }

    const data = await response.json()
    if (process.client) {
      localStorage.setItem("token", data.token)
      localStorage.setItem("role", data.role) // ✅ Stocker le rôle
    }

    // Mise à jour de l'état de connexion
    isLoggedIn.value = true;

    router.push("/") // Rediriger vers la page d'accueil après connexion
  } catch (error) {
    errorMessage.value = error.message
  }
}

const logout = () => {
  if (process.client) {
    localStorage.removeItem("token")
    localStorage.removeItem("role")
  }

  // Mise à jour de l'état de connexion
  isLoggedIn.value = false;

  router.push("/login") // Rediriger vers la page de connexion après déconnexion
}
</script>
