<template>
	<h1 class="mb-6 text-center text-2xl font-semibold md:mb-12 md:text-4xl">
		Connexion
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm class="mb-6">
			<UFormGroup label="Email" class="mb-3">
				<UInput icon="ph:envelope" />
			</UFormGroup>
			<UFormGroup label="Mot de passe" class="mb-6">
				<UInput icon="ph:lock" />
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
useHead({
	title: "Connexion - Roomie, gestion et réservation de salles",
	meta: [
		{
			name: "description",
			content: "Connexion à votre compte Roomie.",
		},
	],
});
</script>
