import { configureStore } from "@reduxjs/toolkit";
import { authSlice } from "./slice/auth";
import { journalSlice } from "./slice/journal";

export const store = configureStore({
  reducer: {
    auth: authSlice.reducer,
    journal: journalSlice.reducer,
  },
});

export type AppStore = typeof store;
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
