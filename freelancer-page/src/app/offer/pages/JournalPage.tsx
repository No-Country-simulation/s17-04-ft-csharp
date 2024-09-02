import { AddOutlined } from "@mui/icons-material";
import { Fab } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../../hooks/hooks";
import { startCreationOffer } from "../../../store";
import { JournalLayout } from "../layout";
import { NoteView, NothingSelectedView } from "../views";

export const JournalPage = () => {
  const dispatch = useAppDispatch();
  const { isSaving, offerActive: noteActive } = useAppSelector(
    (state) => state.offer
  );

  const onCreateNote = () => {
    dispatch(startCreationOffer());
  };

  return (
    <JournalLayout>
      {!noteActive ? <NothingSelectedView /> : <NoteView />}

      <Fab
        disabled={isSaving}
        onClick={onCreateNote}
        size='large'
        sx={{
          position: "fixed",
          right: 50,
          bottom: 50,
        }}
        color='secondary'
        aria-label='add'
      >
        <AddOutlined sx={{ fontSize: 30 }} />
      </Fab>
    </JournalLayout>
  );
};
