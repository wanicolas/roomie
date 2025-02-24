<template>
	<h1 class="mb-6 text-center text-2xl font-semibold md:mb-12 md:text-4xl">
		Inscription
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm @submit.prevent="register" class="mb-6">
			<UFormGroup label="Entrez votre nom" class="mb-3">
				<UInput v-model="fullName" icon="ph:user" />
			</UFormGroup>
			<UFormGroup label="Entrez votre email" class="mb-3">
				<UInput v-model="email" type="email" icon="ph:envelope" />
			</UFormGroup>
			<UFormGroup label="Créez un mot de passe" class="mb-6">
				<UInput
					v-model="password"
					type="password"
					autocomplete="new-password"
					icon="ph:lock"
				/>
			</UFormGroup>

			<UButton type="submit" block>Créer mon compte</UButton>
		</UForm>

		<p v-if="error">{{ error }}</p>

		<p class="text-center">
			Déjà membre ?
			<NuxtLink
				to="/login"
				class="text-primary-700 underline-offset-4 hover:underline dark:text-primary-300"
			>
				On se connecte !
			</NuxtLink>
		</p>
	</div>
</template>

<script setup>
useHead({
	title: "Inscription - Roomie, gestion et réservation de salles",
	meta: [
		{
			name: "description",
			content: "Créez un compte sur Roomie.",
		},
	],
});

const error = ref("");

const fullName = ref("");
const email = ref("");
const password = ref("");

const router = useRouter();

const register = async () => {
	try {
		const result = await $fetch("http://localhost:5184/api/Auth/register", {
			method: "POST",
			body: {
				fullName: fullName.value.trim(),
				email: email.value.trim(),
				password: password.value,
			},
		});
		router.push("/login");
	} catch (e) {
		error.value = e;
	}
};
</script>
