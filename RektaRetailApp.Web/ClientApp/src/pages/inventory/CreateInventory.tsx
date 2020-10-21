import React, { FC, Fragment, useEffect, useRef, useState } from "react";
import { useForm } from "react-hook-form";
import {
  Button,
  Card,
  Col,
  Form,
  FormControl,
  FormLabel,
  FormGroup,
  Modal,
  ModalBody,
  Row,
  Tooltip,
  OverlayTrigger,
} from "react-bootstrap";
import axios from "axios";
import CreateCategory from "../category/CreateCategory";
import { CATEGORY_URL, INVENTORY_URL } from "../../constants";

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
  const { register, handleSubmit, errors } = useForm<CreateInventoryProp>();
  /**
   * Description of props for component
   * 1. Array of values
   * 2. Multiple (boolean) to make multiselect or not
   * 3. Name of the values
   */
  const [dropDown, setDropDown] = useState([]);
  const currentCategories = useRef(categories);
  const [modal, setModal] = useState(false);
  const showModal = () => setModal(!modal);

  useEffect(() => {
    let mounted = true;
    const result = async () => {
      const response = await axios.get(CATEGORY_URL);
      if (mounted) setDropDown(response.data);
    };
    result();

    return () => {
      mounted = false;
    };
  }, []);
  currentCategories.current = dropDown;
  const [formErrors, setFormErrors] = useState([]);
  return (
    <Fragment>
      <Card className="shadow-lg p-3 mb-5 bg-white rounded">
        <Card.Header>
          <Card.Title className="text-center">
            <h3>Create Product Inventory</h3>
          </Card.Title>
        </Card.Header>
        <Card.Body>
          <Form
            onSubmit={handleSubmit(async (data) => {
              const result = await axios.post(INVENTORY_URL, data);
              if (result.status >= 400) {
                console.log(result.status);
                const temp = JSON.stringify(result.data);
                console.log(temp);
              }
              if (result.status == 200)
                console.log("inventory creation was successful!");
            })}
          >
            <FormGroup>
              <FormLabel>Product Name</FormLabel>
              <FormControl
                id="inventoryName"
                type="text"
                name="name"
                placeholder="Inventory name..."
                ref={register({
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
              <FormLabel>Supply Date</FormLabel>
              <FormControl
                id="supplyDate"
                type="date"
                name="supplyDate"
                ref={register({ required: true })}
              />
              {errors.supplyDate ? (
                <span className="text-danger">{errors.supplyDate.message}</span>
              ) : null}
            </FormGroup>
            <FormGroup>
              <FormLabel>Batch Number</FormLabel>
              <FormControl
                id="batchNumber"
                type="text"
                name="batchNumber"
                placeholder="Inventory BatchNumber..."
                ref={register({
                  maxLength: 50,
                  minLength: 2,
                  required: true,
                })}
              />
              {errors.batchNumber ? (
                <span className="text-danger">
                  {errors.batchNumber.message}
                </span>
              ) : null}
            </FormGroup>
            <FormGroup inlist="true">
              <Row>
                <Col sm="3">
                  <FormLabel>Inventory Category</FormLabel>
                  <FormControl
                    as="select"
                    name="inventoryCategory"
                    ref={register({ required: true })}
                    className="py-2"
                  >
                    {currentCategories &&
                      currentCategories.current.map((value) => (
                        <option key={value.categoryId}>
                          {value.categoryName}
                        </option>
                      ))}
                  </FormControl>
                  {errors.category ? (
                    <span className="text-danger">
                      {errors.category.message}
                    </span>
                  ) : null}
                </Col>
                <Col sm="3" style={{ marginLeft: 15, paddingTop: "1.3em" }}>
                  <OverlayTrigger
                    placement="right"
                    delay={{ show: 100, hide: 700 }}
                    overlay={
                      <Tooltip id="new-category-tooltip">
                        Add new Category
                      </Tooltip>
                    }
                  >
                    <Button
                      onClick={showModal}
                      className="font-weight-bold mx-auto"
                      variant="dark"
                      style={{ marginLeft: 0, marginTop: 10 }}
                      id="addCategory"
                    >
                      +
                    </Button>
                  </OverlayTrigger>
                </Col>
              </Row>
            </FormGroup>
            <FormGroup>
              <FormLabel>Product Quantity</FormLabel>
              <FormControl
                name="productQuantity"
                type="number"
                ref={register({
                  required: "You must enter a number",
                  pattern: /[0-9]/,
                })}
              />
              {errors.quantity ? (
                <span className="text-danger">{errors.quantity.message}</span>
              ) : null}
            </FormGroup>

            <FormGroup>
              <FormLabel>Description</FormLabel>
              <FormControl
                id="inventoryDescription"
                as="textarea"
                name="name"
                placeholder="Inventory Description..."
                ref={register()}
              />
            </FormGroup>
            <FormGroup>
              <Col>
                <Button type="submit" color="primary" block>
                  <span>
                    <b>Create</b>
                  </span>
                </Button>
              </Col>
            </FormGroup>
          </Form>
        </Card.Body>
      </Card>

      <div>
        <Modal show={!showModal} onHide={showModal}>
          <Modal.Header closeButton>
            <Modal.Title>ADD</Modal.Title>
          </Modal.Header>
          <ModalBody>
            <CreateCategory />
          </ModalBody>
        </Modal>
      </div>
    </Fragment>
  );
};

export default CreateInventory;
