# Trendlink 
## Find. Connect. Collaborate

**Social blogger search platform for successful advertising integration.**

Trendlink connects bloggers, allowing them to collaborate on advertising cooperations. The platform offers comprehensive profile management, advertisement creation, and seamless real-time communication.

---

## Table of Contents
- [Trendlink](#trendlink)
  - [Table of Contents](#table-of-contents)
  - [🚀 Project Overview](#-project-overview)
  - [🎯 Features](#-features)
  - [✏️ Architecture](#️-architecture)
  - [⚙️ Technologies Used](#️-technologies-used)
    - [🖥 Backend](#-backend)
    - [📱 Frontend](#-frontend)
  - [🟢 Quick Start](#-quick-start)
  - [🤝 Contributing](#-contributing)
  - [🧾 License](#-license)

---

## 🚀 Project Overview
Trendlink is a social platform that connects bloggers, enabling them to collaborate on successful advertising campaigns. The platform focuses on user authentication, seamless integration with Instagram, and real-time communication through notifications. Advertisers can easily find and collaborate with bloggers, manage advertisements, and track campaign results.

---

## 🎯 Features
- 🔐 User Authentication (JWT-based) and Google OAuth
- 🔗 Instagram account integration for profile insights
- ✏️ Profile management and advertisement creation
- 💬 Real-time collaboration requests and notifications
- Upcoming features:
  - ✉️ Email notifications
  - ⭐ Ratings and reviews

---

## ✏️ Architecture
Trendlink follows the **Clean Architecture** principle with **CQRS** (Command Query Responsibility Segregation). It separates concerns by structuring the project into different layers:
- **Domain Layer**: Core business logic and domain models.
- **Application Layer**: Handles commands and queries using MediatR and CQRS.
- **Infrastructure Layer**: Interacts with external systems like databases (PostgreSQL), caching (Redis), and external services (Keycloak).
- **API Layer**: Exposes RESTful endpoints for communication with the frontend.

Key components:
- **CQRS**: Used to separate read and write operations, ensuring scalability and clear separation of responsibilities.
- **Docker & Docker Compose**: Simplifies deployment and testing by containerizing the application.

---

## ⚙️ Technologies Used

### 🖥 Backend
- **Framework**: ASP.NET Core 8
- **Database**: PostgreSQL
- **Authentication**: Keycloak (JWT, OAuth)
- **Caching**: Redis
- **Logging**: Seq
- **Messaging**: SignalR for real-time notifications
- **Containerization**: Docker, Docker Compose

### 📱 Frontend
- **Framework**: React
- **Styles**: Tailwind CSS
- **Bundling**: Vite

---

## 🟢 Quick Start

Follow these steps to quickly set up the project:

1. Clone the repository:
    ```cmd
    git clone https://github.com/IIIA-KO/Trendlink.git
    ```

2. Set up Docker and start the application:
    ```cmd
    docker-compose build
    docker-compose up -d
    ```

For detailed setup instructions, see the [Setup Guide.](https://github.com/IIIA-KO/Trendlink/wiki/Setup-Guide)

---

## 🤝 Contributing

This project welcomes contributions. Please read [Contributing Guide](https://github.com/IIIA-KO/Trendlink/wiki/Contributing-Guide) to learn more about our process and how to get involved.

## 🧾 License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/IIIA-KO/Trendlink?tab=MIT-1-ov-file) file for more details.
