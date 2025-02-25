<template>
	<h1 class="m-3 mb-6 max-w-screen-xl text-2xl font-semibold xl:mx-auto">
		Réserver une salle
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm @submit="onSubmit" class="flex flex-col gap-3">
			<UFormGroup label="Date de disponibilité">
				<UInput
					required
					type="date"
					icon="ph:calendar"
					v-model="availabilityDate"
				/>
			</UFormGroup>
			<UFormGroup label="Heure de début">
				<UInput
					required
					type="time"
					icon="ph:clock"
					v-model="availabilityStartHour"
				/>
			</UFormGroup>
			<UFormGroup label="Heure de fin">
				<UInput
					required
					type="time"
					icon="ph:clock"
					v-model="availabilityEndHour"
				/>
			</UFormGroup>
			<UFormGroup label="Nombre de places assises minimum">
				<UInput type="number" min="0" icon="ph:office-chair" v-model="seats" />
			</UFormGroup>

			<details class="space-y-3">
				<summary
					class="ml-auto flex w-fit list-none items-center gap-2 rounded-md bg-gray-200 px-2 py-1 text-sm text-gray-600 hover:bg-gray-300 dark:bg-gray-800 dark:text-gray-300"
				>
					Plus de filtres
					<UIcon name="ph:caret-down" class="size-5" />
				</summary>
				<UCheckbox
					label="Obligatoirement accessible aux PMR"
					v-model="accessible"
				/>
				<fieldset>
					<legend class="mb-1 text-sm font-medium">
						Équipements disponibles :
					</legend>
					<UCheckbox label="Vidéoprojecteur" v-model="projector" />
					<UCheckbox label="Enceintes" v-model="speaker" />
				</fieldset>
			</details>

			<UButton type="submit" block>Rechercher des salles disponibles</UButton>
		</UForm>
	</div>

	<div class="bg-gray-100 px-3 py-20 dark:bg-gray-800">
		<p v-if="!showRooms" class="text-center text-lg">
			Faites une recherche pour consulter les salles disponibles !
		</p>
		<template v-else>
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
				<NuxtLink
					:to="`/room/${room.id}?date=${availabilityDate}&startHour=${availabilityStartHour}&endHour=${availabilityEndHour}`"
					v-for="room in rooms"
					:key="room.id"
				>
					<div
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
							<div
								class="flex items-center gap-1.5 text-gray-700 dark:text-gray-200"
							>
								{{ room.seats }}
								<UIcon name="ph:users-three" class="size-5" />
							</div>
						</div>
					</div>
				</NuxtLink>
			</div>
			<div v-else-if="rooms.length === 0">Pas de salles trouvées.</div>
		</template>
	</div>
</template>

<script setup>
useHead({
	title: "Réserver une salle - Roomie, gestion et réservation de salles",
	meta: [
		{
			name: "description",
			content: "Réservez une salle et trouvez des disponibilités.",
		},
	],
});

definePageMeta({
	middleware: "auth",
});

const showRooms = ref(false);

const availabilityDate = ref("");
const availabilityStartHour = ref("");
const availabilityEndHour = ref("");
const seats = ref("");
const accessible = ref(false);
const projector = ref(false);
const speaker = ref(false);

const onSubmit = async () => {
	// todo : add query to fetch rooms
	const fetchRooms = await useFetch(`http://localhost:5184/api/Room`);

	showRooms.value = true;
};
</script>
