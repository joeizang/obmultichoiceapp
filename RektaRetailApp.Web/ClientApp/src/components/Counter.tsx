import React, { FC, useState } from "react";
import {
  Button,
  Card,
  CardBody,
  CardImg,
  CardSubtitle,
  CardText,
  CardTitle,
  Col,
} from "reactstrap";

interface Prop {
  currentCount: number;
}

export const Counter: FC<Prop> = () => {
  const [currentCount, setCurrentCount] = useState(0);

  const incrementCounter = () => setCurrentCount(currentCount + 1);

  return (
    <div>
      <h1>Counter</h1>

      <p>This is a simple example of a React component.</p>

      <p aria-live="polite">
        Current count: <strong>{currentCount}</strong>
      </p>

      <button className="btn btn-primary" onClick={incrementCounter}>
        Increment
      </button>

      <Col md="4" sm="6">
        <Card style={{}}>
          <CardImg
            top
            width="10%"
            src="https://images.unsplash.com/photo-1584535553837-33e69fc4ca4d?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80"
            alt="Card image cap"
          />

          <CardBody>
            <CardTitle>User Card</CardTitle>
            <CardSubtitle>Card subtitle</CardSubtitle>
            <CardText>
              Some quick example text to build on the card title and make up the
              bulk of the card's content.
            </CardText>
            <Button>Button</Button>
          </CardBody>
        </Card>
      </Col>
    </div>
  );
};
