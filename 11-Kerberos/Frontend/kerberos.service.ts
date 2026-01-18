// Just like mTLS, Kerberos (Windows Integrated Auth) is handled by the browser.
// The browser automatically responds to the WWW-Authenticate: Negotiate header.

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class WindowsAuthService {
    constructor(private http: HttpClient) { }

    getData() {
        // Send request with credentials (cookies/headers handled by browser)
        return this.http.get('/internal-api', { withCredentials: true });
    }
}
