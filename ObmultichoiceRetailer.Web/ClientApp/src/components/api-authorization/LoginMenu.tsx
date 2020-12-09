import React, { Component, Fragment } from 'react';
import { NavItem, NavLink } from 'react-bootstrap';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';
import { LinkContainer } from 'react-router-bootstrap';

interface IProps {}

interface IState {
  isAuthenticated: boolean;
  userName: string | null;
}
export class LoginMenu extends Component<IProps, IState> {
  _subscription: any;
  constructor(props: IProps) {
    super(props);

    this.state = {
      isAuthenticated: false,
      userName: null,
    };
  }

  componentDidMount() {
    this._subscription = authService.subscribe(() => this.populateState());
    this.populateState();
  }

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  async populateState() {
    const [isAuthenticated, user] = await Promise.all([
      authService.isAuthenticated(),
      authService.getUser(),
    ]);
    this.setState({
      isAuthenticated,
      userName: user && user.name,
    });
  }

  render() {
    const { isAuthenticated, userName } = this.state;
    if (!isAuthenticated) {
      const registerPath = `${ApplicationPaths.Register}`;
      const loginPath = `${ApplicationPaths.Login}`;
      return this.anonymousView(registerPath, loginPath);
    } else {
      const profilePath = `${ApplicationPaths.Profile}`;
      const logoutPath = {
        pathname: `${ApplicationPaths.LogOut}`,
        state: { local: true },
      };
      return this.authenticatedView(userName, profilePath, logoutPath);
    }
  }

  authenticatedView(userName: any, profilePath: any, logoutPath: any) {
    return (
      <Fragment>
        <NavItem>
          <LinkContainer to={profilePath}>
            <NavLink className="text-dark">Hello {userName}</NavLink>
          </LinkContainer>
        </NavItem>

        <LinkContainer to={logoutPath}>
          <NavItem>
            <NavLink className="text-dark">Logout</NavLink>
          </NavItem>
        </LinkContainer>
      </Fragment>
    );
  }

  anonymousView(registerPath: any, loginPath: any) {
    return (
      <Fragment>
        <NavItem>
          <LinkContainer to={registerPath}>
            <NavLink className="text-dark">Register</NavLink>
          </LinkContainer>
        </NavItem>
        <NavItem>
          <LinkContainer to={loginPath}>
            <NavLink className="text-dark">Login</NavLink>
          </LinkContainer>
        </NavItem>
      </Fragment>
    );
  }
}
