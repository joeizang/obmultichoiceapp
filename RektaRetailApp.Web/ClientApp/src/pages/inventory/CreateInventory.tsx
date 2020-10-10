import React, { FC, Fragment, useEffect, useRef, useState } from "react";
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
  Modal,
  ModalBody,
  ModalHeader,
  Row,
  Tooltip,
} from "reactstrap";
import axios from "axios";
import CreateCategory from "../category/CreateCategory";

interface ICategory {
  categoryName: string;
  categoryId: number;
  categoryDescription: string;
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
  const currentCategories = useRef(categories);
  const [tooltipOpen, setTooltipOpen] = useState(false);
  const [modal, setModal] = useState(false);

  const showModal = () => setModal(!modal);
  const toggle = () => setTooltipOpen(!tooltipOpen);

  useEffect(() => {
    let mounted = true;
    const result = async () => {
      const response = await axios.get("https://localhost:5001/api/categories");
      if (mounted) setDropDown(response.data);
    };
    result();

    return () => {
      mounted = false;
    };
  }, []);
  currentCategories.current = dropDown;

  return (
    <Fragment>
      <Card className="shadow-lg p-3 mb-5 bg-white rounded">
        <CardHeader>
          <CardTitle className="text-center">
            <h3>Create Product Inventory</h3>
          </CardTitle>
        </CardHeader>
        <CardBody>
          <Form
            onSubmit={handleSubmit((CreateInventoryProp) => {
              console.log(errors);
            })}
          >
            <FormGroup>
              <Label for="inventoryName">Product Name</Label>
              <Input
                id="inventoryName"
                type="text"
                name="name"
                placeholder="Inventory name..."
                bsSize="lg"
                innerRef={register({
                  maxLength: 50,
                  minLength: 2,
                  required: "You have to give this inventory",
                })}
              />
              {errors.name ? (
                <span className="text-danger">{errors.name.message}</span>
              ) : null}
            </FormGroup>
            <FormGroup>
              <Label for="supplyDate">Supply Date</Label>
              <Input
                id="supplyDate"
                type="date"
                name="supplyDate"
                bsSize="lg"
                innerRef={register({ required: true })}
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
                innerRef={register({
                  maxLength: 50,
                  minLength: 2,
                  required: true,
                })}
              />
            </FormGroup>
            <FormGroup inline>
              <Row form>
                <Col sm="8">
                  <Label>Inventory Category</Label>

                  <Input
                    type="select"
                    name="inventoryCategory"
                    bsSize="lg"
                    innerRef={register({ required: true })}
                  >
                    {currentCategories &&
                      currentCategories.current.map((value) => (
                        <option key={value.categoryId}>
                          {value.categoryName}
                        </option>
                      ))}
                  </Input>
                </Col>
                <Col sm="3" style={{ marginLeft: 45 }}>
                  <Button
                    onClick={showModal}
                    className="font-weight-bold mx-auto"
                    color="dark green"
                    style={{ marginLeft: 0, marginTop: 40 }}
                    id="addCategory"
                  >
                    +
                  </Button>
                  <Tooltip
                    placement="right"
                    isOpen={tooltipOpen}
                    target="addCategory"
                    toggle={toggle}
                  >
                    Add new Category
                  </Tooltip>
                </Col>
              </Row>
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

      <div>
        <Modal isOpen={modal} toggle={toggle} autoFocus>
          <ModalHeader toggle={() => !toggle}>ADD</ModalHeader>
          <ModalBody>
            <CreateCategory />
          </ModalBody>
        </Modal>
      </div>
    </Fragment>
  );
};

export default CreateInventory;
