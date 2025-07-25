/**
 * Utilitários para manipulação e formatação de datas
 */

/**
 * Formata uma data para exibição (dd/MM/yyyy)
 */



export const formatarDataExibicao = (
  value: string | Date | null | undefined
): string => {
  if (!value) return "";
  
  const data = new Date(value);
  
  if (isNaN(data.getTime())) return "";

  const dia = data.getDate().toString().padStart(2, '0');
  const mes = (data.getMonth() + 1).toString().padStart(2, '0');
  const ano = data.getFullYear();

  return `${dia}/${mes}/${ano}`;
};

export const formatarDataHoraExibicao = (
  value: string | Date | null | undefined
): string => {
  if (!value) return "";
  
  const data = new Date(value);
  
  if (isNaN(data.getTime())) return "";

  const pad = (n: number) => n.toString().padStart(2, '0');

  const dia = data.getDate().toString().padStart(2, '0');
  const mes = (data.getMonth() + 1).toString().padStart(2, '0');
  const ano = data.getFullYear();
  const hour = pad(data.getHours());
  const minute = pad(data.getMinutes());

  return `${dia}/${mes}/${ano} ${hour}:${minute}`;
};

/**
 * Formata uma data para input type="date" (yyyy-MM-dd)
 */
export const formatarDataInput = (
  value: string | Date | null | undefined
): string => {
  if (!value) return "";

  const data = new Date(value);
  
  if (isNaN(data.getTime())) return "";

  const pad = (n: number) => n.toString().padStart(2, '0');
  
  const year = data.getFullYear();
  const month = pad(data.getMonth() + 1);
  const day = pad(data.getDate());


  return `${year}-${month}-${day}`;
};




/**
 * Formata data e hora para input type="datetime-local" (yyyy-MM-ddTHH:mm)
 */
export const formatarDataHoraInput = (value: string | Date | null | undefined): string => {
  if (!value) return "";

  const data = new Date(value);
  
  if (isNaN(data.getTime())) return "";

  const pad = (n: number) => n.toString().padStart(2, '0');
  
  const year = data.getFullYear();
  const month = pad(data.getMonth() + 1);
  const day = pad(data.getDate());
  const hours = pad(data.getHours());
  const minutes = pad(data.getMinutes());

  return `${year}-${month}-${day}T${hours}:${minutes}`;
};

/**
 * Converte string ISO para Date object
 */
export const parseDate = (isoString: string): Date | null => {
  try {
    const date = new Date(isoString);
    return isNaN(date.getTime()) ? null : date;
  } catch {
    return null;
  }
};

/**
 * Obtém data/hora atual no formato para input datetime-local
 */
export const getCurrentLocalDatetime = (): string => {
  const now = new Date();
  const pad = (n: number) => n.toString().padStart(2, "0");

  const year = now.getFullYear();
  const month = pad(now.getMonth() + 1);
  const day = pad(now.getDate());
  const hour = pad(now.getHours());
  const minute = pad(now.getMinutes());

  return `${year}-${month}-${day}T${hour}:${minute}`;
};

/**
 * Formata valor monetário
 */
export const formatarMoeda = (value: number | null | undefined): string => {
  if (value === null || value === undefined) return "R$ 0,00";
  return value.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  });
};