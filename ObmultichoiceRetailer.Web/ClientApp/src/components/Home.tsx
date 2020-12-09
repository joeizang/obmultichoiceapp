import React, { Component, Fragment } from 'react'
import DashboardCard from './DashboardCard'

export class Home extends Component {
  static displayName = Home.name

  render() {
    return (
      <Fragment>
        <DashboardCard name="Testing Dash Card" supplyDate={new Date()} />
        <DashboardCard name="Testing Dash Card" supplyDate={new Date()} />
      </Fragment>
    )
  }
}
