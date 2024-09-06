import { Link as RouterLink } from "react-router-dom";
import { Grid, Link } from "@mui/material";

import { LoginFormComponent } from "../components";
import { AuthLayout } from "../layout";

export const LoginPage = () => {
  return (
    <AuthLayout title='Login'>
      <LoginFormComponent />

      <Grid
        container
        direction={"row"}
        mt={1}
        justifyContent={"end"}
        sx={{ gap: 1 }}
      >
        <Link component={RouterLink} to={"/auth/register"} color={"inherit"}>
          Create a account
        </Link>
      </Grid>
    </AuthLayout>
  );
};
