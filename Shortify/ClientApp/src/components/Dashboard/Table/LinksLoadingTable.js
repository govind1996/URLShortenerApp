import React from "react";
import { TableContainer, Paper, Table } from "@material-ui/core";
import { LinearProgress } from "@material-ui/core";
import LinksTableHeader from "./TableHeader";

//TODO make loader in center
const LoadingLinksTable = () => {
	return (
		<div>
			<TableContainer component={Paper}>
				<Table stickyHeader aria-label="sticky table">
					<LinksTableHeader />
				</Table>
			</TableContainer>
			<LinearProgress />
			<br />
			<LinearProgress />
			<br />
			<LinearProgress />
			<br />
		</div>
	);
};

export default LoadingLinksTable;
