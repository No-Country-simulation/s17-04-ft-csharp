import { Link as RouterLink } from "react-router-dom";
import { Grid, Link, Typography } from "@mui/material";

import { RegisterFormComponent } from "../components";
import { AuthLayout } from "../layout";

export const RegisterPage = () => {
  return (
    <AuthLayout title='Register'>
      <RegisterFormComponent />

      <Grid
        container
        alignContent={"center"}
        direction={"row"}
        justifyContent={"end"}
        sx={{ gap: 1 }}
        mt={1}
      >
        <Typography>Do you have a account?</Typography>
        <Link component={RouterLink} to={"/auth/login"} color={"inherit"}>
          Login
        </Link>
      </Grid>
    </AuthLayout>
  );
};
