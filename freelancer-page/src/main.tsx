import { StrictMode } from "react";
import { createRoot } from "react-dom/client";

import "./index.css";
import { JournalApp } from "./JournalApp";
import { ThemeApp } from "./theme/ThemeApp";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ThemeApp>
      <JournalApp />
    </ThemeApp>
  </StrictMode>
);
