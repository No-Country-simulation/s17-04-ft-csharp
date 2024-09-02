import {
  BalanceOutlined,
  DangerousOutlined,
  StarBorderOutlined,
  WarningAmberOutlined,
} from "@mui/icons-material";
import Box from "@mui/material/Box";
import FormControl from "@mui/material/FormControl";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import Select from "@mui/material/Select";
import { ChangeEvent } from "react";

interface Props {
  handleChange: (e: ChangeEvent<number>) => void;
  difficult: number;
  name: string;
}

export const DifficultSelectNoteView = ({
  difficult = 0,
  handleChange,
  name,
}: Props) => {
  return (
    <Box sx={{ minWidth: 120, width: "100%" }}>
      <FormControl fullWidth>
        <InputLabel id='demo-simple-select-label'>Difficult</InputLabel>
        <Select
          fullWidth
          labelId='demo-simple-select-label'
          id='demo-simple-select'
          value={difficult}
          label={name}
          name={name}
          onChange={handleChange}
        >
          <MenuItem value={0}>Select a difficult</MenuItem>
          <MenuItem value={1}>
            <Box display={"flex"} alignItems={"center"} gap={1}>
              <StarBorderOutlined /> Easy
            </Box>
          </MenuItem>
          <MenuItem value={2}>
            <Box display={"flex"} alignItems={"center"} gap={1}>
              <BalanceOutlined /> Medium
            </Box>
          </MenuItem>
          <MenuItem value={4}>
            <Box display={"flex"} alignItems={"center"} gap={1}>
              <WarningAmberOutlined /> Hard
            </Box>
          </MenuItem>
          <MenuItem value={3}>
            <Box display={"flex"} alignItems={"center"} gap={1}>
              <DangerousOutlined /> Very hard
            </Box>
          </MenuItem>
        </Select>
      </FormControl>
    </Box>
  );
};
