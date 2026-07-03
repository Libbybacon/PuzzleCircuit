const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? '';

export class ApiError extends Error {
    constructor(
        public readonly status: number,
        public readonly body: unknown
    ) {
        super(`API error ${status}`);
        this.name = 'ApiError';
    }
}

export async function apiFetch<T>(
    path: string,
    options: RequestInit = {}
): Promise<T> {
    const response = await fetch(`${API_BASE_URL}${path}`, {
        ...options,
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json',
            ...(options.headers ?? {}),
        },
    });

    if (!response.ok) {
        let body: unknown = null;
        try {
            body = await response.json();
        } catch (_e) {
            // non-JSON error body
        }
        throw new ApiError(response.status, body);
    }

    if (response.status === 204) {
        return undefined as T;
    }

    const text = await response.text();
    return text ? (JSON.parse(text) as T) : (undefined as T);
}
