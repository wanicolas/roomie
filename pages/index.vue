<template>
	<div class="px-6">
		<h1
			class="mb-8 text-4xl font-semibold text-primary-700 dark:text-primary-300"
		>
			Roomie, optimisez la gestion de réservation de vos salles
		</h1>
		<p class="mb-10 text-xl font-medium text-gray-600 dark:text-gray-300">
			Votre entreprise possède de nombreuses salles ? En choisir une, s'assurer
			qu'elle soit libre est long et pénible ?
			<br />
			Ne cherchez plus, Roomie est là pour vous !
		</p>
		<div class="mb-12 flex items-center gap-6">
			<NuxtLink
				to="/book"
				class="rounded-md bg-primary-700 px-3 py-2 font-medium text-white"
			>
				C'est parti !
			</NuxtLink>
			<NuxtLink to="#faq" class="group inline-flex gap-1 font-medium">
				En savoir plus
				<UIcon
					name="ph:arrow-right"
					class="size-6 transition-all group-hover:translate-x-1"
				/>
			</NuxtLink>
		</div>

		<div class="my-20 text-center">
			<h2 class="mb-4 text-2xl font-semibold">
				Boostez votre productivité. <br />
				Utilisez Roomie.
			</h2>
			<p class="text-gray-600 dark:text-gray-300">
				Ne perdez pas de temps à chercher une salle, à changer d'étage ou de
				batiment. Oubliez le temps où, une fois arrivé devant la salle, vous
				vous rendez compte q'uelle est déjà occupée. Soyez plus rapides.
			</p>
		</div>

		<h2 id="faq" class="text-2xl font-semibold">Questions fréquentes</h2>
		<div class="-mt-1 mb-8 border-b"></div>
		<div>
			<details v-for="faq in faqs" :key="faq" class="mb-4">
				<summary
					class="mb-2 flex items-center justify-between gap-3 border-b pb-1"
				>
					{{ faq.title }}
					<UIcon name="ph:plus" class="size-6" />
				</summary>
				{{ faq.content }}
			</details>
		</div>
	</div>
</template>

<script setup>
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
