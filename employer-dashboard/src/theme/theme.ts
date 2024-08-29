import { createTheme } from "@mui/material/styles";
import { red } from "@mui/material/colors";

export const theme = createTheme({
  palette: {
    background: {
      default: "#FCF8ED",
    },
    primary: {
      main: "#FEA451",
    },
    secondary: {
      main: "#FFD900",
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
