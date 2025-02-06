import globals from "globals";
import pluginJs from "@eslint/js";
import tseslint from "typescript-eslint";
import pluginVue from "eslint-plugin-vue";
// We import the eslint-config-prettier package to disable all ESLint rules that are unnecessary or might conflict with Prettier.
import eslintConfigPrettier from "eslint-config-prettier";
// We import the createConfigForNuxt function from the @nuxt/eslint-config package to use the recommended ESLint configuration for Nuxt.js.

export default [
	{ languageOptions: { globals: globals.browser } },
	pluginJs.configs.recommended,
	...tseslint.configs.recommended,
	...pluginVue.configs["flat/essential"],
	{
		rules: {
			"vue/no-multiple-template-root": "off",
			"vue/singleline-html-element-content-newline": "off",
			"vue/html-self-closing": [
				"error",
				{
					html: {
						void: "always",
					},
				},
			],
			"vue/no-v-html": "off",
			"vue/multi-word-component-names": "off",
		},
	},
	// Use the eslint-config-prettier
	eslintConfigPrettier,
	// Use the recommended ESLint configuration for Nuxt.js
	createConfigForNuxt,
];
