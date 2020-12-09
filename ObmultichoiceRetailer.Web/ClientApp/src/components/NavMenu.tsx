import { AppBar, Toolbar } from '@material-ui/core'
import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import routes from '../utils/routes'
import { LoginMenu } from './api-authorization/LoginMenu'
import './NavMenu.css'

interface IProps {
  location: { pathname: string | undefined }
}

interface IState {
  collapsed: boolean
  color: string
  dropdownOpen: boolean
  isOpen: boolean
}

export default class NavMenu extends Component<IProps, IState> {
  static displayName = NavMenu.name
  sidebarToggle: React.RefObject<any>

  constructor(props: IProps) {
    super(props)

    this.toggleNavbar = this.toggleNavbar.bind(this)
    this.state = {
      collapsed: true,
      color: '',
      dropdownOpen: false,
      isOpen: false,
    }
    this.toggle = this.toggle.bind(this)
    this.dropdownToggle = this.dropdownToggle.bind(this)
    this.sidebarToggle = React.createRef()
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed,
    })
  }

  toggle() {
    if (this.state.isOpen) {
      this.setState({
        color: 'transparent',
      })
    } else {
      this.setState({
        color: 'dark',
      })
    }
    this.setState({
      isOpen: !this.state.isOpen,
    })
  }
  dropdownToggle(e: any) {
    this.setState({
      dropdownOpen: !this.state.dropdownOpen,
    })
  }
  getBrand() {
    let brandName = 'Default Brand'
    routes.map((prop: any, key: any) => {
      if (window.location.href.indexOf(prop.layout + prop.path) !== -1) {
        brandName = prop.name
      }
      return null
    })
    return brandName
  }
  openSidebar() {
    document.documentElement.classList.toggle('nav-open')
    this.sidebarToggle.current.classList.toggle('toggled')
  }
  // function that adds color dark/transparent to the navbar on resize (this is for the collapse)
  updateColor() {
    if (window.innerWidth < 993 && this.state.isOpen) {
      this.setState({
        color: 'dark',
      })
    } else {
      this.setState({
        color: 'transparent',
      })
    }
  }
  componentDidMount() {
    window.addEventListener('resize', this.updateColor.bind(this))
  }
  componentDidUpdate(e: any) {
    if (
      window.innerWidth < 993 &&
      e.history.location.pathname !== e.location.pathname &&
      document.documentElement.className.indexOf('nav-open') !== -1
    ) {
      document.documentElement.classList.toggle('nav-open')
      this.sidebarToggle.current.classList.toggle('toggled')
    }
  }

  render() {
    return (
      // <LoginMenu></LoginMenu>
      <header>
        <AppBar>
          <Toolbar />
        </AppBar>
      </header>
    )
  }
}
