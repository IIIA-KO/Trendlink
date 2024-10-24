import {ReviewType} from "../types/ReviewType";
import axiosInstance from "./api";
import {handleError} from "../utils/handleError";
import {ReviewInputType} from "../types/ReviewInputType";
import {UserReviewsQueryParamsType} from "../types/UserReviewsQueryParamsType";

export const getReviewById = async (id: string): Promise<ReviewType | null> => {
    try {
        const response = await axiosInstance.get(`/api/reviews/${id}`);
        return response.data as ReviewType;
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const updateReviewById = async (id: string, reviewData: ReviewInputType): Promise<void> => {
    try {
        await axiosInstance.put(`/api/reviews/${id}`, reviewData);
    } catch (error) {
        handleError(error);
    }
};

export const deleteReviewById = async (id: string): Promise<void> => {
    try {
        await axiosInstance.delete(`/api/reviews/${id}`);
    } catch (error) {
        handleError(error);
    }
};

export const getUserReviews = async (userId: string, params: UserReviewsQueryParamsType): Promise<ReviewType[] | null> => {
    try {
        const response = await axiosInstance.get(`/api/reviews/user/${userId}`, { params });
        return response.data as ReviewType[];
    } catch (error) {
        handleError(error);
        return null;
    }
};

export const createReview = async (cooperationId: string, reviewData: ReviewInputType): Promise<void> => {
    try {
        await axiosInstance.post(`/api/reviews/${cooperationId}`, reviewData);
    } catch (error) {
        handleError(error);
    }
};