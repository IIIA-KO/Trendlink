import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import basicSsl from '@vitejs/plugin-basic-ssl'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    port: 3000,
    hmr: {
      protocol: 'wss',
      host: 'localhost',
      port: 3000,
    },
  },
  plugins: [react(), basicSsl()],
})
