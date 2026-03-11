import {
  FormControl,
  InputAdornment,
  InputLabel,
  OutlinedInput,
} from "@mui/material";
import { ChangeEvent } from "react";

interface Props {
  handleChange: (e: ChangeEvent<number>) => void;
  price: number;
  name: string;
}

export const SelectRateForHourNoteView = ({
  price = 0,
  handleChange,
  name,
}: Props) => {
  return (
    <FormControl fullWidth>
      <InputLabel htmlFor='outlined-adornment-amount'>Rate / hour</InputLabel>
      <OutlinedInput
        id='outlined-adornment-amount'
        startAdornment={<InputAdornment position='start'>$</InputAdornment>}
        value={price}
        label={name}
        name={name}
        onChange={handleChange}
      />
    </FormControl>
  );
};
