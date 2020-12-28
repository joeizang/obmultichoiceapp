import React, { FC, Fragment, useState } from 'react'
import { useForm } from 'react-hook-form'
import {
  FormGroup,
  FormHelperText,
  makeStyles,
  MenuItem,
  Select,
  TextField,
  Theme,
} from '@material-ui/core'
import { useQuery } from 'react-query'
import axios from "axios";
interface IProductForm {
  productName: string
  retailPrice: number
  quantity: number
  costPrice: number
  brand?: string
  comments?: string
  supplyDate: string
  unitMeasure: string
}
type UnitMeasure = {
  enumName: string
}
const useStyles = makeStyles((theme: Theme) => ({
  root: {
    display: 'flex'

  },
  formPaper: {
    padding: theme.spacing(3),
  },
  inputStyle: {
    paddingBottom: 15,
  }
}))

const GetUnitsMeasure = async () => {
    const result = await axios('https://localhost:5004/api/unitsmeasure',{ method: 'get'})
    return result;
}
export const CreateProduct: FC = () => {
  const { register, errors } = useForm<IProductForm>()
  const classes = useStyles()
  const { data } = useQuery('GetUnitMeasure',GetUnitsMeasure);
  const [enumValue, setEnumValue] = useState('')
  return (
    <Fragment>
        <form>
        <TextField
            variant="outlined"
            label="Supply Date"
            type="text"
            id="supplyDate"
            fullWidth
            name="supplyDate"
            placeholder="dd/mm/yyyy"
            className={classes.inputStyle}
            inputRef={register({
              required: 'You must provide a valid date',
              maxLength: 50,
            })}
          />
          <FormGroup>
            {errors.productName && (
              <span style={{ color: 'red' }}>{errors.productName.message}</span>
            )}
          </FormGroup>
          <TextField
            variant="outlined"
            label="Product Name"
            type="text"
            fullWidth
            id="productName"
            name="name"
            className={classes.inputStyle}
            inputRef={register({
              required: 'Please provide a name for the product!',
              maxLength: 50,
            })}
          />
          <FormGroup>
            {errors.productName && (
              <span style={{ color: 'red' }}>{errors.productName.message}</span>
            )}
          </FormGroup>
          <TextField
            variant="outlined"
            label="Cost Price"
            type="number"
            id="costPrice"
            fullWidth
            name="costPrice"
            className={classes.inputStyle}
            inputRef={register({
              required: 'Every product must have a cost price!',
              maxLength: 14,
            })}
          />
          <FormGroup>
            {errors.productName && (
              <span style={{ color: 'red' }}>{errors.productName.message}</span>
            )}
          </FormGroup>
          <TextField
            variant="outlined"
            label="Retail Price"
            type="number"
            id="retailPrice"
            fullWidth
            name="retailPrice"
            className={classes.inputStyle}
            inputRef={register({
              required: 'Please a retail or selling price for this product!',
              maxLength: 14,
            })}
          />
          <FormGroup>
            {errors.retailPrice && (
              <span style={{ color: 'red' }}>{errors.retailPrice.message}</span>
            )}
          </FormGroup>
          <TextField
            variant="outlined"
            label="Quantity"
            type="number"
            id="quantity"
            fullWidth
            name="quantity"
            className={classes.inputStyle}
            inputRef={register({
              required: 'Please indicate the quantity of the product you have!',
              maxLength: 14,
            })}
          />
          <FormGroup>
            {errors.quantity && (
              <span style={{ color: 'red' }}>{errors.quantity.message}</span>
            )}
          </FormGroup>
          <TextField
            variant="outlined"
            label="Brand"
            type="text"
            id="brand"
            fullWidth
            name="brand"
            className={classes.inputStyle}
            inputRef={register({
              maxLength: 100,
              minLength: 1
            })}
          />
          <FormGroup>
            {errors.brand && (
              <span style={{ color: 'red' }}>{errors.brand.message}</span>
            )}
          </FormGroup>
          <Select
            variant="outlined"
            label="Unit Measure"
            value={enumValue}
            onChange={(event: any)=>{
              setEnumValue(event.target.value)
              console.log(event.target.value)
            }}
            id="unitMeasure"
            fullWidth
            name="unitMeasure"
            className={classes.inputStyle}
            inputRef={register({ required: true })}
          >
            {data?.data.map((option: UnitMeasure,index: number) => (
              <MenuItem key={index} value={option.enumName}>
                {option.enumName}
              </MenuItem>))
            }
          </Select>
          <FormHelperText>Please pick any unit of measurement for product</FormHelperText>
          <FormGroup>
            {errors.unitMeasure && (
              <span style={{ color: 'red' }}>{errors.unitMeasure.message}</span>
            )}
          </FormGroup>
          <TextField
            variant="outlined"
            label="Comment"
            type="textarea"
            id="brand"
            fullWidth
            name="brand"
            className={classes.inputStyle}
            inputRef={register({
              maxLength: 100,
              minLength: 1
            })}
          />
          <FormGroup>
            {errors.brand && (
              <span style={{ color: 'red' }}>{errors.brand.message}</span>
            )}
          </FormGroup>
        </form>
      
    </Fragment>
  )
}
