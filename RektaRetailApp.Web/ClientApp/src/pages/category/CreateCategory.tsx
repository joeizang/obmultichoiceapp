import React, { FC } from "react";
import {
  Button,
  Card,
  Form,
  FormGroup,
  FormControl,
  FormLabel,
} from "react-bootstrap";
import { useForm } from "react-hook-form";
import axios from "axios";
interface createCategoryProp {
  name?: string;
  description?: string;
}

const CreateCategory: FC<createCategoryProp> = () => {
  const { register, handleSubmit } = useForm<createCategoryProp>();
  return (
    <>
      <Card>
        <Card.Header color="primary">
          <Card.Title className="text-center font-weight-bold">
            <h3>Create Category</h3>
          </Card.Title>
        </Card.Header>
        <Card.Body>
          <Form
            onSubmit={handleSubmit(async (data) => {
              const temp = await axios.post(
                "https://localhost:5001/api/categories",
                data
              );
            })}
          >
            <FormGroup>
              <FormLabel>Category Name</FormLabel>
              <FormControl
                name="name"
                type="text"
                id="categoryName"
                placeholder="a product or inventory category name..."
                ref={register({
                  required: true,
                  maxLength: 50,
                  minLength: 2,
                })}
              />
            </FormGroup>

            <FormGroup>
              <FormLabel>Category Description</FormLabel>
              <FormControl
                name="description"
                type="textarea"
                id="categoryDescription"
                placeholder=" inventory category description..."
                ref={register({ required: false, maxLength: 200 })}
              />
            </FormGroup>

            <FormGroup>
              <Button type="submit" color="primary" block>
                <span>
                  <b>Create Category</b>
                </span>
              </Button>
            </FormGroup>
          </Form>
        </Card.Body>
      </Card>
    </>
  );
};

export default CreateCategory;
