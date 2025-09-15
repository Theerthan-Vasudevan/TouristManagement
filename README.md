# ExploreIt (TourismManagementV2)

**ExploreIt** (a.k.a. `TourismManagementV2`) is a simple ASP.NET Core MVC project for managing travel packages, bookings and users. It includes both user-facing and admin functionality.  
This README explains how to run the app locally and how to log in as **admin without using the database** (handy for demo/evaluation).

---

## Features
- User registration and login
- Admin and User roles
- Browse packages (cards with image, price, duration)
- Booking flow (confirm booking page)
- Admin pages: Add Package, Manage Packages, Manage Users
- Session-based authentication (no ASP.NET Identity required)
- Static images served from `wwwroot/images/`

---

## Quick demo admin credentials (no DB required)

For demo/evaluation you can log in as admin without a DB record:

- **Username:** `admin`  
- **Password:** `admin123`

> The project includes a check in the `Login` action to accept this hard-coded admin credential and create a dummy session (UserId `0`, Role `Admin`). This does **not** persist to the database.

```csharp
// Hardcoded admin credentials (demo only)
if (username == "admin" && password == "admin123")
{
    HttpContext.Session.SetInt32("UserId", 0); // dummy Id for admin
    HttpContext.Session.SetString("Role", "Admin");
    return RedirectToAction("AdminIndex", "User");
}
