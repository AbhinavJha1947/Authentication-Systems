export class BiometricService {

    async register() {
        const publicKeyCredentialCreationOptions: PublicKeyCredentialCreationOptions = {
            challenge: Uint8Array.from("random-challenge-from-server", c => c.charCodeAt(0)),
            rp: {
                name: "My App",
                id: "localhost",
            },
            user: {
                id: Uint8Array.from("user-id", c => c.charCodeAt(0)),
                name: "user@example.com",
                displayName: "User Name",
            },
            pubKeyCredParams: [{ alg: -7, type: "public-key" }],
            authenticatorSelection: {
                authenticatorAttachment: "platform", // Use built-in (FaceID/TouchID)
            },
            timeout: 60000,
            attestation: "direct"
        };

        const credential = await navigator.credentials.create({
            publicKey: publicKeyCredentialCreationOptions
        });

        console.log(credential);
    }
}
