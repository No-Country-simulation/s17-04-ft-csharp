import { BrowserRouter } from "react-router-dom";
import { RouterApp } from "./app/routes/RouterApp";
import { Provider } from "react-redux";
import { store } from "./store";

export const JournalApp = () => {
  return (
    <>
      <Provider store={store}>
        <BrowserRouter>
          <RouterApp />
        </BrowserRouter>
      </Provider>
    </>
  );
};
