import { Dispatch } from "@reduxjs/toolkit";

import { Offer } from "../../../@types/types";
import { RootState } from "../../store";
import {
  addNewEmptyNote,
  deleteNoteById,
  savingNewNote,
  setActiveNote,
  setTechnologies,
  // setUploadImages,
  udpateNote,
} from "./offerSlice";
import { loadTechnologies } from "../../../helpers/loadTechnologies";
import { login } from "../auth";

export const startCreationOffer = () => {
  return async (dispatch: Dispatch, getState: () => RootState) => {
    // const { uid } = getState().auth;
    const { offers } = getState().offer;

    dispatch(savingNewNote());

    const newOffer: Offer = {
      id: offers.length + 1,
      title: "New title",
      description: "Dolore minim esse culpa ullamco.",
      price: 0,
      estimatedTime: 0,
      state: 0,
      difficult: 0,
      technology: [],
    };

    // const newDoc = await doc(collection(FirebaseDB, `${uid}/offer/notes/`));

    // await setDoc(newDoc, newNote);

    // newNote.id = newDoc.id;

    dispatch(addNewEmptyNote(newOffer));
    dispatch(setActiveNote(newOffer));
  };
};

// export const startLoadingNotes = () => {
//   return async (dispatch: Dispatch, getState: () => RootState) => {
//     const { uid } = getState().auth;
//     if (!uid) throw new Error("UID is undefined");

//     const res = await loadNotes(uid);
//     dispatch(setNotes(res));
//   };
// };

export const startLoadingTechnologies = () => {
  return async (dispatch: Dispatch /* getState: () => RootState */) => {
    // const { uid } = getState().auth;
    // if (!uid) throw new Error("UID is undefined");

    const res = await loadTechnologies();

    dispatch(setTechnologies(res));
  };
};

export const startUpdateOffer = ({
  title,
  description,
  price,
  estimatedTime,
  state,
  difficult,
  technology,
}: Offer) => {
  return async (dispatch: Dispatch, getState: () => RootState) => {
    dispatch(savingNewNote());

    // const { uid } = getState().auth;
    const { offerActive } = getState().offer;
    if (!offerActive) throw new Error("noteActive is empty");

    // const noteForFirebase = {
    //   ...noteActive,
    //   title,
    //   body,
    // };
    // delete noteForFirebase.id;

    // try {
    //   const docRef = doc(FirebaseDB, `${uid}/offer/notes/${noteActive?.id}`);
    //   await setDoc(docRef, noteForFirebase, { merge: true });
    // } catch (error) {
    //   console.log(error);
    // }

    dispatch(
      udpateNote({
        id: offerActive.id,
        title: title,
        description: description,
        price: price || offerActive.price,
        estimatedTime: estimatedTime || offerActive.estimatedTime,
        state: state || offerActive.state,
        difficult: difficult || offerActive.difficult,
        technology: technology || offerActive.technology,
      })
    );
  };
};

export const startDeleteNote = () => {
  return async (dispatch: Dispatch, getState: () => RootState) => {
    dispatch(savingNewNote());
    const { offerActive: noteActive } = getState().offer;
    // const { uid } = getState().auth;

    // const docRef = doc(FirebaseDB, `${uid}/offer/notes/${noteActive?.id}`);
    // await deleteDoc(docRef);

    dispatch(deleteNoteById(noteActive?.id as number));
  };
};

// export const startUploadImages = (files: FileList) => {
//   return async (dispatch: Dispatch) => {
//     if (files.length <= 0) throw new Error("$files is empty");

//     dispatch(setSaving());

//     console.log("startUploadImages", files);

//     for (const file of files) {
//       const formData = new FormData();
//       formData.append("upload_preset", "offer-app");
//       formData.append("file", file);

//       try {
//         const resp = await fetch(
//           "https://api.cloudinary.com/v1_1/react-exercises/upload",
//           {
//             method: "POST",
//             body: formData,
//           }
//         );

//         if (!resp.ok) throw new Error("Couldn't upload image");
//         const data = await resp.json();

//         dispatch(setUploadImages(data.secure_url));
//       } catch (error) {
//         console.error(error);
//       }
//     }
//   };
// };
