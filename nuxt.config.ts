// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
	compatibilityDate: "2024-04-03",
	devtools: { enabled: true },
	modules: ["@nuxt/ui"],
	css: ["~/assets/fonts.css"],
	app: {
		rootAttrs: {
			class: "min-h-dvh flex flex-col",
		},
		pageTransition: { name: "page", mode: "out-in" },
	},
	tailwindcss: {
		config: {
			content: ["nuxt.config.ts"],
		},
	},
});
