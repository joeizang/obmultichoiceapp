import { Box, Grid, Paper, Typography } from '@material-ui/core'
import React, { FC, Fragment } from 'react'
import { DashboardProps } from '../../types/supplierTypes'

export const ReportDashboard: FC<DashboardProps> = ({
  fixedHeightPaper,
  classes,
}) => {
  return (
    <Fragment>
      <Grid container spacing={3}>
        <Grid item xs={12} md={8} lg={9}>
          <Paper className={fixedHeightPaper}>
            <Box>
              <Typography variant="h4">Report Dashboard</Typography>
            </Box>
          </Paper>
        </Grid>
      </Grid>
    </Fragment>
  )
}
