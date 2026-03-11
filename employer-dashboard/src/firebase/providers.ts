import {
  createUserWithEmailAndPassword,
  GoogleAuthProvider,
  signInWithEmailAndPassword,
  signInWithPopup,
  updateProfile,
} from "firebase/auth";
import { FirebaseAuth } from "./config";

const googleAuthProvider = new GoogleAuthProvider();

export const signInWithGoogle = async () => {
  try {
    const result = await signInWithPopup(FirebaseAuth, googleAuthProvider);
    const { displayName, uid, email, photoURL } = result.user;

    return {
      ok: true,
      displayName,
      uid,
      email,
      photoURL,
    };
  } catch (error: unknown) {
    if (error instanceof Error) {
      return {
        ok: false,
        errorMessage: error.message,
      };
    }
  }
};

export const registerUserWithEmailAndPassword = async ({
  email = "",
  password = "",
  displayName = "",
}) => {
  try {
    const res = await createUserWithEmailAndPassword(
      FirebaseAuth,
      email,
      password
    );

    const { uid, photoURL = "" } = res.user;

    if (!FirebaseAuth.currentUser)
      throw new Error("$FirebaseAuth.currentUser is null");
    if (!photoURL) throw new Error("$photoURL is null");

    await updateProfile(FirebaseAuth.currentUser, { displayName, photoURL });

    return {
      ok: true,
      email,
      password,
      displayName,
      uid,
      photoURL,
    };
  } catch (error: unknown) {
    if (error instanceof Error) {
      return {
        ok: false,
        errorMessage: error.message,
      };
    }
  }
};

export const signInWithEmailPassword = async ({
  password = "",
  email = "",
}) => {
  try {
    const res = await signInWithEmailAndPassword(FirebaseAuth, email, password);

    const { displayName, uid, photoURL } = res.user;

    return {
      ok: true,
      displayName,
      uid,
      email,
      photoURL,
    };
  } catch (error: unknown) {
    if (error instanceof Error) {
      return {
        ok: false,
        errorMessage: error.message,
      };
    }
  }
};

export const logOutUserFirebase = async () => {
  return await FirebaseAuth.signOut();
};
