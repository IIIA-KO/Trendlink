import axiosInstance from './Api';
import { CountryType  } from '../models/CountryType';
import { StateType   } from '../models/StateType';

export const getCountries = async (): Promise<CountryType[]> => {
    const response = await axiosInstance.get('/countries');
    return response.data;
};

export const getStates = async (countryId: string): Promise<StateType[]> => {
    const response = await axiosInstance.get(`/countries/${countryId}/states`);
    return response.data;
};