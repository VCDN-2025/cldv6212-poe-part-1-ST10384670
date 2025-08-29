# ABCretailpoe1

## 📦 Overview

**ABCretailpoe1** is a web-based retail management system developed using **ASP.NET MVC**. The application allows users to manage **Customers**, **Products**, and **Orders** efficiently. In addition to basic CRUD operations, the system integrates with **Microsoft Azure** for cloud storage and messaging, making it scalable and production-ready.

---

## 🔑 Key Features

- ✅ Customer Management (Add, View, Update, Delete)
- ✅ Product Catalog Management
- ✅ Order Processing and Tracking
- ✅ Integration with Azure File Share for document storage
- ✅ Azure Blob Storage for storing images or binary files
- ✅ Azure Queue Storage for asynchronous order handling

---

## 🛠️ Technologies Used

- ASP.NET MVC (C#)
- Entity Framework
- Microsoft SQL Server
- Azure Storage Services:
  - Azure File Share
  - Azure Blob Storage
  - Azure Queue Storage
- GitHub Copilot & ChatGPT (for AI-assisted development)

---

## ☁️ Azure Integration Details

### 1. 🔗 Azure File Share
Used to store downloadable or shared documents related to products or customers.

- Configured via Azure Storage Account
- Files uploaded and retrieved via `CloudFileClient` in C#

### 2. 🧱 Azure Blob Storage
Stores media assets (e.g., product images or receipts).

- Uses `CloudBlobClient` for upload/download
- Blobs are organized by container (e.g., `product-images`)

### 3. 📬 Azure Queue Storage
Handles background processing (e.g., sending order confirmation messages).

- Messages are serialized JSON payloads
- Read using `CloudQueueClient`

---

## 🚀 How to Run the Project

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/ABCretailpoe1.git
