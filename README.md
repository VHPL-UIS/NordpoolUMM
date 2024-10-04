# UMM Project (Urgent Market Messages)

This project is designed to handle Urgent Market Messages (UMM) using a .NET Core backend and a frontend built with JavaScript and Chart.js.

## Table of Contents
- [Introduction](#introduction)
- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [License](#license)

## Introduction
The UMM Project is a web application that allows users to view and analyze urgent market messages related to production unavailability. The backend is built with .NET Core, and the frontend uses JavaScript and Chart.js for data visualization.

## Technologies Used
- **Backend:** .NET Core
- **Frontend:** JavaScript, HTML, CSS, Chart.js

## Setup and Installation
To set up and run the project locally, follow these steps:

1. Clone the repository:
    ```bash
    git clone /home/vahid/DEV/NordpoolUMMAppTask
    cd NordpoolUMMAppTask
    ```

2. The backend will start and listen on a localhost port. Open your browser and navigate to the following URLs to access the API:

    - With date:
      ```
      http://localhost:{port}/api/umm/production-unavailability/2024-09-20&2024-09-21
      ```

    - Without date:
      ```
      http://localhost:{port}/api/umm/production-unavailability
      ```

## Usage
Once the backend is running, you can use the frontend to visualize the data. The frontend is built with JavaScript and Chart.js, providing an interactive interface for analyzing urgent market messages.

## API Endpoints
- **GET /api/umm/production-unavailability/{startDate}&{endDate}**: Retrieves production unavailability data for the specified date range.
- **GET /api/umm/production-unavailability**: Retrieves all production unavailability data.

## License
This project is licensed under the MIT License. 
