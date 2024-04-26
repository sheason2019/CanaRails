import { Route, Router } from "@solidjs/router";
import HomePage from "./pages";
import DashboardPage from "./pages/dashboard";
import AppPage from "./pages/app";
import NewAppPage from "./pages/app/new";

function App() {
  return (
    <Router>
      <Route path="/" component={HomePage} />
      <Route path="/dashboard" component={DashboardPage} />
      <Route path="/app" component={AppPage} />
      <Route path="/app/new" component={NewAppPage} />
    </Router>
  );
}

export default App;
