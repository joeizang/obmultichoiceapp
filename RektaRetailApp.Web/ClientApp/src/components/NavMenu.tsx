import React, { Component } from "react";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavItem,
  NavLink,
} from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { LoginMenu } from "./api-authorization/LoginMenu";
import "./NavMenu.css";

interface IProps {}

interface IState {
  collapsed: boolean;
}

export class NavMenu extends Component<IProps, IState> {
  static displayName = NavMenu.name;

  constructor(props: IProps) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
    };
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed,
    });
  }

  render() {
    return (
      <header>
        <Navbar
          variant="light"
          className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
        >
          <Container>
            <NavbarBrand href="/" className="text-dark">
              Rekta Retail App
            </NavbarBrand>
            <Navbar.Toggle onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse">
              <ul className="navbar-nav flex-grow">
                <LoginMenu></LoginMenu>
                <NavItem>
                  <LinkContainer to="/">
                    <NavLink>Home</NavLink>
                  </LinkContainer>
                </NavItem>
                <NavItem>
                  <LinkContainer to="/inventory">
                    <NavLink className="text-dark">Inventory</NavLink>
                  </LinkContainer>
                </NavItem>
                <NavItem>
                  <LinkContainer to="/sales">
                    <NavLink className="text-dark">Sales</NavLink>
                  </LinkContainer>
                </NavItem>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
