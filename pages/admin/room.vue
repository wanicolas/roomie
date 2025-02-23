<template>
	<div class="mx-4 max-w-screen-xl md:mx-12 xl:mx-auto">
		<h1 class="mb-6 text-3xl font-semibold text-primary-700 dark:text-primary-300">
			Gestion des salles
		</h1>

		<!-- Vérification du rôle avant l'affichage de l'option d'ajout -->
		<UButton v-if="isAdmin" color="primary" @click="router.push('/admin/room/edit')">
			Ajouter une salle
		</UButton>

		<UTable class="mt-6" :rows="rooms" :columns="columns">
			<template #actions="{ row }">
				<!-- Vérification du rôle avant l'affichage des actions -->
				<UButton v-if="isAdmin" size="xs" color="primary" :to="`/admin/room/edit/${row.id}`">
					Modifier
				</UButton>
				<UButton v-if="isAdmin" size="xs" color="red" variant="outline" @click="deleteRoom(row.id)">
					Supprimer
				</UButton>
			</template>
		</UTable>
	</div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router"; // Importation de useRouter

const router = useRouter();

// Vérifie si l'utilisateur est admin
const isAdmin = ref(false);

// Vérifie si on est côté client et si l'utilisateur est un admin
onMounted(() => {
	if (process.client) {
		const userRole = localStorage.getItem("role");
		if (userRole !== "Admin") {
			router.push("/"); // Redirige vers la page d'accueil si l'utilisateur n'est pas admin
		} else {
			isAdmin.value = true; // L'utilisateur est admin
		}
	}
});

const { data: rooms, refresh } = await useFetch("http://localhost:5184/api/Room");

const columns = [
	{ key: "name", label: "Nom" },
	{ key: "capacity", label: "Capacité" },
	{ key: "surface", label: "Surface (m²)" },
	{ key: "building", label: "Bâtiment" },
	{ key: "actions", label: "Actions" },
];

const deleteRoom = async (id) => {
	if (!confirm("Voulez-vous vraiment supprimer cette salle ?")) return;

	await fetch(`http://localhost:5184/api/Room/${id}`, {
		method: "DELETE",
		headers: {
			Authorization: `Bearer ${localStorage.getItem("token")}`,
		},
	});

	refresh();
};
</script>
