import { createTheme } from "@mui/material/styles";
import { red } from "@mui/material/colors";

export const theme = createTheme({
  palette: {
    background: {
      default: "#FFF7F0",
    },
    primary: {
      // main: "#2e294eff",
      main: "#FEA451ff",
    },
    secondary: {
      main: "#9A1010ff",
    },
    info: {
      main: "#2e294eff",
    },
    // text: {
    //   secondary: "#000000",
    //   primary: "#000000",
    // },
    error: {
      main: red.A400,
    },
  },
});

// $space-cadet: #2e294eff;
// $penn-red: #9A1010ff;
// $sandy-brown: #FEA451ff;
// $white: #fbfbfbff;
// $platinum: #E6E6E6ff;
