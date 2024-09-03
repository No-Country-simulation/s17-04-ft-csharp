import { ApplicationsList } from "./components/ApplicationsList";
import { Navbar } from "./components/Navbar";
import { ApplicationLayout } from "./layout/ApplicationLayout";
import { ApplicationDetail } from "./view/ApplicationDetail";

export const ApplicationPage = () => {
  return (
    <>
      <Navbar />
      <ApplicationLayout>
        <ApplicationsList />
        <ApplicationDetail />
      </ApplicationLayout>
    </>
  );
};
