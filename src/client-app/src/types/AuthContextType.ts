import { AuthResponseType } from './AuthResponseType';

export interface AuthContextType {
    user: AuthResponseType | null;
    login: (userData: AuthResponseType) => void;
    loading: boolean;
    logout: () => void;
    refreshTokens: () => Promise<AuthResponseType | null>;
}