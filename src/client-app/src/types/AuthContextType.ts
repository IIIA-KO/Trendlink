import { AuthResponseType } from './AuthResponseType';

export interface AuthContextType {
    user: AuthResponseType | null;
    login: (userData: AuthResponseType) => void;
    logout: () => void;
    refreshTokens: () => Promise<void>;
}