// nuxt.config.ts
export default defineNuxtConfig({
	compatibilityDate: "2024-04-03",
	devtools: { enabled: true },
	modules: ["@nuxt/ui", "@pinia/nuxt", "nuxt-primevue"],
	css: [
		"~/assets/fonts.css",
		"primevue/resources/themes/lara-light-blue/theme.css",
		"primevue/resources/primevue.css",
		"primeicons/primeicons.css",
	],
	app: {
		rootAttrs: {
			class: "min-h-dvh flex flex-col",
		},
	},
	build: {
		transpile: ["primevue"],
	},
});
