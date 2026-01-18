import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from './auth.service';

export const refreshInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);

    return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
            if (error.status === 401 && !req.url.includes('/refresh')) {
                // Token expired, try to refresh
                return authService.refreshToken().pipe(
                    switchMap((newTokens: any) => {
                        // Update storage
                        localStorage.setItem('access_token', newTokens.accessToken);

                        // Retry original request with new token
                        const cloned = req.clone({
                            setHeaders: {
                                Authorization: `Bearer ${newTokens.accessToken}`
                            }
                        });
                        return next(cloned);
                    }),
                    catchError((refreshErr) => {
                        // Refresh failed, logout user
                        authService.logout();
                        return throwError(() => refreshErr);
                    })
                );
            }
            return throwError(() => error);
        })
    );
};
