import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Box from "@material-ui/core/Box";
import Collapse from "@material-ui/core/Collapse";
import IconButton from "@material-ui/core/IconButton";
import TableCell from "@material-ui/core/TableCell";
import TableRow from "@material-ui/core/TableRow";
import KeyboardArrowDownIcon from "@material-ui/icons/KeyboardArrowDown";
import KeyboardArrowUpIcon from "@material-ui/icons/KeyboardArrowUp";
import DeleteIcon from "@material-ui/icons/Delete";
import moment from "moment";
import "../../../styles/style.scss";
import { Row, Col } from "reactstrap";
import FileCopyIcon from "@material-ui/icons/FileCopy";
import Tooltip from "@material-ui/core/Tooltip";
import { CopyToClipboard } from "react-copy-to-clipboard";

const useRowStyles = makeStyles({
	root: {
		"& > *": {
			border: "unset",
			backgroundColor: "#424242",
			color: "#fff",
		},
	},
	collapsedTableCell: {
		paddingBottom: 0,
		paddingTop: 0,
		backgroundColor: "#424242",
		borderBottom: "0.5px solid #515151",
		borderTop: 0,
		color: "#fff",
	},
});

const Rows = (props) => {
	const maxLength = 30;
	const { row, index } = props;
	const [open, setOpen] = React.useState(false);
	const classes = useRowStyles();

	return (
		<React.Fragment>
			<TableRow className={classes.root}>
				<TableCell>
					<IconButton
						aria-label="expand row"
						size="small"
						onClick={() => setOpen(!open)}
						style={{
							color: "#fff",
						}}
					>
						{open ? (
							<KeyboardArrowUpIcon />
						) : (
							<KeyboardArrowDownIcon />
						)}
					</IconButton>
				</TableCell>
				<TableCell>
					{row.title.length > maxLength
						? row.title.substring(0, maxLength - 3) + "..."
						: row.title}
				</TableCell>
				<TableCell>{row.shortUrl}</TableCell>
				<TableCell align="right">{row.clicks}</TableCell>
			</TableRow>
			<TableRow>
				<TableCell className={classes.collapsedTableCell} colSpan={6}>
					<Collapse in={open} timeout="auto" unmountOnExit>
						<Box margin={1}>
							<Row xs="2" md="2" lg="2">
								<Col className="orignalUrl">
									<h6>Orignal Url</h6>
									<p>
										{row.orignalUrl.length > maxLength
											? row.orignalUrl.substring(
													0,
													maxLength - 3
											  ) + "..."
											: row.orignalUrl}
									</p>
								</Col>
								<Col className="expandedRowRight">
									<Row>
										<Col xs="6" md="6" sm="6">
											<CopyToClipboard
												text={row.shortUrl}
											>
												<Tooltip title="Copy">
													<IconButton
														style={{
															color: "#fff",
														}}
														aria-label="copy"
													>
														<FileCopyIcon />
													</IconButton>
												</Tooltip>
											</CopyToClipboard>
											<Tooltip title="Delete">
												<IconButton
													style={{
														color: "#fff",
													}}
													aria-label="delete"
													onClick={(event) => {
														props.handleDelete(
															event,
															row.id,
															index
														);
													}}
												>
													<DeleteIcon />
												</IconButton>
											</Tooltip>
										</Col>
										<Col xs="3" md="3" sm="3">
											<h6>Last Visited</h6>
											<p>
												{row.clicks > 0
													? moment(
															row.lastClicked
													  ).format("Do MMM YYYY")
													: "Not Yet Visited"}
											</p>
										</Col>
										<Col xs="3" md="3" sm="3">
											<h6>Created At</h6>
											<p>
												{moment(row.createdAt).format(
													"Do MMM YYYY"
												)}
											</p>
										</Col>
									</Row>
								</Col>
							</Row>
						</Box>
					</Collapse>
				</TableCell>
			</TableRow>
		</React.Fragment>
	);
};

export default Rows;
