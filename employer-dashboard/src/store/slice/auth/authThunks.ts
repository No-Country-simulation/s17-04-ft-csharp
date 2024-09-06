import { Dispatch } from "@reduxjs/toolkit";
import { jwtVerify, JWTPayload } from "jose";
import {
  logOutUserFirebase,
  signInWithEmailPassword,
  signInWithGoogle,
} from "../../../firebase/providers";
import { checkingCredentials, login, logout } from "./authSlice";
import { verifyToken } from "../../../helpers";

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

export const startLogin = () => {
  return async (dispatch: Dispatch /* getState: () => RootState */) => {
    // const { uid } = getState().auth;
    // if (!uid) throw new Error("UID is undefined");
    const token = localStorage.getItem("token");
    if (!token) return;

    const payload = await verifyToken(token);

    dispatch(
      login({
        uid: "",
        photoURL: "",
        email: payload.email,
        firstName: payload.name,
        lastName: payload.lastName,
      })
    );
  };
};

export const startSignInWithEmailPassword = ({ email = "", password = "" }) => {
  return async (dispatch: Dispatch) => {
    // dispatch(checkingCredentials());
    // const result = await signInWithEmailPassword({ email, password });
    try {
      const res = await fetch("https://juniorhub.somee.com/api/account/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      });
      const data = await res.json();

      if (!res.ok) {
        throw new Error("Error en la autenticación");
      }
      const token = data.token;
      localStorage.setItem("token", token);

      const payload = await verifyToken(token);

      dispatch(
        login({
          uid: "",
          photoURL: "",
          email: payload.email,
          firstName: payload.name,
          lastName: payload.lastName,
        })
      );

      // return token;
    } catch (error) {
      throw new Error("Error en la autenticación");
    }

    // if (result?.errorMessage === undefined)
    //   throw new Error("result is undefined");

    // if (!result?.ok) return dispatch(logout(result.errorMessage));
  };
};

export const startCreatingUserWithEmailPassword = ({
  email = "",
  password = "",
  firstName: name = "",
  lastName = "",
}) => {
  return async (dispatch: Dispatch) => {
    dispatch(checkingCredentials());

    const res = await fetch(
      "https://juniorhub.somee.com/api/account/register",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password, name, lastName, role: 0 }),
      }
    );
    // const data = await res.json();

    console.log("res:", res);

    if (!res.ok) {
      // console.error("error data:", data);
      return;
    }
    // console.log("well data :", data);
    dispatch(
      login({
        uid: "",
        photoURL: "",
        email,
        firstName: name,
        lastName,
      })
    );

    //TODO: if (!ok) return dispatch(logout(data));

    //* **CHECK IT**
    //* const res = await registerUserWithEmailAndPassword({
    //*   email,
    //*   password,
    //*   displayName,
    //* });

    //* if (!res || res.errorMessage === undefined)
    //*   throw new Error("variables of $res are empties");

    //* const { photoURL, uid, ok, errorMessage } = res;
  };
};

export const startLogOutUser = () => {
  return async (dispatch: Dispatch) => {
    dispatch(checkingCredentials());

    localStorage.removeItem("token");
    dispatch(logout(""));
  };
};
