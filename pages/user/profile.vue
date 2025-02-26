<template>
	<h1 class="m-3 mb-6 max-w-screen-xl text-2xl font-semibold xl:mx-auto">
		Profil utilisateur
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-screen-lg">
		<UButton @click="logout">Déconnexion</UButton>

		<div v-for="info in userInfos">
			<p>Email : {{ info.email }}</p>
			<p>Nom : {{ info.fullName }}</p>
			<p>Rôle(s) : {{ info.roles }}</p>
		</div>
	</div>

	<h2 class="m-3 mb-6 max-w-screen-xl text-2xl font-semibold xl:mx-auto">
		Historique des réservations
	</h2>
	<!-- <div v-for="booking in userInfos.bookings">
		<p>
			Salle :
			<NuxtLink :to="`/room/${booking.roomId}`">
				{{ booking.roomName }}
			</NuxtLink>
		</p>
		<p>Date : {{ booking.date }}</p>
		<p>heure de début : {{ booking.startHour }}</p>
		<p>heure de fin : {{ booking.endHour }}</p>
	</div> -->
</template>

<script setup>
definePageMeta({
	middleware: "auth",
});

const router = useRouter();

const logout = () => {
	useCookie("auth_token").value = "";
	router.push("/");
};

const {
	data: userInfos,
	error,
	status,
} = await useFetch("http://localhost:5184/api/User/info", {
	headers: {
		Authorization: `Bearer ${useCookie("auth_token").value}`,
	},
});
</script>
