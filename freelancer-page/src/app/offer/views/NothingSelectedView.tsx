import { AddOutlined, SearchOutlined } from "@mui/icons-material";
import { Fab, Grid, Typography } from "@mui/material";
import { useAppDispatch } from "../../../hooks/hooks";
import { startCreationOffer } from "../../../store";

export const NothingSelectedView = () => {
  const dispatch = useAppDispatch();
  const onCreateNote = () => {
    dispatch(startCreationOffer());
  };
  return (
    <Grid display={"flex"} flexDirection={"column"} gap={2}>
      <Grid
        onClick={onCreateNote}
        aria-label='add'
        className='animate__animated animate__fadeIn  animate__faster'
        container
        direction={"column"}
        alignItems={"center"}
        justifyContent={"center"}
        sx={{
          minHeight: "calc(40vh )",
          backgroundColor: "secondary.main",
          borderRadius: 5,
          "&:hover": {
            backgroundColor: "info.main",
            cursor: "pointer",
          },
        }}
      >
        <Grid item>
          <AddOutlined sx={{ color: "white", fontSize: 80 }} />
        </Grid>
        <Grid item>
          <Typography color='white' variant='h6'>
            Create your offer
          </Typography>
        </Grid>
      </Grid>
      <Grid
        className='animate__animated animate__fadeIn  animate__faster'
        container
        direction={"column"}
        alignItems={"center"}
        justifyContent={"center"}
        sx={{
          minHeight: "calc(40vh )",
          backgroundColor: "primary.main",
          borderRadius: 5,
        }}
      >
        <Grid item>
          <SearchOutlined sx={{ color: "text.secondary", fontSize: 80 }} />
        </Grid>
        <Grid item>
          <Typography color='text.secondary' variant='h6'>
            Here you will find your freelancers' applications!
          </Typography>
        </Grid>
      </Grid>
    </Grid>
  );
};
