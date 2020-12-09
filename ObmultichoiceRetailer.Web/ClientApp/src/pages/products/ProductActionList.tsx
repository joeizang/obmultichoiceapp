import {
  Divider,
  Icon,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  makeStyles,
  Typography,
} from '@material-ui/core'
import React, { FC, Fragment } from 'react'
import Title from '../../components/Title'

const useStyles = makeStyles((theme) => ({
  targetColor: {
    color: `${theme.palette.secondary}`,
  },
}))

export const ProductActionList: FC = () => {
  const classes = useStyles()

  return (
    <Fragment>
      <Typography variant="h5" align="center">
        <Title>Actions</Title>
      </Typography>
      <Divider />
      <List>
        <ListItem
          button
          onClick={(evt) => {
            // evt.currentTarget.style.backgroundColor = 'teal'
            console.log(classes.targetColor)
            evt.currentTarget.className = classes.targetColor
          }}
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
    </Fragment>
  )
}
