# 1️⃣1️⃣ Kerberos

Kerberos is a computer network authentication protocol that works on the basis of tickets to allow nodes communicating over a non-secure network to prove their identity to one another in a secure manner.

## 🔹 Key Component: Windows Active Directory
It is the default authentication protocol for **Windows Active Directory**.

## 🔹 How it works (Simplified)
It relies on a trusted third party called the **Key Distribution Center (KDC)**.

1. **User Login:** User enters password. The client turns this into a key and requests a TGT (Ticket-Granting Ticket) from the KDC.
2. **TGT Issued:** If the password is correct, KDC sends an encrypted TGT back.
3. **Service Request:** When the user wants to access a service (e.g., File Share), they send the TGT to the KDC requesting a Service Ticket.
4. **Service Ticket:** KDC validates TGT and issues a Service Ticket.
5. **Access:** Client sends the Service Ticket to the File Server. File Server verifies it and grants access.

## 🔹 Pros
- **Strong security:** Passwords are never sent over the network (not even encrypted ones).
- **Mutual Authentication:** Both client and server verify each other.
- **Single Sign-On (Internal):** Once you log into Windows, you can access file shares, printers, and intranet sites without logging in again.

## 🔹 Cons
- **Complex:** Very hard to set up and debug outside of the Windows ecosystem.
- **Time Synchronization:** Requires all clocks on all machines to be synchronized (NTP) within 5 minutes, or it fails to prevent replay attacks.
- **Enterprise-only:** Overkill for public web apps.

## 🔹 Use cases
- **Enterprise Corporate Networks:** Windows Domains.
- **Universities:** Campus networks.
- **Internal Intranets:** Securing internal resources.
