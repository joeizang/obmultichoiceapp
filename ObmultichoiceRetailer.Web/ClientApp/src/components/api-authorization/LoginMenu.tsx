import React, { Component, Fragment } from 'react';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';
import { Link } from 'react-router-dom';

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
        <div>
          <Link to={profilePath}>
            <span className="text-dark">Hello {userName}</span>
          </Link>
        </div>

        <Link to={logoutPath}>
          <div>
            <span className="text-dark">Logout</span>
          </div>
        </Link>
      </Fragment>
    );
  }

  anonymousView(registerPath: any, loginPath: any) {
    return (
      <Fragment>
        <div>
          <Link to={registerPath}>
            <span className="text-dark">Register</span>
          </Link>
        </div>
        <div>
          <Link to={loginPath}>
            <span className="text-dark">Login</span>
          </Link>
        </div>
      </Fragment>
    );
  }
}
