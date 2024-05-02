import { Route, Router } from "@solidjs/router";
import HomePage from "./pages";
import DashboardPage from "./pages/dashboard";
import AppPage from "./pages/app";
import NewAppPage from "./pages/app/new";
import AppDetailPage from "./pages/app/detail";
import AppEntryPage from "./pages/app/detail/entry";
import NewAppEntryPage from "./pages/app/detail/entry/new";
import AppImagePage from "./pages/app/detail/image";
import NewAppImagePage from "./pages/app/detail/image/new";
import AppEntryDetailPage from "./pages/app/detail/entry/detail";

function App() {
  return (
    <Router>
      <Route path="/" component={HomePage} />
      <Route path="/dashboard" component={DashboardPage} />
      <Route path="/app" component={AppPage} />
      <Route path="/app/new" component={NewAppPage} />
      <Route path="/app/:id" component={AppDetailPage} />
      <Route path="/app/:id/entry" component={AppEntryPage} />
      <Route path="/app/:id/entry/new" component={NewAppEntryPage} />
      <Route path="/app/:id/entry/:entryID" component={AppEntryDetailPage} />
      <Route path="/app/:id/image" component={AppImagePage} />
      <Route path="/app/:id/image/new" component={NewAppImagePage} />
    </Router>
  );
}

export default App;
