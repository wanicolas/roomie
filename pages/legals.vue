<template>
	<div class="prose dark:prose-invert mx-4 max-w-screen-xl md:mx-12 xl:mx-auto">
		<h1>Mentions Légales</h1>
		<h2>1. Éditeur du site</h2>
		<p>
			Nom de l'entreprise : Roomie<br />
			Représentants légaux : Nicolas Walter, Nicolas Arena.<br />
		</p>
		<h2>2. Hébergement</h2>
		<p>Hébergeur : [Nom de l'hébergeur fictif]<br /></p>
		<h2>3. Propriété intellectuelle</h2>
		<p>
			Le contenu du site (textes, images, logo) est protégé par les lois sur la
			propriété intellectuelle. Toute reproduction sans autorisation est
			interdite.
		</p>
		<h2>4. Collecte des données</h2>
		<p>
			Les informations collectées sont utilisées uniquement dans le cadre de la
			réservation des salles et ne sont pas partagées avec des tiers.
		</p>
		<h2>5. Cookies</h2>
		<p>
			Ce site utilise des cookies pour améliorer l'expérience utilisateur. Vous
			pouvez les désactiver dans les paramètres de votre navigateur au détriment
			de l'expérience utilisateur et de certaines fonctionnalités.
		</p>
	</div>
</template>

<script setup>
useHead({
	title: "Mentions légales - Roomie, gestion et réservation de salles",
	meta: [
		{
			name: "description",
			content: "Retrouvez ici les mentions légales du site.",
		},
	],
});
</script>
