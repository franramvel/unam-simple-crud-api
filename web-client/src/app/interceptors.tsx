import axios from 'axios';
import { API_URL } from './global';

// Configura la instancia de Axios con una URL base y otros ajustes si es necesario
export const axiosReaderInstance = axios.create({
  baseURL: API_URL, 

  timeout: 1200, 
});

// Agrega un interceptor para todas las solicitudes
axiosReaderInstance.interceptors.request.use(
  (config) => {
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

