# **Game of Life API**

An API built using **.NET Core** to simulate Conway's Game of Life. The API allows you to create, update, and retrieve the states of the game as it evolves over generations based on a set of predefined rules.

## **Table of Contents**

- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the API](#running-the-api)
- [API Endpoints](#api-endpoints)
- [Technologies Used](#technologies-used)
- [How to Contribute](#how-to-contribute)
- [License](#license)

## **Features**

- Create and initialize a new game board with a specified size and initial state.
  - (Plus, permit to create with a Random Data)
- Fetch the next generation based on the current state of the game.
- Retrieve a specific number of generations ahead.
- Check the final state after a set number of iterations.
- SQLite for persistent storage of game states.
- Swagger documentation for easy API testing and exploration.

## **API Demo in Production**

### Swagger

https://game-of-life-production-de86.up.railway.app/index.html

### Simple Front End consuming API

https://game-of-life-ui-production.up.railway.app/

## **Getting Started**

Follow these instructions to set up and run the project locally.

### **Prerequisites**

Ensure you have the following installed:

- [.NET Core SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker](https://www.docker.com/get-started) (for containerized deployment)

### **Installation**

1. Clone the repository:

   ```bash
   git clone git@github.com:danielv2/game-of-life.git
   cd game-of-life-api
   ```

2. Install the necessary dependencies:

   ```bash
   dotnet restore
   ```

3. Set up the database (SQLite by default):

   ```bash
   dotnet ef database update --project GOF.Host --startup-project GOF.Host
   ```

### **Running the API**

**Locally**

To run the API locally, execute the following command:

```bash
dotnet run --project GOF.Host

```

The API will be available at https://localhost:5001 and http://localhost:5000.

**Using Docker**

Build and run the API using Docker:

```bash
docker build -t game-of-life-api .
docker run -p 5000:5000 -p 5001:5001 game-of-life-api
```

## **API Endpoints**

Here is a list of the primary endpoints provided by the API.

### **Game Management**

- **Create New Game**  
  `POST /api/v1/games`
  Initializes a new game board.

  **Request Body:**

  ```json
  {
    "squareSideSize": 5,
    "initialState": [
      [0, 1, 0],
      [1, 0, 1],
      [0, 1, 0]
    ],
    "maxGenerations": 50
  }
  ```

- **Get All games**  
  `GET /api/v1/games`
  Fetches all games

- **Get game**  
  `GET /api/v1/games/{id}`
  Fetches specific game

- **Get Next Generation**  
  `GET /api/v1/games/{id}/next?quantity=1&lastState=false`
  Retrieves the next generation(s) based on the current state.

## **Health Check**

- **Health Status**  
  `GET /health`
  Check the health status of the API.

## **Technologies Used**

- **.NET Core 7.0**
- **Entity Framework Core** (SQLite)
- **Swagger** for API documentation
- **Docker** for containerization
- **Railway** for cloud deployment

## **Swagger Documentation**

Swagger is integrated into the API for easy exploration and testing of endpoints. After starting the API, visit the following URL to access the Swagger UI:

```bash
http://localhost:5000/index.html
```
