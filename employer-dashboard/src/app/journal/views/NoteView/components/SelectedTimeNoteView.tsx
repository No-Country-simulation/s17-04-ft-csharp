import Box from "@mui/material/Box";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select, { SelectChangeEvent } from "@mui/material/Select";
import { useState } from "react";

export const SelectedTimeNoteView = () => {
  const [time, setTime] = useState("");

  const handleChange = (event: SelectChangeEvent) => {
    setTime(event.target.value as string);
  };

  return (
    <Box sx={{ minWidth: 120, width: "100%" }}>
      <FormControl fullWidth>
        <InputLabel id='demo-simple-select-label'>Delivery time</InputLabel>
        <Select
          fullWidth
          labelId='demo-simple-select-label'
          id='demo-simple-select'
          value={time}
          label='deliveryTime'
          onChange={handleChange}
        >
          <MenuItem value={1}>1 week</MenuItem>
          <MenuItem value={2}>2 week</MenuItem>
          <MenuItem value={3}>3 week</MenuItem>
          <MenuItem value={4}>1 month</MenuItem>
        </Select>
      </FormControl>
    </Box>
  );
};
