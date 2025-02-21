<template>
	<h1 class="m-3 mb-6 max-w-screen-xl text-2xl font-semibold xl:mx-auto">
		Réserver une salle
	</h1>
	<div class="m-3 mb-12 sm:mx-auto sm:max-w-lg">
		<UForm class="flex flex-col gap-3">
			<UFormGroup label="Bâtiment">
				<USelect icon="ph:building" />
			</UFormGroup>
			<UFormGroup label="Date de disponibilité">
				<UInput type="date" icon="ph:calendar" v-model="availabilityDate" />
			</UFormGroup>
			<UFormGroup label="Heure de début" v-model="availabilityStartHour">
				<UInput type="time" icon="ph:clock" />
			</UFormGroup>
			<UFormGroup label="Heure de fin" v-model="availabilityEndHour">
				<UInput type="time" icon="ph:clock" />
			</UFormGroup>

			<details class="space-y-3">
				<summary
					class="ml-auto flex w-fit list-none items-center gap-2 rounded-md bg-gray-200 px-2 py-1 text-sm text-gray-600 dark:bg-gray-800 dark:text-gray-300"
					a
				>
					Plus de filtres
					<UIcon name="ph:caret-down" class="size-5" />
				</summary>
				<UFormGroup label="Capacité en nombre de personnes">
					<UInput type="number" min="0" icon="ph:users-three" />
				</UFormGroup>
				<UFormGroup label="Places assises minimum">
					<UInput type="number" min="0" icon="ph:office-chair" />
				</UFormGroup>
				<UCheckbox label="Accessible aux PMR" />
				<fieldset>
					<legend class="mb-1 text-sm font-medium">
						Équipements disponibles :
					</legend>
					<UCheckbox label="Vidéoprojecteur" />
					<UCheckbox label="Enceintes" />
				</fieldset>
			</details>

			<UButton type="submit" block>Rechercher</UButton>
		</UForm>
	</div>

	<div class="bg-gray-100 px-3 py-20 dark:bg-gray-800">
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
			<NuxtLink :to="`/room/${room.id}`" v-for="room in rooms" :key="room.id">
				<div
					class="overflow-hidden rounded-md bg-white shadow dark:bg-gray-900"
				>
					<img class="h-60 w-full object-cover" src="/room.webp" alt="" />
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
							{{ room.capacity }} <UIcon name="ph:users-three" class="size-5" />
						</div>
					</div>
				</div>
			</NuxtLink>
		</div>
		<div v-else-if="rooms.length === 0">Pas de salles trouvées.</div>
	</div>
</template>

<script setup>
const {
	data: rooms,
	error,
	status,
} = await useFetch("http://localhost:5184/api/Room");

const availabilityDate = ref("");
const availabilityStartHour = ref("");
const availabilityEndHour = ref("");
</script>
