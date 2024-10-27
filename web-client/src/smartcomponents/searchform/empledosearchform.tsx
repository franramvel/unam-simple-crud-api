"use client";
import { DatePicker } from "@mui/x-date-pickers";
import styles from "./loginform.module.scss";
import { Box, Button, Card, CardActions, CardContent, FormControl, FormLabel, MenuItem, TextField,Select, outlinedInputClasses } from "@mui/material";
import { Controller, useForm } from "react-hook-form";

import styled from "@emotion/styled";
import { useRef, useState } from "react";
import EmpleadoSearchFormModel from "./empleadosearchformmodel";

export interface EmpleadoSearchFormProps{
    model:EmpleadoSearchFormModel
    onSubmit: (model:EmpleadoSearchFormModel) => void;
}

export default function EmpleadoSearchForm(formProps:EmpleadoSearchFormProps) {

  //Este hook nos permite transformar el formulario a un objeto del tipo que le digamos, en este caso, un loginformmodel, 
  //con ese objeto, ya podemos manejar los request a donde queramos
    const { handleSubmit, control, reset,formState,trigger,setValue} = useForm({
      mode: "onChange",
      defaultValues:  {id:0} as EmpleadoSearchFormModel,
      values: formProps.model,
      resetOptions: { keepDefaultValues: true }, //Mantiene como default los valores que declaramos arriba, si no, toma los que se le mandan del props
    });

    const [clickedButton, setClickedButton] = useState<string | null>(null);
    const { isDirty, isValid,errors,touchedFields,dirtyFields} = formState;
  

    const handleFormSubmit = (data:EmpleadoSearchFormModel) => {
      formProps.onSubmit(data);
    };


    


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
                    name={"id"}
                    control={control}
                    rules={{ 
                        required: 'Campo Requerido',
                        validate: {
                            matchPattern: (v) => //match pattern indica que necesitamos que el campo cumpla el patron
                              /^\d+$/.test(v.toString()) || //Este es un regex, que valida que metan un correo valido y luego usa el metodo .test, donde v es el valor de entrada del input
                              "Ingrese un número válido", //Lo que dice es que en caso de que el testeo salga verdadero, entonces deja pasar, si no, muestra el error de introduce un correo valido
                          },
                     }}
                    render={({ field }) => {
                    return (
                        <TextField 
                        id="inp_id"
                        {...field}
                        helperText={formState.errors.id?.message}
                        label="Id" 
                        variant="outlined" 
                            />
                    );
                    }}
                />
            </FormControl>



          </CardContent>
          <CardActions>
            <div className="form-actions-container_dt">
            <Button 
                id="btn_search"
                type="submit"  
                disabled={!isValid}
                variant="contained" 
                color='primary'>Buscar</Button>
            </div>
          </CardActions>
        </Card>
      </Box>
      </>
    );
  }
  
  