import React, { FC, Fragment, useState } from 'react'
import ListItem from '@material-ui/core/ListItem'
import ListItemIcon from '@material-ui/core/ListItemIcon'
import ListItemText from '@material-ui/core/ListItemText'
import ListSubheader from '@material-ui/core/ListSubheader'
import AssignmentIcon from '@material-ui/icons/Assignment'
import { NavLink, useHistory } from 'react-router-dom'
import routes from '../utils/routes'
import {
  Collapse,
  createStyles,
  Icon,
  List,
  makeStyles,
  Theme,
} from '@material-ui/core'
import { ExpandLess, ExpandMore, StarBorder } from '@material-ui/icons'
import { SubRoute } from '../types/supplierTypes'

interface ISubMenu {
  route: SubRoute
  open: boolean
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      width: '100%',
      maxWidth: 360,
      backgroundColor: theme.palette.background.paper,
    },
    nested: {
      paddingLeft: theme.spacing(4),
    },
  })
)

const SubMenu: FC<ISubMenu> = ({ route, open }) => {
  const classes = useStyles()
  return (
    <Collapse in={open} timeout="auto" unmountOnExit>
      <List component="div" disablePadding>
        <ListItem button className={classes.nested}>
          <ListItemIcon>
            <Icon>{route.icon}</Icon>
          </ListItemIcon>
          <ListItemText primary={route.name} />
        </ListItem>
      </List>
    </Collapse>
  )
}

export const MainListItems: FC = () => {
  return (
    <div>
      {routes.map((route) => (
        //@eslint-ignore
        <Fragment>
          <NavLink
            to={route.path}
            key={route.path}
            style={{
              textDecoration: 'none',
              fontWeight: 900,
              color: 'inherit',
            }}
          >
            <ListItem button key={route.path}>
              <ListItemIcon>
                <Icon>{route.icon}</Icon>
              </ListItemIcon>
              <ListItemText primary={route.name} />
            </ListItem>
          </NavLink>
        </Fragment>
      ))}
    </div>
  )
}

// export const secondaryListItems = (
//   <div>
//     <ListSubheader inset>Saved reports</ListSubheader>
//     <ListItem button>
//       <ListItemIcon>
//         <AssignmentIcon />
//       </ListItemIcon>
//       <ListItemText primary="Current month" />
//     </ListItem>
//     <ListItem button>
//       <ListItemIcon>
//         <AssignmentIcon />
//       </ListItemIcon>
//       <ListItemText primary="Last quarter" />
//     </ListItem>
//     <ListItem button>
//       <ListItemIcon>
//         <AssignmentIcon />
//       </ListItemIcon>
//       <ListItemText primary="Year-end sale" />
//     </ListItem>
//   </div>
// )
