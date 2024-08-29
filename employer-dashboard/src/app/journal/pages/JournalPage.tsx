import { AddOutlined } from "@mui/icons-material";
import { IconButton } from "@mui/material";
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

      <IconButton
        disabled={isSaving}
        onClick={onCreateNote}
        size='large'
        sx={{
          color: "white",
          bgcolor: "error.main",
          ":hover": { backgroundColor: "error.main", opacity: 0.85 },
          position: "fixed",
          right: 50,
          bottom: 50,
        }}
      >
        <AddOutlined sx={{ fontSize: 30 }} />
      </IconButton>
    </JournalLayout>
  );
};
