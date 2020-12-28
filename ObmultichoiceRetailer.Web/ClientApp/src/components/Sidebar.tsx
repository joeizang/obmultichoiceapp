import React, { FC } from 'react'
import ListItem from '@material-ui/core/ListItem'
import ListItemIcon from '@material-ui/core/ListItemIcon'
import ListItemText from '@material-ui/core/ListItemText'
import { NavLink } from 'react-router-dom'
import routes from '../utils/routes'
import {
  Icon,
} from '@material-ui/core'




export const MainListItems: FC = () => {
  return (
    <div>
      {routes.map((route) => (
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
