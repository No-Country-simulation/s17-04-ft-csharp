import { Box } from "@mui/material";
import { ReactNode } from "react";

interface Props {
  children: ReactNode;
}

export const ApplicationLayout = ({ children }: Props) => {
  return (
    <Box marginX={"200px"} marginY={1} display={"flex"}>
      {children}
    </Box>
  );
};
