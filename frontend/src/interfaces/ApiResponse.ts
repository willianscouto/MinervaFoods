export interface ValidationErrorDetail {
  error: string;
  detail: string;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  errors?: ValidationErrorDetail[];
  data?: T;
}
