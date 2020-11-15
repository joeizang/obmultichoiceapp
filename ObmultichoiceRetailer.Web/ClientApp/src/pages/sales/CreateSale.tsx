import React, { FC, Fragment } from 'react';
import { Card } from 'react-bootstrap';

export const CreateSale: FC = () => {
  return (
    <Fragment>
      <Card className="shadow-sm mb-5 bg-white rounded">
        <Card.Header>Products In This Sale</Card.Header>
      </Card>
    </Fragment>
  );
};
