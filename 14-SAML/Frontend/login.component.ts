import { Component } from '@angular/core';

@Component({
    selector: 'app-login',
    template: '<button (click)="login()">Login with SSO</button>'
})
export class LoginComponent {

    login() {
        // Redirect to Backend Login endpoint which constructs SAML Request
        window.location.href = '/api/auth/login';
    }
}
