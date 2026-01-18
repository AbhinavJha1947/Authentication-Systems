import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
    selector: 'app-user-profile',
    template: `<p>User: {{ name }}</p>`
})
export class UserProfileComponent {
    name: string;

    constructor(private oauthService: OAuthService) {
        const claims = this.oauthService.getIdentityClaims();
        if (claims) {
            this.name = claims['name'];
        }
    }
}
