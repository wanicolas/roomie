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

	<p v-if="error" class="text-red-600 dark:text-red-400">{{ error }}</p>
</template>

<script setup>
useHead({
	title: "Connexion - Roomie, gestion et réservation de salles",
	meta: [
		{
			name: "description",
			content: "Connexion à votre compte Roomie.",
		},
	],
});

const email = ref("");
const password = ref("");

const login = async () => {
	const {
		data: auth_token,
		error,
		status,
	} = await $fetch("http://localhost:5184/api/Auth/login", {
		body: JSON.stringify({
			email: email.value,
			password: password.value,
		}),
	});

	if (!error) {
		useCookie("auth_token", auth_token);
		router.push("/book");
	}
};
</script>
