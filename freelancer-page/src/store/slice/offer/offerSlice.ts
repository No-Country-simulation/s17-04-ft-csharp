import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { Offer } from "../../../@types/types";

interface offerState {
  isSaving: boolean;
  messageSaved: string;
  offers: Offer[];
  offerActive: Offer | null;
}

const initialState: offerState = {
  isSaving: false,
  messageSaved: "",
  offers: [],
  offerActive: null,
};

export const offerSlice = createSlice({
  name: "offer",
  initialState,
  reducers: {
    setSaving: (state) => {
      state.isSaving = !state.isSaving;
    },
    savingNewNote: (state) => {
      state.isSaving = true;
    },
    addNewEmptyNote: (state, action: PayloadAction<Offer>) => {
      state.offers.push(action.payload);
      state.isSaving = false;
    },
    setActiveNote: (state, action: PayloadAction<Offer>) => {
      state.offerActive = action.payload;
    },
    setNotes: (state, action: PayloadAction<Offer[]>) => {
      state.offers = action.payload;
    },
    updatedNotes: (state, action: PayloadAction<Offer[]>) => {
      state.offers = action.payload;
    },
    udpateNote: (state, action: PayloadAction<Offer>) => {
      state.offerActive = action.payload;
      state.offers = state.offers.map((note) => {
        if (note.id === action.payload.id) return action.payload;
        return note;
      });
      state.isSaving = false;
    },
    deleteNoteById: (state, action: PayloadAction<number>) => {
      state.offers = state.offers.filter((note) => note.id !== action.payload);
      state.offerActive = null;
      state.isSaving = false;
    },
    // setUploadImages: (state, action: PayloadAction<number>) => {
    //   state.noteActive?.imageURLs?.push(action.payload);
    //   state.isSaving = false;
    // },
  },
});

export const {
  addNewEmptyNote,
  setActiveNote,
  setNotes,
  setSaving,
  udpateNote,
  deleteNoteById,
  savingNewNote,
  // setUploadImages,
} = offerSlice.actions;
