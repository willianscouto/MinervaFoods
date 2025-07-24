import axios from "axios";
import { CepResponse } from "@/interfaces/Cep";

export const cepService = {
  getCep: async (cep: string): Promise<{ data: CepResponse }> => {
    const response = await axios.get(`https://viacep.com.br/ws/${cep}/json/`);
    return response;
  },
};
