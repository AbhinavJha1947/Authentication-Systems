// In mTLS, the BROWSER handles the detailed handshake and certificate selection (popup).
// The Javascript/Angular application merely makes requests as normal.
// If the certificate is missing/invalid, the browser connection fails before JS runs.

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class MulsService {
    constructor(private http: HttpClient) { }

    getData() {
        return this.http.get('/secure-data');
    }
}
