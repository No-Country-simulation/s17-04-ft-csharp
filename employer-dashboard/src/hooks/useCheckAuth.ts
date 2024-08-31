/* eslint-disable react-hooks/exhaustive-deps */
import { onAuthStateChanged } from "firebase/auth";
import { useEffect } from "react";
import { FirebaseAuth } from "../firebase/config";
import { login, logout, startLoadingNotes } from "../store";
import { useAppDispatch, useAppSelector } from "./hooks";

export const useCheckAuth = () => {
  const dispatch = useAppDispatch();
  const { status } = useAppSelector((state) => state.auth);

  useEffect(() => {
    onAuthStateChanged(FirebaseAuth, async (user) => {
      if (!user) return dispatch(logout(""));

      const { displayName, email, photoURL, uid } = user;
      dispatch(login({ displayName, email, photoURL, uid }));
      // dispatch(startLoadingNotes());
    });
  }, []);

  return status;
};
