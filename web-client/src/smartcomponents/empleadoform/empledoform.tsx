
import { DatePicker } from "@mui/x-date-pickers";
import styles from "./loginform.module.scss";
import { Box, Button, Card, CardActions, CardContent, FormControl, FormLabel, MenuItem, TextField,Select, outlinedInputClasses } from "@mui/material";
import { Controller, useForm } from "react-hook-form";

import styled from "@emotion/styled";
import { useRef, useState } from "react";
import EmpleadoFormModel from "./empleadoformmodel";

export interface EmpleadoFormProps{
    model:EmpleadoFormModel
    onInsert: (model:EmpleadoFormModel) => void;
    onUpdate?: (model:EmpleadoFormModel) => void;
}

export default function EmpleadoForm(formProps:EmpleadoFormProps) {

  //Este hook nos permite transformar el formulario a un objeto del tipo que le digamos, en este caso, un loginformmodel, 
  //con ese objeto, ya podemos manejar los request a donde queramos
    const { handleSubmit, control, reset,formState,trigger,setValue} = useForm({
      mode: "onChange",
      defaultValues:  {email:"",telefono:"",direccion:""} as EmpleadoFormModel,
      values: formProps.model,
      resetOptions: { keepDefaultValues: true }, //Mantiene como default los valores que declaramos arriba, si no, toma los que se le mandan del props
    });

    const [clickedButton, setClickedButton] = useState<string | null>(null);
    const { isDirty, isValid,errors,touchedFields,dirtyFields} = formState;
  
    const formularioRef = useRef<HTMLFormElement | null>(null);

    const handleFormSubmit = (data:EmpleadoFormModel) => {
        if (clickedButton === "insert") {
            formProps.onInsert(data);
          } else if (clickedButton === "update") {
            formProps.onUpdate!(data);
          }
    };


    

    const determinaValidez = (fieldName: keyof EmpleadoFormModel) => {
      return formState.errors[fieldName] ? 2 : (formState.dirtyFields[fieldName] ? (formState.touchedFields[fieldName] ? 1 : 0) : 0)
    }


    return (
      <>
      <Box sx={{
        '& .MuiTextField-root': { m: 2,  },
      }}
      component="form"
      onSubmit={handleSubmit(handleFormSubmit)}
      >
        <Card >
          <CardContent className="form-container" >

            <FormControl component="fieldset">
                <Controller
                    name={"direccion"}
                    control={control}
                    rules={{ 
                        required: 'Campo Requerido',
                        validate: {
                            matchPattern: (v) => //match pattern indica que necesitamos que el campo cumpla el patron
                              /^.{10,200}$/.test(v) || //Este es un regex, que valida que metan un correo valido y luego usa el metodo .test, donde v es el valor de entrada del input
                              "Ingrese de 10 a 200 caracteres", //Lo que dice es que en caso de que el testeo salga verdadero, entonces deja pasar, si no, muestra el error de introduce un correo valido
                          },
                     }}
                    render={({ field }) => {
                    return (
                        <TextField 
                        {...field}
                        label="Direccion" 
                        variant="outlined" 
                        helperText={formState.errors.direccion?.message}
                        
                            />
                    );
                    }}
                />
            </FormControl>
            <FormControl component="fieldset">
                <Controller
                    name={"email"}
                    control={control}
                    rules={{ 
                        required: 'Campo Requerido',
                        validate: {
                            matchPattern: (v) => //match pattern indica que necesitamos que el campo cumpla el patron
                              /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) || //Este es un regex, que valida que metan un correo valido y luego usa el metodo .test, donde v es el valor de entrada del input
                              "Ingrese un correo válido", //Lo que dice es que en caso de que el testeo salga verdadero, entonces deja pasar, si no, muestra el error de introduce un correo valido
                          },
                     }}
                    render={({ field }) => {
                    return (
                        <TextField 
                        {...field}
                        label="Email" 
                        variant="outlined" 
                        helperText={formState.errors.email?.message}
                            />
                    );
                    }}
                />

            </FormControl>

            <FormControl component="fieldset">
                <Controller
                    name={"telefono"}
                    control={control}
                    rules={{ 
                        required: 'Campo Requerido',
                        validate: {
                            matchPattern: (v) => //match pattern indica que necesitamos que el campo cumpla el patron
                              /^\d+$/.test(v) || //Este es un regex, que valida que metan un correo valido y luego usa el metodo .test, donde v es el valor de entrada del input
                              "Ingrese un número válido", //Lo que dice es que en caso de que el testeo salga verdadero, entonces deja pasar, si no, muestra el error de introduce un correo valido
                          },
                     }}
                    render={({ field }) => {
                    return (
                        <TextField 
                        {...field}
                        helperText={formState.errors.telefono?.message}
                        label="Telefono" 
                        variant="outlined" 
                            />
                    );
                    }}
                />
            </FormControl>



          </CardContent>
          <CardActions>
            <div className="form-actions-container_dt">
                {formProps.model.id == 0 ?
                <Button type="submit"  
                onClick={() => setClickedButton("insert")}
                variant="contained" color='primary'>Crear</Button>
                : <Button 
                onClick={() => setClickedButton("update")}
                type="submit"  variant="contained" color='primary'>Actualizar</Button>}

            </div>
          </CardActions>
        </Card>
      </Box>
      </>
    );
  }
  
  