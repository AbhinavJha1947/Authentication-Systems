# 6️⃣ SSO (Single Sign-On)

## 🔹 What it means
Single Sign-On (SSO) is an authentication scheme that allows a user to log in with a single ID and password to any of several related, yet independent, software systems.

**Simple concept:** "Login once, access multiple apps."

## 🔹 Behind the scenes
SSO is not a protocol itself but a concept enforced by protocols like:
- **OAuth 2.0**
- **OpenID Connect (OIDC)**
- **SAML (Security Assertion Markup Language)** (Common in Enterprise)

## 🔹 Example
1. You log into **Gmail** (Google).
2. You open **YouTube** in a new tab.
3. You are **already logged in** to YouTube without typing your password again.
4. You open **Google Drive**.
5. You are **already logged in**.

Here, Google is the Identity Provider (IdP), and Gmail, YouTube, and Drive are the Service Providers (SP).

## 🔹 Use cases
- **Enterprise systems:** An employee logs into the corporate network once and gets access to Email, HR Portal, Jira, and Slack.
- **Multiple internal apps:** A company with several internal tools (Admin, Analytics, Dashboard) connecting to a central auth server.
