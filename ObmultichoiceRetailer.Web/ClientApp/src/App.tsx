import React, { Component, Suspense } from 'react'
import { Route } from 'react-router'
import Layout from './components/Layout'
import { Home } from './components/Home'
//import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes'
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants'
import CreateCategory from './pages/category/CreateCategory'
import { SalesDashboard } from './pages/sales'
import { AddInventory, InventoriesDashboard } from './pages/inventory'
import { CreateProduct } from './pages/products'
import { ProductDashboard } from './pages/products'
import { ReportDashboard } from './pages/reports'
//import './custom.css'
//import './bootstrap.min.css'

// const CreateInventory = lazy(() => import("./pages/inventory/CreateInventory"));
// const CreateCategory = lazy(() => import("./pages/category/CreateCategory"));
// const SalesDashboard = lazy(() => import("./pages/sales/salesDashboard"));

export default class App extends Component {
  static displayName = App.name

  render() {
    return (
      <Suspense fallback={<h3>Loading, please hold...</h3>}>
        <Layout>
          <Route exact path="/" component={Home} />
          <Route path="/products/create" component={CreateProduct} />
          <Route path="/inventory/add" component={AddInventory} />
          <Route path="/inventory" component={InventoriesDashboard} />
          <Route path="/category" component={CreateCategory} />
          <Route path="/sales" component={SalesDashboard} />
          <Route path="/products" component={ProductDashboard} />
          <Route path="/reports" component={ReportDashboard} />
          {/* <AuthorizeRoute path="/fetch-data" component={FetchData} /> */}
          <Route
            path={ApplicationPaths.ApiAuthorizationPrefix}
            component={ApiAuthorizationRoutes}
          />
        </Layout>
      </Suspense>
    )
  }
}
