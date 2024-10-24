import {RequestCooperationType} from "./RequestCooperationType";

export interface CooperationContextType {
    requestNewCooperation: (cooperationData: RequestCooperationType) => Promise<void>;
    confirmCooperationById: (id: string) => Promise<void>;
    rejectCooperationById: (id: string) => Promise<void>;
    cancelCooperationById: (id: string) => Promise<void>;
    markAsDoneCooperationById: (id: string) => Promise<void>;
    completeCooperationById: (id: string) => Promise<void>;
}