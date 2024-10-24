import {useContext} from "react";
import {AudienceContext} from "../context/AudienceContext";

export const useAudience = () => {
    const context = useContext(AudienceContext);

    if (!context) {
        throw new Error('useAudience must be used within a AudienceProvider');
    }
    return context;
};