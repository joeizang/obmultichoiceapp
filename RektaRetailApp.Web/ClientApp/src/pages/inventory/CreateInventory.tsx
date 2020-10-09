import React, { FC, Fragment, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import {
  Button,
  Card,
  CardBody,
  CardHeader,
  CardTitle,
  Col,
  Form,
  FormGroup,
  Input,
  Label,
} from "reactstrap";
import axios from 'axios';

interface ICategory {
  name: string;
  id: number;
  description: string;
}
interface CreateInventoryProp {
  name: string;
  description: string;
  batchNumber: string;
  category: number;
  quantity: number;
  productName: string;
  supplyDate: Date;
  supplier: number;
}

interface formData {
  data: CreateInventoryProp;
  categories: ICategory[];
}


const CreateInventory: FC<formData> = ({ categories }) => {
  const { register, errors, handleSubmit } = useForm<CreateInventoryProp>();
  /**
   * Description of props for component
   * 1. Array of values
   * 2. Multiple (boolean) to make multiselect or not
   * 3. Name of the values
   */
  const [dropDown, setDropDown] = useState([]);

  useEffect(() => {
    let mounted = true;
    const result = async () => {
      const response = await axios.get('https://localhost:5001/api/categories');
      if (mounted)
        setDropDown(response.data);
      categories = dropDown;
    };
    result();

    return () => {
      mounted = false;
    };
  }, []);
  console.log(dropDown);
  
  return (
    <Fragment>
      <Card className="shadow-lg p-3 mb-5 bg-white rounded">
        <CardHeader>
          <CardTitle className="text-center">
            <h3>Create Product Inventory</h3>
          </CardTitle>
        </CardHeader>
        <CardBody>
          <Form onSubmit={handleSubmit((CreateInventoryProp) => {
            console.log(errors);
          })}>
            <FormGroup>
              <Label for="inventoryName">Product Name</Label>
              <Input
                id="inventoryName"
                type="text"
                name="name"
                placeholder="Inventory name..."
                bsSize="lg"
                innerRef={register({ maxLength: 50, minLength: 2, required: "You have to give this inventory"})}
              />
              {errors.name ? <span className="text-danger">{errors.name.message}</span> : null}
            </FormGroup>
            <FormGroup>
              <Label for="supplyDate">Supply Date</Label>
              <Input
                id="supplyDate"
                type="date"
                name="supplyDate"
                bsSize="lg"
                innerRef={register({ required: true})}
              />
            </FormGroup>
            <FormGroup>
              <Label for="batchNumber">Batch Number</Label>
              <Input
                id="batchNumber"
                type="text"
                name="batchNumber"
                placeholder="Inventory BatchNumber..."
                bsSize="lg"
                innerRef={register({ maxLength: 50, minLength: 2, required: true})}
              />
            </FormGroup>
            <FormGroup>
              <Label>Inventory Category</Label>
              
              <Input type="select" name="inventoryCategory" bsSize="lg" innerRef={register({ required: true})}>
                {categories &&
                  categories.map((value) => <option key={value.id}>{value.name}</option>)}
              </Input>
            </FormGroup>
            <FormGroup>
              <Label for="quantity">Product Quantity</Label>
              <Input name="productQuantity" type="number" bsSize="lg" />
            </FormGroup>
            <FormGroup>
              <Label for="inventoryDescription">Description</Label>
              <Input
                id="inventoryDescription"
                type="textarea"
                name="name"
                placeholder="Inventory Description..."
                bsSize="lg"
                innerRef={register()}
              />
            </FormGroup>
            <FormGroup row>
              <Col>
                <Button type="submit" color="primary" block>
                  <span>
                    <b>Create</b>
                  </span>
                </Button>
              </Col>
            </FormGroup>
          </Form>
        </CardBody>
      </Card>
    </Fragment>
  );
};

export default CreateInventory;
