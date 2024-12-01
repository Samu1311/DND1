# Database Design and Implementation for Mobile Health App

## Overview

The database for the Mobile Health App for Skin and Medical Image Analysis is implemented using SQLite with Entity Framework Core as the ORM. The schema efficiently handles user profiles, medical data, image storage, and alerts, ensuring seamless interaction between the app's backend and frontend. 

The Entity Framework (EF) Core implementation provides a flexible and scalable way to define relationships and handle database operations programmatically, while SQLite serves as the lightweight and file-based database engine ideal for the app's requirements.

---

## Database Tables

### 1. *Users*
Stores user information such as name, email, password, and demographic details.

- *Primary Key*: UserID
- *Columns*:
  - UserID (INTEGER, NOT NULL)
  - FirstName (TEXT, NOT NULL)
  - LastName (TEXT, NOT NULL)
  - Email (TEXT, NOT NULL)
  - PasswordHash (TEXT, NOT NULL)
  - DateOfBirth (TEXT, NULLABLE)
  - Gender (TEXT, NULLABLE)

---

### 2. *MedicalData*
Stores medical information for users.

- *Primary Key*: MedicalDataID
- *Columns*:
  - MedicalDataID (INTEGER, NOT NULL)
  - UserID (INTEGER, NOT NULL)
  - BloodType (TEXT, NULLABLE)
  - Height (REAL, NULLABLE)
  - Weight (REAL, NULLABLE)

---

### 3. *ProfilePictures*
Stores profile pictures uploaded by users.

- *Primary Key*: ProfilePictureID
- *Columns*:
  - ProfilePictureID (INTEGER, NOT NULL)
  - UserID (INTEGER, NOT NULL)
  - FileName (TEXT, NOT NULL)
  - FilePath (TEXT, NOT NULL)
  - UploadedAt (TEXT, NOT NULL)

---

### 4. *MoleImages*
Stores images of skin moles uploaded by users.

- *Primary Key*: MoleImageID
- *Columns*:
  - MoleImageID (INTEGER, NOT NULL)
  - UserID (INTEGER, NOT NULL)
  - FileName (TEXT, NOT NULL)
  - ImageData (BLOB, NOT NULL)
  - UploadedAt (TEXT, NOT NULL)
  - AnalysisResults (TEXT, NULLABLE)

---

### 5. *MRIImages*
Stores MRI scan images uploaded by users.

- *Primary Key*: MRIImageID
- *Columns*:
  - MRIImageID (INTEGER, NOT NULL)
  - UserID (INTEGER, NOT NULL)
  - FileName (TEXT, NOT NULL)
  - FilePath (TEXT, NOT NULL)
  - UploadedAt (TEXT, NOT NULL)
  - AnalysisResults (TEXT, NULLABLE)

---

### 6. *XrayImages*
Stores X-ray images uploaded by users.

- *Primary Key*: XrayImageID
- *Columns*:
  - XrayImageID (INTEGER, NOT NULL)
  - UserID (INTEGER, NOT NULL)
  - FileName (TEXT, NOT NULL)
  - FilePath (TEXT, NOT NULL)
  - UploadedAt (TEXT, NOT NULL)
  - AnalysisResults (TEXT, NULLABLE)

---

### 7. *Alerts*
Manages health-related alerts linked to images or other user data.

- *Primary Key*: AlertID
- *Columns*:
  - AlertID (INTEGER, NOT NULL)
  - UserID (INTEGER, NOT NULL)
  - IsRead (INTEGER, NOT NULL, DEFAULT 0)
  - RelatedItemType (TEXT, NULLABLE)
  - MoleImageID (INTEGER, NULLABLE)
  - MRIImageID (INTEGER, NULLABLE)
  - XrayImageID (INTEGER, NULLABLE)

---

## AppDbContext Class

The AppDbContext class is the cornerstone of the Entity Framework Core implementation, serving as the bridge between the database and the application logic. It defines the database schema, manages relationships, and facilitates database operations.

### Key Features of AppDbContext:

1. *DbSet Properties*:
   - Each DbSet corresponds to a database table, making it easy to query and manipulate data.
   - Example:
     csharp
     public DbSet<User>? Users { get; set; }
     public DbSet<MoleImage>? MoleImages { get; set; }
     

2. *Entity Relationships*:
   - Relationships are explicitly defined in the OnModelCreating method to ensure data integrity and proper foreign key constraints.
   - Example:
     csharp
     modelBuilder.Entity<MoleImage>()
         .HasOne(mi => mi.User)
         .WithMany(u => u.MoleImages)
         .HasForeignKey(mi => mi.UserID)
         .OnDelete(DeleteBehavior.Cascade);
     

3. *Cascade Deletion*:
   - When a user is deleted, their associated records (e.g., MoleImages, Alerts) are automatically removed to maintain database consistency.

4. *Composite Keys*:
   - Where applicable, composite keys are defined to uniquely identify records in tables like MoleImages.

5. *Migration Support*:
   - Changes to the database schema can be managed via migrations, ensuring version control and seamless updates.

---

## Key Features of the Database Design

1. *Blob Storage*:
   - The ImageData column in the MoleImages table stores binary data for images, allowing quick retrieval without external file dependencies.

2. *Timestamp Tracking*:
   - Tables include an UploadedAt column to log the upload time for images and data, providing traceability.

3. *Alert System*:
   - The Alerts table links health alerts to specific images or user activities, ensuring timely notifications.

4. *Scalability*:
   - The design supports potential migration to cloud-based storage for larger datasets, such as Azure Blob Storage or AWS S3.

5. *Flexibility*:
   - The schema allows easy integration of additional image types or user data by extending existing tables or adding new ones.

---

## Challenges and Next Steps

### Challenges
- Efficiently managing large binary data (e.g., images) directly in SQLite can lead to performance bottlenecks for high data volumes.
- Maintaining database integrity when handling multiple foreign key relationships requires careful design and testing.

### Next Steps
1. *Optimization*:
   - Evaluate the use of external file storage for images to reduce database load.
2. *Advanced Features*:
   - Add stored procedures or triggers for automatic alert generation based on specific health data thresholds.
3. *Analytics Integration*:
   - Implement queries to derive insights from medical data and image analysis.
