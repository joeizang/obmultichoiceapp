import React, { FC, Fragment, useState } from 'react'
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Divider,
  Icon,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  makeStyles,
  Paper,
  PaperProps,
  Typography,
} from '@material-ui/core'
import Draggable from 'react-draggable'
import Title from '../../components/Title'
import { CreateProduct } from './CreateProduct'

const useStyles = makeStyles((theme) => ({
  targetColor: {
    color: `${theme.palette.secondary}`,
  },
  titleSize: {
    fontSize: '2rem',
    fontWeight: 900
  }
}))

const PaperComponent = (props: PaperProps) => (
  <Draggable handle='#draggable-dialog-title' cancel={'[class*="MuiDialogCOntent-root"]'}>
    <Paper {...props} />
  </Draggable>
)

const ProductActionList: FC = () => {
  const classes = useStyles()
  const [open, setOpen] = useState(false);

  const onClickOpen = () => {
    setOpen(true);
  }

  const onClickClose = () => {
    setOpen(false);
  }

  return (
    <Fragment>
        <Title>
          <Typography className={classes.titleSize} align="center">
            Actions
          </Typography>
        </Title>
      <Divider />
      <List>
        <ListItem
          button
          onClick={onClickOpen}
        >
          <ListItemIcon>
            <Icon>add</Icon>
          </ListItemIcon>
          <ListItemText>Add New Product</ListItemText>
        </ListItem>
        <ListItem button>
          <ListItemIcon>
            <Icon>update</Icon>
          </ListItemIcon>
          <ListItemText>Update Product</ListItemText>
        </ListItem>
        <ListItem button>
          <ListItemIcon>
            <Icon>delete</Icon>
          </ListItemIcon>
          <ListItemText>Remove Product</ListItemText>
        </ListItem>
      </List>
      
      <Dialog
        open={open}
        onClose={onClickClose}
        PaperComponent={PaperComponent}
        maxWidth='sm'
        fullWidth
        aria-labelledby="draggable-dialog-title"
      >
        <DialogTitle style={{ cursor: 'move'}} id="draggable-dialog-title">
          Create New Product
        </DialogTitle>
        <DialogContent>
          <CreateProduct />
        </DialogContent>
        <DialogActions>
          <Button type="submit">
            Create
          </Button>
          <Button onClick={onClickClose} color="default">
            Cancel
          </Button>
        </DialogActions>
      </Dialog>
      
    </Fragment>
  )
}

export default ProductActionList