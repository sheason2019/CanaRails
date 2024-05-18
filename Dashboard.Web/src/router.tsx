import { createBrowserRouter } from "react-router-dom";
import HomePage from "./pages";
import DashboardPage from "./pages/dashboard";
import AdminLayout from "./components/admin-layout";
import AppPage from "./pages/app";
import NewAppPage from "./pages/app/new";
import AppDetailPage from "./pages/app/[appId]";

export const router = createBrowserRouter([
  {
    path: "",
    children: [
      // HomePage 没有 Layout
      {
        path: "/",
        element: <HomePage />,
      },
      // 带有 Layout 的后台管理页面
      {
        path: "",
        element: <AdminLayout />,
        children: [
          {
            path: "/dashboard",
            element: <DashboardPage />,
          },
          {
            path: "/app",
            element: <AppPage />,
          },
          {
            path: "/app/new",
            element: <NewAppPage />,
          },
          {
            path: "/app/:appId",
            element: <AppDetailPage />,
          },
        ],
      },
    ],
  },
]);
