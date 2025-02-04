# RentRoof - Blazor WebAssembly Project

## Overview
RentRoof is a Blazor WebAssembly-based real estate rental platform designed to streamline property listings, tenant interactions, and booking management. The application provides an intuitive and responsive interface for users to explore rental properties and connect with property owners.

## Features
- **User Authentication**: Secure login and registration system.
- **Property Listings**: Landlords can list rental properties with details and images.
- **Search & Filters**: Users can search and filter properties based on location, price, and amenities.
- **Booking System**: Tenants can request bookings and landlords can approve/reject requests.
- **Messaging System**: Secure communication between landlords and tenants.
- **Payment Integration**: Supports online rent payments (future feature).
- **Admin Dashboard**: Manage users, properties, and transactions efficiently.

## Technologies Used
- **Frontend**: Blazor WebAssembly
- **Backend**: ASP.NET Core Web API
- **Database**: SQL Server / PostgreSQL
- **Authentication**: JWT-based authentication
- **Styling**: Bootstrap 5 & Custom CSS
- **Deployment**: Azure / AWS (Optional)

## Installation & Setup
### Prerequisites
Ensure you have the following installed:
- .NET SDK (latest version)
- Node.js (for package management if required)
- SQL Server or PostgreSQL for database
- Visual Studio / VS Code

### Steps to Run Locally
1. **Clone the Repository**
   ```sh
   git clone https://github.com/jalalgorithm/rentroof.git
   cd rentroof
   ```
2. **Backend Setup**
   - Navigate to the API project folder
   - Configure the database connection in `appsettings.json`
   - Run migrations (if using EF Core):
     ```sh
     dotnet ef database update
     ```
   - Start the API:
     ```sh
     dotnet run
     ```
3. **Frontend Setup**
   - Navigate to the Blazor WebAssembly project folder
   - Restore dependencies:
     ```sh
     dotnet restore
     ```
   - Run the application:
     ```sh
     dotnet run
     ```
   - Open the application in the browser at `http://localhost:5000`

## API Endpoints
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Authenticate and receive JWT token
- `GET /api/properties` - Retrieve available properties
- `POST /api/bookings` - Request a booking

## Contribution Guidelines
1. Fork the repository.
2. Create a feature branch (`feature-newfeature`).
3. Commit your changes and push to your forked repo.
4. Submit a Pull Request.

## License
This project is licensed under the MIT License.

## Contact
For inquiries or contributions, contact **Your Name** at [your.email@example.com](mailto:temitomzi@gmail.com) or visit the GitHub repository: [GitHub Repo](https://github.com/jalalgorithm/rentroof).

