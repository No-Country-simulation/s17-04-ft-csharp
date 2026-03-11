import Box from "@mui/material/Box";
import FormControl from "@mui/material/FormControl";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import Select from "@mui/material/Select";
import { ChangeEvent } from "react";

interface Props {
  handleChange: (e: ChangeEvent<number>) => void;
  estimatedTime: number;
  name: string;
}

export const TimeSelectorNoteView = ({
  handleChange,
  estimatedTime = 0,
  name,
}: Props) => {
  return (
    <Box sx={{ minWidth: 120, width: "100%" }}>
      <FormControl fullWidth>
        <InputLabel id='demo-simple-select-label'>Delivery time</InputLabel>
        <Select
          fullWidth
          labelId='demo-simple-select-label'
          id='demo-simple-select'
          value={estimatedTime}
          label={name}
          name={name}
          onChange={handleChange}
        >
          <MenuItem value={0}>Select a delivery time</MenuItem>
          <MenuItem value={1}>1 week</MenuItem>
          <MenuItem value={2}>2 week</MenuItem>
          <MenuItem value={3}>3 week</MenuItem>
          <MenuItem value={4}>1 month</MenuItem>
        </Select>
      </FormControl>
    </Box>
  );
};
