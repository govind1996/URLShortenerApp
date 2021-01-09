import React from "react";
import { connect } from "react-redux";
import LinksTable from "./Table/LinksTable";
import LoadingLinksTable from "./Table/LinksLoadingTable";

//TODO change imports

const LinksList = (props) => {
	return props.isLoading ? (
		<LoadingLinksTable />
	) : (
		<LinksTable links={props.links} handleDelete={props.handleDelete} />
	);
};
const mapStateToProps = (state) => {
	return {
		links: state.links.links,
		isLoading: state.links.isLoading || state.auth.isLoading,
	};
};
export default connect(mapStateToProps)(LinksList);
