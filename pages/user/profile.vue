<template>
	<h1 class="m-3 mb-6 max-w-screen-xl text-2xl font-semibold xl:mx-auto">
		Profil utilisateur
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-screen-lg">
		<UButton @click="logout">Déconnexion</UButton>

		<div v-for="info in userInfos">
			<p>Nom : {{ info.name }}</p>
			<p>Rôle : {{ info.role }}</p>
		</div>
	</div>
</template>

<script setup>
definePageMeta({
	middleware: "auth",
});

const logout = () => {
	useCookie("auth_token").value = "";
	router.push("/login");
};
</script>
