import { useState } from 'react';
import { useNavigate, Link } from '@tanstack/react-router';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { useAuth } from '@/context/AuthContext';
import { ApiError } from '@/api/apiClient';
import { type RegisterErrorResponse } from '@/api/auth';
import { US_STATES } from '@/constants/states';

const schema = z
    .object({
        email: z.string().email('Enter a valid email address'),
        displayName: z
            .string()
            .min(2, 'Display name must be at least 2 characters')
            .max(50, 'Display name must be 50 characters or fewer'),
        password: z
            .string()
            .min(8, 'Must be at least 8 characters')
            .regex(/[0-9]/, 'Must contain at least one digit')
            .regex(/[a-z]/, 'Must contain at least one lowercase letter'),
        confirmPassword: z.string(),
        location: z.string().optional(),
    })
    .refine((d) => d.password === d.confirmPassword, {
        message: "Passwords don't match",
        path: ['confirmPassword'],
    });

type FormData = z.infer<typeof schema>;

export default function RegisterPage() {
    const { register: registerUser, login } = useAuth();
    const navigate = useNavigate();
    const [serverError, setServerError] = useState<string | null>(null);

    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<FormData>({
        resolver: zodResolver(schema),
        mode: 'onTouched',
    });

    const onSubmit = async (data: FormData) => {
        setServerError(null);
        try {
            await registerUser({
                email: data.email,
                password: data.password,
                displayName: data.displayName,
                location: data.location || undefined,
            });
            // Auto-login after successful registration
            await login(data.email, data.password);
            void navigate({ to: '/dashboard' });
        } catch (err) {
            if (err instanceof ApiError) {
                if (err.status === 400) {
                    const body = err.body as RegisterErrorResponse | null;
                    if (body?.reason === 'EMAIL_ALREADY_IN_USE') {
                        setServerError('An account with that email already exists.');
                    } else {
                        setServerError(body?.message ?? 'Registration failed. Please try again.');
                    }
                } else {
                    setServerError('Something went wrong. Please try again.');
                }
            }
        }
    };

    return (
        <div className="flex-center">
            <div className="login-box">
                <h2 className="text-center mb-1">Create an account</h2>
                <p className="text-center mb-5">
                    Join the Puzzle Circuit - you'll fit right in!
                </p>

                <form onSubmit={handleSubmit(onSubmit)} noValidate className="flex flex-col gap-4">
                    {/* Email */}
                    <div className="mb-4 px-5">
                        <div className="d-flex">
                            <label htmlFor="email" className="nowrap me-3">
                                Email
                            </label>
                            <input
                                id="email"
                                type="email"
                                autoComplete="email"
                                {...register('email')}
                                className={'form-control w-50'}
                            />
                        </div>

                        {errors.email && (
                            <p className="form-error">{errors.email.message}</p>
                        )}
                    </div>

                    {/* Display name */}
                    <div className="mb-4 px-5">
                        <div className="d-flex">
                            <label htmlFor="displayName" className="nowrap me-3">
                                Display name
                            </label>
                            <input
                                id="displayName"
                                type="text"
                                autoComplete="nickname"
                                {...register('displayName')}
                                className={'form-control w-50'}
                            />
                        </div>

                        {errors.displayName && (
                            <p className="form-error">{errors.displayName.message}</p>
                        )}
                    </div>

                    {/* Password */}
                    <div className="mb-4 px-5">
                        <div className="d-flex">
                            <label htmlFor="password" className="nowrap me-3">
                                Password
                            </label>
                            <input
                                id="password"
                                type="password"
                                autoComplete="new-password"
                                {...register('password')}
                                className={'form-control w-50'}
                            />
                        </div>

                        {errors.password && (
                            <p className="form-error">{errors.password.message}</p>
                        )}
                    </div>

                    {/* Confirm password */}
                    <div className="mb-4 px-5">
                        <div className="d-flex">
                            <label htmlFor="confirmPassword" className="nowrap me-3">
                                Confirm password
                            </label>
                            <input
                                id="confirmPassword"
                                type="password"
                                autoComplete="new-password"
                                {...register('confirmPassword')}
                                className={'form-control w-50'}
                            />
                        </div>

                        {errors.confirmPassword && (
                            <p className="form-error">{errors.confirmPassword.message}</p>
                        )}
                    </div>

                    {/* Location (optional) */}
                    <div className="mb-4 px-5">
                        <div className="d-flex">
                            <label htmlFor="location" className="me-3 nowrap">
                                State{' '}
                                <span className="">(optional)</span>
                            </label>
                            <select
                                id="location"
                                {...register('location')}
                                className={`${'form-control w-50'} cursor-pointer`}
                            >
                                <option value="">— Select a state —</option>
                                {US_STATES.map((s) => (
                                    <option key={s.value} value={s.value}>
                                        {s.label}
                                    </option>
                                ))}
                            </select>
                        </div>

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
                            {isSubmitting ? 'Creating account…' : 'Create account'}
                        </button>
                    </div>

                </form>

                <p className="mt-5 text-center text-sm">
                    Already have an account?{' '}
                    <Link to="/login" className="text-[var(--accent)] font-medium hover:underline">
                        Sign in
                    </Link>
                </p>
            </div>
        </div>
    );
}
