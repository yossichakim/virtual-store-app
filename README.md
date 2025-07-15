# Virtual Store App

Desktop application for managing and simulating a simple virtual store, implemented as a C# Visual Studio solution. The project is organized into distinct layers for data access, business logic and presentation, with XML-based storage and a basic simulator component.

---

## Features

- **Data Access Layer (DAL)**
  - `DalList` – List-based repository implementation  
  - `DalXml` – XML-backed repository with serialization/deserialization  
  - `DalTest` – Unit tests for repository functionality  

- **Business Logic & Initialization**
  - `InitializeXML` – Helper to create or reset the XML data store  
  - `PL` (Presentation Layer) – WinForms/WPF UI for browsing and editing store items  

- **Simulator**
  - `Simulator` – Console-style simulation of store operations  

- **Extras**
  - Input validation using `int.TryParse`  
  - Custom window icon and taskbar integration  

---

## Tech Stack & Tools

- **Language:** C# (.NET Framework)  
- **IDE:** Visual Studio 2019/2022 (solution file included)  
- **Data Storage:** XML files under `xml/`  
- **Testing Framework:** MSTest (in `DalTest`)  
- **Version Control:** Git (`.gitignore` provided)
