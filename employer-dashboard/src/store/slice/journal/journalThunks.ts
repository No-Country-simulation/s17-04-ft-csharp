import { Dispatch } from "@reduxjs/toolkit";
import { collection, deleteDoc, doc, setDoc } from "firebase/firestore/lite";
import { FirebaseDB } from "../../../firebase/config";

import { Note } from "../../../@types/types";
import { loadNotes } from "../../../helpers";
import { RootState } from "../../store";
import {
  addNewEmptyNote,
  deleteNoteById,
  savingNewNote,
  setActiveNote,
  setNotes,
  setSaving,
  setUploadImages,
  udpateNote,
} from "./journalSlice";

export const startCreationNote = () => {
  return async (dispatch: Dispatch, getState: () => RootState) => {
    const { uid } = getState().auth;

    dispatch(savingNewNote());

    const newNote: Note = {
      title: "New title",
      body: "Dolore minim esse culpa ullamco.",
      date: Date.now(),
      imageURLs: [],
    };

    const newDoc = await doc(collection(FirebaseDB, `${uid}/journal/notes/`));

    await setDoc(newDoc, newNote);

    newNote.id = newDoc.id;

    dispatch(addNewEmptyNote(newNote));
    dispatch(setActiveNote(newNote));
  };
};

export const startLoadingNotes = () => {
  return async (dispatch: Dispatch, getState: () => RootState) => {
    const { uid } = getState().auth;
    if (!uid) throw new Error("UID is undefined");

    const res = await loadNotes(uid);
    dispatch(setNotes(res));
  };
};

export const startUpdateNote = ({
  title,
  body,
}: {
  title: string;
  body: string;
}) => {
  return async (dispatch: Dispatch, getState: () => RootState) => {
    dispatch(savingNewNote());

    const { uid } = getState().auth;
    const { noteActive } = getState().journal;
    if (!noteActive) throw new Error("noteActive is empty");

    const noteForFirebase = {
      ...noteActive,
      title,
      body,
    };
    delete noteForFirebase.id;

    try {
      const docRef = doc(FirebaseDB, `${uid}/journal/notes/${noteActive?.id}`);
      await setDoc(docRef, noteForFirebase, { merge: true });
    } catch (error) {
      console.log(error);
    }

    dispatch(
      udpateNote({
        title: title,
        body: body,
        date: noteActive?.date,
        imageURLs: noteActive?.imageURLs,
        id: noteActive?.id,
      })
    );
  };
};

export const startDeleteNote = () => {
  return async (dispatch: Dispatch, getState: () => RootState) => {
    dispatch(savingNewNote());
    const { noteActive } = getState().journal;
    const { uid } = getState().auth;

    const docRef = doc(FirebaseDB, `${uid}/journal/notes/${noteActive?.id}`);
    await deleteDoc(docRef);

    dispatch(deleteNoteById(noteActive?.id as string));
  };
};

export const startUploadImages = (files: FileList) => {
  return async (dispatch: Dispatch) => {
    if (files.length <= 0) throw new Error("$files is empty");

    dispatch(setSaving());

    console.log("startUploadImages", files);

    for (const file of files) {
      const formData = new FormData();
      formData.append("upload_preset", "journal-app");
      formData.append("file", file);

      try {
        const resp = await fetch(
          "https://api.cloudinary.com/v1_1/react-exercises/upload",
          {
            method: "POST",
            body: formData,
          }
        );

        if (!resp.ok) throw new Error("Couldn't upload image");
        const data = await resp.json();

        dispatch(setUploadImages(data.secure_url));
      } catch (error) {
        console.error(error);
      }
    }
  };
};
