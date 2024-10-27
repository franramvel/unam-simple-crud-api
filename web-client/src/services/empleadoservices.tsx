import { axiosInstance } from "@/interceptors";
import EmpleadoFormModel from "@/smartcomponents/empleadoform/empleadoformmodel";

let controllerName = "Empleado/"
export async function createEmpleado(model:EmpleadoFormModel,onSuccess:(responseData:any)=>void,onError:(error:any)=>void) {
    try {
        
        const response = await axiosInstance.post(`${controllerName}`,model);
        const responseData: any = response.data;
        
        if (response.status !== 200) {
            
            throw new Error("Servidor remoto:" + responseData.message);
        }
        
        onSuccess(responseData);
    } catch (error:any) {
        onError(error)
    }
}

export async function updateEmpleado(model:EmpleadoFormModel,onSuccess:(responseData:any)=>void,onError:(error:any)=>void) {
    try {
        
        const response = await axiosInstance.put(`${controllerName}`,model);
        const responseData: any = response.data;
        
        if (response.status !== 200) {
            
            throw new Error("Servidor remoto:" + responseData.message);
        }
        
        onSuccess(responseData);
    } catch (error:any) {
        onError(error)
    }
}


export async function getEmpleado(id:number,onSuccess:(responseData:any)=>void,onError:(error:any)=>void) {
    try {
        
        const response = await axiosInstance.get(`${controllerName}${id}`);
        const responseData: any = response.data;
        
        if (response.status !== 200) {
            
            throw new Error("Servidor remoto:" + responseData.message);
        }
        
        onSuccess(responseData);
    } catch (error:any) {
        onError(error)
    }
}

export async function deleteEmpleado(id:number,onSuccess:(responseData:any)=>void,onError:(error:any)=>void) {
    try {
        
        const response = await axiosInstance.delete(`${controllerName}${id}`);
        const responseData: any = response.data;
        
        if (response.status !== 200) {
            
            throw new Error("Servidor remoto:" + responseData.message);
        }
        
        onSuccess(responseData);
    } catch (error:any) {
        onError(error)
    }
}
