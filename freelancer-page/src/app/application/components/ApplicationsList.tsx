import { Box, List, Pagination, Typography } from "@mui/material";
import { Offer } from "../../../@types/types";
import { SideBarItem } from "../../offer/components/SideBarItem";

export const ApplicationsList = () => {
  return (
    <Box
      overflow={"auto"}
      bgcolor={"info.main"}
      height={"calc(100vh - 80px)"}
      width={"30%"}
      sx={{ boxShadow: "-10px 10px 35px -15px rgba(0,0,0,0.7)" }}
    >
      <List disablePadding>
        <Box bgcolor={"primary.main"} paddingX={2} paddingY={1}>
          <Typography variant='h6'>React</Typography>
          <Typography variant='subtitle2'>1.230 resultados</Typography>
        </Box>
        {[
          {
            id: 1,
            title: "Front-end developer",
            description: "No Country SRL.",
            price: 2,
            estimatedTime: 2,
            state: 2,
            difficult: 2,
            technology: ["React"],
          },
          {
            id: 1,
            title: "Front-end developer",
            description: "No Country SRL.",
            price: 2,
            estimatedTime: 2,
            state: 2,
            difficult: 2,
            technology: ["React"],
          },
          {
            id: 1,
            title: "Front-end developer",
            description: "No Country SRL.",
            price: 2,
            estimatedTime: 2,
            state: 2,
            difficult: 2,
            technology: ["React"],
          },
          {
            id: 1,
            title: "Front-end developer",
            description: "No Country SRL.",
            price: 2,
            estimatedTime: 2,
            state: 2,
            difficult: 2,
            technology: ["React"],
          },
          {
            id: 1,
            title: "Front-end developer",
            description: "No Country SRL.",
            price: 2,
            estimatedTime: 2,
            state: 2,
            difficult: 2,
            technology: ["React"],
          },
          {
            id: 1,
            title: "Front-end developer",
            description: "No Country SRL.",
            price: 2,
            estimatedTime: 2,
            state: 2,
            difficult: 2,
            technology: ["React"],
          },
          {
            id: 1,
            title: "Front-end developer",
            description: "No Country SRL.",
            price: 2,
            estimatedTime: 2,
            state: 2,
            difficult: 2,
            technology: ["React"],
          },
          {
            id: 1,
            title: "Front-end developer",
            description: "No Country SRL.",
            price: 2,
            estimatedTime: 2,
            state: 2,
            difficult: 2,
            technology: ["React"],
          },
          {
            id: 1,
            title: "Front-end developer",
            description: "No Country SRL.",
            price: 2,
            estimatedTime: 2,
            state: 2,
            difficult: 2,
            technology: ["React"],
          },
          {
            id: 1,
            title: "Front-end developer",
            description: "No Country SRL.",
            price: 2,
            estimatedTime: 2,
            state: 2,
            difficult: 2,
            technology: ["React"],
          },
          {
            id: 1,
            title: "Front-end developer",
            description: "No Country SRL.",
            price: 2,
            estimatedTime: 2,
            state: 2,
            difficult: 2,
            technology: ["React"],
          },
        ].map((note: Offer) => (
          <SideBarItem key={note.id} note={note} />
        ))}
        <Box display={"flex"} justifyContent={"center"} my={2}>
          <Pagination count={5} color='primary' />
        </Box>
      </List>
    </Box>
  );
};
