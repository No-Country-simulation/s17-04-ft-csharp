import { Route, Routes } from "react-router-dom";
import { ApplicationPage } from "../application/ApplicationPage";

export const RouterApp = () => {
  // const status = useCheckAuth();

  // if (status === "checking") {
  //   return <Loading />;
  // }

  return (
    <Routes>
      {/* {status === "not-authenticated" ? (
        <Route path='/auth/*' element={<AuthRoute />} />
      ) : (
        <Route path='/*' element={<JournalRoutes />} />
      )} */}
      <Route path='/*' element={<ApplicationPage />} />
    </Routes>
  );
};
