// auth.config.ts
import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
    issuer: 'https://my-identity-server.com',
    redirectUri: window.location.origin + '/index.html',
    clientId: 'my-client-app',
    scope: 'openid profile api1',
    responseType: 'code',
    showDebugInformation: true,
    strictDiscoveryDocumentValidation: false
};
