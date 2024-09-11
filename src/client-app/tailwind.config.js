/** @type {import('tailwindcss').Config} */
export default {
  content: ["./src/**/*.{html,js,jsx,ts,tsx}"],
  theme: {
    extend: {},
  },
  plugins: [],
}

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      colors: {
        aliceblue: {
          "100": "#eff7ff",
          "200": "#f0f4f9",
        },
        "main-black": "#3c3c3c",
        "main-green": "#009ea0",
        white: "#fff",
        text: "#444",
        gray: {
          "100": "#7c7c7c",
          "200": "#181d25",
        },
        crimson: "#dd1e1e",
        whitesmoke: {
          "100": "#f6f6f6",
          "200": "#ebebeb",
          "300": "#eaeaea",
        },
        sandybrown: "#f3ae5f",
        transparent: "#c0bebe",
        darkslategray: {
          "100": "#4b4b4b",
          "200": "rgba(60, 60, 60, 0)",
          "300": "rgba(38, 46, 46, 0)",
        },
        "text-2": "#616161",
        gainsboro: {
          "100": "#d9d9d9",
          "200": "rgba(217, 217, 217, 0.8)",
          "300": "rgba(217, 217, 217, 0.75)",
        },
        darkgray: "#8da7ad",
        "neutral-blue-black-10": "#fdfdfd",
        slategray: "#7e818c",
        "neutral-blue-black-40": "#e4e5e7",
        black: "#000",
        orange: "#f6a801",
        silver: {
          "100": "#c5c5c5",
          "200": "#b8b6b6",
        },
      },
      spacing: {},
      fontFamily: {
        inter: "Inter",
        "logo-font": "Kodchasan",
      },
      borderRadius: {
        "21xl": "40px",
        xl: "20px",
        "20xl": "39px",
        "19xl": "38px",
        "8xs": "5px",
        "3xs": "10px",
        mini: "15px",
        "12xs": "1px",
      },
    },
    fontSize: {
      xs: "12px",
      sm: "14px",
      base: "16px",
      xl: "20px",
      "3xs": "10px",
      "2xs": "11px",
      "5xl": "24px",
      lgi: "19px",
      inherit: "inherit",
    },
    screens: {
      lg: {
        max: "1200px",
      },
      mq1050: {
        raw: "screen and (max-width: 1050px)",
      },
      mq900: {
        raw: "screen and (max-width: 900px)",
      },
      mq750: {
        raw: "screen and (max-width: 750px)",
      },
      mq700: {
        raw: "screen and (max-width: 700px)",
      },
      mq450: {
        raw: "screen and (max-width: 450px)",
      },
    },
  },
  corePlugins: {
    preflight: false,
  },
};
