import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

interface AuthResponse {
    token: string;
}

/**
 * Modern Angular AuthService using Signals for state management.
 */
@Injectable({ providedIn: 'root' })
export class AuthService {
    private http = inject(HttpClient);

    // Reactive state for current user
    currentUser = signal<any>(null);

    login(credentials: { user: string; pass: string }) {
        return this.http.post<AuthResponse>('/api/auth/login', credentials).pipe(
            tap((res) => {
                const token = res.token;
                localStorage.setItem('jwt_token', token);
                this.currentUser.set(this.decodeToken(token));
            })
        );
    }

    logout() {
        localStorage.removeItem('jwt_token');
        this.currentUser.set(null);
    }

    private decodeToken(token: string) {
        try {
            // Simple base64 decode for the payload segment
            const payload = token.split('.')[1];
            return JSON.parse(atob(payload));
        } catch {
            return null;
        }
    }

    getToken(): string | null {
        return localStorage.getItem('jwt_token');
    }

    isAuthenticated(): boolean {
        const token = this.getToken();
        if (!token) return false;

        // Check expiry
        const payload = this.decodeToken(token);
        return payload.exp > Date.now() / 1000;
    }
}
