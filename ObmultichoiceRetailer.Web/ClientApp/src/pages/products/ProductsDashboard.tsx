import Grid from '@material-ui/core/Grid/Grid'
import Paper from '@material-ui/core/Paper/Paper'
import React, { FC, Fragment, useEffect, useState } from 'react'
import { DashboardProps } from '../../types/supplierTypes'
import Orders from '../../components/Orders'
import { makeStyles, Theme, Typography } from '@material-ui/core'
import ProductActionList from './ProductActionList'
import { CreateProduct } from './CreateProduct'
import Title from '../../components/Title'

const useStyles = makeStyles((theme: Theme) => ({
  typographyPadding: {
    paddingBottom: 15,
      fontSize: '2rem',
      fontWeight: 900
  }
}))

export const ProductDashboard: FC<DashboardProps> = ({ fixedHeightPaper }) => {
  const [showAddProduct, setShowAddProduct] = useState(false)
  const [showUpdateProduct, setUpdateProduct] = useState(false)

  const toggleAddProduct = () => {
    setShowAddProduct(!showAddProduct)
    setUpdateProduct(false)
  }
  const toggleUpdateProduct = () => {
    setUpdateProduct(!showUpdateProduct)
    setShowAddProduct(false)
  }
  const classes = useStyles()


  return (
    <Fragment>
      <Grid container spacing={3}>
        {/* Chart */}
        <Grid item xs={12} md={8} lg={9}>
          <Paper className={fixedHeightPaper}>
            {/* <CreateProduct /> */}
            <Title>
              <Typography className={classes.typographyPadding} align="center">
                {!showAddProduct && !showUpdateProduct ? <span>Product Dashboard</span> : null}
                {showAddProduct && !showUpdateProduct ? <span>Create Product</span> : null}
                {showUpdateProduct && !showAddProduct ? <span>Update Product</span> : null}
              </Typography>
            </Title>
            {showAddProduct === true ? <CreateProduct /> : null}
            {showUpdateProduct === true ? <h4>Coming Soon!</h4> : null}
          </Paper>
        </Grid>
        {/* Recent Deposits */}
        <Grid item xs={12} md={4} lg={3}>
          <Paper className={fixedHeightPaper}>
            <ProductActionList toggleAddProduct={toggleAddProduct} toggleUpdateProduct={toggleUpdateProduct}/>
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
