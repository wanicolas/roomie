<template>
	<Body class="bg-white text-gray-900 dark:bg-gray-900 dark:text-white">
		<NuxtLink
			to="#content"
			class="fixed -top-10 left-4 rounded-md bg-gray-900 px-2 py-1 text-white shadow-md focus:top-4 dark:bg-white dark:text-gray-900"
		>
			Aller au contenu
		</NuxtLink>
		<header
			class="mx-4 mb-20 flex w-full max-w-screen-xl items-center justify-between pt-6 md:mx-12 md:mb-32 md:pt-8 lg:mb-40 xl:mx-auto"
		>
			<Logo />
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

		<div id="content" class="grow">
			<NuxtPage />
		</div>

		<footer
			class="mx-4 my-6 flex w-full max-w-screen-xl items-center justify-between md:mx-12 md:my-8 xl:mx-auto"
		>
			<Logo />
			<nav class="flex items-center gap-6 md:gap-10">
				<NuxtLink to="/legals" class="underline-offset-4 hover:underline">
					Mentions légales
				</NuxtLink>
				<NuxtLink
					to="https://github.com/wanicolas/roomie"
					class="underline-offset-4 hover:underline"
				>
					Code source
				</NuxtLink>
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
