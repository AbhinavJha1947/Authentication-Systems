import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError, BehaviorSubject, filter, take } from 'rxjs';
import { AuthService } from './auth.service';

/**
 * Advanced Interceptor that handles 401 errors and performs silent token refresh.
 * Includes a Mutex-like logic to prevent multiple simultaneous refresh calls.
 */
let isRefreshing = false;
const refreshTokenSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

export const refreshInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);

    return next(req).pipe(
        catchError((error) => {
            if (error instanceof HttpErrorResponse && error.status === 401) {
                return handle401Error(req, next, authService);
            }
            return throwError(() => error);
        })
    );
};

function handle401Error(req: HttpRequest<any>, next: HttpHandlerFn, authService: AuthService) {
    if (!isRefreshing) {
        isRefreshing = true;
        refreshTokenSubject.next(null);

        return authService.refreshToken().pipe(
            switchMap((res: any) => {
                isRefreshing = false;
                refreshTokenSubject.next(res.accessToken);
                return next(injectToken(req, res.accessToken));
            }),
            catchError((err) => {
                isRefreshing = false;
                authService.logout(); // Critical: Force login if refresh fails
                return throwError(() => err);
            })
        );
    } else {
        // If already refreshing, wait for the first call to finish
        return refreshTokenSubject.pipe(
            filter(token => token !== null),
            take(1),
            switchMap(token => next(injectToken(req, token!)))
        );
    }
}

function injectToken(req: HttpRequest<any>, token: string) {
    return req.clone({
        setHeaders: { Authorization: `Bearer ${token}` }
    });
}
