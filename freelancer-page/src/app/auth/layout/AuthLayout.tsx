import { Grid, Typography } from "@mui/material";
import { ReactNode } from "react";

interface Props {
  children: ReactNode;
  title: "Login" | "Register";
}

export const AuthLayout = ({ children, title }: Props) => {
  return (
    <Grid
      container
      direction={"column"}
      alignItems={"center"}
      justifyContent={"center"}
      sx={{ minHeight: "100vh", backgroundColor: "primary.main" }}
    >
      <Grid
        container
        direction={"column"}
        width={{ xs: "100%", sm: "70vw", md: "35vw" }}
        sx={{ backgroundColor: "white", padding: 3, borderRadius: 5 }}
      >
        <Typography mb={1} variant='h5'>
          {title}
        </Typography>

        {children}
      </Grid>
    </Grid>
  );
};
