<template>
	<h1 class="m-3 mb-6 max-w-screen-xl text-2xl font-semibold xl:mx-auto">
		Inscription
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm @submit.prevent="register" class="flex flex-col gap-3">
			<UFormGroup label="Nom complet">
				<UInput v-model="fullName" icon="ph:user" type="text" required />
			</UFormGroup>
			<UFormGroup label="Email">
				<UInput v-model="email" icon="ph:envelope" type="email" required />
			</UFormGroup>
			<UFormGroup label="Mot de passe">
				<UInput v-model="password" icon="ph:lock" type="password" required />
			</UFormGroup>

			<UButton type="submit" block :loading="isLoading">S'inscrire</UButton>

            <!-- Affichage des erreurs -->
            <p v-if="errorMessage" class="text-red-500 mt-2">{{ errorMessage }}</p>
            <ul v-if="errorDetails.length">
                <li v-for="(error, index) in errorDetails" :key="index" class="text-red-500">
                    - {{ error }}
                </li>
            </ul>
		</UForm>
	</div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const fullName = ref('')
const email = ref('')
const password = ref('')
const errorMessage = ref(null)
const errorDetails = ref([])
const isLoading = ref(false)
const router = useRouter()

const register = async () => {
  errorMessage.value = ""
  errorDetails.value = []
  isLoading.value = true

  try {
    const response = await fetch("http://localhost:5184/api/Auth/register", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        fullName: fullName.value.trim(),
        email: email.value.trim(),
        password: password.value,
      }),
    });

    if (!response.ok) {
      const errorData = await response.json();
      console.error("Erreur API :", errorData)
      
      errorMessage.value = errorData.message || "Une erreur est survenue."
      errorDetails.value = errorData.errors || [] // Affiche les erreurs détaillées

      throw new Error(errorMessage.value)
    }

    console.log("Utilisateur créé avec succès !")
    router.push("/login") // Redirige vers la connexion
  } catch (error) {
    console.error("Erreur d'inscription :", error.message)
    errorMessage.value = error.message
  } finally {
    isLoading.value = false
  }
};
</script>
