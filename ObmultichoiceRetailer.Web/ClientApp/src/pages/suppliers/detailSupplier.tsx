import React, { FC, Fragment, useEffect, useState } from 'react';
import { Card, ListGroup } from 'react-bootstrap';
import axios from 'axios';
import { BASE_URL, COLOURS } from '../../constants';
import { useLocation } from 'react-router-dom';

interface IProductSupplied {
  id: number;
  name: string;
  quantity: number;
}
interface ISupplierDetail {
  name: string;
  phoneNumber: string;
  description: string;
  supplierId: number;
  suppliedProducts: IProductSupplied[];
}

interface IResponse {
  data: ISupplierDetail;
  errors: string[];
  currentResponseStatus: string;
}

export const DetailSupplier: FC<IResponse> = () => {
  const [supplier, setSupplier] = useState<ISupplierDetail>();
  //const { id } = useParams<>();
  const location = useLocation();
  console.log(location);

  useEffect(() => {
    //const url = `${SUPPLIERS_URL}/${id}`;
    async function getSupplier() {
      const result = (await axios.get(`${BASE_URL}${location.pathname}`)).data
        .data;
      setSupplier(result);
    }
    getSupplier();
  }, []);
  console.log(supplier);
  return (
    <Fragment>
      <Card className="shadow-sm mb-5 bg-white rounded">
        {/* <Card.Header>{this.state.supplier.supplierName}</Card.Header> */}
        <Card.Header
          as="h2"
          style={{ backgroundColor: COLOURS.primary, color: 'white' }}
        >
          <Card.Title className="text-center">Supplier - Details</Card.Title>
        </Card.Header>
        <Card.Body>
          <Card.Subtitle>{supplier?.name && supplier?.name}</Card.Subtitle>
          {supplier?.suppliedProducts === undefined ||
          supplier?.suppliedProducts?.length < 1
            ? null
            : supplier?.suppliedProducts.map((product) => (
                <ListGroup key={product.id}>
                  <ListGroup.Item>
                    <Card.Text>Product Name: {product.name}</Card.Text>
                    <Card.Text>Quantity: {product.quantity}</Card.Text>
                  </ListGroup.Item>
                </ListGroup>
              ))}
        </Card.Body>
      </Card>
    </Fragment>
  );
};
