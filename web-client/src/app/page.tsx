"use client";
import { Typography, Card, IconButton, Dialog, DialogTitle, DialogContent, Tabs, Tab, Box } from "@mui/material";
import React from "react";
import CloseIcon from "@mui/icons-material/Close";
import useIndex from "./useindex";
import TabPanel from "@/components/tabpanel/tabpanel";
import EmpleadoFormModel from "@/smartcomponents/empleadoform/empleadoformmodel";
import EmpleadoForm from "@/smartcomponents/empleadoform/empledoform";
import EmpleadoSearchForm from "@/smartcomponents/searchform/empledosearchform";
import EmpleadoSearchFormModel from "@/smartcomponents/searchform/empleadosearchformmodel";

// Font files can be colocated inside of `pages`

export default function Home() {

  const [value, setValue] = React.useState(0);

  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  const {
  } = useIndex();



  return (
    <>
    <Card>
      <Box>
        <Tabs
        value={value}
        onChange={handleChange}
        textColor="primary"
        indicatorColor="primary"
        aria-label="primary tabs example"
        >
        <Tab value={0} label="Buscar" />
        <Tab value={1} label="Crear" />
      </Tabs>
      </Box>
      <TabPanel value={value} index={0} >
        <EmpleadoSearchForm model={new EmpleadoSearchFormModel} onSubmit={()=>[]}/>
      </TabPanel>
      <TabPanel value={value} index={1} >
        <EmpleadoForm model={new EmpleadoFormModel} onInsert={()=>[]}/>
      </TabPanel> 
    </Card>

    </>
  );
}

