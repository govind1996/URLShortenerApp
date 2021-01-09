import React from "react";
import { TableCell, TableHead, TableRow } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles({
	headerStyle: {
		backgroundColor: "black",
		borderBottom: "0.5px solid #515151",
		color: "#fff",
	},
});
const LinksTableHeader = () => {
	const classes = useStyles();
	return (
		<TableHead>
			<TableRow>
				<TableCell className={classes.headerStyle} />
				<TableCell className={classes.headerStyle} align="left">
					Title
				</TableCell>
				<TableCell className={classes.headerStyle} align="left">
					Short Url
				</TableCell>
				<TableCell className={classes.headerStyle} align="right">
					Total Visits
				</TableCell>
			</TableRow>
		</TableHead>
	);
};
export default LinksTableHeader;
