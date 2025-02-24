<template>
	<h1 class="mb-6 text-center text-2xl font-semibold md:mb-12 md:text-4xl">
		Connexion
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm @submit.prevent="login" class="mb-6">
			<UFormGroup label="Email" class="mb-3">
				<UInput v-model="email" required icon="ph:envelope" type="email" />
			</UFormGroup>
			<UFormGroup label="Mot de passe" class="mb-6">
				<UInput v-model="password" required icon="ph:lock" type="password" />
			</UFormGroup>

			<UButton type="submit" block>Se connecter</UButton>
		</UForm>

		<p class="text-center">
			Pas encore de compte ?
			<NuxtLink
				to="/register"
				class="text-primary-700 underline-offset-4 hover:underline dark:text-primary-300"
			>
				On va régler ça !
			</NuxtLink>
		</p>
	</div>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "@/store/auth";

const email = ref("");
const password = ref("");
const errorMessage = ref(null);
const router = useRouter();
const authStore = useAuthStore();
useHead({
	title: "Connexion - Roomie, gestion et réservation de salles",
	meta: [
		{
			name: "description",
			content: "Connexion à votre compte Roomie.",
		},
	],
});

const router = useRouter();
const authCookie = useCookie("auth_token");

const login = async () => {
	errorMessage.value = "";
	const email = ref("");
	const password = ref("");

	try {
		const response = await fetch("http://localhost:5184/api/Auth/login", {
			method: "POST",
			headers: { "Content-Type": "application/json" },
			body: JSON.stringify({
				email: email.value,
				password: password.value,
			}),
		});

		if (!response.ok) {
			const errorData = await response.json();
			throw new Error(errorData.message || "Échec de la connexion.");
		}

		const data = await response.json();

		// ✅ Mettre à jour Pinia immédiatement
		authStore.login(data.token, data.role);

		router.push("/"); // Rediriger vers la page d'accueil
	} catch (error) {
		errorMessage.value = error.message;
	}
};
async function login() {
	const result = await $fetch("http://localhost:5184/api/Auth/login", {
		method: "POST",
		body: {
			email: email.value,
			password: password.value,
		},
	});

	authCookie.value = result;
	router.push("/book");
}
</script>
