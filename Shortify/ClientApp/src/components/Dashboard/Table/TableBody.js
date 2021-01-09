import React from "react";
import { TableCell, TableRow, TableBody } from "@material-ui/core";
import Row from "./Row";

const LinksTableBody = (props) => {
	const emptyRows =
		props.rowsPerPage -
		Math.min(
			props.rowsPerPage,
			props.rows.length - props.page * props.rowsPerPage
		);

	return (
		<TableBody
			style={{
				border: 0,
			}}
		>
			{props.rows
				.slice(
					props.page * props.rowsPerPage,
					props.page * props.rowsPerPage + props.rowsPerPage
				)
				.map((row, index) => {
					return (
						<Row
							key={row.id}
							row={row}
							index={index}
							handleDelete={props.handleDelete}
						/>
					);
				})}
			{emptyRows > 0 && (
				<TableRow style={{ height: 53 * emptyRows }}>
					<TableCell
						style={{
							border: 0,
						}}
						colSpan={6}
					/>
				</TableRow>
			)}
		</TableBody>
	);
};

export default LinksTableBody;
