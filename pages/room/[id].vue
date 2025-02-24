<template>
	<div v-if="error">{{ error }}</div>
	<div v-else class="mx-4 max-w-screen-xl md:mx-12 xl:mx-auto">
		<div
			class="gzp-3 mb-8 flex flex-col justify-between lg:flex-row lg:items-center"
		>
			<h1 class="text-4xl font-semibold text-primary-700 dark:text-primary-300">
				{{ room.name }}
			</h1>
			<UButton>
				Réserver cette salle le {{ route.query.date }} de
				{{ route.query.startHour }} à {{ route.query.endHour }}
			</UButton>
		</div>

		<UCarousel
			v-slot="{ item }"
			:items="items"
			:ui="{ item: 'basis-full' }"
			class="mb-6 overflow-hidden rounded-lg"
			arrows
			indicators
		>
			<img :src="item" class="w-full" draggable="false" />
		</UCarousel>

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
</script>
