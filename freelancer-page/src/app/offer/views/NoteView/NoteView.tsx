import { DeleteOutlined, SaveOutlined } from "@mui/icons-material";
import { Button, Grid, TextField, Typography } from "@mui/material";
import { useFormik } from "formik";
import { useEffect } from "react";
import Swal from "sweetalert2";
import { useAppDispatch, useAppSelector } from "../../../../hooks/hooks";
import { startDeleteNote, startUpdateNote } from "../../../../store";
import { DifficultSelectNoteView } from "./components/DifficultSelectNoteView";
import { MultipleSelectChipNoteView } from "./components/MultipleSelectChipNoteView";
import { TimeSelectorNoteView } from "./components/TimeSelectorNoteView";
import { SelectRateForHourNoteView } from "./components/SelectRateForHourNoteView";
import { StatusSelectNoteView } from "./components/StatusSelectNoteView";
import { TableNoteView } from "./components/TableNoteView";
import { Offer } from "../../../../@types/types";

export const NoteView = () => {
  const { offerActive } = useAppSelector((state) => state.offer);
  const dispatch = useAppDispatch();
  // const fileInputRef = useRef<HTMLInputElement>(null);

  const initialValues: Offer = {
    title: offerActive?.title || "",
    description: offerActive?.description || "",
    price: offerActive?.price || 0,
    estimatedTime: offerActive?.estimatedTime || 0,
    state: offerActive?.state || 0,
    difficult: offerActive?.difficult || 0,
    technology: offerActive?.technology || [],
  };

  const { handleChange, values, setValues } = useFormik({
    initialValues,
    onSubmit: () => {},
  });

  const onSaveNote = () => {
    values.price = Number(values.price);
    // console.table(values);

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

  const onChangeTechnologies = (technologies: string[]) => {
    values.technology = technologies;
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
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [offerActive]);

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
        <Button onClick={onSaveNote} color={"info"} sx={{ p: 2 }}>
          <SaveOutlined sx={{ fontSize: 30, mr: 1 }} />
          Save
        </Button>
        <Button onClick={onDeleteNote} color='secondary' sx={{ p: 2 }}>
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
          name='description'
          value={values.description}
        />
        <TimeSelectorNoteView
          handleChange={handleChange}
          name='estimatedTime'
          estimatedTime={values.estimatedTime}
        />
        <StatusSelectNoteView
          handleChange={handleChange}
          name='state'
          state={values.state}
        />
        <DifficultSelectNoteView
          handleChange={handleChange}
          name='difficult'
          difficult={values.difficult}
        />
        <SelectRateForHourNoteView
          handleChange={handleChange}
          name='price'
          price={values.price}
        />
        <MultipleSelectChipNoteView
          onChangeTechnologies={onChangeTechnologies}
          technologiesSelected={offerActive?.technology}
        />
      </Grid>
      {/* <StandardImageList /> */}

      <Grid container>
        <TableNoteView />
      </Grid>
    </Grid>
  );
};
