import { format } from 'date-fns';

/**
 * Formata uma data para o padrão brasileiro dd/MM/yyyy.
 * @param data A data em string (ISO) ou objeto Date.
 * @returns A data formatada ou string vazia se for inválida.
 */
export function formatarData(data: string | Date | undefined | null): string {
  if (!data) return '';

  try {
    const dateObj = typeof data === 'string' ? new Date(data) : data;
    return format(dateObj, 'dd/MM/yyyy');
  } catch {
    return '';
  }
}
