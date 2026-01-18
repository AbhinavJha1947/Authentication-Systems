// auth.config.ts
import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
    issuer: 'https://accounts.google.com',
    redirectUri: window.location.origin + '/index.html',
    clientId: 'YOUR_CLIENT_ID',
    scope: 'openid profile email',
    responseType: 'code',
    showDebugInformation: true,
    strictDiscoveryDocumentValidation: false
};
