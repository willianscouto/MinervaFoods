import { Controller, Control, FieldValues, Path } from "react-hook-form";
import {
  Select,
  MenuItem,
  FormControl,
  InputLabel,
  FormHelperText,
} from "@mui/material";

interface Option {
  value: string | number;
  label: string;
}

interface Props<T extends FieldValues> {
  name: Path<T>;
  label: string;
  control: Control<T>;
  required?: boolean;
  options: Option[];
  disabled?: boolean;
}

export default function ControllerSelectInput<T extends FieldValues>({
  name,
  label,
  control,
  required = false,
  options,
  disabled= false
}: Props<T>) {
  return (
    <Controller
      name={name}
      control={control}
      render={({ field, fieldState }) => (
        <>
          <FormControl
            fullWidth
            required={required}
            error={!!fieldState.error}
            margin="normal"
            
          >
            <InputLabel>{label}</InputLabel>
            <Select
              {...field}
              label={label}
              value={field.value ?? ""} 
              disabled ={disabled}
            >
              {options.map((option) => (
                <MenuItem key={option.value} value={option.value}>
                  {option.label}
                </MenuItem>
              ))}
            </Select>
            <FormHelperText>{fieldState.error?.message}</FormHelperText>
          </FormControl>
          {fieldState.invalid && (
            <span className="error-message">{fieldState.error?.message}</span>
          )}
        </>
      )}
    />
  );
}
