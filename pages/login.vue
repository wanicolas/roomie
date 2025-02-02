<template>
	<h1 class="m-3 mb-6 max-w-screen-xl text-2xl font-semibold xl:mx-auto">
		Connexion
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm class="flex flex-col gap-3">
			<UFormGroup label="Email">
				<UInput icon="ph:envelope" />
			</UFormGroup>
			<UFormGroup label="Mot de passe">
				<UInput icon="ph:lock" />
			</UFormGroup>

			<UButton type="submit" block>Se connecter</UButton>
		</UForm>
	</div>
</template>
