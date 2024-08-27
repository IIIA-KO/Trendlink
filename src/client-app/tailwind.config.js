
/** @type {import('tailwindcss').Config} */
export default {
  content: ["./src/**/*.{html,js,jsx,ts,tsx}"],
  theme: {
    extend: {
      fontFamily: {
        inter: ['Inter', 'sans-serif'],
        kodchasan: ['Kodchasan', 'sans-serif'],
      },
      fontWeight: {
        light: '300',
        regular: '400',
        medium: '500',
        semibold: '600',
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

