// @ts-ignore
import { Card, CardHeader, Button, CardActionArea } from '@material-ui/core'
import { ArrowRight } from '@material-ui/icons'
import React, { FC } from 'react'

interface DashboardCardProp {
  name: string
  categoryName?: string
  numberOfProductsInStock?: number
  supplyDate: Date
}

const DashboardCard: FC<DashboardCardProp> = ({ name, supplyDate }) => {
  return (
    <>
      <Card style={{ width: '16rem', marginBottom: 20 }} className="shadow-lg">
        <CardHeader title={name} />
        <CardActionArea>
          <small className="text-muted">
            Added : {new Date().toLocaleDateString()}{' '}
          </small>
          <Button color="primary">
            Details <ArrowRight color="action" />
          </Button>
        </CardActionArea>
      </Card>
    </>
  )
}

export default DashboardCard
