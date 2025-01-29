import axios from "axios";

const API_BASE_URL = "http://localhost:8080/api";

export const getAccounts = async (customerId) => {
    return axios.get(`${API_BASE_URL}/user/${customerId}/details`);
};

export const createAccount = async (customerId, initialCredit) => {
    return axios.post(`${API_BASE_URL}/account/create`, { customerId, initialCredit });
};

export const addTransaction = async (accountId, amount) => {
    return axios.post(`${API_BASE_URL}/transaction/add`, { accountId, amount });
};
