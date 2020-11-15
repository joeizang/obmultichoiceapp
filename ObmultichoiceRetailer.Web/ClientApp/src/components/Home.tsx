import React, { Component } from 'react';
import { CardDeck } from 'react-bootstrap';
import DashboardCard from './DashboardCard';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <CardDeck>
          <DashboardCard name="Testing Dash Card" supplyDate={new Date()} />
          <DashboardCard name="Testing Dash Card" supplyDate={new Date()} />
        </CardDeck>
      </div>
    );
  }
}
