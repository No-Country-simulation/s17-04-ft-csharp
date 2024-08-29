import {
  FormControl,
  InputAdornment,
  InputLabel,
  OutlinedInput,
} from "@mui/material";

export const SelectRateForHourNoteView = () => {
  return (
    <FormControl fullWidth>
      <InputLabel htmlFor='outlined-adornment-amount'>Rate / hour</InputLabel>
      <OutlinedInput
        id='outlined-adornment-amount'
        startAdornment={<InputAdornment position='start'>$</InputAdornment>}
        label='rate / hour'
      />
    </FormControl>
  );
};
