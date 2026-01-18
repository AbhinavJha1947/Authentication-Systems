import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

/**
 * Basic Authentication Interceptor.
 * Automatically attaches the Base64 encoded 'username:password' to every outgoing request.
 */
export const basicAuthInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {

  // 1. In a real scenario, these would come from an AuthService or environment
  const credentials = {
    username: 'admin',
    password: 'password123'
  };

  // 2. Encode to Base64 (Standard browser API)
  const authToken = btoa(`${credentials.username}:${credentials.password}`);

  // 3. Clone request and add Header
  const authenticatedRequest = req.clone({
    setHeaders: {
      Authorization: `Basic ${authToken}`,
      'X-Requested-With': 'XMLHttpRequest' // Helps identify AJAX requests
    }
  });

  return next(authenticatedRequest);
};
