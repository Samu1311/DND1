# Web Service Development for Mobile Health App

## Overview
As part of the ongoing development of the **Mobile Health App for Skin and Medical Image Analysis**, the focus now is on setting up the **backend web service** that will handle key functionalities such as **user registration**, **login**, and **image uploads** (both for skin images and medical images like MRI or CT scans). This web service is essential for handling all data exchange between the mobile frontend and the server.

Below is a summary of what has been accomplished in terms of building the web service, including the setup of APIs for authentication and image processing.

---

## Web Service Components Implemented

### 1. **User Authentication (Registration & Login)**

To allow users to securely interact with the app and its features, we have implemented user authentication functionalities using **ASP.NET Core Web API**. These include:

- **User Registration API**:
  - Users can register by providing their email, full name, and password.
  - The password is securely hashed using SHA256 before storing it, ensuring that sensitive information is not stored in plain text.
  - If a user attempts to register with an already existing email, they will receive an error.

- **User Login API**:
  - Users can log in by providing their registered email and password.
  - If the credentials are correct, the system generates a **JWT (JSON Web Token)** which the user can use for authenticated requests.
  - The JWT is signed using the HS256 algorithm and expires after one hour, making it secure and limited in scope.

### 2. **JWT Authentication for Protected Endpoints**

The generated **JWT token** from the login API is used to secure certain endpoints in the application. These endpoints will only allow access to users who provide a valid token in the `Authorization` header.

- **Why JWT?**
  - JWT provides a stateless, scalable way to authenticate users. Instead of storing session data on the server, the token contains all the information needed for the server to verify the user.

### 3. **Image Upload and Metadata Storage API**

One of the key functionalities of the app is allowing users to upload images for analysis—whether it’s a **skin image** (like a mole or lesion) or a **medical image** (like an MRI or CT scan). Here's what has been achieved so far:

- **Image Upload API**:
  - Users can upload images (skin images or medical images) to the server.
  - Alongside the image, users can provide additional information, such as a **description** or **notes** about the image.
  
- **Metadata Storage**:
  - For every image uploaded, a corresponding **metadata JSON file** is created. This metadata includes information such as:
    - Description of the image.
    - Date and time when the image was uploaded.
    - File path to the image stored on the server.
  - This ensures that both the image and its associated information are stored securely and can be retrieved when needed for comparison or further analysis.

### 4. **File Storage for Uploaded Images**
For now, the system stores the uploaded images and metadata in the server’s file system under a dedicated directory (e.g., `/wwwroot/uploads/images`). This allows us to keep the app lightweight and avoids the complexity of integrating a database or external file storage solution at this early stage.

---

## Key Technologies and Tools

- **ASP.NET Core Web API**: Used to develop the RESTful web services that handle user authentication and image upload functionalities.
- **JWT (JSON Web Token)**: Used to secure the web service endpoints and allow stateless authentication.
- **SHA256 Encryption**: Used to hash passwords before storing them securely.
- **Swagger**: Integrated to help document the API endpoints and enable easy testing of each service.

---

## Challenges and Next Steps

### Challenges Faced
1. **JWT Key Size Issue**:
   - I encountered an issue with the JWT token generation where the key used for signing the token was too short. This was resolved by ensuring the signing key was at least 32 characters long (256 bits) to meet the security requirements for the **HS256** algorithm.
  
2. **Storing Uploaded Images**:
   - While storing images on the server is simple and effective for now, the next steps may involve moving to a more scalable solution like **cloud storage** (e.g., AWS S3 or Azure Blob Storage) as the project grows.

### Next Steps
- **Implementing the Image Analysis Functionality**:
  - We will be working on integrating image processing libraries that can analyze the uploaded images and detect changes in moles, lesions, or areas of interest in MRI or CT scans.
  
- **Data Privacy and Security**:
  - Focus will be on enhancing the security of the image upload process, including encryption of the image files and ensuring secure transmission of sensitive data.
  
- **Database Integration**:
  - In future stages, the system will likely need to integrate with a database to store user data, image metadata, and other information in a more scalable way.

---

## Conclusion

The web service layer is now in place, providing the core functionality needed to support **user authentication** and **image uploads**. This service is a crucial part of the app, ensuring that data is stored securely and can be accessed reliably for analysis.

Stay tuned for the next update, where I’ll be working on image processing and analysis features to help detect skin-related health issues!

