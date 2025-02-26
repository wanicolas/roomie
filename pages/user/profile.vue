<template>
	<div>
		<h1 class="m-3 mb-6 text-2xl font-semibold">Profil utilisateur</h1>
		<div class="m-3 mb-12">
			<p><span class="font-semibold">Email :</span> {{ userInfos.email }}</p>
			<p><span class="font-semibold">Nom :</span> {{ userInfos.fullName }}</p>
			<p class="mb-3">
				<span class="font-semibold"
					>Rôle<template v-if="userInfos.roles.length >= 2">s</template> :
				</span>
				<template v-for="(role, index) in userInfos.roles" :key="index">
					{{ role }}
					<template v-if="index !== userInfos.roles.length - 1">,</template>
				</template>
			</p>
			<UButton @click="logout" color="red" class="mb-6" size="lg">
				Déconnexion
			</UButton>
			<h2 class="m-3 mb-6 text-2xl font-semibold">
				Historique des réservations
			</h2>
			<div
				v-for="booking in userBookings"
				class="mb-6 rounded-lg border px-3 py-2"
			>
				<p class="mb-2 text-lg">
					Salle :
					<NuxtLink
						:to="`/room/${booking.roomId}`"
						class="underline underline-offset-4 hover:no-underline"
					>
						{{ booking.room.name }}
					</NuxtLink>
				</p>
				<p>Date : {{ booking.startTime }}</p>
				<p>heure de début : {{ booking.startTime }}</p>
				<p>heure de fin : {{ booking.endTime }}</p>
				<UButton
					@click="deleteBooking"
					color="red"
					variant="outline"
					class="mb-6"
				>
					Supprimer la réservation
				</UButton>
			</div>
		</div>
	</div>
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
		Authorization: `Bearer ${useCookie("auth_token").value.token}`,
	},
});

const {
	data: userBookings,
	bookingError,
	bookingStatus,
} = await useFetch("http://localhost:5184/api/Reservation", {
	headers: {
		Authorization: `Bearer ${useCookie("auth_token").value.token}`,
	},
});

const deleteBooking = async () => {
	const response = await $fetch(
		`http://localhost:5184/api/Reservation/${booking.id}`,
		{
			method: "DELETE",
			headers: {
				Authorization: `Bearer ${useCookie("auth_token").value.token}`,
			},
		},
	);

	if (response.error) {
		alert(response.error);
	} else {
		alert("Réservation supprimée avec succès !");
	}
};
</script>
