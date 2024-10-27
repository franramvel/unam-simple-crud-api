import { use, useContext, useEffect, useState } from "react";
import { redirect } from "next/navigation";
import { useRouter } from "next/navigation";
import { useScreenDetector } from "./hooks/usescreendetector";
import { createEmpleado, deleteEmpleado, getEmpleado, updateEmpleado } from "@/services/empleadoservices";
import EmpleadoSearchFormModel from "@/smartcomponents/searchform/empleadosearchformmodel";
import EmpleadoFormModel from "@/smartcomponents/empleadoform/empleadoformmodel";
import React from "react";





export default function useIndex() {

  const [model,setModel] = useState(new EmpleadoFormModel);
  const [tabValue, setTabValue] = React.useState(0);

  const handleChangeTabValue = (event: React.SyntheticEvent, newValue: number) => {
    setTabValue(newValue);
  };


  const handleGet = (model: EmpleadoSearchFormModel) => {
    const onSuccess = async (response:EmpleadoFormModel) => {
      console.log(response)
      setModel(response)
      setTabValue(1)
    };

    const onError = (error: any) => {

    alert(error.response.data.toString())
    }
    try {
      getEmpleado(model.id,onSuccess,onError);
    } catch (error: any) {
      alert("Ha ocurrido un problema al intentar contactar al servidor, por favor, reintente más tarde");
    }
  }


  const handleInsert = (model: EmpleadoFormModel) => {
    const onSuccess = async (response:EmpleadoFormModel) => {
      alert("Empleado creado correctamente")
      setModel(new EmpleadoFormModel)
      setTabValue(0)
      console.log(response)
    };

    const onError = (error: any) => {

    alert(error.response.data.toString())
    }
    try {
      createEmpleado(model,onSuccess,onError);
    } catch (error: any) {
      alert("Ha ocurrido un problema al intentar contactar al servidor, por favor, reintente más tarde");
    }
  }

  const handleUpdate = (model: EmpleadoFormModel) => {
    const onSuccess = async (response:EmpleadoFormModel) => {
      alert("Empleado actualizado correctamente")
      setModel(new EmpleadoFormModel)
      setTabValue(0)
      console.log(response)
    };

    const onError = (error: any) => {

    alert(error.response.data.toString())
    }
    try {
      updateEmpleado(model,onSuccess,onError);
    } catch (error: any) {
      alert("Ha ocurrido un problema al intentar contactar al servidor, por favor, reintente más tarde");
    }
  }

  const handleDelete = (id: number) => {
    const onSuccess = async (response:string) => {
      console.log(response)
      alert(response)
      setModel(new EmpleadoFormModel)
      setTabValue(1)
    };

    const onError = (error: any) => {

    alert(error.response.data.toString())
    }
    try {
      deleteEmpleado(id,onSuccess,onError);
    } catch (error: any) {
      alert("Ha ocurrido un problema al intentar contactar al servidor, por favor, reintente más tarde");
    }
  }


  const state = {
    handleGet,
    handleInsert,
    handleUpdate,
    handleDelete,
    handleChangeTabValue,
    tabValue,
    model
  }



  return state;

}



