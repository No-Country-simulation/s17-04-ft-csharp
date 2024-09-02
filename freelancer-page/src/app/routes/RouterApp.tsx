import { Navigate, Route, Routes } from "react-router-dom";
import { Loading } from "../../ui";
import { AuthRoute } from "../auth/routes/AuthRoute";
import { JournalRoutes } from "../offer/routes/JournalRoutes";
import { useCheckAuth } from "../../hooks";

export const RouterApp = () => {
  const status = useCheckAuth();

  if (status === "checking") {
    return <Loading />;
  }

  return (
    <Routes>
      {status === "not-authenticated" ? (
        <Route path='/auth/*' element={<AuthRoute />} />
      ) : (
        <Route path='/*' element={<JournalRoutes />} />
      )}
      <Route path='/*' element={<Navigate to={"/auth/login"} />} />
    </Routes>
  );
};
