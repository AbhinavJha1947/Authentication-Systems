import { HttpInterceptorFn } from '@angular/common/http';

export const apiKeyInterceptor: HttpInterceptorFn = (req, next) => {
    const apiKey = 'abc123xyz789'; // In real app, load from environment

    const cloned = req.clone({
        setHeaders: {
            'X-API-KEY': apiKey
        }
    });

    return next(cloned);
};
