import { apiFetch } from './apiClient';

// ── Request shapes (must match server Contracts/Authentication.cs) ──────────

export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
    email: string;
    password: string;
    displayName: string;
    location?: string;
}

// ── Response shapes ──────────────────────────────────────────────────────────

export interface CurrentUser {
    id: string;
    email: string;
    displayName: string;
}

export interface AuthMessageResponse {
    reason: string;
    message: string;
}

export interface RegisterErrorResponse extends AuthMessageResponse {
    errors?: { code: string; description: string }[];
}

// ── API functions ────────────────────────────────────────────────────────────

export const authApi = {
    login: (data: LoginRequest) =>
        apiFetch<void>('/api/auth/login', {
            method: 'POST',
            body: JSON.stringify(data),
        }),

    register: (data: RegisterRequest) =>
        apiFetch<AuthMessageResponse>('/api/auth/register', {
            method: 'POST',
            body: JSON.stringify(data),
        }),

    logout: () =>
        apiFetch<AuthMessageResponse>('/api/auth/logout', {
            method: 'POST',
        }),

    me: () => apiFetch<CurrentUser>('/api/auth/me'),
};
