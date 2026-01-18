import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
    selector: 'app-mfa',
    template: `
    <input [(ngModel)]="otp" placeholder="Enter 6-digit code">
    <button (click)="verify2fa()">Verify</button>
  `
})
export class MfaComponent {
    otp: string = '';

    constructor(private http: HttpClient, private router: Router) { }

    verify2fa() {
        this.http.post('/api/verify-2fa', { code: this.otp })
            .subscribe({
                next: () => this.router.navigate(['/home']),
                error: () => alert("Invalid Code")
            });
    }
}
