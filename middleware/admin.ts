export default defineNuxtRouteMiddleware(() => {
	const authStore = useAuthStore();

	if (process.client) {
		if (!authStore.isLoggedIn || !authStore.isAdmin) {
			return navigateTo("/login");
		}
	}
});
