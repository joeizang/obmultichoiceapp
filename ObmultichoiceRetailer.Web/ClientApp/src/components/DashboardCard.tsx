// @ts-ignore
import React, { FC } from 'react';
import { Card, Col, NavLink, Row } from 'react-bootstrap';
import { ArrowRight } from 'react-bootstrap-icons';
import { LinkContainer } from 'react-router-bootstrap';

interface DashboardCardProp {
  name: string;
  categoryName?: string;
  numberOfProductsInStock?: number;
  supplyDate: Date;
}

const DashboardCard: FC<DashboardCardProp> = ({ name, supplyDate }) => {
  return (
    <>
      <Card style={{ width: '16rem' }} className="shadow-lg">
        <Card.Body>
          <Card.Title>{name}</Card.Title>
        </Card.Body>
        <Card.Footer>
          <Row>
            <Col>
              <small className="text-muted">
                Added : {new Date().toLocaleDateString()}{' '}
              </small>
            </Col>
            <Col>
              <LinkContainer to="/inventory/id">
                <NavLink className="btn btn-primary p-1">
                  Details {''}
                  <ArrowRight className="text-warning" />
                </NavLink>
              </LinkContainer>
            </Col>
          </Row>
        </Card.Footer>
      </Card>
    </>
  );
};

export default DashboardCard;
