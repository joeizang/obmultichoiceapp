import Grid from '@material-ui/core/Grid/Grid'
import Paper from '@material-ui/core/Paper/Paper'
import React, { FC, Fragment } from 'react'
import { DashboardProps } from '../../types/supplierTypes'
import Orders from '../../components/Orders'
import { Typography } from '@material-ui/core'
import { ProductActionList } from '.'

export const ProductDashboard: FC<DashboardProps> = ({ fixedHeightPaper }) => {
  return (
    <Fragment>
      <Grid container spacing={3}>
        {/* Chart */}
        <Grid item xs={12} md={8} lg={9}>
          <Paper className={fixedHeightPaper}>
            {/* <CreateProduct /> */}
            <Typography variant="h4" align="center">
              Product Dashboard Cards
            </Typography>
          </Paper>
        </Grid>
        {/* Recent Deposits */}
        <Grid item xs={12} md={4} lg={3}>
          <Paper className={fixedHeightPaper}>
            <ProductActionList />
          </Paper>
        </Grid>
        {/* Recent Orders */}
        <Grid item xs={12}>
          <Paper>
            <Orders />
          </Paper>
        </Grid>
      </Grid>
    </Fragment>
  )
}
