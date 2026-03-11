import { FilterListOutlined } from "@mui/icons-material";
import {
  Avatar,
  Button,
  Divider,
  IconButton,
  Toolbar,
  Tooltip,
  Typography,
} from "@mui/material";
import Box from "@mui/material/Box";
import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import * as React from "react";

interface Data {
  id: number;
  userName: string;
  mainRol: string;
  technologies: string;
  showPorfile: string;
}

function createData(
  id: number,
  userName: string,
  mainRol: string,
  technologies: string,
  showPorfile: string
): Data {
  return {
    id,
    userName,
    mainRol,
    technologies,
    showPorfile,
  };
}

const rows = [
  createData(1, "Juan Perez", "Back-end", "Java", "https/..."),
  createData(2, "Juan Perez", "Back-end", "C#", "https/..."),
  createData(3, "Juan Perez", "Back-end", "Python", "https/..."),
  createData(4, "Juan Perez", "Back-end", "Node", "https/..."),
  createData(5, "Juan Perez", "Back-end", "Java", "https/..."),
  createData(6, "Juan Perez", "Back-end", "Java", "https/..."),
  createData(7, "Juan Perez", "Back-end", "C#", "https/..."),
  createData(8, "Juan Perez", "Back-end", "Python", "https/..."),
  createData(9, "Juan Perez", "Back-end", "Node", "https/..."),
  createData(10, "Juan Perez", "Back-end", "Java", "https/..."),
  createData(11, "Juan Perez", "Back-end", "Java", "https/..."),
  createData(12, "Juan Perez", "Back-end", "C#", "https/..."),
  createData(13, "Juan Perez", "Back-end", "Python", "https/..."),
  createData(14, "Juan Perez", "Back-end", "Node", "https/..."),
  createData(15, "Juan Perez", "Back-end", "Java", "https/..."),
  createData(16, "Juan Perez", "Back-end", "Java", "https/..."),
  createData(17, "Juan Perez", "Back-end", "C#", "https/..."),
  createData(18, "Juan Perez", "Back-end", "Python", "https/..."),
  createData(19, "Juan Perez", "Back-end", "Node", "https/..."),
  createData(20, "Juan Perez", "Back-end", "Java", "https/..."),
];

interface HeadCell {
  label: string;
}

const headCells: readonly HeadCell[] = [
  {
    label: "User",
  },
  {
    label: "Main Rol",
  },
  {
    label: "Technologies",
  },
  {
    label: "Actions",
  },
];

function EnhancedTableHead() {
  return (
    <TableHead>
      <TableRow>
        <TableCell padding='checkbox'></TableCell>
        {headCells.map((headCell, index) => (
          <TableCell
            key={index}
            align={headCell.label === "Actions" ? "right" : "inherit"}
          >
            {headCell.label}
          </TableCell>
        ))}
      </TableRow>
    </TableHead>
  );
}
export const TableNoteView = () => {
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(5);

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

  const visibleRows = React.useMemo(
    () => [...rows].slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage),
    [page, rowsPerPage]
  );

  return (
    <Box sx={{ width: "100%" }}>
      <Paper sx={{ width: "100%", mb: 2 }}>
        <TableContainer>
          <Toolbar
            sx={[
              {
                pl: { sm: 2 },
                pr: { xs: 1, sm: 1 },
              },
            ]}
          >
            <Typography
              sx={{ flex: "1 1 100%" }}
              variant='h6'
              id='tableTitle'
              component='div'
            >
              Freelancers' applications
            </Typography>

            <Tooltip title='Filter list'>
              <IconButton>
                <FilterListOutlined />
              </IconButton>
            </Tooltip>
          </Toolbar>
          <Divider />
          <Table sx={{ minWidth: 750 }} aria-labelledby='tableTitle'>
            <EnhancedTableHead />
            <TableBody>
              {visibleRows.map((row) => {
                return (
                  <TableRow
                    hover
                    role='checkbox'
                    tabIndex={-1}
                    key={row.id}
                    sx={{ cursor: "pointer" }}
                  >
                    <TableCell>
                      <Avatar>H</Avatar>
                    </TableCell>
                    <TableCell component='th' scope='row' padding='none'>
                      {row.userName}
                    </TableCell>
                    <TableCell>{row.mainRol}</TableCell>
                    <TableCell>{row.technologies}</TableCell>
                    <TableCell align='right'>
                      <Button>See profile</Button>
                    </TableCell>
                  </TableRow>
                );
              })}
              {emptyRows > 0 && (
                <TableRow>
                  <TableCell colSpan={6} />
                </TableRow>
              )}
            </TableBody>
          </Table>
        </TableContainer>
        <TablePagination
          rowsPerPageOptions={[5, 10, 25]}
          component='div'
          count={rows.length}
          rowsPerPage={rowsPerPage}
          page={page}
          onPageChange={handleChangePage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </Paper>
    </Box>
  );
};
