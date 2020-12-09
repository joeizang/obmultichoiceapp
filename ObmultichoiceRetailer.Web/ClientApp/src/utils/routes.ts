import Dashboard from '../components/Layout'
import { SalesDashboard } from '../pages/sales'
import { AddInventory } from '../pages/inventory'
import { CreateProduct } from '../pages/products'
import { ReportDashboard } from '../pages/reports'
import { SubRoute } from '../types/supplierTypes'

const blank = new Array<SubRoute>()

var routes = [
  {
    path: '/sales',
    name: 'Sales',
    icon: 'shopping_cart',
    component: SalesDashboard,
    subMenu: blank,
  },
  {
    path: '/inventory',
    name: 'Inventory',
    icon: 'store',
    component: AddInventory,
    subMenu: [
      {
        path: '/inventory/add',
        name: 'Add New Inventory',
        icon: 'add',
        component: AddInventory,
      },
      {
        path: '/inventory/update',
        name: 'Update Inventory',
        icon: 'update',
        component: SalesDashboard,
      },
    ],
  },
  {
    path: '/products',
    name: 'Products',
    icon: 'shopping_bag',
    component: CreateProduct,
    subMenu: [
      {
        path: '/product/add',
        name: 'Add New Product',
        icon: 'add',
        component: AddInventory,
      },
      {
        path: '/product/update',
        name: 'Update Product',
        icon: 'update',
        component: SalesDashboard,
      },
    ],
  },
  {
    path: '/reports',
    name: 'Reports',
    icon: 'assignment',
    component: ReportDashboard,
    subMenu: blank,
  },
]
export default routes
