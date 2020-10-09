import React, { FC } from "react";
import { Input } from "reactstrap";
import axios from "axios";

interface DropDownProp {
  values: any[];
  name: string;
  bsSize: "lg" | "sm";
  multiple: boolean;
}
const DropDown: FC<DropDownProp> = ({
  values,
  name,
  multiple,
  bsSize,
}) => {
    
  return (
    <>
      
    </>
  );
};

export default DropDown;
