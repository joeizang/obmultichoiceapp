import React, { Fragment } from 'react';
import { Card, Form, FormControl, FormGroup } from 'react-bootstrap';
import { COLOURS } from '../../constants';
import { useForm } from 'react-hook-form';

interface IProductForm {
  productName: string;
}

export function CreateProduct() {
  const { register, handleSubmit, errors } = useForm<IProductForm>();

  return (
    <Fragment>
      <Card className="shadow-sm mb-5 bg-white rounded">
        <Card.Header
          as="h3"
          style={{ backgroundColor: COLOURS.primary, color: 'whitesmoke' }}
        >
          Add a New Product
        </Card.Header>
        <Card.Body>
          <Form>
            <FormGroup>
              <FormControl
                type="text"
                id="productName"
                ref={register({
                  required: 'Please provide a name for the product!',
                  maxLength: 50,
                })}
              />
              {errors.productName && (
                <span style={{ color: 'red' }}>
                  {errors.productName.message}
                </span>
              )}
            </FormGroup>
          </Form>
        </Card.Body>
      </Card>
    </Fragment>
  );
}
