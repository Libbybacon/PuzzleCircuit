import { createContext, useContext, useEffect, useState, type ReactNode } from 'react';
import { authApi, type CurrentUser, type RegisterRequest } from '@/api/auth';

interface AuthContextValue {
    user: CurrentUser | null;
    isLoading: boolean;
    login: (email: string, password: string) => Promise<void>;
    logout: () => Promise<void>;
    register: (data: RegisterRequest) => Promise<void>;
}

const AuthContext = createContext<AuthContextValue | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
    const [user, setUser] = useState<CurrentUser | null>(null);
    const [isLoading, setIsLoading] = useState(true);

    // Rehydrate session from existing cookie on mount
    useEffect(() => {
        authApi
            .me()
            .then(setUser)
            .catch(() => setUser(null))
            .finally(() => setIsLoading(false));
    }, []);

    const login = async (email: string, password: string) => {
        await authApi.login({ email, password });
        const me = await authApi.me();
        setUser(me);
    };

    const logout = async () => {
        await authApi.logout();
        setUser(null);
    };

    const register = async (data: RegisterRequest) => {
        await authApi.register(data);
    };

    return (
        <AuthContext.Provider value={{ user, isLoading, login, logout, register }}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth(): AuthContextValue {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error('useAuth must be used within <AuthProvider>');
    return ctx;
}
