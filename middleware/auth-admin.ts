export default defineNuxtRouteMiddleware((to, from) => {
    // Vérifie que l'on est bien côté client avant d'accéder à localStorage
    if (process.client) {
      const userRole = localStorage.getItem("role");
      if (userRole !== "Admin") {
        return navigateTo("/"); // Rediriger vers la page d'accueil ou une page d'erreur
      }
    }
  });
  