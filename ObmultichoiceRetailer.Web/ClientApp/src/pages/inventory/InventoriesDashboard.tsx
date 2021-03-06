import { Typography } from '@material-ui/core'
import Grid from '@material-ui/core/Grid/Grid'
import Paper from '@material-ui/core/Paper/Paper'
import React, { FC, Fragment, useContext } from 'react'
import { ThemeContext } from '../../contexts/ThemeContext'
import { DashboardProps } from '../../types/supplierTypes'

export const InventoriesDashboard: FC<DashboardProps> = () => {
  const classes = useContext(ThemeContext)
  return (
    <Fragment>
      <Grid container spacing={3}>
        {/* Chart */}
        <Grid item xs={12} md={8} lg={9}>
          <Paper className={classes.fixedHeightPaper}>{/* <Chart /> */}</Paper>
        </Grid>
        {/* Recent Deposits */}
        <Grid item xs={12} md={4} lg={3}>
          <Paper className={classes.fixedHeightPaper}>{/* <Deposits /> */}</Paper>
        </Grid>
        {/* Recent Orders */}
        <Grid item xs={12}>
          <Paper className={classes.paper}>
            <Typography variant="h3">
              This is the Inventories Dashboard
            </Typography>
          </Paper>
        </Grid>
      </Grid>
    </Fragment>
  )
}
