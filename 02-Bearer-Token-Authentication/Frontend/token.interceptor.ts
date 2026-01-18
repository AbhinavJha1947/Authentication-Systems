import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpEvent } from '@angular/common/http';
import { inject } from '@angular/core';
import { APP_CONFIG } from '../../app.config'; // Hypothetical config

/**
 * Bearer Token Interceptor.
 * Appends the 'Authorization: Bearer <token>' header to API calls.
 */
export const bearerTokenInterceptor: HttpInterceptorFn = (
    req: HttpRequest<unknown>,
    next: HttpHandlerFn
) => {
    // 1. Retrieve token from storage (e.g., localStorage, cookie, or signal-based store)
    const token = localStorage.getItem('auth_token');

    // 2. If token exists, clone and modify headers
    if (token) {
        const clonedReq = req.clone({
            headers: req.headers.set('Authorization', `Bearer ${token}`)
        });
        return next(clonedReq);
    }

    // 3. Fallback to original request
    return next(req);
};
