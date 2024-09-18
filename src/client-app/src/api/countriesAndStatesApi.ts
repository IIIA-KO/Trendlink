import axiosInstance from './Api';
import { CountryType  } from '../models/CountryType';
import { StateType   } from '../models/StateType';
import axios from "axios";
import {handleError} from "../helpers/handleError";

export const getCountries = async (): Promise<CountryType[]> => {
    try {
        const response = await axiosInstance.get('/countries');
        return response.data;
    } catch (error: unknown) {
        handleError(error);
        return [];
    }
};

export const getStates = async (countryId: string): Promise<StateType[]> => {
    try {
        const response = await axiosInstance.get(`/countries/${countryId}/states`);
        return response.data;
    } catch (error: unknown) {
        handleError(error);
        return [];
    }
};