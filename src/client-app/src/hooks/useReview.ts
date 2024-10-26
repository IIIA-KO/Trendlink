import {useContext} from "react";
import {ReviewContext} from "../context/ReviewContext";

export const useReview = () => {
    const context = useContext(ReviewContext);

    if (!context) {
        throw new Error('useReview must be used within a ReviewProvider');
    }
    return context;
};