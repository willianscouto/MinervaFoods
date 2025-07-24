import { TextField, TextFieldProps } from "@mui/material";
import { Controller, Control, FieldValues, Path } from "react-hook-form";

interface Props<T extends FieldValues>
  extends Omit<TextFieldProps, "name" | "control"> {
  name: Path<T>;
  label: string;
  control: Control<T>;
  required?: boolean;
  type?: React.InputHTMLAttributes<unknown>["type"];
  maxLength?: number;
}

export default function FormInput<T extends FieldValues>({
  name,
  label,
  control,
  required = false,
  type = "text",
  maxLength,
  ...textFieldProps
}: Props<T>) {
  return (
    <Controller
      name={name}
      control={control}
      render={({ field, fieldState }) => (
        <>
          <TextField
            {...field}
            label={label}
            type={type}
            fullWidth
            required={required}
            margin="normal"
            value={field.value ?? ""}
            error={!!fieldState.error}
            helperText={fieldState.error?.message}
            inputProps={{
              maxLength,
              ...(textFieldProps.inputProps || {}),
            }}
            onChange={(e) => {
              let value = e.target.value;
              if (type === "number" && maxLength) {
                if (value.length > maxLength) {
                  value = value.slice(0, maxLength);
                }
              }
              field.onChange(value);
            }}
            {...textFieldProps}
          />
          {fieldState.invalid && (
            <span className="error-message">{fieldState.error?.message}</span>
          )}
        </>
      )}
    />
  );
}
