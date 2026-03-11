import { Grid } from "@mui/material";
import Box from "@mui/material/Box";
import Chip from "@mui/material/Chip";
import FormControl from "@mui/material/FormControl";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import OutlinedInput from "@mui/material/OutlinedInput";
import Select, { SelectChangeEvent } from "@mui/material/Select";
import { Theme, useTheme } from "@mui/material/styles";
import { useEffect, useState } from "react";
import { useAppSelector } from "../../../../../hooks/hooks";

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

// const names = [
//   "JavaScript",
//   "React",
//   "Node.js",
//   "TypeScript",
//   "Python",
//   "Angular",
//   "Java",
//   "Vue.js",
//   "Docker",
//   "Kubernetes",
// ];

interface Props {
  onChangeTechnologies: (technologies: string[]) => void;
  technologiesSelected?: string[];
}

function getStyles(name: string, personName: readonly string[], theme: Theme) {
  return {
    fontWeight:
      personName.indexOf(name) === -1
        ? theme.typography.fontWeightRegular
        : theme.typography.fontWeightMedium,
  };
}

export const MultipleSelectChipNoteView = ({
  onChangeTechnologies,
  technologiesSelected = [],
}: Props) => {
  const { technologies: technologiesList } = useAppSelector(
    (state) => state.offer
  );

  useEffect(() => {
    setTechnology(technologiesSelected);
  }, [technologiesSelected]);

  const theme = useTheme();
  const [technology, setTechnology] = useState<string[]>([]);

  const handleChange = (event: SelectChangeEvent<typeof technology>) => {
    const {
      target: { value },
    } = event;
    const result = typeof value === "string" ? value.split(",") : value;
    setTechnology(result);
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
          value={technology}
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
          {technologiesList.map((tech) => (
            <MenuItem
              key={tech.id}
              value={tech.name}
              style={getStyles(tech.name, technology, theme)}
            >
              {tech.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </Grid>
  );
};
