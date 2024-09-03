import { TurnedInNot } from "@mui/icons-material";
import {
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Grid2 as Grid,
} from "@mui/material";
import { setActiveNote } from "../../../store";

import { useMemo } from "react";
import { Offer } from "../../../@types/types";
import { useAppDispatch } from "../../../hooks/hooks";

interface Props {
  note: Offer;
}

export const SideBarItem = ({ note }: Props) => {
  const dispatch = useAppDispatch();

  const onActiveNote = () => {
    dispatch(setActiveNote(note));
  };

  const fixTitle = useMemo(() => {
    return note.title.length > 25
      ? note.title.substring(0, 25) + "..."
      : note.title;
  }, [note.title]);

  return (
    <ListItem onClick={onActiveNote} disablePadding>
      <ListItemButton>
        <ListItemIcon>
          <TurnedInNot color={"secondary"} />
        </ListItemIcon>
        <Grid container>
          <ListItemText primary={fixTitle} secondary={note.description} />
          {/* <ListItemText /> */}
        </Grid>
      </ListItemButton>
    </ListItem>
  );
};
