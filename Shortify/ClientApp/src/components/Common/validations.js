export const validateEmail = (email) => {
	let valid = true;
	const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

	email = email.trim();

	if (email === "") {
		valid = valid && false;
	}
	if (!re.test(email.toLowerCase())) {
		valid = valid && false;
	}

	return valid;
};

export const validatePassword = (password) => {
	let valid = true;

	password = password.trim();

	//not empty
	if (password === "") {
		valid = valid && false;
	}
	if (password.length < 6) {
		valid = valid && false;
	}
	//has a lowercase
	if (password.toUpperCase() === password) {
		valid = valid && false;
	}
	//has uppercase
	if (password.toLowerCase() === password) {
		valid = valid && false;
	}
	//has digit
	if (!/\d/.test(password)) {
		valid = valid && false;
	}
	//has alphanumeric
	let flag = true;
	for (let i = 0; i < password.length; i++) {
		let code = password.charCodeAt(i);
		if (
			!(code > 47 && code < 58) && // numeric (0-9)
			!(code > 64 && code < 91) && // upper alpha (A-Z)
			!(code > 96 && code < 123)
		) {
			flag = false;
			break;
		}
	}
	if (flag) {
		valid = valid && false;
	}

	return valid;
};

export const validateConfirmPassword = (password, confirmPassword, valid) => {
	if (password !== confirmPassword) {
		valid = valid && false;
	}
	return valid;
};

export const validateUsername = (username) => {
	let valid = true;
	username = username.trim();
	if (username === "") {
		valid = valid && false;
	}
	return valid;
};

export const validateUrl = (url) => {
	let valid = true;
	url = url.trim();
	const re = /([a-zA-Z]{3,}):\/\/([\w-]+\.)+[\w-]+(\/[\w- ./?%&=]*)?/;
	if (!re.test(url.toLowerCase())) {
		valid = valid && false;
	}
	return valid;
};
