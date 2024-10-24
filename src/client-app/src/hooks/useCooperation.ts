import {useContext} from "react";
import {CooperationContext} from "../context/CooperationContext";

export const useCooperation = () => {
    const context = useContext(CooperationContext);

    if (!context) {
        throw new Error('useCooperation must be used within a CooperationProvider');
    }
    return context;
};