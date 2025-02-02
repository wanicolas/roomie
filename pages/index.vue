<template>
	<div class="px-6">
	  <h1 class="mb-8 text-4xl font-semibold text-primary-700 dark:text-primary-300">
		Roomie, optimisez la gestion de réservation de vos salles
	  </h1>
	  <p class="mb-10 text-xl font-medium text-gray-600 dark:text-gray-300">
		Votre entreprise possède de nombreuses salles ? En choisir une, s'assurer
		qu'elle soit libre est long et pénible ?
		<br />
		Ne cherchez plus, Roomie est là pour vous !
	  </p>
	  <!-- Liste des salles -->
	  <div v-if="rooms.length > 0" class="mb-12">
		<h2 class="text-2xl font-semibold">Nos salles disponibles :</h2>
		<ul>
		  <li v-for="room in rooms" :key="room.id" class="py-4">
			<strong>{{ room.name }}</strong> - {{ room.capacity }} personnes
		  </li>
		</ul>
	  </div>
	  <!-- Alerte si aucune salle n'est disponible -->
	  <div v-else class="mb-12 text-red-500">
		Aucune salle disponible pour le moment.
	  </div>
  
	  <!-- Le reste de la page reste inchangé -->
	  <div class="my-20 text-center">
		<h2 class="mb-4 text-2xl font-semibold">
		  Boostez votre productivité. <br />
		  Utilisez Roomie.
		</h2>
		<p class="text-gray-600 dark:text-gray-300">
		  Ne perdez pas de temps à chercher une salle, à changer d'étage ou de
		  bâtiment. Oubliez le temps où, une fois arrivé devant la salle, vous
		  vous rendez compte qu'elle est déjà occupée. Soyez plus rapides.
		</p>
	  </div>
  
	  <h2 id="faq" class="text-2xl font-semibold">Questions fréquentes</h2>
	  <div class="-mt-1 mb-8 border-b"></div>
	  <div>
		<details v-for="faq in faqs" :key="faq" class="mb-4">
		  <summary class="mb-2 flex items-center justify-between gap-3 border-b pb-1">
			{{ faq.title }}
			<UIcon name="ph:plus" class="size-6" />
		  </summary>
		  {{ faq.content }}
		</details>
	  </div>
	</div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue';
  
  // Stocke la liste des salles
  const rooms = ref([]);
  
// Appel API pour récupérer les salles
onMounted(async () => {
  try {
    const response = await fetch('http://localhost:5184/api/Room'); // Modifie l'URL ici
    if (!response.ok) {
      throw new Error('Erreur lors de la récupération des salles');
    }
    rooms.value = await response.json(); // Enregistre les salles récupérées
  } catch (error) {
    console.error('Erreur:', error);
  }
});
  
  // Données FAQ (inchangées)
  const faqs = [
	{
	  title: "Comment réserver une salle ?",
	  content:
		"Pour réserver une salle, il vous suffit de vous rendre sur la page de réservation et de choisir la salle que vous souhaitez réserver. Vous pourrez ensuite choisir la date et l'heure de début et de fin de votre réservation.",
	},
	{
	  title: "Comment annuler une réservation ?",
	  content:
		"Pour annuler une réservation, il vous suffit de vous rendre sur la page de votre compte et de se rendre sur votre réservation afin de l'annuler.",
	},
	{
	  title: "Peut-on filtrer les salles par capacité ?",
	  content:
		"Oui, vous pouvez filtrer les salles par capacité. Pour cela, il vous suffit de vous rendre sur la page de réservation et de choisir la capacité minimale que vous souhaitez. D'autres filtres sont également disponibles.",
	},
  ];
  </script>
  