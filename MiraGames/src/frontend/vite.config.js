import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      "/auth": {
        target: "http://backend:8080",
        changeOrigin: true,
        secure: false,
      },
      "/clients": {
        target: "http://backend:8080",
        changeOrigin: true,
        secure: false,
      },
      "/rate": {
        target: "http://backend:8080",
        changeOrigin: true,
        secure: false,
      },
      "/payments": {
        target: "http://backend:8080",
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
