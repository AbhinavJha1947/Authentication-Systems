import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private http: HttpClient) { }

    login(credentials: any) {
        return this.http.post<{ token: string }>('/api/login', credentials)
            .pipe(tap(response => {
                localStorage.setItem('access_token', response.token);
            }));
    }

    logout() {
        localStorage.removeItem('access_token');
    }
}
