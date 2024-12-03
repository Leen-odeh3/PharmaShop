/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
  colors:{
        'primary':"#24AEB1",
        'secondary':"#1d2a38",
        'subColor':'#EB3A7B',
        'gray':"#7d879c"
      }
    },
  },
  plugins: [],
}