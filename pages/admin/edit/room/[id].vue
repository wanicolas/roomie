<template>
	<h1 class="m-3 mb-6 max-w-screen-xl text-2xl font-semibold xl:mx-auto">
		Modifier la salle
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm @submit="updateRoom" class="flex flex-col gap-3">
			<UFormGroup label="Capacité en nombre de personnes">
				<UInput
					type="number"
					min="0"
					icon="ph:users-three"
					v-model="peopleCapacity"
				/>
			</UFormGroup>
			<UFormGroup label="Nombre de places assises minimum">
				<UInput type="number" min="0" icon="ph:office-chair" v-model="seats" />
			</UFormGroup>
			<UCheckbox label="Accessible aux PMR" v-model="accessible" />
			<fieldset>
				<legend class="mb-1 text-sm font-medium">
					Équipements disponibles :
				</legend>
				<UCheckbox label="Vidéoprojecteur" v-model="projector" />
				<UCheckbox label="Enceintes" v-model="speaker" />
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
const peopleCapacity = ref("");
const seats = ref("");
const accessible = ref(false);
const projector = ref(false);
const speaker = ref(false);

// todo : send data to the edit api
async function updateRoom() {
	const result = await $fetch(
		`http://localhost:5184/api/Room?${route.params.id}`,
		{
			method: "POST",
			body: {
				peopleCapacity: peopleCapacity.value,
				seats: seats.value,
				accessible: accessible.value,
				projector: projector.value,
				speaker: speaker.value,
			},
		},
	);
}
</script>
