import React, { FC, Fragment, useEffect, useRef, useState } from 'react';
import { useForm } from 'react-hook-form';
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
} from 'react-bootstrap';
import axios from 'axios';
import CreateCategory from '../category/CreateCategory';
import { CATEGORY_URL, INVENTORY_URL } from '../../constants';

interface ICategory {
  categoryName: string;
  categoryId: number;
  categoryDescription: string;
}
interface CreateInventoryProp {
  name: string;
  description: string;
  batchNumber: string;
  categoryName: string;
  productQuantity: number;
  supplyDate: Date;
}

interface formData {
  data: CreateInventoryProp;
  categories: ICategory[];
}

const AddInventory: FC<formData> = ({ categories }) => {
  const { register, handleSubmit, errors } = useForm<CreateInventoryProp>();
  // TODO: FIX MODAL FOR CATEGORY CREATION
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

  const doSubmit = (
    data: CreateInventoryProp,
    evt: React.BaseSyntheticEvent<object, any, any> | undefined
  ) => {
    try {
      axios
        .post(`${INVENTORY_URL}`, data, {
          headers: {
            'Content-Type': 'application/json',
          },
        })
        .then((response) => {
          console.log(response);
          if (response.status >= 200 && response.status < 300) {
            // @ts-ignore
            evt.target.reset();
          }
        });
    } catch (error) {
      console.log('error on submitting inventory');
    }
  };
  return (
    <Fragment>
      <Card className="shadow-lg mb-5 bg-white rounded">
        <Card.Header as="h3" className="text-center">
          Create Inventory
        </Card.Header>
        <Card.Body>
          <Form onSubmit={handleSubmit(doSubmit)}>
            <FormGroup>
              <FormLabel>
                Inventory Name <span className="text-danger">*</span>
              </FormLabel>
              <FormControl
                id="inventoryName"
                type="text"
                name="name"
                placeholder="Inventory name..."
                ref={register({
                  required: 'You have to give this inventory a name',
                  pattern: /[a-zA-Z0-9]/,
                })}
              />
              {errors.name ? (
                <span className="text-danger">{errors.name.message}</span>
              ) : null}
            </FormGroup>
            <FormGroup>
              <FormLabel>
                Supply Date <span className="text-danger">*</span>
              </FormLabel>
              <FormControl
                id="supplyDate"
                type="date"
                name="supplyDate"
                ref={register({
                  required: 'Please add a date before submitting!',
                })}
              />
              {errors.supplyDate ? (
                <span className="text-danger">{errors.supplyDate.message}</span>
              ) : null}
            </FormGroup>
            <FormGroup>
              <FormLabel>
                Batch Number <span className="text-danger">*</span>
              </FormLabel>
              <FormControl
                id="batchNumber"
                type="text"
                name="batchNumber"
                placeholder="Inventory BatchNumber..."
                ref={register({
                  required:
                    'A batch number is required for the inventory to be created!',
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
                  <FormLabel>
                    Inventory Category <span className="text-danger">*</span>
                  </FormLabel>
                  <FormControl
                    as="select"
                    name="categoryName"
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
                  {errors.categoryName ? (
                    <span className="text-danger">
                      {errors.categoryName.message}
                    </span>
                  ) : null}
                </Col>
                <Col sm="3" style={{ marginLeft: 15, paddingTop: '1.3em' }}>
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
              <FormLabel>
                Product Quantity <span className="text-danger">*</span>
              </FormLabel>
              <FormControl
                name="productQuantity"
                type="number"
                ref={register({
                  required: 'You must enter a number',
                  pattern: /[0-9]/,
                })}
              />
              {errors.productQuantity ? (
                <span className="text-danger">
                  {errors.productQuantity.message}
                </span>
              ) : null}
            </FormGroup>

            <FormGroup>
              <FormLabel>
                Description <span className="text-danger">*</span>
              </FormLabel>
              <FormControl
                id="inventoryDescription"
                as="textarea"
                name="description"
                placeholder="Inventory Description..."
                ref={register({
                  required: 'Please add a description for this inventory!',
                })}
              />
              {errors.description ? (
                <span className="text-danger">
                  {errors.description.message}
                </span>
              ) : null}
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

export default AddInventory;
