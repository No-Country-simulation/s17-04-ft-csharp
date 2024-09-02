import { AppBar, Box, Drawer, List } from "@mui/material";
import { Offer } from "../../@types/types";
import { SideBarItem } from "../offer/components/SideBarItem";
import { Navbar } from "./components/Navbar";

export const ApplicationPage = () => {
  return (
    <>
      {/* <AppBar
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
    </AppBar> */}

      <Navbar />

      <Box flexShrink={{ sm: 0 }} component={"nav"} width={{ sm: 240 }}>
        <Drawer
          variant='permanent'
          open
          sx={{
            display: { xs: "block" },
            "& .MuiDrawer-paper": {
              boxSizing: "border-box",
              width: 240,
            },
          }}
        >
          <List>
            {[].map((note: Offer) => (
              <SideBarItem key={note.id} note={note} />
            ))}
          </List>
        </Drawer>
      </Box>
    </>
  );
};
