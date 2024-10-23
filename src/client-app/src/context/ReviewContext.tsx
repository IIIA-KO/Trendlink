import React, { createContext, ReactNode, useState } from "react";
import { getReviewById, updateReviewById, deleteReviewById, getUserReviews, createReview } from "../services/reviews";
import { ReviewType } from "../types/ReviewType";
import { ReviewInputType } from "../types/ReviewInputType";
import { UserReviewsQueryParamsType } from "../types/UserReviewsQueryParamsType";
import { handleError } from "../utils/handleError";
import {ReviewContextType} from "../types/ReviewContextType";

const ReviewContext = createContext<ReviewContextType | undefined>(undefined);

const ReviewProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [review, setReview] = useState<ReviewType | null>(null);
    const [userReviews, setUserReviews] = useState<ReviewType[] | null>(null);

    const fetchReviewById = async (id: string) => {
        try {
            const data = await getReviewById(id);
            setReview(data);
        } catch (error) {
            handleError(error);
        }
    };

    const updateReview = async (id: string, reviewData: ReviewInputType) => {
        try {
            await updateReviewById(id, reviewData);
            fetchReviewById(id);
        } catch (error) {
            handleError(error);
        }
    };

    const deleteReview = async (id: string) => {
        try {
            await deleteReviewById(id);
            setReview(null);
        } catch (error) {
            handleError(error);
        }
    };

    const fetchUserReviews = async (userId: string, params: UserReviewsQueryParamsType) => {
        try {
            const data = await getUserReviews(userId, params);
            setUserReviews(data);
        } catch (error) {
            handleError(error);
        }
    };

    const createNewReview = async (cooperationId: string, reviewData: ReviewInputType) => {
        try {
            await createReview(cooperationId, reviewData);
        } catch (error) {
            handleError(error);
        }
    };

    return (
        <ReviewContext.Provider
            value={{
                review,
                userReviews,
                fetchReviewById,
                updateReview,
                deleteReview,
                fetchUserReviews,
                createNewReview,
            }}
        >
            {children}
        </ReviewContext.Provider>
    );
};

export { ReviewProvider, ReviewContext };
