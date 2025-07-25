'use client';

import { Select, MenuItem, FormControl, InputLabel, FormHelperText } from "@mui/material";

interface Option {
  value: string | number;
  label: string;
}

interface Props {
  name?: string;
  label: string;
  value: any;
  onChange: (e: any) => void;
  required?: boolean;
  options: Option[];
  disabled?: boolean;
  error?: boolean;
  helperText?: string;
}

export default function SelectInput({
  name,
  label,
  value,
  onChange,
  required = false,
  options,
  disabled = false,
  error = false,
  helperText
}: Props) {
  return (
    <FormControl fullWidth required={required} error={error} margin="normal" disabled={disabled}>
      <InputLabel>{label}</InputLabel>
      <Select
        name={name}
        label={label}
        value={value}
        onChange={onChange}
      >
        {options.map((option) => (
          <MenuItem key={option.value} value={option.value}>
            {option.label}
          </MenuItem>
        ))}
      </Select>
      {helperText && <FormHelperText>{helperText}</FormHelperText>}
    </FormControl>
  );
}