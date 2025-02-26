<template>
	<div>
		<h1 class="mb-10 text-lg">
			Bienvenue sur l'interface d'administration de Roomie. Ici, vous pouvez
			modifier, ajouter ou supprimer les salles de votre organisation.
		</h1>

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
			class="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3"
		>
			<div
				v-for="room in rooms"
				:key="room.id"
				class="overflow-hidden rounded-md border bg-white shadow dark:bg-gray-900"
			>
				<img
					class="h-60 w-full object-cover"
					src="https://images.unsplash.com/photo-1517502884422-41eaead166d4?q=80&w=2525&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
					alt=""
				/>
				<div class="p-3">
					<div class="mb-3 flex items-baseline justify-between">
						<div class="text-xl font-medium">{{ room.name }}</div>
						<div
							class="flex items-center gap-1.5 text-gray-700 dark:text-gray-200"
						>
							{{ room.capacity }}
							<UIcon name="ph:users-three" class="size-5" />
						</div>
					</div>
					<UButton :to="'/admin/edit/room/' + room.id" block variant="soft">
						Modifier
					</UButton>
				</div>
			</div>
			<UButton
				to="/admin/add/room"
				class="flex flex-col items-center justify-center gap-3 rounded-md text-xl"
			>
				<UIcon name="ph:plus" class="size-20" />
				Ajouter une salle
			</UButton>
		</div>
		<div v-else-if="rooms.length === 0">Pas de salles trouvées.</div>
	</div>
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
