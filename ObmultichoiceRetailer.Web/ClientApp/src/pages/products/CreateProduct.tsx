import React, { FC, Fragment } from 'react'
import { useForm } from 'react-hook-form'
import {
  FormGroup,
  makeStyles,
  Paper,
  TextField,
  Theme,
} from '@material-ui/core'
import clsx from 'clsx'

interface IProductForm {
  productName: string
}
const useStyles = makeStyles((theme: Theme) => ({
  root: {
    display: 'flex',
  },
  formPaper: {
    padding: theme.spacing(3),
  },
}))
export const CreateProduct: FC = () => {
  const { register, errors } = useForm<IProductForm>()
  const classes = useStyles()
  return (
    <Fragment>
      <Paper elevation={4} className={clsx(classes.root, classes.formPaper)}>
        <form>
          <TextField
            variant="outlined"
            label="Product Name"
            type="text"
            id="productName"
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
        </form>
      </Paper>
    </Fragment>
  )
}
