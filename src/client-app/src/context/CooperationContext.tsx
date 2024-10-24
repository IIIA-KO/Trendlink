import React, { createContext, ReactNode } from "react";
import {
    requestCooperation,
    confirmCooperation,
    rejectCooperation,
    cancelCooperation,
    markCooperationAsDone,
    completeCooperation,
} from "../services/cooperations";
import { handleError } from "../utils/handleError";
import { RequestCooperationType } from "../types/RequestCooperationType";
import {CooperationContextType} from "../types/CooperationContextType";

const CooperationContext = createContext<CooperationContextType | undefined>(undefined);

const CooperationProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const requestNewCooperation = async (cooperationData: RequestCooperationType) => {
        try {
            await requestCooperation(cooperationData);
        } catch (error) {
            handleError(error);
        }
    };

    const confirmCooperationById = async (id: string) => {
        try {
            await confirmCooperation(id);
        } catch (error) {
            handleError(error);
        }
    };

    const rejectCooperationById = async (id: string) => {
        try {
            await rejectCooperation(id);
        } catch (error) {
            handleError(error);
        }
    };

    const cancelCooperationById = async (id: string) => {
        try {
            await cancelCooperation(id);
        } catch (error) {
            handleError(error);
        }
    };

    const markAsDoneCooperationById = async (id: string) => {
        try {
            await markCooperationAsDone(id);
        } catch (error) {
            handleError(error);
        }
    };

    const completeCooperationById = async (id: string) => {
        try {
            await completeCooperation(id);
        } catch (error) {
            handleError(error);
        }
    };

    return (
        <CooperationContext.Provider
            value={{
                requestNewCooperation,
                confirmCooperationById,
                rejectCooperationById,
                cancelCooperationById,
                markAsDoneCooperationById,
                completeCooperationById,
            }}
        >
            {children}
        </CooperationContext.Provider>
    );
};

export { CooperationProvider, CooperationContext };
