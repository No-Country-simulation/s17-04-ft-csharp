/* eslint-disable react-hooks/exhaustive-deps */
import {
  DeleteOutlined,
  SaveOutlined,
  UploadFileOutlined,
} from "@mui/icons-material";
import { Button, Grid, TextField, Typography } from "@mui/material";
import { useFormik } from "formik";
import { ChangeEvent, useEffect, useMemo, useRef } from "react";
import { useAppDispatch, useAppSelector } from "../../../hooks/hooks";
import {
  startDeleteNote,
  startUpdateNote,
  startUploadImages,
} from "../../../store";
import { StandardImageList } from "../components";

interface Credentials {
  title: string;
  body: string;
}

export const NoteView = () => {
  const { noteActive } = useAppSelector((state) => state.journal);
  const dispatch = useAppDispatch();
  const fileInputRef = useRef<HTMLInputElement>(null);

  const initialValues: Credentials = {
    title: noteActive?.title || "",
    body: noteActive?.body || "",
  };

  const { handleChange, values, setValues } = useFormik({
    initialValues,
    onSubmit: () => {},
  });

  const onSaveNote = () => {
    dispatch(startUpdateNote(values));
  };

  const onDeleteNote = () => {
    dispatch(startDeleteNote());
  };

  useEffect(() => {
    setValues(initialValues);
  }, [noteActive]);

  const dateString = useMemo(
    () => (date: number | undefined) => {
      if (!date) throw new Error("Date is empty");
      return new Date(date).toUTCString();
    },
    [noteActive?.date]
  );

  const onFileInputChange = ({ target }: ChangeEvent<HTMLInputElement>) => {
    if (!target.files) throw new Error("$Files is empty");
    dispatch(startUploadImages(target.files));
  };

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
        <input
          type='file'
          multiple
          ref={fileInputRef}
          style={{ display: "none" }}
          onChange={onFileInputChange}
        />
        <Button
          onClick={() => fileInputRef.current?.click()}
          color='primary'
          sx={{ p: 2 }}
        >
          <UploadFileOutlined sx={{ fontSize: 30, mr: 1 }} />
          Upload
        </Button>
        <Button onClick={onSaveNote} color='primary' sx={{ p: 2 }}>
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
          placeholder='What have happend today?'
          minRows={5}
          onChange={handleChange}
          name='body'
          value={values.body}
        />
      </Grid>
      <StandardImageList />
    </Grid>
  );
};
