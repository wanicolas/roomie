<template>
	<h1>
		Bienvenue sur l'interface d'administration de Roomie. Ici, vous pouvez
		modifier, ajouter ou supprimer les salles de vos bâtiments.
	</h1>
	<UButton to="/admin/add/room">Ajouter une salle</UButton>

	<div
		v-if="status === 'pending'"
		class="flex items-center justify-center gap-2"
	>
		<UIcon name="ph:circle-notch" class="size-12 animate-spin" />
		<p>Salles en cours de chargement</p>
	</div>
	<div v-else-if="error">
		<p class="text-center text-gray-600 dark:text-gray-300">{{ error }}</p>
	</div>
	<div
		v-else-if="rooms"
		class="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3 xl:mx-auto xl:max-w-screen-xl"
	>
		<div
			v-for="room in rooms"
			:key="room.id"
			class="overflow-hidden rounded-md bg-white shadow dark:bg-gray-900"
		>
			<img class="h-60 w-full object-cover" :src="room.img" alt="" />
			<div class="flex items-baseline justify-between p-3">
				<div>
					<div class="text-xl font-medium">{{ room.name }}</div>
					<div class="flex items-center gap-1.5">
						<UIcon name="ph:stairs" class="size-5" />
						<div class="text-gray-600 dark:text-gray-300">
							Étage {{ room.floor }}, {{ room.building }}
						</div>
					</div>
				</div>
				<div class="flex items-center gap-1.5 text-gray-700 dark:text-gray-200">
					{{ room.seats }}
					<UIcon name="ph:users-three" class="size-5" />
				</div>
			</div>
			<UButton :to="'/admin/edit/room/' + room.id" block> Modifier </UButton>
		</div>
	</div>
	<div v-else-if="rooms.length === 0">Pas de salles trouvées.</div>
</template>

<script setup>
useHead({
	title: "Admin - Roomie, gestion et réservation de salles",
	meta: [
		{
			name: "description",
			content:
				"Ajoutez, modifiez et supprimez les salles de votre organisation.",
		},
	],
});

definePageMeta({
	middleware: "auth",
});

const {
	data: rooms,
	error,
	status,
} = await useFetch("http://localhost:5184/api/Room", {
	headers: {
		Authorization: `Bearer ${useCookie("auth_token").value.token}`,
	},
});
</script>
