import { Alert, Button, Grid2 as Grid, TextField } from "@mui/material";

import { useFormik } from "formik";
import { z, ZodError } from "zod";
import { startCreatingUserWithEmailPassword } from "../../../store/slice/auth/authThunks";

import { useMemo } from "react";
import { useAppDispatch, useAppSelector } from "../../../hooks/hooks";

interface Credentials {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

const credentialsSchema = z.object({
  firstName: z
    .string({
      required_error: "First name is required",
      invalid_type_error: "Name must be a string",
    })
    .min(2, "First name must be at least 2 characters long.")
    .max(50, "First name must be no more than 50 characters long."),
  lastName: z
    .string({
      required_error: "Last name is required",
      invalid_type_error: "Name must be a string",
    })
    .min(2, "Last name must be at least 2 characters long.")
    .max(50, "Last name must be no more than 50 characters long."),
  email: z.string().email(),
  password: z
    .string()
    .min(1, "Password is required")
    .min(6, "Password must be at least 6 characters long.")
    .regex(/[A-Z]/, "Password must contain at least one uppercase letter.")
    .regex(/[a-z]/, "Password must contain at least one lowercase letter.")
    .regex(/[0-9]/, "Password must contain at least one number.")
    .regex(/[\W_]/, "Password must contain at least one special character."),
});

export const RegisterFormComponent = () => {
  const dispatch = useAppDispatch();
  const { status, errorMessage } = useAppSelector((state) => state.auth);
  const isAuthentication = useMemo(() => status === "checking", [status]);
  // const isCheckingAuthentication = useMemo(() => status === "checking", [status]);

  const { handleChange, handleSubmit, values, errors, touched } = useFormik({
    initialValues: {
      firstName: "",
      lastName: "",
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
        <Grid
          sx={{
            display: "flex",
            flexDirection: "row",
            gap: 2,
          }}
        >
          <TextField
            variant='outlined'
            label={"First name"}
            placeholder='Enter your name'
            type='text'
            name='firstName'
            value={values.firstName}
            fullWidth
            onChange={handleChange}
            helperText={
              errors.firstName && touched.firstName ? errors.firstName : ""
            }
            error={!!(errors.firstName && touched.firstName)}
          />
          <TextField
            variant='outlined'
            label={"Last name"}
            placeholder='Enter your name'
            type='text'
            name='lastName'
            value={values.lastName}
            fullWidth
            onChange={handleChange}
            helperText={
              errors.lastName && touched.lastName ? errors.lastName : ""
            }
            error={!!(errors.lastName && touched.lastName)}
          />
        </Grid>
        <Grid size={{ xs: 12 }}>
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

        <Grid size={{ xs: 12 }}>
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

        <Grid>
          <Grid
            container
            direction={"row"}
            spacing={2}
            justifyContent={"space-between"}
          >
            <Grid
              size={{ xs: 12 }}
              display={!!errorMessage === true ? "" : "none"}
            >
              <Alert severity='error'>{errorMessage}</Alert>
            </Grid>
            <Grid size={{ xs: 12 }}>
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
