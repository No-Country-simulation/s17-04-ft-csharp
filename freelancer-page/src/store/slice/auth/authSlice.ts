import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

interface AuthState {
  status?: "checking" | "not-authenticated" | "autenticated";
  uid: string | null | undefined;
  displayName: string | null | undefined;
  email: string | null | undefined;
  photoURL: string | null | undefined;
  errorMessage?: string | null | undefined;
}

const initialState: AuthState = {
  status: "not-authenticated",
  uid: "",
  displayName: "",
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
      state.displayName = action.payload.displayName;
      state.email = action.payload.email;
      state.photoURL = action.payload.photoURL;
      state.errorMessage = null;
    },
    logout: (state, action: PayloadAction<string>) => {
      state.status = "not-authenticated";
      state.uid = null;
      state.displayName = null;
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
