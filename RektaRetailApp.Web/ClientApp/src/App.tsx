import React, { Component, lazy, Suspense } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import { FetchData } from "./components/FetchData";
import { Counter } from "./components/Counter";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import { ApplicationPaths } from "./components/api-authorization/ApiAuthorizationConstants";
//import CreateInventory from "./pages/inventory/CreateInventory";
import "./custom.css";

const CreateInventory = lazy(() => import("./pages/inventory/CreateInventory"));
const CreateCategory = lazy(() => import('./pages/category/CreateCategory'));

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Suspense fallback={<h3>Loading, please hold...</h3>}>
        <Layout>
          <Route exact path="/" component={Home} />
          <Route path="/counter" component={Counter} />
          <Route path="/inventory" component={CreateInventory} />
          <Route path="/category" component={CreateCategory} />
          <AuthorizeRoute path="/fetch-data" component={FetchData} />
          <Route
            path={ApplicationPaths.ApiAuthorizationPrefix}
            component={ApiAuthorizationRoutes}
          />
        </Layout>
      </Suspense>
    );
  }
}
