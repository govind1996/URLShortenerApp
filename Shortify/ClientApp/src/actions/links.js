import axios from "../axios";

import { setResponse } from "./notification";

//actions for links reducer

export const setLoader = (isLoading) => ({
	type: "SET_LOADER",
	isLoading,
});

export const addLink = (link) => ({
	type: "ADD_LINK",
	link,
});

export const setLinks = (links) => ({
	type: "SET_LINKS",
	links,
});
export const setLinksFailed = () => ({
	type: "SET_LINKS_FAILED",
});
export const startAddLink = (url, token) => {
	return (dispatch) => {
		console.log(token);
		dispatch(setLoader(true));

		axios
			.post(
				"/shortify/ShortenUrl",
				{ url: url },
				{
					headers: {
						Authorization: `Bearer ${token}`,
					},
				}
			)
			.then((response) => {
				console.log(response);
				const link = {
					id: response.data.url.id,
					shortUrl: response.data.url.shortUrl,
					orignalUrl: response.data.url.orignalUrl,
					createdAt: response.data.url.createdAt,
					lastClicked: response.data.url.lastClicked,
					title: response.data.url.title,
				};

				dispatch(addLink(link));
				dispatch(setResponse(link));
			})
			.catch((error) => {
				console.log("addlinkfailed");
				dispatch(setLoader(false));
			});
	};
};
export const startSetLinks = (token) => {
	return (dispatch) => {
		dispatch(setLoader(true));
		axios
			.get("/shortify/Urls", {
				headers: {
					Authorization: `Bearer ${token}`,
				},
			})
			.then((response) => {
				console.log("success");
				const links = response.data.urlList;
				dispatch(setLoader(false));
				dispatch(setLinks(links));
			})
			.catch((error) => {
				console.log("setlinksfailed");
				//console.log(error);
				dispatch(setLoader(false));
				dispatch(setLinksFailed());
			});
	};
};
export const deleteLink = (index) => ({
	type: "DELETE_LINK",
	index,
});

export const startDeleteLink = (token, id, index) => {
	return (dispatch) => {
		console.log(id, index);
		dispatch(setLoader(true));
		axios
			.delete("/shortify/deleteUrl", {
				headers: {
					Authorization: `Bearer ${token}`,
				},
				data: {
					UrlId: id,
				},
			})
			.then((response) => {
				console.log(response.data);
				dispatch(deleteLink(id));
				dispatch(setLoader(false));
			})
			.catch((err) => {
				console.log(err);
				dispatch(setLoader(false));
			});
	};
};
