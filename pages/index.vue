<template>
	<div class="mx-4 max-w-screen-xl md:mx-12 xl:mx-auto">
		<h1 class="mb-8 text-center text-3xl font-semibold md:text-7xl">
			Optimisez la gestion <br />
			de réservation de vos salles
		</h1>
		<p
			class="mb-10 text-center text-xl font-medium text-gray-600 dark:text-gray-300"
		>
			Votre entreprise possède de nombreuses salles ? En choisir une, s'assurer
			qu'elle soit libre est long et pénible ?
			<br />
			Ne cherchez plus, Roomie est là pour vous !
		</p>
		<div class="flex items-center justify-center gap-3">
			<UButton to="/book">C'est parti</UButton>
			<NuxtLink to="#faq" class="group flex items-center gap-1.5">
				En savoir plus
				<UIcon
					name="ph:arrow-right"
					class="size-4 transition-all group-hover:translate-x-1"
				/>
			</NuxtLink>
		</div>

		<div class="mt-32 md:mx-auto md:max-w-screen-md">
			<div class="mb-20 text-center">
				<h2 class="mb-4 text-2xl font-semibold md:text-4xl">
					Boostez votre productivité. <br />
					Utilisez Roomie.
				</h2>
				<p class="text-gray-600 md:text-lg dark:text-gray-300">
					Ne perdez pas de temps à chercher une salle, à changer d'étage ou de
					bâtiment. Oubliez le temps où, une fois arrivé devant la salle, vous
					vous rendez compte qu'elle est déjà occupée. Soyez plus rapides.
				</p>
			</div>
			<h2 id="faq" class="mb-10 text-4xl font-semibold">
				Questions fréquentes
			</h2>
			<UAccordion color="gray" variant="solid" size="lg" :items="faqs" />
		</div>
	</div>
</template>

<script setup>
useHead({
	title: "Mentions légales - Roomie, gestion et réservation de salles",
	meta: [
		{
			name: "description",
			content:
				"Roomie est une plateforme de réservation de salles de réunion. Retrouvez ici les mentions légales du site.",
		},
	],
});

// Données FAQ
const faqs = [
	{
		label: "Comment réserver une salle ?",
		content:
			"Pour réserver une salle, il vous suffit de vous rendre sur la page de réservation et de choisir la salle que vous souhaitez réserver. Vous pourrez ensuite choisir la date et l'heure de début et de fin de votre réservation.",
	},
	{
		label: "Comment annuler une réservation ?",
		content:
			"Pour annuler une réservation, il vous suffit de vous rendre sur la page de votre compte et de se rendre sur votre réservation afin de l'annuler.",
	},
	{
		label: "Peut-on filtrer les salles par capacité ?",
		content:
			"Oui, vous pouvez filtrer les salles par capacité. Pour cela, il vous suffit de vous rendre sur la page de réservation et de choisir la capacité minimale que vous souhaitez. D'autres filtres sont également disponibles.",
	},
];
</script>
