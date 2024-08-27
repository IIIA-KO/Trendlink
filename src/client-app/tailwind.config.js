
/** @type {import('tailwindcss').Config} */
export default {
  content: ["./src/**/*.{html,js,jsx,ts,tsx}"],
  theme: {
    extend: {
      fontFamily: {
        inter: ['Inter', 'sans-serif'],
      },
      fontWeight: {
        light: '300',
        normal: '450',
        bold: '700',
      },
      colors: {
        primary: "#009EA0",
        hover: "#21CBCD",
        background: "#FFFFFF",
        textPrimary: "#FFFFFF",
        textSecondary: "#3C3C3C",
        // и т.д.
      },
    },
  },
  plugins: [],
}

