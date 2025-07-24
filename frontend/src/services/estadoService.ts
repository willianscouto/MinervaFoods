import { Estado } from "@/interfaces/Estado";
import { proxyRequest } from "@/services/common/proxyClient";


export const estadoService = {
  getAll: async (): Promise<Estado[]> => {
    return proxyRequest<Estado[]>({
      path: "api/estado",
      method: "GET",
    });
  },
    getById: async (id: string): Promise<Estado[]> => {
      return proxyRequest<Estado[]>({
        path: `api/estado/${id}`,
        method: "GET",
      });
    },
}