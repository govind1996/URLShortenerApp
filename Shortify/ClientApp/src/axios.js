import axios from "axios";

const instance = axios.create({
	baseURL: "https://localhost:44322/api/",
});
instance.defaults.headers.common["Content-Type"] = "application/json";
export default instance;
