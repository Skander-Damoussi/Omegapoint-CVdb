const axios = require("axios");

const defaultOptions = {
  baseURL: "https://localhost:44390/",
  headers: {}
};

let axiosInstance = axios.create(defaultOptions);

export default axiosInstance;
