export interface ICreateSupplier {
  name: string
  phoneNumber: string
  description: string
}

export type Routes = {
  path: string
  name: string
  icon: string
  component: React.FC<any>
  subMenu: SubRoute[]
}[]

export type SubRoute = {
  path: string
  name: string
  icon: string
  component: React.FC<any>
}

export type DashboardProps = {
  fixedHeightPaper: string
  classes: Record<
    | 'root'
    | 'content'
    | 'toolbar'
    | 'toolbarIcon'
    | 'appBar'
    | 'appBarShift'
    | 'menuButton'
    | 'menuButtonHidden'
    | 'title'
    | 'drawerPaper'
    | 'drawerPaperClose'
    | 'appBarSpacer'
    | 'container'
    | 'paper'
    | 'fixedHeight',
    string
  >
}
