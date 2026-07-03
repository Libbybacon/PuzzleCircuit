import { createRouter, createRoute, createRootRoute, Outlet, redirect } from '@tanstack/react-router';
import { AuthProvider } from '@/context/AuthContext';
import AuthLayout from '@/layouts/AuthLayout';
import ProtectedLayout from '@/layouts/ProtectedLayout';
import LoginPage from '@/pages/LoginPage';
import RegisterPage from '@/pages/RegisterPage';
import DashboardPage from '@/pages/DashboardPage';
import EventsPage from '@/pages/EventsPage';

// Root

const rootRoute = createRootRoute({
    component: () => (
        <AuthProvider>
            <Outlet />
        </AuthProvider>
    ),
});

// Index: redirect / → /dashboard

const indexRoute = createRoute({
    getParentRoute: () => rootRoute,
    path: '/',
    beforeLoad: () => {
        throw redirect({ to: '/dashboard' });
    },
});

// Public layout (redirect to /dashboard if already authenticated)

const authLayoutRoute = createRoute({
    getParentRoute: () => rootRoute,
    id: '_auth',
    component: AuthLayout,
});

const loginRoute = createRoute({
    getParentRoute: () => authLayoutRoute,
    path: '/login',
    component: LoginPage,
});

const registerRoute = createRoute({
    getParentRoute: () => authLayoutRoute,
    path: '/register',
    component: RegisterPage,
});

// Protected layout (redirect to /login if not authenticated)

const protectedLayoutRoute = createRoute({
    getParentRoute: () => rootRoute,
    id: '_protected',
    component: ProtectedLayout,
});

const dashboardRoute = createRoute({
    getParentRoute: () => protectedLayoutRoute,
    path: '/dashboard',
    component: DashboardPage,
});

const eventsRoute = createRoute({
    getParentRoute: () => protectedLayoutRoute,
    path: '/events',
    component: EventsPage,
});

// Route tree

const routeTree = rootRoute.addChildren([
    indexRoute,
    authLayoutRoute.addChildren([loginRoute, registerRoute]),
    protectedLayoutRoute.addChildren([dashboardRoute, eventsRoute]),
]);

export const router = createRouter({ routeTree });

// Register router type for useNavigate / Link type-safety
declare module '@tanstack/react-router' {
    interface Register {
        router: typeof router;
    }
}
