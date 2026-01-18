# 1️⃣4️⃣ SAML (Security Assertion Markup Language)

**SAML** is an open standard for exchanging authentication and authorization data between an **Identity Provider (IdP)** and a **Service Provider (SP)**. It is essentially the "XML version" of OIDC, predating it, and is still the gold standard for Enterprise SSO.

## 🔹 How it works
It uses **XML documents** to communicate.

## 🔹 Roles
- **Principal:** The User.
- **Identity Provider (IdP):** The system that authenticates the user (e.g., Active Directory, Okta, OneLogin).
- **Service Provider (SP):** The app the user wants to access (e.g., Salesforce, Slack, Workday).

## 🔹 Flow (SP-Initiated SSO)
1. **User attempts to access App (SP):** e.g., accessing the company HR portal.
2. **Redirect:** The App sees the user is not logged in and creates a **SAML Request (XML)**, then redirects the user's browser to the IdP.
3. **Authentication:** User logs in at the IdP (e.g., Corporate Login Page).
4. **SAML Response:** The IdP generates a **SAML Assertion (XML)**, signs it digitally with a private key, and passes it back to the browser.
5. **Post to App:** The browser POSTs this XML assertion back to the App (SP).
6. **Validation:** The App verifies the signature using the IdP's public certificate. If valid, the user is logged in.

## 🔹 Pros
- **Standard for Enterprise:** Supported by effectively every enterprise software (Salesforce, Zoom, Slack).
- **Secure:** XML signatures ensure the data hasn't been tampered with.
- **Single Sign-On:** The core enabler of "Log in to Windows, assume access to everything."

## 🔹 Cons ❌
- **XML is heavy:** Verbose and harder to parse than JSON.
- **Complexity:** Debugging XML signature errors is notoriously difficult.
- **Browser-based:** Primarily designed for web browsers, not mobile native apps (OIDC is better there).

## 🔹 Use cases
- **Corporate Environments:** Connecting internal employees to SaaS tools.
- **Government / Legacy Systems.**
