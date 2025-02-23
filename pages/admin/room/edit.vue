<template>
	<div class="mx-4 max-w-screen-md md:mx-12 xl:mx-auto">
		<h1 class="mb-6 text-3xl font-semibold text-primary-700 dark:text-primary-300">
			{{ isEditing ? "Modifier" : "Créer" }} une salle
		</h1>

		<UForm :state="room" @submit="saveRoom">
			<UFormGroup label="Nom de la salle" name="name" required>
				<UInput v-model="room.name" />
			</UFormGroup>

			<UFormGroup label="Capacité" name="capacity" required>
				<UInput v-model="room.capacity" type="number" />
			</UFormGroup>

			<UFormGroup label="Surface (m²)" name="surface" required>
				<UInput v-model="room.surface" type="number" />
			</UFormGroup>

			<UFormGroup label="Bâtiment" name="building" required>
				<UInput v-model="room.building" />
			</UFormGroup>

			<UFormGroup label="Accessibilité PMR" name="isAccessible">
				<UCheckbox v-model="room.isAccessible" />
			</UFormGroup>

			<UFormGroup label="Équipements" name="equipments">
				<UCheckbox v-model="room.equipments.projector" label="Vidéoprojecteur" />
				<UCheckbox v-model="room.equipments.speakers" label="Enceintes" />
			</UFormGroup>

			<UButton type="submit" class="mt-4">
				{{ isEditing ? "Modifier" : "Créer" }} la salle
			</UButton>
		</UForm>

		<UButton color="red" variant="outline" v-if="isEditing" class="mt-4" @click="deleteRoom">
			Supprimer la salle
		</UButton>
	</div>
</template>

<script setup>
import { onMounted, ref, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const isEditing = computed(() => !!route.params.id);

const room = ref({
	name: "",
	capacity: 0,
	surface: 0,
	building: "",
	isAccessible: false,
	equipments: {
		projector: false,
		speakers: false,
	},
});

const loading = ref(true);

// Vérifie si l'utilisateur est admin à l'aide de `localStorage` après le montage du composant
onMounted(() => {
	if (process.client) {
		const userRole = localStorage.getItem("role");

		if (userRole !== "Admin") {
			router.push("/"); // Redirige si l'utilisateur n'est pas admin
		}

		const token = localStorage.getItem("token");
		if (!token) {
			router.push("/login"); // Redirige vers la page de login si le token est absent
		}
	}
});

// Récupère les données de la salle si on est en mode édition
if (isEditing.value) {
	try {
		const { data } = await useFetch(`http://localhost:5184/api/Room/${route.params.id}`);
		room.value = data.value;
	} catch (error) {
		console.error("Erreur lors de la récupération de la salle", error);
	} finally {
		loading.value = false;
	}
}

const saveRoom = async () => {
	const url = isEditing.value
		? `http://localhost:5184/api/Room/${route.params.id}`
		: "http://localhost:5184/api/Room";

	const method = isEditing.value ? "PUT" : "POST";

	await fetch(url, {
		method,
		headers: {
			"Content-Type": "application/json",
			Authorization: `Bearer ${localStorage.getItem("token")}`,
		},
		body: JSON.stringify(room.value),
	});

	router.push("/admin/rooms");
};

const deleteRoom = async () => {
	if (!confirm("Voulez-vous vraiment supprimer cette salle ?")) return;

	await fetch(`http://localhost:5184/api/Room/${route.params.id}`, {
		method: "DELETE",
		headers: {
			Authorization: `Bearer ${localStorage.getItem("token")}`,
		},
	});

	router.push("/admin/rooms");
};
</script>
