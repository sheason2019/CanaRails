import { createBrowserRouter } from "react-router-dom";
import HomePage from "./pages";
import DashboardPage from "./pages/dashboard";
import AdminLayout from "./components/admin-layout";
import AppPage from "./pages/app";
import NewAppPage from "./pages/app/new";
import AppDetailPage from "./pages/app/[appId]";
import AppEntryPage from "./pages/app/[appId]/entry";
import NewEntryPage from "./pages/app/[appId]/entry/new";
import EntryDetailPage from "./pages/app/[appId]/entry/[entryId]";
import AppImagePage from "./pages/app/[appId]/image";
import LoginPage from "./pages/login";
import UserAccessSecretPage from "./pages/user-access-secret";

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
          {
            path: "/app/:appId/entry",
            element: <AppEntryPage />,
          },
          {
            path: "/app/:appId/entry/new",
            element: <NewEntryPage />,
          },
          {
            path: "/app/:appId/entry/:entryId",
            element: <EntryDetailPage />,
          },
          {
            path: "/app/:appId/image",
            element: <AppImagePage />,
          },
          {
            path: "/login",
            element: <LoginPage />,
          },
          {
            path: "/user-access-secret",
            element: <UserAccessSecretPage />,
          },
        ],
      },
    ],
  },
]);
