import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { Note } from "../../../@types/types";

interface journalState {
  isSaving: boolean;
  messageSaved: string;
  notes: Note[];
  noteActive: Note | null;
}

const initialState: journalState = {
  isSaving: false,
  messageSaved: "",
  notes: [],
  noteActive: null,
};

export const journalSlice = createSlice({
  name: "journal",
  initialState,
  reducers: {
    setSaving: (state) => {
      state.isSaving = !state.isSaving;
    },
    savingNewNote: (state) => {
      state.isSaving = true;
    },
    addNewEmptyNote: (state, action: PayloadAction<Note>) => {
      state.notes.push(action.payload);
      state.isSaving = false;
    },
    setActiveNote: (state, action: PayloadAction<Note>) => {
      state.noteActive = action.payload;
    },
    setNotes: (state, action: PayloadAction<Note[]>) => {
      state.notes = action.payload;
    },
    updatedNotes: (state, action: PayloadAction<Note[]>) => {
      state.notes = action.payload;
    },
    udpateNote: (state, action: PayloadAction<Note>) => {
      state.noteActive = action.payload;
      state.notes = state.notes.map((note) => {
        if (note.id === action.payload.id) return action.payload;
        return note;
      });
      state.isSaving = false;
    },
    deleteNoteById: (state, action: PayloadAction<string>) => {
      state.notes = state.notes.filter((note) => note.id !== action.payload);
      state.noteActive = null;
      state.isSaving = false;
    },
    setUploadImages: (state, action: PayloadAction<string>) => {
      state.noteActive?.imageURLs?.push(action.payload);
      state.isSaving = false;
    },
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
  setUploadImages,
} = journalSlice.actions;
