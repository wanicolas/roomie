<template>
	<h1 class="m-3 mb-6 max-w-screen-xl text-2xl font-semibold xl:mx-auto">
		Modifier la salle
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm @submit="updateRoom" class="flex flex-col gap-3">
			<UFormGroup label="Nom de la salle">
				<UInput required icon="ph:office-building" v-model="name" />
			</UFormGroup>
			<UFormGroup label="Capacité en nombre de personnes">
				<UInput
					type="number"
					min="0"
					icon="ph:users-three"
					v-model="minSeatingCapacity"
				/>
			</UFormGroup>
			<UFormGroup label="Surface en m²">
				<UInput
					type="number"
					min="0"
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
				<UCheckbox label="Enceintes" v-model="hasSpeaker" />
			</fieldset>

			<UButton color="red" variant="outline" block>Supprimer la salle</UButton>
			<UButton type="submit" block>Mettre à jour la salle</UButton>
		</UForm>
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

const {
	data: room,
	error,
	status,
} = await useFetch(`http://localhost:5184/api/Room/${route.params.id}`);

// todo : bind v-model to received data
const name = ref("");
const minSeatingCapacity = ref(null);
const surface = ref(null);
const accessiblePMR = ref(false);
const hasProjector = ref(false);
const hasSpeaker = ref(false);

// todo : send data to the edit api
async function updateRoom() {
	const result = await $fetch(
		`http://localhost:5184/api/Room?${route.params.id}`,
		{
			method: "POST",
			body: {
				minSeatingCapacity: minSeatingCapacity.value,
				surface: surface.value,
				accessiblePMR: accessiblePMR.value,
				hasProjector: hasProjector.value,
				hasSpeaker: hasSpeaker.value,
			},
		},
	);
}
</script>
