import React, { FC, Fragment, useState } from 'react'
import { useForm } from 'react-hook-form'
import {
  Button,
  FormGroup,
  FormHelperText,
  Grid,
  makeStyles,
  MenuItem,
  Paper,
  Select,
  TextField,
  Theme,
  Typography,
} from '@material-ui/core'
import { useMutation, useQuery } from 'react-query'
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
  inventory: string
}
type UnitMeasure = {
  enumName: string
}

type Inventory = {
  name: string,
  id: number
}
const useStyles = makeStyles((theme: Theme) => ({
  root: {
    display: 'block',
    marginLeft: 'auto',
    padding: theme.spacing(3),
    margin: theme.spacing(3),
    position: 'relative',
    width: '90%'
  },
  formPaper: {
    
    position: 'relative',
    width: '80%'
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
  const { register, handleSubmit, errors, reset } = useForm<IProductForm>()
  const classes = useStyles()
  const { data } = useQuery('GetUnitMeasure',GetUnitsMeasure)
  const inventories = useQuery('GetInventories', () => axios.get('https://localhost:5004/api/inventories')
                      .then((response) => response.data))
  console.log(inventories)
  const [enumValue, setEnumValue] = useState('')
  const [inventoryValue, setInventoryValue] = useState('')

  const [processAnime, showProcessingAnime] = useState(false)

  const submitForm = async (product: IProductForm) => {
    showProcessingAnime(true)
    const result = await axios.post('/api/products', product)
    if(result.data !== null || result.data !== undefined && result.status === 200) {
      console.log(await result.data)
      reset()
    }
    console.log(result.statusText)
  }
  return (
    <Fragment>
      {/* <Paper className={classes.formPaper}> */}
      <form className={classes.root} onSubmit={handleSubmit(submitForm)}>
        <Grid container direction="row" justify="flex-start" alignItems="flex-start" spacing={1}>
          <Grid item xs={12}>
            <TextField
              variant="outlined"
              fullWidth
              label="Supply Date"
              type="text"
              id="supplyDate"
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
          </Grid>
          <Grid item xs={12}>
          <TextField
            variant="outlined"
            fullWidth
            label="Product Name"
            type="text"
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
          </Grid>
          <Grid item xs={12}>
          <TextField
            variant="outlined"
            fullWidth
            label="Cost Price"
            type="number"
            id="costPrice"
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
          </Grid>
          <Grid item xs={12}>
          <TextField
            variant="outlined"
            fullWidth
            label="Retail Price"
            type="number"
            id="retailPrice"
            name="retailPrice"
            className={classes.inputStyle}
            inputRef={register({
              required: 'Please enter a retail or selling price for this product!',
              maxLength: 14,
            })}
          />
          <FormGroup>
            {errors.retailPrice && (
              <span style={{ color: 'red' }}>{errors.retailPrice.message}</span>
            )}
          </FormGroup>
          </Grid>
          <Grid item xs={12}>
          <TextField
            variant="outlined"
            fullWidth
            label="Quantity"
            type="number"
            id="quantity"
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
          </Grid>
          <Grid xs={12} item>
          <TextField
            variant="outlined"
            fullWidth
            label="Brand"
            type="text"
            id="brand"
            name="brand"
            className={classes.inputStyle}
            inputRef={register({
              maxLength: 100,
              minLength: 1,
            })}
          />
          <FormGroup>
            {errors.brand && (
              <span style={{ color: 'red' }}>{errors.brand.message}</span>
            )}
          </FormGroup>
          </Grid>
          <Grid item xs={12}>
          <Select
            variant="outlined"
            fullWidth
            label="Unit Measure"
            value={enumValue}
            onChange={(event: any) => {
              setEnumValue(event.target.value)
              console.log(event.target.value)
            }}
            id="unitMeasure"
            name="unitMeasure"
            className={classes.inputStyle}
            inputRef={register({ required: true })}
          >
            {data?.data.map((option: UnitMeasure, index: number) => (
              <MenuItem key={index} value={option.enumName}>
                {option.enumName}
              </MenuItem>
            ))}
          </Select>
          <FormHelperText>
            Please pick any unit of measurement for product
          </FormHelperText>
          <FormGroup>
            {errors.unitMeasure && (
              <span style={{ color: 'red' }}>{errors.unitMeasure.message}</span>
            )}
          </FormGroup>
          </Grid>
          <Grid item xs={12}>
          <Select
            variant="outlined"
            fullWidth
            label="Inventory"
            value={inventoryValue}
            onChange={(event: any) => {
              setInventoryValue(event.target.value)
              console.log(event.target.value)
            }}
            id="inventoryId"
            name="inventoryId"
            className={classes.inputStyle}
            inputRef={register({ required: true })}
          >
            {inventories.data.map((option: Inventory) => (
              <MenuItem key={option.id} value={option.name}>
                {option.name}
              </MenuItem>
            ))}
          </Select>
          <FormHelperText>
            Please pick an inventory for product
          </FormHelperText>
          <FormGroup>
            {errors.inventory && (
              <span style={{ color: 'red' }}>{errors.inventory.message}</span>
            )}
          </FormGroup>
          </Grid>
          <Grid item xs={12}>
          <TextField
            variant="outlined"
            fullWidth
            label="Comment"
            type="textarea"
            id="brand"
            name="brand"
            className={classes.inputStyle}
            inputRef={register({
              maxLength: 100,
              minLength: 1,
            })}
          />
          <FormGroup>
            {errors.brand && (
              <span style={{ color: 'red' }}>{errors.brand.message}</span>
            )}
          </FormGroup>
          </Grid>
          <Grid item xs={12}>
            <Button type="submit" fullWidth color="primary" variant="contained">
              <Typography variant="button">
                Create
              </Typography>
            </Button>
          </Grid>
          <Grid item xs={3} justify="center" alignItems="center">
              {processAnime ? <p>Processing...</p> : null}
          </Grid>
        </Grid>
      </form>
      {/* </Paper> */}
    </Fragment>
  )
}
