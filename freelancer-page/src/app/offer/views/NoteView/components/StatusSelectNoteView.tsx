import {
  CampaignOutlined,
  CheckBoxOutlined,
  LockOutlined,
  SearchOutlined,
} from "@mui/icons-material";
import Box from "@mui/material/Box";
import FormControl from "@mui/material/FormControl";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import Select, { SelectChangeEvent } from "@mui/material/Select";

interface Props {
  handleChange: (e: SelectChangeEvent<number>) => void;
  state: number;
  name: string;
}

export const StatusSelectNoteView = ({
  state = 0,
  handleChange,
  name,
}: Props) => {
  return (
    <Box sx={{ minWidth: 120, width: "100%" }}>
      <FormControl fullWidth>
        <InputLabel id='demo-simple-select-label'>State</InputLabel>
        <Select
          fullWidth
          labelId='demo-simple-select-label'
          id='demo-simple-select'
          value={state}
          label={name}
          name={name}
          onChange={handleChange}
        >
          <MenuItem value={0}>Select a state</MenuItem>
          <MenuItem value={1}>
            <Box display={"flex"} alignItems={"center"} gap={1}>
              <CampaignOutlined /> Job Posted
            </Box>
          </MenuItem>
          <MenuItem value={2}>
            <Box display={"flex"} alignItems={"center"} gap={1}>
              <SearchOutlined /> In Selection Process
            </Box>
          </MenuItem>
          <MenuItem value={3}>
            <Box display={"flex"} alignItems={"center"} gap={1}>
              <LockOutlined /> Position Filled
            </Box>
          </MenuItem>
          <MenuItem value={4}>
            <Box display={"flex"} alignItems={"center"} gap={1}>
              <CheckBoxOutlined /> Position Closed
            </Box>
          </MenuItem>
        </Select>
      </FormControl>
    </Box>
  );
};
