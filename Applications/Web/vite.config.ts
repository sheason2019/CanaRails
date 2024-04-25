import { defineConfig } from "vite";
import solid from "vite-plugin-solid";

export default defineConfig({
  plugins: [solid()],
  base: "/web",
  server: {
    proxy: {
      "^\/(?!web)": {
        target: "http://localhost:8080",
        changeOrigin: true,
      },
    },
  },
});
