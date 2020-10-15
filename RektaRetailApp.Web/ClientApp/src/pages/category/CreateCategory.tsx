import React, { FC } from "react";
import {
  Button,
  Card,
  CardBody,
  CardHeader,
  CardTitle,
  Form,
  FormGroup,
  Input,
  Label,
} from "reactstrap";
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
        <CardHeader color="primary">
          <CardTitle className="text-center font-weight-bold">
            <h3>Create Category</h3>
          </CardTitle>
        </CardHeader>
        <CardBody>
          <Form
            onSubmit={handleSubmit(async (data) => {
              const temp = await axios.post(
                "https://localhost:5001/api/categories",
                data
              );
            })}
          >
            <FormGroup>
              <Label for="categoryName">Category Name</Label>
              <Input
                name="name"
                type="text"
                id="categoryName"
                placeholder="a product or inventory category name..."
                innerRef={register({
                  required: true,
                  maxLength: 50,
                  minLength: 2,
                })}
              />
            </FormGroup>

            <FormGroup>
              <Label for="categoryDescription">Category Description</Label>
              <Input
                name="description"
                type="textarea"
                id="categoryDescription"
                placeholder=" inventory category description..."
                innerRef={register({ required: false, maxLength: 200 })}
              />
            </FormGroup>

            <FormGroup row>
              <Button type="submit" color="primary" block>
                <span>
                  <b>Create Category</b>
                </span>
              </Button>
            </FormGroup>
          </Form>
        </CardBody>
      </Card>
    </>
  );
};

export default CreateCategory;
