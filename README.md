
# 🚗 Ride Sharing System
A simple C# console-based ride sharing system implementing object-oriented principles like **inheritance**, **interfaces**, **polymorphism**, and **encapsulation**, with JSON-based data persistence.

---

## 📂 Project Structure
```
RideSharingSystem/
│
├── Models/
│   ├── Driver.cs
│   ├── Passenger.cs
│   ├── Ride.cs
│   ├── SaveData.cs
│   └── User.cs
│
├── Interfaces/
│   ├── IPayable.cs
│   └── IRideable.cs
│
├── Layouts/
│   ├── AuthLayout.cs
│   ├── DriverLayout.cs
│   └── PassengerLayout.cs
│
├── DataAccess/
│   └── DataManager.cs
│
└── Program.cs
```

---

## ⚙️ How it works
### ✔ Features
- Register as a **Passenger** or **Driver**
- Passenger can:
  - Request rides (**distance is randomised between 1 and 20 km**)
  - View wallet & add funds
  - View ride history
- Driver can:
  - View and accept ride requests  
    - If the ride distance is **greater than 15 km**, it is **too far to accept**
  - Complete rides (charges passenger & adds to earnings)
  - View total earnings
- Data (users, rides) is saved and loaded automatically using JSON.

---

## 🔍 OOP Concepts
- **Inheritance:** 
  - `Passenger` and `Driver` inherit from abstract `User`.
- **Interfaces:**
  - `Passenger` implements `IPayable` (for wallet) and `IRideable` (for requesting rides).
- **Polymorphism:** 
  - Shared `User` base allows treating `Passenger` and `Driver` uniformly where needed.

---

## 🚀 Running the application
1. Open the project in **Visual Studio 2022** (or similar IDE).
2. Build & run. You’ll see:
   ```
   === Ride Sharing System ===
   1. Register as Passenger
   2. Register as Driver
   3. Login
   4. Exit
   ```
3. Follow on-screen instructions.

---

