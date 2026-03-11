import { useMemo } from "react";

import { Google } from "@mui/icons-material";
import { Alert, Button, Grid, TextField, Typography } from "@mui/material";

import { useFormik } from "formik";
import { z, ZodError } from "zod";
import { useAppDispatch, useAppSelector } from "../../../hooks/hooks";
import {
  startGoogleSignIn,
  startSignInWithEmailPassword,
} from "../../../store/slice/auth/authThunks";

export const LoginFormComponent = () => {
  interface Credentials {
    email: string;
    password: string;
  }
  const credentialsSchema = z.object({
    email: z.string().email(),
    password: z.string().min(6),
  });

  const dispatch = useAppDispatch();
  const { status, errorMessage } = useAppSelector((state) => state.auth);
  const isAuthentication = useMemo(() => status === "checking", [status]);

  const { handleChange, handleSubmit, values, errors, touched } = useFormik({
    initialValues: {
      email: "",
      password: "",
    },
    validate: (values: Credentials) => {
      try {
        credentialsSchema.parse(values);
      } catch (errors) {
        if (errors instanceof ZodError) return errors.formErrors.fieldErrors;
      }
    },
    onSubmit: (values: Credentials) => {
      dispatch(startSignInWithEmailPassword(values));
    },
  });

  const onGoogleSignIn = () => {
    dispatch(startGoogleSignIn());
  };

  return (
    <form
      onSubmit={handleSubmit}
      className='animate__animated animate__fadeIn  animate__faster'
    >
      <Grid container flexDirection={"column"} spacing={2}>
        <Grid item xs={12}>
          <TextField
            variant='outlined'
            label={"Email"}
            placeholder='Enter your email'
            type='email'
            name='email'
            fullWidth
            onChange={handleChange}
            value={values.email}
            helperText={errors.email && touched.email ? errors.email : ""}
            error={!!(errors.email && touched.email)}
          />
        </Grid>

        <Grid item xs={12}>
          <TextField
            variant='outlined'
            label={"Password"}
            placeholder='Enter your password'
            name='password'
            type='password'
            fullWidth
            onChange={handleChange}
            value={values.password}
            helperText={
              errors.password && touched.password ? errors.password : ""
            }
            error={!!(errors.password && touched.password)}
          />
        </Grid>

        <Grid item>
          <Grid
            container
            direction={"row"}
            spacing={2}
            justifyContent={"space-between"}
          >
            <Grid item xs={12} display={!!errorMessage === true ? "" : "none"}>
              <Alert severity='error'>{errorMessage}</Alert>
            </Grid>
            <Grid item xs={12} sm={6}>
              <Button
                color={"secondary"}
                disabled={isAuthentication}
                type='submit'
                variant='contained'
                fullWidth
              >
                Login
              </Button>
            </Grid>
            <Grid item xs={12} sm={6}>
              <Button
                color={"secondary"}
                disabled={isAuthentication}
                onClick={onGoogleSignIn}
                variant='contained'
                fullWidth
              >
                <Google />
                <Typography color={"white"} ml={1}>
                  Google
                </Typography>
              </Button>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </form>
  );
};
