import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

interface AuthState {
  status?: "checking" | "not-authenticated" | "autenticated";
  uid: string | null | undefined;
  firstName: string | null | undefined;
  lastName: string | null | undefined;
  email: string | null | undefined;
  photoURL: string | null | undefined;
  errorMessage?: string | null | undefined;
}

const initialState: AuthState = {
  status: "not-authenticated",
  uid: "",
  firstName: "",
  lastName: "",
  email: "",
  photoURL: "",
  errorMessage: "",
};

export const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login: (state, action: PayloadAction<AuthState>) => {
      state.status = "autenticated";
      state.uid = action.payload.uid;
      state.firstName = action.payload.firstName;
      state.lastName = action.payload.lastName;
      state.email = action.payload.email;
      state.photoURL = action.payload.photoURL;
      state.errorMessage = null;
    },
    logout: (state, action: PayloadAction<string>) => {
      state.status = "not-authenticated";
      state.uid = null;
      state.firstName = null;
      state.lastName = null;
      state.email = null;
      state.photoURL = null;
      state.errorMessage = action.payload;
    },
    checkingCredentials: (state) => {
      state.status = "checking";
    },
  },
});

export const { login, logout, checkingCredentials } = authSlice.actions;
