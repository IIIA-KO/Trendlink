import {ReviewType} from "./ReviewType";
import {ReviewInputType} from "./ReviewInputType";
import {UserReviewsQueryParamsType} from "./UserReviewsQueryParamsType";

export interface ReviewContextType {
    review: ReviewType | null;
    userReviews: ReviewType[] | null;
    fetchReviewById: (id: string) => Promise<void>;
    updateReview: (id: string, reviewData: ReviewInputType) => Promise<void>;
    deleteReview: (id: string) => Promise<void>;
    fetchUserReviews: (userId: string, params: UserReviewsQueryParamsType) => Promise<void>;
    createNewReview: (cooperationId: string, reviewData: ReviewInputType) => Promise<void>;
}