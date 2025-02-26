<template>
	<div>
		<div v-if="error">{{ error }}</div>
		<div v-else class="mx-4 max-w-screen-xl md:mx-12 xl:mx-auto">
			<h1
				class="mb-8 text-4xl font-semibold text-primary-700 dark:text-primary-300"
			>
				{{ room.name }}
			</h1>
			<p v-if="route.query.date" class="mb-4 text-lg">
				Salle-vous-plaît ?
				<UButton size="xl" @click="bookRoom">
					Réserver cette salle le {{ route.query.date }} de
					{{ route.query.startTime }} à {{ route.query.endTime }}
				</UButton>
			</p>
			<!-- <UCarousel
				v-slot="{ item }"
				:items="items"
				:ui="{ item: 'basis-full' }"
				class="mb-6 overflow-hidden rounded-lg"
				arrows
				indicators
			>
				<img :src="item" class="w-full" draggable="false" />
			</UCarousel> -->
			<p class="mb-4">
				Nombre de places assises : {{ room.minSeatingCapacity }}
			</p>
			<p class="mb-4">
				Accessibilité PMR : {{ room.accessiblePMR ? "Oui" : "Non" }}
			</p>
			<div>
				Équipements disponibles :
				<ul>
					<li class="flex items-center gap-2">
						<UIcon
							:name="room.hasProjector ? 'ph:check' : 'ph:x'"
							class="size-5"
							:class="room.hasProjector ? 'text-green-600' : 'text-red-600'"
						/>
						Vidéoprojecteur
					</li>
					<li class="flex items-center gap-2">
						<UIcon
							:name="room.hasSpeakers ? 'ph:check' : 'ph:x'"
							class="size-5"
							:class="room.hasSpeakers ? 'text-green-600' : 'text-red-600'"
						/>
						Enceintes
					</li>
				</ul>
			</div>
			<p>
				{{ room.description }}
			</p>
		</div>
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
const router = useRouter();

const {
	data: room,
	error,
	status,
} = await useFetch(`http://localhost:5184/api/Room/${route.params.id}`);

// const items = [
// 	"https://picsum.photos/1920/1080?random=1",
// 	"https://picsum.photos/1920/1080?random=2",
// 	"https://picsum.photos/1920/1080?random=3",
// 	"https://picsum.photos/1920/1080?random=4",
// 	"https://picsum.photos/1920/1080?random=5",
// 	"https://picsum.photos/1920/1080?random=6",
// ];

const bookRoom = async () => {
	const response = await $fetch("http://localhost:5184/api/Reservation", {
		method: "POST",
		headers: {
			Authorization: `Bearer ${useCookie("auth_token").value.token}`,
		},
		body: {
			roomId: room.value.id,
			userId: useCookie("auth_token").value.userId,
			startTime: `${route.query.date}T${route.query.startTime}`,
			endTime: `${route.query.date}T${route.query.endTime}`,
		},
	});

	if (response.error) {
		alert(response.error);
	} else {
		alert("Réservation effectuée avec succès !");
		router.push("/");
	}
};
</script>
