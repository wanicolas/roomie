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
				<UInput type="date" icon="ph:calendar" />
			</UFormGroup>
			<UFormGroup label="Heure de début">
				<UInput type="time" icon="ph:clock" />
			</UFormGroup>
			<UFormGroup label="Heure de fin">
				<UInput type="time" icon="ph:clock" />
			</UFormGroup>

			<details class="space-y-3">
				<summary>Plus de filtres</summary>
				<UFormGroup label="Capacité en nombre de personnes">
					<UInput type="number" min="0" icon="ph:users-three" />
				</UFormGroup>
				<UFormGroup label="Surface en m2">
					<UInput type="number" min="0" icon="ph:resize" />
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
			class="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3 xl:mx-auto xl:max-w-screen-xl"
		>
			<NuxtLink
				to="/room"
				v-for="i in 10"
				:key="i"
				class="overflow-hidden rounded-md bg-white shadow dark:bg-gray-900"
			>
				<img class="h-60 w-full object-cover" src="/room.webp" alt="" />
				<div class="flex items-baseline justify-between p-3">
					<div>
						<div class="text-xl font-medium">Nom de la salle</div>
						<div class="flex items-center gap-1.5">
							<UIcon name="ph:stairs" class="size-5" />
							<div class="text-gray-600 dark:text-gray-300">
								Étage de la salle, Bâtiment
							</div>
						</div>
					</div>
					<div
						class="flex items-center gap-1.5 text-gray-700 dark:text-gray-200"
					>
						5
						<UIcon name="ph:users-three" class="size-5" />
					</div>
				</div>
			</NuxtLink>
		</div>
	</div>
</template>
