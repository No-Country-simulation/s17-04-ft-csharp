import { StarOutlineOutlined } from "@mui/icons-material";
import { Grid, Typography } from "@mui/material";

export const NothingSelectedView = () => {
  return (
    <Grid
      className='animate__animated animate__fadeIn  animate__faster'
      container
      direction={"column"}
      alignItems={"center"}
      justifyContent={"center"}
      sx={{
        minHeight: "calc(100vh - 110px)",
        backgroundColor: "primary.main",
        borderRadius: 5,
      }}
    >
      <Grid item>
        <StarOutlineOutlined sx={{ color: "text.secondary", fontSize: 80 }} />
      </Grid>
      <Grid item>
        <Typography color='text.secondary' variant='h6'>
          Select or create an entry
        </Typography>
      </Grid>
    </Grid>
  );
};
