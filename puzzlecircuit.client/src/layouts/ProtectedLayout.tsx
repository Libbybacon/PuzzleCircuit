import { Outlet, useNavigate } from '@tanstack/react-router';
import { useEffect } from 'react';
import { useAuth } from '@/context/AuthContext';
import TopNavBar from '@/components/TopNavBar';

export default function ProtectedLayout() {
    const { user, isLoading } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        if (!isLoading && !user) {
            void navigate({ to: '/login' });
        }
    }, [user, isLoading, navigate]);

    if (isLoading) {
        return (
            <div className="h-100 flex items-center justify-center bg-[var(--bg)]">
                <div className="w-8 h-8 rounded-full border-2 border-[var(--border)] border-t-[var(--accent)] animate-spin" />
            </div>
        );
    }

    if (!user) return null;

    return (
        <div className="body h-100 w-100">
            <TopNavBar />
            <main className="main">
                <Outlet />
            </main>
        </div>
    );
}
