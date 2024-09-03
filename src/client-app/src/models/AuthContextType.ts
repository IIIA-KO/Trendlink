import { AuthResponseType } from './AuthResponseType.ts';

export interface AuthContextType {
    user: AuthResponseType | null;
    login: (userData: AuthResponseType) => void;
    logout: () => void;
    refreshTokens: () => Promise<void>;
}