import {
  Box,
  Button,
  Chip,
  Divider,
  Grid2 as Grid,
  Typography,
} from "@mui/material";
import { TableApplicationDetail } from "./ApplicationDetail/components/TableApplicationDetail";

export const ApplicationDetail = () => {
  return (
    <Box
      bgcolor={"Background"}
      overflow={"auto"}
      sx={{
        boxShadow: "10px 10px 35px -15px rgba(0,0,0,0.7)",
      }}
      height={"calc(100vh - 80px)"}
      width={"70%"}
    >
      <Grid container margin={8}>
        <Grid display={"flex"} flexDirection={"column"} gap={2}>
          <Box display={"flex"} flexDirection={"column"}>
            <Typography variant='h5' fontWeight={"bold"}>
              Front-end developer
            </Typography>
            <Typography variant='subtitle2'>
              Hecho por No Country SRL.
            </Typography>
          </Box>
          <Box display={"flex"} gap={1}>
            <Chip color='primary' label='React' />
            <Chip color='primary' label='TypeScript' />
            <Chip color='primary' label='C#' />
            <Chip color='primary' label='MySQL' />
          </Box>
          <Typography textAlign={"justify"}>
            Est officia ea minim elit nostrud aliquip ut cillum aute labore
            pariatur. Sint enim deserunt exercitation officia consectetur veniam
            do culpa adipisicing mollit ullamco. Tempor magna non nostrud ad
            quis eu non laborum minim in amet et. Ipsum anim magna tempor cillum
            consequat eiusmod dolor dolore nisi. Ad non mollit cillum irure est
            dolor nisi velit pariatur laborum aliqua enim. Tempor aliqua quis
            duis excepteur consectetur consectetur ipsum laborum duis aute. Id
            nisi cillum exercitation mollit amet nostrud qui cupidatat qui.
            Pariatur nulla magna est proident minim irure. Est reprehenderit
            irure irure ut ullamco veniam aute anim sit. Minim magna id ex ea
            pariatur incididunt eiusmod in culpa occaecat voluptate proident
            consectetur laboris. Dolore nisi amet esse sunt anim cillum dolor in
            qui quis esse. Excepteur ut ad pariatur aute tempor dolore. Et eu
            sit nulla qui aute eu quis incididunt occaecat mollit minim. Ex sit
            aute aliquip magna sint occaecat cillum proident tempor est nulla
            irure. Eiusmod ut ut ut aliquip irure aliqua dolor qui deserunt elit
            quis nulla. Pariatur commodo cupidatat sit quis do adipisicing ad
            est. Magna et occaecat eu fugiat consequat consequat. Non
            adipisicing proident tempor deserunt cupidatat officia aute. Quis
            deserunt dolore ad occaecat. Est nisi ea Lorem proident ipsum
            eiusmod mollit. Ex pariatur fugiat est amet eiusmod velit ullamco et
            pariatur minim ex quis.
          </Typography>
          {/* <Divider /> */}
          <TableApplicationDetail />
          <Button variant='contained' size='large'>
            Postularse
          </Button>
          {/* <Box display={"flex"}>
            <Typography variant='h6'>Precio por hora: $</Typography>
            <Typography variant='h5' fontWeight={"bold"}>
              10
            </Typography>
          </Box>
          <Box display={"flex"} gap={1}>
            <Typography variant='h6'>Tiempo estipulado</Typography>
            <Typography variant='h5' fontWeight={"bold"}>
              una semana
            </Typography>
          </Box>
          <Box display={"flex"} gap={1}>
            <Typography variant='h6'>Dificultad:</Typography>
            <Typography variant='h5' fontWeight={"bold"}>
              Media
            </Typography>
          </Box> */}
        </Grid>
      </Grid>
    </Box>
  );
};
