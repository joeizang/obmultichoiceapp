import { ICreateSupplier } from '../../types/supplierTypes';
import React from 'react';
import axios from 'axios';
import { SUPPLIERS_URL } from '../../constants';

export const createSupplier = (
  data: ICreateSupplier,
  evt: React.BaseSyntheticEvent<object, any, any> | undefined
): string => {
  let location = '';
  axios
    .post(SUPPLIERS_URL, data, {
      headers: {
        'Content-Type': 'application/json',
      },
    })
    .then((response) => {
      if (response.status >= 200 && response.status < 300) {
        location = response.headers.location;
        // @ts-ignore
        evt.target.reset();
      }
    })
    .catch((reason) => {
      return reason;
    });
  return location;
};
