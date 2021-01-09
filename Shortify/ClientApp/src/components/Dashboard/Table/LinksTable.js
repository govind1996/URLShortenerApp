import React from "react";
import { TableContainer, Paper, Table } from "@material-ui/core";
import LinksTableHeader from "./TableHeader";
import LinksTableBody from "./TableBody";
import { makeStyles } from "@material-ui/core/styles";
import TablePagination from "@material-ui/core/TablePagination";

const useStyles = makeStyles({
	table: {
		minWidth: 650,
		backgroundColor: "#424242",
		borderBottom: "unset",
	},
	container: {
		maxHeight: 440,
		borderRight: "unset",
		backgroundColor: "#424242",
	},
	pagination: {
		borderTop: "0px",
		background: "#424242",
		color: "#fff",
	},
});
const LinksTable = (props) => {
	const classes = useStyles();

	//pagination
	const [page, setPage] = React.useState(0);
	const [rowsPerPage, setRowsPerPage] = React.useState(5);

	const handleChangePage = (event, newPage) => {
		setPage(newPage);
	};

	const handleChangeRowsPerPage = (event) => {
		setRowsPerPage(parseInt(event.target.value, 10));
		setPage(0);
	};

	return (
		<TableContainer component={Paper} className={classes.container}>
			<Table
				className={classes.table}
				stickyHeader
				aria-label="sticky table"
			>
				<LinksTableHeader />
				<LinksTableBody
					rows={props.links}
					page={page}
					rowsPerPage={rowsPerPage}
					handleDelete={props.handleDelete}
				/>
			</Table>
			<TablePagination
				className={classes.pagination}
				rowsPerPageOptions={[5, 10, 25]}
				component="div"
				count={props.links.length}
				rowsPerPage={rowsPerPage}
				page={page}
				onChangePage={handleChangePage}
				onChangeRowsPerPage={handleChangeRowsPerPage}
			/>
		</TableContainer>
	);
};

export default LinksTable;
