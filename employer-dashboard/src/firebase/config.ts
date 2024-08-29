import { initializeApp } from "firebase/app";
import { getAuth } from "firebase/auth";
import { getFirestore } from "firebase/firestore/lite";

const firebaseConfig = {
  apiKey: "AIzaSyBJwRxBMPLH4AILQYQTVLyeDQwmcleqMQI",
  authDomain: "journal-app-3bfd8.firebaseapp.com",
  projectId: "journal-app-3bfd8",
  storageBucket: "journal-app-3bfd8.appspot.com",
  messagingSenderId: "941296170031",
  appId: "1:941296170031:web:31c45d5c1572ff266a02da",
};

export const FirebaseApp = initializeApp(firebaseConfig);
export const FirebaseAuth = getAuth(FirebaseApp);
export const FirebaseDB = getFirestore(FirebaseApp);
