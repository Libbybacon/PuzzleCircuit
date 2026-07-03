import { useState } from 'react';
import { useNavigate, Link } from '@tanstack/react-router';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { useAuth } from '@/context/AuthContext';
import { ApiError } from '@/api/apiClient';

const schema = z.object({
    email: z.string().email('Enter a valid email address'),
    password: z.string().min(1, 'Password is required'),
});

type FormData = z.infer<typeof schema>;

export default function LoginPage() {
    const { login } = useAuth();
    const navigate = useNavigate();
    const [serverError, setServerError] = useState<string | null>(null);

    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<FormData>({
        resolver: zodResolver(schema),
    });

    const onSubmit = async (data: FormData) => {
        setServerError(null);
        try {
            await login(data.email, data.password);
            void navigate({ to: '/dashboard' });
        } catch (err) {
            if (err instanceof ApiError && err.status === 401) {
                setServerError('Invalid email or password.');
            } else {
                setServerError('Something went wrong. Please try again.');
            }
        }
    };

    return (
        <div className="flex-center">
            <div className="login-box px-5">
                <h2 className="text-center mb-1">Sign in</h2>
                <p className="text-center mb-5">
                    Welcome back! Enter your credentials to continue.
                </p>

                <form onSubmit={handleSubmit(onSubmit)} noValidate className="flex flex-col gap-4">
                    {/* Email */}
                    <div className="mb-4 px-5">
                        <div className="d-flex">
                        <label htmlFor="email" className="me-3">
                            Email
                        </label>
                        <input
                            id="email"
                            type="email"
                            autoComplete="email"
                            {...register('email')}
                            className="form-control"
                            />
                        </div>

                        {errors.email && (
                            <p className="form-error">{errors.email.message}</p>
                        )}
                    </div>

                    {/* Password */}
                    <div className="mb-4 px-5">
                        <div className="d-flex">
                            <label htmlFor="password" className="text-sm font-medium me-3">
                                Password
                            </label>
                            <input
                                id="password"
                                type="password"
                                autoComplete="current-password"
                                {...register('password')}
                                className="form-control"
                            />
                        </div>
                        {errors.password && (
                            <p className="form-error">{errors.password.message}</p>
                        )}
                    </div>

                    {/* Server error */}
                    {serverError && (
                        <p className="form-error">
                            {serverError}
                        </p>
                    )}
                    <div className="flex-center">
                        <button
                            type="submit"
                            disabled={isSubmitting}
                            className="pc-btn"
                        >
                            {isSubmitting ? 'Signing in…' : 'Sign in'}
                        </button>
                    </div>
                </form>

                <p className="mt-5 text-center text-sm text-[var(--text)]">
                    Don't have an account?{' '}
                    <Link to="/register" className="text-[var(--accent)] font-medium hover:underline">
                        Create one
                    </Link>
                </p>
            </div>
        </div>
    );
}
