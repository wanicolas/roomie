<template>
	<div v-if="error">{{ error }}</div>
	<div v-else class="mx-4 max-w-screen-xl md:mx-12 xl:mx-auto">
		<h1
			class="mb-8 text-4xl font-semibold text-primary-700 dark:text-primary-300"
		>
			{{ room.name }}
		</h1>
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
const route = useRoute();

const {
	data: room,
	error,
	status,
} = await useFetch(`http://localhost:5184/api/Room/${route.params.id}`);
</script>
