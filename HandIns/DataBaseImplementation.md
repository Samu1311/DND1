# Database Design and Implementation for HealthTrack+

## Overview

The database for **HealthTrack+** is designed to efficiently manage user profiles, medical data, image storage, and health alerts, ensuring seamless integration between the app's frontend and backend. It leverages **SQLite** for its lightweight and file-based database capabilities, combined with **Entity Framework Core (EF Core)** as the ORM for flexible schema definition and data operations.

The implementation ensures scalability, flexibility, and security, accommodating the app's current and future needs.

---

## Database Tables

### 1. **Users**
Stores user details such as personal information, authentication data, and demographic attributes.

- **Primary Key**: `UserID`
- **Columns**:
  - `UserID` (INTEGER, NOT NULL)
  - `FirstName` (TEXT, NOT NULL)
  - `LastName` (TEXT, NOT NULL)
  - `Email` (TEXT, UNIQUE, NOT NULL)
  - `PasswordHash` (TEXT, NOT NULL)
  - `DateOfBirth` (TEXT, NULLABLE)
  - `Gender` (TEXT, NULLABLE)
  - `PhoneNumber` (TEXT, NULLABLE)
  - `EmergencyContact` (TEXT, NULLABLE)
  - `UserType` (TEXT, DEFAULT "Basic")
  - `Bio` (TEXT, NULLABLE)

---

### 2. **MedicalData**
Tracks health metrics for users, such as height, weight, and blood type.

- **Primary Key**: `MedicalDataID`
- **Columns**:
  - `MedicalDataID` (INTEGER, NOT NULL)
  - `UserID` (INTEGER, NOT NULL)
  - `BloodType` (TEXT, NULLABLE)
  - `Height` (REAL, NULLABLE)
  - `Weight` (REAL, NULLABLE)

---

### 3. **ProfilePictures**
Manages the storage of user-uploaded profile pictures.

- **Primary Key**: `ProfilePictureID`
- **Columns**:
  - `ProfilePictureID` (INTEGER, NOT NULL)
  - `UserID` (INTEGER, NOT NULL)
  - `FileName` (TEXT, NOT NULL)
  - `FilePath` (TEXT, NOT NULL)
  - `ThumbnailPath` (TEXT, NULLABLE)
  - `UploadedAt` (TEXT, NOT NULL)

---

### 4. **MoleImages**
Tracks user-uploaded mole images for skin analysis.

- **Primary Key**: `MoleImageID`
- **Columns**:
  - `MoleImageID` (INTEGER, NOT NULL)
  - `UserID` (INTEGER, NOT NULL)
  - `FileName` (TEXT, NOT NULL)
  - `FilePath` (TEXT, NOT NULL)
  - `ThumbnailPath` (TEXT, NULLABLE)
  - `UploadedAt` (TEXT, NOT NULL)
  - `AnalysisResults` (TEXT, NULLABLE)

---

### 5. **MRIImages**
Stores MRI scan data uploaded by users.

- **Primary Key**: `MRIImageID`
- **Columns**:
  - `MRIImageID` (INTEGER, NOT NULL)
  - `UserID` (INTEGER, NOT NULL)
  - `FileName` (TEXT, NOT NULL)
  - `FilePath` (TEXT, NOT NULL)
  - `ThumbnailPath` (TEXT, NULLABLE)
  - `UploadedAt` (TEXT, NOT NULL)
  - `AnalysisResults` (TEXT, NULLABLE)

---

### 6. **XrayImages**
Handles storage of X-ray images uploaded by users.

- **Primary Key**: `XrayImageID`
- **Columns**:
  - `XrayImageID` (INTEGER, NOT NULL)
  - `UserID` (INTEGER, NOT NULL)
  - `FileName` (TEXT, NOT NULL)
  - `FilePath` (TEXT, NOT NULL)
  - `ThumbnailPath` (TEXT, NULLABLE)
  - `UploadedAt` (TEXT, NOT NULL)
  - `AnalysisResults` (TEXT, NULLABLE)

---

### 7. **Alerts**
Manages health-related notifications triggered by app functionalities.

- **Primary Key**: `AlertID`
- **Columns**:
  - `AlertID` (INTEGER, NOT NULL)
  - `UserID` (INTEGER, NOT NULL)
  - `IsRead` (INTEGER, NOT NULL, DEFAULT 0)
  - `RelatedItemType` (TEXT, NULLABLE)
  - `MoleImageID` (INTEGER, NULLABLE)
  - `MRIImageID` (INTEGER, NULLABLE)
  - `XrayImageID` (INTEGER, NULLABLE)

---

## AppDbContext Class

The **AppDbContext** is central to the Entity Framework Core implementation, acting as the link between the database and application logic.

### Key Features:

1. **DbSet Properties**:
   - Maps database tables to C# classes, making queries and data manipulation straightforward.
   - Example:
     ```csharp
     public DbSet<User>? Users { get; set; }
     public DbSet<MoleImage>? MoleImages { get; set; }
     ```

2. **Entity Relationships**:
   - Relationships between tables are defined in the `OnModelCreating` method.
   - Example:
     ```csharp
     modelBuilder.Entity<MoleImage>()
         .HasOne(mi => mi.User)
         .WithMany(u => u.MoleImages)
         .HasForeignKey(mi => mi.UserID)
         .OnDelete(DeleteBehavior.Cascade);
     ```

3. **Cascade Deletion**:
   - Ensures related data (e.g., images and alerts) is automatically removed when a user is deleted.

4. **Migration Support**:
   - Allows for controlled schema evolution using EF Core migrations, ensuring compatibility and version control.

---

## Key Features of the Database Design

1. **Local File Storage**:
   - Image files are stored locally on the server, with their paths saved in the database for easy retrieval.

2. **Scalability**:
   - The schema is designed to accommodate additional image types or data by extending existing tables or adding new ones.

3. **Alert System Integration**:
   - Links health alerts to specific image analyses, providing users with timely updates.

4. **Timestamp Tracking**:
   - The `UploadedAt` column in image tables
