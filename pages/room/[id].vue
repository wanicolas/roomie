<template>
	<div v-if="error">{{ error }}</div>
	<div v-else class="mx-4 max-w-screen-xl md:mx-12 xl:mx-auto">
		<h1 class="mb-8 text-4xl font-semibold text-primary-700 dark:text-primary-300">
			{{ room.name }}
		</h1>

		<UCarousel
			v-slot="{ item }"
			:items="items"
			:ui="{ item: 'basis-full' }"
			class="overflow-hidden rounded-lg"
			arrows
			indicators
		>
			<img :src="item" class="w-full" draggable="false" />
		</UCarousel>

		<!-- Formulaire de réservation pour les utilisateurs -->
		<div v-if="!isAdmin" class="mt-6 p-4 border rounded-lg bg-gray-100 dark:bg-gray-800">
			<h2 class="text-xl font-semibold mb-4">Réserver cette salle</h2>
			<UInput v-model="startTime" type="datetime-local" label="Heure de début" />
			<UInput v-model="endTime" type="datetime-local" label="Heure de fin" class="mt-2" />
			<UButton @click="reserveRoom" class="mt-4">Réserver</UButton>
			<p v-if="reservationMessage" class="mt-2 text-green-600">{{ reservationMessage }}</p>
		</div>

		<!-- Boutons pour l'admin -->
		<div v-if="isAdmin" class="mt-4 flex gap-2">
			<UButton color="yellow" @click="editRoom">Modifier</UButton>
			<UButton color="red" @click="deleteRoom">Supprimer</UButton>
		</div>

		<p class="mb-4 text-lg text-gray-600 dark:text-gray-300">
			Capacité : {{ room.capacity }} personnes
		</p>
		<p class="mb-4 text-lg text-gray-600 dark:text-gray-300">
			Bâtiment : {{ room.building }}
		</p>
		<p class="mb-4 text-lg text-gray-600 dark:text-gray-300">
			Surface : {{ room.surface }} m²
		</p>
		<p class="mb-4 text-lg text-gray-600 dark:text-gray-300">
			Accessibilité PMR : {{ room.isAccessible ? "Oui" : "Non" }}
		</p>
		<p class="mb-4 text-lg text-gray-600 dark:text-gray-300">
			{{ room.description }}
		</p>
	</div>
</template>

<script setup>
useHead({
	title: "Détails de la salle - Roomie, gestion et réservation de salles",
	meta: [
		{
			name: "description",
			content:
				"Vous trouverez les informations et disponibilités de cette salle sur cette page.",
		},
	],
});

const route = useRoute();
const token = process.client ? localStorage.getItem("token") : null;
const isAdmin = process.client ? localStorage.getItem("role") === "Admin" : false;
const startTime = ref("");
const endTime = ref("");
const reservationMessage = ref("");

const {
	data: room,
	error,
	status,
} = await useFetch(`http://localhost:5184/api/Room/${route.params.id}`);

const items = [
	"https://picsum.photos/1920/1080?random=1",
	"https://picsum.photos/1920/1080?random=2",
	"https://picsum.photos/1920/1080?random=3",
	"https://picsum.photos/1920/1080?random=4",
	"https://picsum.photos/1920/1080?random=5",
	"https://picsum.photos/1920/1080?random=6",
];

const reserveRoom = async () => {
	if (!startTime.value || !endTime.value) {
		reservationMessage.value = "Veuillez choisir une date et une heure.";
		return;
	}

	const response = await fetch("http://localhost:5184/api/Reservation", {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
			Authorization: `Bearer ${token}`,
		},
		body: JSON.stringify({
			roomId: route.params.id,
			startTime: startTime.value,
			endTime: endTime.value,
		}),
	});

	if (response.ok) {
		reservationMessage.value = "Réservation réussie ! ✅";
	} else {
		reservationMessage.value = "Erreur lors de la réservation ❌";
	}
};

const editRoom = () => {
	navigateTo(`/admin/room/edit/${route.params.id}`);
};

const deleteRoom = async () => {
	if (!confirm("Voulez-vous vraiment supprimer cette salle ?")) return;

	const response = await fetch(`http://localhost:5184/api/Room/${route.params.id}`, {
		method: "DELETE",
		headers: {
			Authorization: `Bearer ${token}`,
		},
	});

	if (response.ok) {
		alert("Salle supprimée !");
		navigateTo("/admin/room");
	} else {
		alert("Erreur lors de la suppression.");
	}
};
</script>