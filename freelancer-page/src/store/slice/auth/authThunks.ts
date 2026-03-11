import { Dispatch } from "@reduxjs/toolkit";
import {
  logOutUserFirebase,
  registerUserWithEmailAndPassword,
  signInWithEmailPassword,
  signInWithGoogle,
} from "../../../firebase/providers";
import { checkingCredentials, login, logout } from "./authSlice";

// export const checkingAuthentication = ({ email = "", password = "" }) => {
//   return async (dispatch: Dispatch) => {
//     dispatch(checkingCredentials());
//     dispatch(createAnAccount(email, password));
//   };
// };

export const startGoogleSignIn = () => {
  return async (dispatch: Dispatch) => {
    dispatch(checkingCredentials());
    const result = await signInWithGoogle();

    if (result?.errorMessage === undefined)
      throw new Error("result is undefined");

    if (!result?.ok) return dispatch(logout(result.errorMessage));

    dispatch(
      login({
        displayName: result.displayName,
        email: result.email,
        photoURL: result.photoURL,
        uid: result.uid,
      })
    );
  };
};

export const startSignInWithEmailPassword = ({ email = "", password = "" }) => {
  return async (dispatch: Dispatch) => {
    dispatch(checkingCredentials());
    const result = await signInWithEmailPassword({ email, password });

    if (result?.errorMessage === undefined)
      throw new Error("result is undefined");

    if (!result?.ok) return dispatch(logout(result.errorMessage));

    dispatch(
      login({
        displayName: result.displayName,
        email: result.email,
        photoURL: result.photoURL,
        uid: result.uid,
      })
    );
  };
};

export const startCreatingUserWithEmailPassword = ({
  email = "",
  password = "",
  displayName = "",
}) => {
  return async (dispatch: Dispatch) => {
    dispatch(checkingCredentials());

    const res = await registerUserWithEmailAndPassword({
      email,
      password,
      displayName,
    });

    if (!res || res.errorMessage === undefined)
      throw new Error("variables of $res are empties");

    const { photoURL, uid, ok, errorMessage } = res;

    if (!ok) return dispatch(logout(errorMessage));

    dispatch(
      login({
        email,
        displayName,
        photoURL,
        uid,
      })
    );
  };
};

export const startLogOutUser = () => {
  return async (dispatch: Dispatch) => {
    dispatch(checkingCredentials());
    await logOutUserFirebase();
    dispatch(logout(""));
  };
};
