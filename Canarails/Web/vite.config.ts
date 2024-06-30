import { defineConfig } from "vite";
import react from "@vitejs/plugin-react-swc";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  build: {
    // 构建产物应放置到 Dashboard/webroot
    // 随 Dashboard 一起构建为单体 Web 应用
    outDir: "../Admin/wwwroot",
    emptyOutDir: true,

    // 由于该应用是一个后台管理页面，对于包体积并不特别敏感
    // 因此这里将警告包体积修改为 2MB
    chunkSizeWarningLimit: 2_000,
  },
  server: {
    proxy: {
      "/api/": {
        target: "http://localhost:8080",
        changeOrigin: true,
      },
    },
  },
});
