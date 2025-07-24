// services/api.ts
import axios from 'axios';
import { ApiResponse } from '@/interfaces/ApiResponse';

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000',
});

api.interceptors.response.use(
  (response) => {
   
    const res = response.data as ApiResponse<unknown>;

    if (!res.success) {
      return Promise.reject(res); 
    }

    return response;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default api;
