<template>
	<div>
		<h1 class="m-3 mb-6 text-2xl font-semibold">Modifier la salle</h1>
		<div class="m-3 mb-12">
			<UForm @submit="updateRoom" class="flex flex-col gap-3">
				<UFormGroup label="Nom de la salle">
					<UInput required icon="ph:door" v-model="name" />
				</UFormGroup>
				<UFormGroup label="Capacité en nombre de personnes">
					<UInput
						type="number"
						min="1"
						icon="ph:users-three"
						v-model="capacity"
					/>
				</UFormGroup>
				<UFormGroup label="Nombre de sièges minimum">
					<UInput
						type="number"
						min="1"
						icon="ph:chair"
						v-model="minSeatingCapacity"
					/>
				</UFormGroup>
				<UFormGroup label="Surface en m²">
					<UInput
						type="number"
						min="1"
						icon="ph:office-chair"
						v-model="surface"
					/>
				</UFormGroup>
				<UCheckbox label="Accessible aux PMR" v-model="accessiblePMR" />
				<fieldset>
					<legend class="mb-1 text-sm font-medium">
						Équipements disponibles :
					</legend>
					<UCheckbox label="Vidéoprojecteur" v-model="hasProjector" />
					<UCheckbox label="Enceintes" v-model="hasSpeakers" />
				</fieldset>
				<UButton color="red" variant="outline" block @click="deleteRoom"
					>Supprimer la salle</UButton
				>
				<UButton type="submit" block>Mettre à jour la salle</UButton>
			</UForm>
		</div>
	</div>
</template>

<script setup>
useHead({
	title: "Modifier une salle - Roomie, gestion et réservation de salles",
	meta: [
		{
			name: "description",
			content: "Modifiez les informations de cette salle.",
		},
	],
});

definePageMeta({
	middleware: "auth",
});

const route = useRoute();
const router = useRouter();

onMounted(async () => {
	const room = await $fetch(
		`http://localhost:5184/api/Room/${route.params.id}`,
	);

	// bind received data to v-model
	name.value = room.name;
	capacity.value = room.capacity;
	minSeatingCapacity.value = room.minSeatingCapacity;
	surface.value = room.surface;
	accessiblePMR.value = room.accessiblePMR;
	hasProjector.value = room.hasProjector;
	hasSpeakers.value = room.hasSpeakers;
});

// todo : bind v-model to received data
const name = ref("");
const capacity = ref(null);
const minSeatingCapacity = ref(null);
const surface = ref(null);
const accessiblePMR = ref(false);
const hasProjector = ref(false);
const hasSpeakers = ref(false);

async function deleteRoom() {
	await $fetch(`http://localhost:5184/api/Room/${route.params.id}`, {
		method: "DELETE",
		headers: {
			Authorization: `Bearer ${useCookie("auth_token").value.token}`,
		},
	});

	// redirect to the room list
	router.push("/admin");
}

// todo : send data to the edit api
async function updateRoom() {
	await $fetch(`http://localhost:5184/api/Room/${route.params.id}`, {
		method: "PUT",
		body: {
			name: name.value,
			capacity: capacity.value,
			minSeatingCapacity: minSeatingCapacity.value,
			surface: surface.value,
			accessiblePMR: accessiblePMR.value,
			hasProjector: hasProjector.value,
			hasSpeakers: hasSpeakers.value,
		},
		headers: {
			Authorization: `Bearer ${useCookie("auth_token").value.token}`,
		},
	});

	// redirect to the room list
	router.push("/admin");
}
</script>
