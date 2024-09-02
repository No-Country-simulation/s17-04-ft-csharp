import { Alert, Button, Grid, TextField } from "@mui/material";

import { useFormik } from "formik";
import { z, ZodError } from "zod";
import { startCreatingUserWithEmailPassword } from "../../../store/slice/auth/authThunks";

import { useMemo } from "react";
import { useAppDispatch, useAppSelector } from "../../../hooks/hooks";

interface Credentials {
  displayName: string;
  email: string;
  password: string;
}

const credentialsSchema = z.object({
  displayName: z.string().min(2),
  email: z.string().email(),
  password: z.string().min(6),
});

export const RegisterFormComponent = () => {
  const dispatch = useAppDispatch();
  const { status, errorMessage } = useAppSelector((state) => state.auth);
  const isAuthentication = useMemo(() => status === "checking", [status]);
  // const isCheckingAuthentication = useMemo(() => status === "checking", [status]);

  const { handleChange, handleSubmit, values, errors, touched } = useFormik({
    initialValues: {
      displayName: "",
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
      dispatch(startCreatingUserWithEmailPassword(values));
    },
  });

  return (
    <form
      onSubmit={handleSubmit}
      className='animate__animated animate__fadeIn  animate__faster'
    >
      <Grid container flexDirection={"column"} spacing={2}>
        <Grid item xs={12}>
          <TextField
            variant='outlined'
            label={"Full name"}
            placeholder='Enter your name'
            type='text'
            name='displayName'
            value={values.displayName}
            fullWidth
            onChange={handleChange}
            helperText={
              errors.displayName && touched.displayName
                ? errors.displayName
                : ""
            }
            error={!!(errors.displayName && touched.displayName)}
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            variant='outlined'
            label={"Email"}
            placeholder='Enter your email'
            type='email'
            name='email'
            value={values.email}
            fullWidth
            onChange={handleChange}
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
            value={values.password}
            type='password'
            fullWidth
            onChange={handleChange}
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
            <Grid item xs={12}>
              <Button
                color={"secondary"}
                disabled={isAuthentication}
                type='submit'
                variant='contained'
                fullWidth
              >
                Register
              </Button>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </form>
  );
};
