import { Route, Router } from "@solidjs/router";
import HomePage from "./pages";
import DashboardPage from "./pages/dashboard";
import AppPage from "./pages/app";
import NewAppPage from "./pages/app/new";
import AppDetailPage from "./pages/app/detail";
import AppEntryPage from "./pages/app/detail/entry";
import NewAppEntryPage from "./pages/app/detail/entry/new";

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
    </Router>
  );
}

export default App;
