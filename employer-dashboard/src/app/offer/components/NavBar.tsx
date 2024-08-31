import { LogoutOutlined, MenuOutlined } from "@mui/icons-material";
import { AppBar, Grid, IconButton, Toolbar, Typography } from "@mui/material";
import { useAppDispatch } from "../../../hooks/hooks";
import { startLogOutUser } from "../../../store/slice/auth/authThunks";

interface Props {
  drawerWidth: number;
}

export const NavBar = ({ drawerWidth = 240 }: Props) => {
  const dispatch = useAppDispatch();

  const onLogOut = () => {
    dispatch(startLogOutUser());
  };

  return (
    <AppBar
      position='fixed'
      sx={{
        width: {
          sm: `calc(100% - ${drawerWidth}px)`,
          ml: { sm: `${drawerWidth}px` },
        },
      }}
    >
      <Toolbar>
        <IconButton
          color='inherit'
          sx={{
            display: { sm: "none" },
          }}
        >
          <MenuOutlined />
        </IconButton>
        <Grid
          container
          direction={"row"}
          justifyContent={"space-between"}
          alignItems={"center"}
        >
          <Typography variant='h6' color={"text.secondary"}>
            JuniorHub
          </Typography>
          <IconButton onClick={onLogOut} color='secondary'>
            <LogoutOutlined />
          </IconButton>
        </Grid>
      </Toolbar>
    </AppBar>
  );
};
