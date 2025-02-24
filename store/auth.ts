import { defineStore } from "pinia";

export const useAuthStore = defineStore("auth", {
	state: () => ({
		token: process.client ? localStorage.getItem("token") || null : null,
		role: process.client ? localStorage.getItem("role") || null : null,
	}),
	getters: {
		isLoggedIn: (state) => !!state.token,
		isAdmin: (state) => state.role === "Admin",
	},
	actions: {
		login(token: string, role: string) {
			this.token = token;
			this.role = role;
			if (process.client) {
				localStorage.setItem("token", token);
				localStorage.setItem("role", role);
			}
		},
		logout() {
			this.token = null;
			this.role = null;
			if (process.client) {
				localStorage.removeItem("token");
				localStorage.removeItem("role");
			}
		},
		checkAuth() {
			if (process.client) {
				this.token = localStorage.getItem("token");
				this.role = localStorage.getItem("role");
			}
		},
	},
});
