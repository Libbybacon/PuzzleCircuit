import { Outlet, useNavigate } from '@tanstack/react-router';
import { useEffect } from 'react';
import { useAuth } from '@/context/AuthContext';

/**
 * Wraps public-only routes (login, register).
 * Redirects to /dashboard if the user already has an active session.
 */
export default function AuthLayout() {
    const { user, isLoading } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        if (!isLoading && user) {
            void navigate({ to: '/dashboard' });
        }
    }, [user, isLoading, navigate]);

    if (isLoading) return <AppSpinner />;

    return (
        <div className="vh-100 bg-teal-dk p-4 text-mint-mid">
            <div className="w-100">
                <div className="d-flex justify-content-center mb-2">
                    <div className="d-flex align-items-center">
                        <h1 className="title-text">
                            Puzzle
                        </h1>
                    </div>
                    <img
                        src="/logo.png"
                        width="6%"
                        min-width="80px"
                        max-width="150px"
                        className="d-inline-block align-middle mx-3"
                        alt="Puzzle Circuit logo"
                    />
                    <div className="d-flex align-items-center">
                        <h1 className="title-text">
                            Circuit
                        </h1>
                    </div>

                </div>
                <div className="title-subtext mb-2">
                    A place to connect, compete, and share a passion for puzzling
                </div>
                <div className="title-subtext mb-2">
                    Join the Puzzle Circuit to discover and host puzzling events near you
                </div>
                <Outlet />
            </div>
        </div>
    );
}

function AppSpinner() {
    return (
        <div className="min-h-screen flex items-center justify-center bg-[var(--bg)]">
            <div className="w-8 h-8 rounded-full border-2 border-[var(--border)] border-t-[var(--accent)] animate-spin" />
        </div>
    );
}
