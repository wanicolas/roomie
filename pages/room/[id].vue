<template>
	<div class="mx-4 max-w-screen-xl md:mx-12 xl:mx-auto">
		<h1 class="mb-8 text-4xl font-semibold text-primary-700 dark:text-primary-300">
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

		<!-- Retour vers la liste -->
		<NuxtLink to="/book" class="text-blue-600 underline">Retour</NuxtLink>
	</div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();
const room = ref({});

// Récupérer l'ID de l'URL et appeler l'API
onMounted(async () => {
	try {
		const response = await fetch(`http://localhost:5184/api/Room/${route.params.id}`);
		if (!response.ok) throw new Error("Salle non trouvée");
		room.value = await response.json();
	} catch (error) {
		console.error("Erreur:", error);
	}
});
</script>
