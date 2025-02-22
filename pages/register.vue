<template>
	<h1 class="mb-6 text-center text-2xl font-semibold md:mb-12 md:text-4xl">
		Inscription
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm class="mb-6">
			<UFormGroup label="Entrez votre email" class="mb-3">
				<UInput icon="ph:envelope" />
			</UFormGroup>
			<UFormGroup label="Créez un mot de passe" class="mb-6">
				<UInput icon="ph:lock" />
			</UFormGroup>

			<UButton type="submit" block>Créer mon compte</UButton>
		</UForm>

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
</script>
