// nuxt.config.ts
export default defineNuxtConfig({
    compatibilityDate: "2024-04-03",
    devtools: { enabled: true },
    modules: ["@nuxt/ui", "@pinia/nuxt"],
    css: ["~/assets/fonts.css"],
    app: {
        rootAttrs: {
            class: "min-h-dvh flex flex-col",
        },
    },
    tailwindcss: {
        config: {
            content: [
                "./components/**/*.{vue,js,ts}", 
                "./layouts/**/*.vue", 
                "./pages/**/*.vue", 
                "./plugins/**/*.{js,ts}", 
                "./store/**/*.{js,ts}", 
                "./app.vue"
            ],
        },
    },
    
});
