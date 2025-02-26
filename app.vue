<template>
	<Body class="bg-white text-gray-900 dark:bg-gray-900 dark:text-white">
		<UButton to="#content" class="fixed -top-10 left-4 z-10 focus:top-4">
			Aller au contenu principal
		</UButton>
		<header
			class="mb-20 flex w-full max-w-screen-xl items-center justify-between px-4 pt-6 md:mb-32 md:px-12 md:pt-8 lg:mb-40 xl:mx-auto"
		>
			<Logo />
			<nav class="flex items-center gap-6 md:gap-10">
				<NuxtLink to="/book" class="underline-offset-4 hover:underline">
					Réserver
				</NuxtLink>
				<NuxtLink v-if="userConnected" to="/user/profile" class="leading-[0]">
					<UIcon name="ph:user" class="size-5" />
				</NuxtLink>
				<template v-else>
					<NuxtLink to="/login" class="underline-offset-4 hover:underline">
						Connexion
					</NuxtLink>
					<NuxtLink to="/register" class="underline-offset-4 hover:underline">
						Inscription
					</NuxtLink>
				</template>
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

		<main
			id="content"
			class="w-full max-w-screen-xl grow px-4 md:px-12 xl:mx-auto"
		>
			<NuxtPage />
		</main>

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

const authCookie = useCookie("auth_token");
const userConnected = ref(false);

watchEffect(() => {
	if (authCookie.value) userConnected.value = true;
	else userConnected.value = false;
});

const router = useRouter();

const logout = () => {
	useCookie("auth_token").value = "";
	router.push("/login");
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
