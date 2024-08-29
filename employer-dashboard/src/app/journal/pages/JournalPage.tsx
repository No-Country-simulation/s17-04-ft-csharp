import { AddOutlined } from "@mui/icons-material";
import { Fab, IconButton } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../../hooks/hooks";
import { startCreationNote } from "../../../store";
import { JournalLayout } from "../layout";
import { NoteView, NothingSelectedView } from "../views";

export const JournalPage = () => {
  const dispatch = useAppDispatch();
  const { isSaving, noteActive } = useAppSelector((state) => state.journal);

  const onCreateNote = () => {
    dispatch(startCreationNote());
  };

  return (
    <JournalLayout>
      {!noteActive ? <NothingSelectedView /> : <NoteView />}

      <Fab
        disabled={isSaving}
        onClick={onCreateNote}
        size='large'
        sx={{
          color: "white",
          position: "fixed",
          right: 50,
          bottom: 50,
        }}
        color='error'
        aria-label='add'
      >
        <AddOutlined sx={{ fontSize: 30 }} />
      </Fab>
    </JournalLayout>
  );
};
