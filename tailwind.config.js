import defaultTheme from "tailwindcss/defaultTheme";
import colors from "tailwindcss/colors";

export default {
	theme: {
		extend: {
			colors: {
				primary: colors.sky,
			},
			fontFamily: {
				sans: ["DMSans", ...defaultTheme.fontFamily.sans],
			},
		},
	},
};
