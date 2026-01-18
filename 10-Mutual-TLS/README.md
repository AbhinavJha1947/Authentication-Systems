# 🔟 Mutual TLS (mTLS)

mTLS (Mutual Transport Layer Security) is a method where both the client and the server authenticate each other.

> [!NOTE]
> Standard TLS (HTTPS): Only the **Client** verifies the **Server** (checking the lock icon in the browser).
> mTLS: The **Server** ALSO verifies the **Client** using a client-side certificate.

## 🔹 How it works
Both parties verify each other using digital certificates from a trusted Certificate Authority (CA).

## 🔹 Flow
1. **Client connects:** Client says "Hello" to Server.
2. **Server presents certificate:** "Here is my ID, signed by a CA you trust."
3. **Client verifies server:** Checks if the server's certificate is valid.
4. **Server requests certificate:** "Now show me YOUR ID."
5. **Client presents certificate:** Sends its own client certificate.
6. **Server verifies client:** Checks if the client's certificate is valid and allowed to access.
7. **Connection established:** Encrypted tunnel is created.

## 🔹 Pros
- **Extremely secure:** Unlikely to be spoofed if keys are managed correctly.
- **No tokens/passwords:** Authentication happens at the network layer, before any application logic runs.
- **Zero Trust:** Every single connection is verified.

## 🔹 Cons
- **Complex setup:** Requires a Private CA (Certificate Authority) and infrastructure to issue/rotate certificates.
- **Certificate management:** Distributing certificates to every client (mobile, IoT device, service) is hard.
- **Hard to debug:** Connection failures happen at the handshake level, often with opaque error messages.

## 🔹 Use cases
- **Microservices:** Service-to-service communication (e.g., Service Mesh like Istio or Linkerd).
- **Banking systems:** High-value transactions between financial institutions.
- **Zero-trust networks:** Ensuring only authorized devices can connect to the network.
- **IoT:** Authenticating sensors and devices.
