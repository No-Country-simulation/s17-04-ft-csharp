import * as React from "react";
import { Theme, useTheme } from "@mui/material/styles";
import Box from "@mui/material/Box";
import OutlinedInput from "@mui/material/OutlinedInput";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select, { SelectChangeEvent } from "@mui/material/Select";
import Chip from "@mui/material/Chip";
import { Grid } from "@mui/material";

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
    },
  },
};

const names = [
  "JavaScript",
  "React",
  "Node.js",
  "TypeScript",
  "Python",
  "Angular",
  "Java",
  "Vue.js",
  "Docker",
  "Kubernetes",
];

interface Props {
  onChangeTechnologies: (technologies: string[]) => void;
}

function getStyles(name: string, personName: readonly string[], theme: Theme) {
  return {
    fontWeight:
      personName.indexOf(name) === -1
        ? theme.typography.fontWeightRegular
        : theme.typography.fontWeightMedium,
  };
}

export const MultipleSelectChipNoteView = ({ onChangeTechnologies }: Props) => {
  const theme = useTheme();
  const [personName, setPersonName] = React.useState<string[]>([]);

  const handleChange = (event: SelectChangeEvent<typeof personName>) => {
    const {
      target: { value },
    } = event;
    const result = typeof value === "string" ? value.split(",") : value;
    setPersonName(result);
    onChangeTechnologies(result);
  };

  return (
    <Grid container>
      <FormControl sx={{ minWidth: "100%" }}>
        <InputLabel id='demo-multiple-chip-label'>Technologies</InputLabel>
        <Select
          fullWidth
          labelId='demo-multiple-chip-label'
          id='demo-multiple-chip'
          multiple
          value={personName}
          onChange={handleChange}
          input={
            <OutlinedInput id='select-multiple-chip' label='technologies' />
          }
          renderValue={(selected) => (
            <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
              {selected.map((value) => (
                <Chip key={value} label={value} />
              ))}
            </Box>
          )}
          MenuProps={MenuProps}
        >
          {names.map((name) => (
            <MenuItem
              key={name}
              value={name}
              style={getStyles(name, personName, theme)}
            >
              {name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </Grid>
  );
};
