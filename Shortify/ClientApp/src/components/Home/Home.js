const Home = (props) => {
	console.log(props);
	document.body.style = "background: white;";
	window.location.href = `https://localhost:44322/api/shortify/${props.match.params.url}`;
	return null;
};

export default Home;
