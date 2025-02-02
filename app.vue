<template>
	<Body class="bg-white text-gray-900 dark:bg-gray-900 dark:text-white">
		<header
			class="mx-4 mb-20 flex max-w-screen-xl items-center justify-between pt-6 md:mx-12 md:pt-8 xl:mx-auto"
		>
			<NuxtLink to="/" class="group flex items-center gap-1">
				<span class="sr-only">Accueil</span>
				<UIcon
					name="ph:door"
					class="size-8 text-primary-700 group-hover:hidden dark:text-primary-300"
				/>
				<UIcon
					name="ph:door-open"
					class="hidden size-8 text-primary-700 group-hover:block dark:text-primary-300"
				/>
				<span>Roomie</span>
			</NuxtLink>
			<nav class="flex items-center gap-6 md:gap-10">
				<NuxtLink to="/book" class="underline-offset-4 hover:underline"
					>Réserver</NuxtLink
				>
				<NuxtLink to="/login" class="underline-offset-4 hover:underline"
					>Login</NuxtLink
				>
				<button
					class="rounded-md p-1 hover:bg-gray-100 dark:hover:bg-gray-700"
					@click="toggleColorMode"
				>
					<svg
						class="size-5 fill-gray-900 dark:fill-white"
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
			class="mx-4 my-6 flex max-w-screen-xl items-center justify-between md:mx-12 md:my-8 xl:mx-auto"
		>
			<NuxtLink to="/" class="group flex items-center gap-1">
				<span class="sr-only">Accueil</span>
				<UIcon
					name="ph:door"
					class="size-8 text-primary-700 group-hover:hidden dark:text-primary-300"
				/>
				<UIcon
					name="ph:door-open"
					class="hidden size-8 text-primary-700 group-hover:block dark:text-primary-300"
				/>
				<span>Roomie</span>
			</NuxtLink>
			<nav class="flex items-center gap-6 md:gap-10">
				<NuxtLink to="/legals" class="underline-offset-4 hover:underline"
					>Mentions légales</NuxtLink
				>
				<NuxtLink
					to="https://github.com/wanicolas/roomie"
					class="underline-offset-4 hover:underline"
					>Code source</NuxtLink
				>
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
