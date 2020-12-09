import { Card, CardContent, CardHeader, Typography } from '@material-ui/core'
import axios from 'axios'
import React, { FC, Fragment, useEffect, useState } from 'react'
import { useForm } from 'react-hook-form'
import { PRODUCTS_URL } from '../../constants'

interface formData {
  product: {}
}

type Product = {
  name: string
  id: number
  price: number
}

type ServerResult = {
  currentPage: number
  currentResponseStatus: string
  error: {}
  list: Product[]
  nextPageLink: string | null
  previousPageLink: string | null
  totalCount: number
}

type CreateSaleForm = {
  formData: formData
}
export const CreateSale: FC<CreateSaleForm> = ({ formData }) => {
  // const { register, handleSubmit } = useForm<formData>()
  // const [serverData, setServerData] = useState<ServerResult>()
  // const [products, setProducts] = useState<Product[]>()
  // const [searchText, setSearchText] = useState('')

  // useEffect(() => {
  //   //get the products so as to populate search field
  //   const process = async () => {
  //     const serverResult = await axios.get(`${PRODUCTS_URL}/forSale`)
  //     const result = await serverResult.data
  //     setServerData(result)
  //     setProducts(serverData?.list.slice())
  //   }
  //   process()
  // }, [])
  // console.log(serverData!)

  // return (
  //   <Fragment>
  //     <div>
  //       <Card>
  //         <CardHeader>Make A Sale</CardHeader>
  //         <CardBody>
  //           <Table size="sm">
  //             <thead>
  //               <tr>
  //                 <th>#</th>
  //                 <th>Product Name</th>
  //                 <th>Quantity</th>
  //                 <th>Product Price</th>
  //               </tr>
  //             </thead>
  //             <tbody>
  //               <tr></tr>
  //               <tr></tr>
  //             </tbody>
  //           </Table>
  //           <hr />
  //           <Form method="GET">
  //             <FormGroup>
  //               <Input as="input" InputRef={register({})} />
  //             </FormGroup>
  //           </Form>
  //         </CardBody>
  //       </Card>
  //     </div>
  //   </Fragment>
  // )
  return (
    <Fragment>
      <Card>
        <CardHeader>
          <Typography variant="h4">Create Sale</Typography>
        </CardHeader>
        <CardContent>
          <Typography variant="h3">Welcome</Typography>
        </CardContent>
      </Card>
    </Fragment>
  )
}
