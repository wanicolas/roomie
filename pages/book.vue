<template>
	<h1>Réserver une salle</h1>
	<div class="flex flex-col gap-3 p-3">
		<UForm>
			<UFormGroup label="Bâtiment">
				<USelect icon="ph:building" />
			</UFormGroup>
			<UFormGroup label="Date de disponibilité">
				<UInput type="date" icon="ph:calendar" />
			</UFormGroup>
			<UFormGroup label="Heures de disponibilité">
				<UInput type="time" icon="ph:clock" />
			</UFormGroup>
			<UFormGroup label="Capacité en nombre de personnes">
				<UInput type="number" min="0" icon="ph:users-three" />
			</UFormGroup>
			<UFormGroup label="Surface en m2">
				<UInput type="number" min="0" icon="ph:resize" />
			</UFormGroup>
			<UCheckbox label="Accessible aux PMR" />
			<fieldset>
				Équipements disponibles :
				<UCheckbox label="Vidéoprojecteur" />
				<UCheckbox label="Enceintes" />
			</fieldset>
			<UFormGroup label="Places assises minimum">
				<UInput type="number" min="0" icon="ph:office-chair" />
			</UFormGroup>
			<UFormGroup label="Nom de la salle">
				<UInput type="text" />
			</UFormGroup>
			<UButton type="submit">Rechercher</UButton>
		</UForm>
	</div>

	<div class="grid">
		<div v-for="i in 10">
			<img class="h-40 w-full object-cover" src="/room.webp" alt="" />
		</div>
	</div>
</template>
