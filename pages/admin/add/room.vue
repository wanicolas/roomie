<template>
	<h1 class="m-3 mb-6 max-w-screen-xl text-2xl font-semibold xl:mx-auto">
		Ajouter une salle
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm @submit="addRoom" class="flex flex-col gap-3">
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

			<UButton type="submit" block>Ajouter la salle</UButton>
		</UForm>
	</div>
</template>

<script setup>
useHead({
	title: "Ajouter une salle - Roomie, gestion et réservation de salles",
	meta: [
		{
			name: "description",
			content: "Ajoutez une salle à votre organisation.",
		},
	],
});

definePageMeta({
	middleware: "auth",
});

const router = useRouter();

const name = ref("");
const capacity = ref(null);
const minSeatingCapacity = ref(null);
const surface = ref(null);
const accessiblePMR = ref(false);
const hasProjector = ref(false);
const hasSpeakers = ref(false);

// todo : send data to the edit api
async function addRoom() {
	await $fetch("http://localhost:5184/api/Room", {
		method: "POST",
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
