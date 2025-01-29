<template>
	<Body class="bg-white text-gray-900 dark:bg-gray-900 dark:text-white">
		<header
			class="mx-4 mb-28 mt-6 flex items-center justify-between md:mx-12 md:my-8"
		>
			<NuxtLink to="/" class="flex items-center gap-1">
				<span class="sr-only">Accueil</span>
				<UIcon
					name="ph:door-open"
					class="size-8 text-primary-700 dark:text-primary-300"
				/>
				<span>Roomie</span>
			</NuxtLink>
			<nav class="flex items-center gap-6 md:gap-10">
				<NuxtLink to="/book">Réserver</NuxtLink>
				<NuxtLink to="/login">Login</NuxtLink>
				<button @click="toggleColorMode">
					<svg
						class="fill-gray-900 dark:fill-white"
						xmlns="http://www.w3.org/2000/svg"
						width="24"
						height="24"
						viewBox="0 0 24 24"
					>
						<path
							d="M0 12c0 6.627 5.373 12 12 12s12-5.373 12-12-5.373-12-12-12-12 5.373-12 12zm2 0c0-5.514 4.486-10 10-10v20c-5.514 0-10-4.486-10-10z"
						></path>
					</svg>
					<span class="sr-only">Activer/Désactiver le mode sombre</span>
				</button>
			</nav>
		</header>

		<NuxtPage />

		<footer
			class="mx-4 my-6 flex items-center justify-between md:mx-12 md:my-8"
		>
			<NuxtLink to="/" class="flex items-center gap-1">
				<span class="sr-only">Accueil</span>
				<UIcon
					name="ph:door-open"
					class="size-8 text-primary-700 dark:text-primary-300"
				/>
				<span>Roomie</span>
			</NuxtLink>
			<nav class="flex items-center gap-6 md:gap-10">
				<NuxtLink to="/legals">Mentions légales</NuxtLink>
				<NuxtLink to="https://github.com">Code source</NuxtLink>
			</nav>
		</footer>
	</Body>
</template>

<script setup>
const colorMode = useColorMode();

const toggleColorMode = () => {
	colorMode.preference = colorMode.preference === "light" ? "dark" : "light";
};
</script>

<style>
/* Transition between pages */
.page-enter-active,
.page-leave-active {
	transition: all 0.5s;
}

.page-enter-from,
.page-leave-to {
	opacity: 0;
	filter: blur(0.1rem);
}
</style>
