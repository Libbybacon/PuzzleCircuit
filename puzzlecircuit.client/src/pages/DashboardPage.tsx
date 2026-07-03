import { useAuth } from '@/context/AuthContext';

export default function DashboardPage() {
    const { user } = useAuth();

    return (
        <div className="p-8">
            <div className="max-w-2xl mx-auto">
                <h1 className="text-2xl font-semibold text-[var(--text-h)] mb-8">
                    Welcome, {user?.displayName}
                </h1>
            </div>
        </div>
    );
}
