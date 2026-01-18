import { HttpInterceptorFn } from '@angular/common/http';

export const credentialsInterceptor: HttpInterceptorFn = (req, next) => {
    if (req.url.includes('my-api.com')) {
        const authReq = req.clone({
            withCredentials: true
        });
        return next(authReq);
    }
    return next(req);
};
