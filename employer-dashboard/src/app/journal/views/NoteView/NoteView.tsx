/* eslint-disable react-hooks/exhaustive-deps */
import { DeleteOutlined, SaveOutlined } from "@mui/icons-material";
import { Button, Grid, TextField, Typography } from "@mui/material";
import { useFormik } from "formik";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../../../hooks/hooks";
import { startDeleteNote, startUpdateNote } from "../../../../store";
import { MultipleSelectChipNoteView } from "./components/MultipleSelectChipNoteView";
import { SelectedTimeNoteView } from "./components/SelectedTimeNoteView";
import { SelectRateForHourNoteView } from "./components/SelectRateForHourNoteView";
import { TableNoteView } from "./components/TableNoteView";
import Swal from "sweetalert2";

interface Credentials {
  title: string;
  body: string;
}

export const NoteView = () => {
  const { noteActive } = useAppSelector((state) => state.journal);
  const dispatch = useAppDispatch();
  // const fileInputRef = useRef<HTMLInputElement>(null);

  const initialValues: Credentials = {
    title: noteActive?.title || "",
    body: noteActive?.body || "",
  };

  const { handleChange, values, setValues } = useFormik({
    initialValues,
    onSubmit: () => {},
  });

  const onSaveNote = () => {
    Swal.fire({
      title: "Do you want to SAVE?",

      icon: "question",
      showConfirmButton: true,
      showCancelButton: true,
      confirmButtonText: "Save",
      cancelButtonText: "Cancel",
    }).then((value) => {
      if (value.value) {
        dispatch(startUpdateNote(values));
      }
    });
  };

  const onDeleteNote = () => {
    Swal.fire({
      title: "Do you want to DELETE?",

      icon: "error",
      showConfirmButton: true,
      showCancelButton: true,
      confirmButtonText: "Delete",
      cancelButtonText: "Cancel",
    }).then((value) => {
      if (value.value) {
        dispatch(startDeleteNote());
      }
    });
  };

  useEffect(() => {
    setValues(initialValues);
  }, [noteActive]);

  // const dateString = useMemo(
  //   () => (date: number | undefined) => {
  //     if (!date) throw new Error("Date is empty");
  //     return new Date(date).toUTCString();
  //   },
  //   [noteActive?.date]
  // );

  // const onFileInputChange = ({ target }: ChangeEvent<HTMLInputElement>) => {
  //   if (!target.files) throw new Error("$Files is empty");
  //   dispatch(startUploadImages(target.files));
  // };

  return (
    <Grid
      className='animate__animated animate__fadeIn  animate__faster'
      container
      direction={"row"}
      justifyContent={"space-between"}
      alignItems={"center"}
      mb={1}
      gap={3}
    >
      <Grid item>
        <Typography variant='h4'>
          Oferta de trabajo / proyecto
          {/* {dateString(noteActive?.date)} */}
        </Typography>
      </Grid>
      <Grid item>
        {/* <input
          type='file'
          multiple
          ref={fileInputRef}
          style={{ display: "none" }}
          onChange={onFileInputChange}
        />
        <Button
          onClick={() => fileInputRef.current?.click()}
          color='inherit'
          sx={{ p: 2 }}
        >
          <UploadFileOutlined sx={{ fontSize: 30, mr: 1 }} />
          Upload
        </Button> */}
        <Button onClick={onSaveNote} color='inherit' sx={{ p: 2 }}>
          <SaveOutlined sx={{ fontSize: 30, mr: 1 }} />
          Save
        </Button>
        <Button onClick={onDeleteNote} color='error' sx={{ p: 2 }}>
          <DeleteOutlined sx={{ fontSize: 30, mr: 1 }} />
          Delete
        </Button>
      </Grid>
      <Grid container gap={2}>
        <TextField
          type='text'
          variant='filled'
          fullWidth
          placeholder='Enter a title'
          label='Title'
          onChange={handleChange}
          name='title'
          value={values.title}
        />
        <TextField
          type='text'
          variant='filled'
          fullWidth
          multiline
          placeholder='What do you need?'
          minRows={5}
          onChange={handleChange}
          name='body'
          value={values.body}
        />
        <SelectedTimeNoteView />
        <SelectRateForHourNoteView />
        <MultipleSelectChipNoteView />
      </Grid>
      {/* <StandardImageList /> */}

      <Grid container>
        <TableNoteView />
      </Grid>
    </Grid>
  );
};
