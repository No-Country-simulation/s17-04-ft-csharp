import { CircularProgress, Grid } from "@mui/material";

export const Loading = () => {
  return (
    <Grid
      container
      direction={"column"}
      alignItems={"center"}
      justifyContent={"center"}
      sx={{ minHeight: "100vh", backgroundColor: "primary.main" }}
    >
      <Grid item>
        <CircularProgress color='error' />
      </Grid>
    </Grid>
  );
};
