<template>
	<div class="mx-4 max-w-screen-md md:mx-12 xl:mx-auto">
		<h1 class="mb-6 text-3xl font-semibold text-primary-700 dark:text-primary-300">
			Ajouter un administrateur
		</h1>

		<UForm @submit="addAdmin">
			<UFormGroup label="Email de l'utilisateur" name="email" required>
				<UInput v-model="email" />
			</UFormGroup>

			<UButton type="submit" class="mt-4">
				Ajouter comme admin
			</UButton>
		</UForm>
	</div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router"; // Importation de useRouter

const router = useRouter(); // Initialisation de useRouter
const email = ref("");

// Vérifie si l'utilisateur est admin avant de permettre l'ajout
onMounted(() => {
	if (process.client) {
		const userRole = localStorage.getItem("role");
		if (userRole !== "Admin") {
			router.push("/"); // Redirige vers la page d'accueil si l'utilisateur n'est pas admin
		}
	}
});

const addAdmin = async () => {
	try {
		const response = await fetch("http://localhost:5184/api/User/add-admin", {
			method: "POST",
			headers: {
				"Content-Type": "application/json",
				Authorization: `Bearer ${localStorage.getItem("token")}`,
			},
			body: JSON.stringify({ email: email.value }),
		});

		const data = await response.json();

		if (response.ok) {
			alert("Utilisateur ajouté comme admin.");
			router.push("/admin/rooms"); // Redirection après ajout
		} else {
			alert(data.message || "Une erreur s'est produite.");
		}
	} catch (error) {
		alert("Une erreur s'est produite.");
	}
};
</script>
