import { TurnedInNot } from "@mui/icons-material";
import {
  Grid,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
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
    return note.title.length > 17
      ? note.title.substring(0, 17) + "..."
      : note.title;
  }, [note.title]);

  return (
    <ListItem onClick={onActiveNote} disablePadding>
      <ListItemButton>
        <ListItemIcon>
          <TurnedInNot color='info' />
        </ListItemIcon>
        <Grid container display={"flex"} flexDirection={"column"}>
          <ListItemText primary={fixTitle} />
          <ListItemText secondary={note.description} />
        </Grid>
      </ListItemButton>
    </ListItem>
  );
};
