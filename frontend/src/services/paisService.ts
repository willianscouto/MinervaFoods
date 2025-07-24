import { Pais } from "@/interfaces/Pais";
import { proxyRequest } from "@/services/common/proxyClient";


export const paisService = {
  getAll: async (): Promise<Pais[]> => {
    return proxyRequest<Pais[]>({
      path: "api/pais",
      method: "GET",
    });
  },
}