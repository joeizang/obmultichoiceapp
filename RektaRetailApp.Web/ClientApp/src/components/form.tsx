import React from 'react';
import { Form } from 'react-bootstrap';

/**
 * Description of Props that AppForm should take
 * 1. type or interface which expresses the number of properties and fields that should be available in the form.
 * the members of the types should be objects that contain error message, validation rules and other metadata to build
 * form elements with.
 * 2. any interface or type members that are arrays or enums will be made into dropdowns
 * 3. all other types will become text fields and their types will determine the types of their generated input elements.
 * 4. it should provide the title of the form and the text that goes on the button of the form.
 * 5. the method to be used in the form eg: POST, GET, PUT etc should be string
 * 6. it should have an action (a method that returns void) which should have the logic to handle form submission.
 * 7. the submission handling method should take a react event and the interface that expresses form fields in point 1.
 */

function AppForm() {
  return (
    <React.Fragment>
      <Form></Form>
    </React.Fragment>
  );
}

export default AppForm;
