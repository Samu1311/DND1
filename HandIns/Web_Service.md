# Web Service Development for HealthTrack+

## Overview
The **HealthTrack+** project has progressed significantly with the development of its **backend web service**, designed to support critical functionalities like **user authentication**, **image uploads**, and metadata storage. This backend infrastructure forms the foundation for seamless interaction between the Blazor Server frontend and the backend API, enabling efficient and secure data handling.

The following document highlights the progress made, the technologies leveraged, and the challenges addressed during this phase.

---

## Web Service Components Implemented

### 1. **User Authentication (Registration & Login)**

To ensure secure access to the app’s features, robust user authentication mechanisms have been implemented using **ASP.NET Core Web API**:

- **User Registration API**:
  - Users register with basic details such as their name, email, and password.
  - Passwords are securely hashed using **SHA256** before being stored, ensuring sensitive information is not compromised.
  - Validation checks prevent duplicate registrations by email.

- **User Login API**:
  - Users log in with their registered email and password.
  - Upon successful login, a **JWT (JSON Web Token)** is generated and returned to the client for authenticated access to secure endpoints.
  - The token is signed using **HS256** and includes claims such as user ID, name, and role, expiring after a predefined duration.

### 2. **JWT Authentication for Protected Endpoints**

The web service uses **JWT tokens** to secure API endpoints, ensuring only authenticated users can access protected functionalities.

- **Why JWT?**
  - Stateless: The server does not need to store session data, reducing overhead and scaling issues.
  - Secure: Token expiration and HS256 signing prevent unauthorized use.

### 3. **Image Upload and Metadata Management**

HealthTrack+ supports the upload of various images, including skin images for mole tracking and medical scans like MRI or X-rays.

- **Image Upload API**:
  - Users can upload images, which are validated and stored locally on the server.
  - Along with the image, metadata (e.g., upload timestamp, description, and user ID) is captured to maintain contextual information.

- **Metadata and File Storage**:
  - Uploaded images are stored in a dedicated directory on the server (e.g., `/wwwroot/uploads/profile-images`).
  - Only the **file paths** to the stored images are sent to the database, significantly reducing database storage requirements and ensuring efficient retrieval.
  - For every image uploaded, a corresponding **metadata JSON file** is created. This metadata includes:
    - Description of the image.
    - Date and time of upload.
    - File path for retrieving the image.

---

## Key Technologies and Tools

- **ASP.NET Core Web API**: Forms the backbone of the RESTful web services, handling requests for authentication, image uploads, and more.
- **JWT (JSON Web Token)**: Provides secure, scalable, and stateless user authentication.
- **SHA256 Encryption**: Used for securely hashing user passwords.
- **Swagger**: Offers a user-friendly interface for testing and documenting API endpoints, improving the developer experience.

---

## Challenges and Lessons Learned

### Challenges
1. **JWT Key Configuration**:
   - A key size issue initially caused token generation to fail. This was resolved by using a 256-bit signing key to meet the HS256 requirements.

2. **File Storage Complexity**:
   - Managing locally stored files required a well-organized directory structure and strict validation processes to ensure consistency between file storage and database records.

3. **Metadata Synchronization**:
   - Ensuring metadata files and images remain in sync required a rigorous validation process during the upload flow.

4. **Scaling Considerations**:
   - While local storage is sufficient for the current phase, the system architecture has been designed with flexibility to transition to cloud-based storage (e.g., AWS S3 or Azure Blob Storage) as user demand increases.

---

## Next Steps

### Planned Enhancements
1. **Image Analysis**:
   - Integration of advanced image processing tools (e.g., **OpenCV** or **Azure Cognitive Services**) to detect changes in moles and identify anomalies in medical images.

2. **Data Security**:
   - Implement encryption for uploaded images to comply with stringent privacy standards such as GDPR and HIPAA.

3. **Database Integration**:
   - Transition from file-based metadata to a relational database (**SQLite** or **EF Core**) for more efficient and scalable data management.

4. **User Management**:
   - Add features for users to manage their profiles, such as editing their data, viewing upload history, and securely sharing results with healthcare providers.

---

## Conclusion

The web service infrastructure for **HealthTrack+** is now operational, providing core functionality for user authentication and image uploads. By storing files locally and maintaining their paths in the database, the system achieves a balance between simplicity and scalability. These developments pave the way for integrating advanced features like automated image analysis and secure data sharing, bringing us closer to making **HealthTrack+** a vital tool for proactive health monitoring.

Stay tuned for updates on upcoming functionalities and enhancements!
