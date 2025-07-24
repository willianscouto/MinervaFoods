import axios from 'axios';
import { ApiResponse } from '@/interfaces/ApiResponse';

interface ProxyOptions {
  path: string;
  method?: 'GET' | 'POST' | 'PUT' | 'DELETE';
  params?: Record<string, unknown>;
  data?: unknown;
}

export async function proxyRequest<T>({
  path,
  method = 'GET',
  params,
  data,
}: ProxyOptions): Promise<T> {
  const response = await axios.post<ApiResponse<T>>('/api', {
    path,
    method,
    params,
    data,
  });

  const result = response.data.data;

  if (method !== 'DELETE' && result === undefined) {
    throw new Error(response.data.message || 'Erro na requisição');
  }

  return result!;
}
