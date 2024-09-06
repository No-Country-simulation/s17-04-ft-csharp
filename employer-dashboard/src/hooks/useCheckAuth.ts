/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect } from "react";
import { startLoadingTechnologies, startLogin } from "../store";
import { useAppDispatch, useAppSelector } from "./hooks";

export const useCheckAuth = () => {
  const dispatch = useAppDispatch();
  const { status } = useAppSelector((state) => state.auth);

  useEffect(() => {
    dispatch(startLoadingTechnologies());
    dispatch(startLogin());

    // onAuthStateChanged(FirebaseAuth, async (user) => {
    //   if (!user) return dispatch(logout(""));

    //   const { displayName, email, photoURL, uid } = user;
    //   // dispatch(startLoadingNotes());
    // });
  }, []);

  return status;
};
