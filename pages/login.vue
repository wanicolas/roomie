<template>
	<h1 class="mb-6 text-center text-2xl font-semibold md:mb-12 md:text-4xl">
		Connexion
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm @submit.prevent="login(email, password)" class="flex flex-col gap-3">
			<UFormGroup label="Email">
				<UInput v-model="email" icon="ph:envelope" type="email" required />
			</UFormGroup>
			<UFormGroup label="Mot de passe">
				<UInput v-model="password" icon="ph:lock" type="password" required />
			</UFormGroup>

			<UButton type="submit" block>Se connecter</UButton>

			<p v-if="errorMessage" class="text-red-500 mt-2">{{ errorMessage }}</p>
		</UForm>
		<!-- ✅ Ajout du bouton d'inscription -->
		<div class="mt-4 text-center">
			<p class="text-gray-600">Vous n'avez pas de compte ?</p>
			<UButton color="gray" variant="link" @click="router.push('/register')">
				Créer un compte
			</UButton>
		</div>
	</div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/store/auth'

const email = ref('')
const password = ref('')
const errorMessage = ref(null)
const router = useRouter()
const authStore = useAuthStore()

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

    // ✅ Mettre à jour Pinia immédiatement
    authStore.login(data.token, data.role)

    router.push("/") // Rediriger vers la page d'accueil
  } catch (error) {
    errorMessage.value = error.message
  }
}
</script>

