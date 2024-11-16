# NFL Depth Chart Management System

## Overview

This project is a C# .NET application that manages the depth charts of NFL teams. It provides functionality to add, remove, and retrieve players from the depth chart based on their positions.
Currently it only allows to add for NFL team Tampa Bay Buccaneers. However, the solution is supporing additional sports and additional teams.
It is consisting of 3 Projects

- TradingSolutionsAPI: Web API project with Swagger API
- TradingSolutionsCore: Core project where the repository, service, models sits
- TradingSolutionsTests: Unit Test project in XUnit

  
## Prerequisites

- .NET SDK (version 8.0 or higher)
- Visual studio 2022 or higher

## How to Build and Run the Code

### 1. Clone the Repository

First, clone the repository to your local machine:

```bash
git clone https://github.com/farjaf/FanDuel.git
```

### 2. Build the Solution

Open the solution (TradingSolutions.sln) in visual studio and build the project. 

```bash
dotnet build
```

### 3. Run the Application

Select TradingSolutionsAPI as startup project and run the project. It will open swagger as below:

![image](https://github.com/user-attachments/assets/1b571641-32f8-495b-96b1-1192fd9d946a)



The swagger API consists of 4 APIS

- AddPlayerToDepthChart
- RemovePlayerFromDepthChart
- GetBackups
- GetFullDepthChart

To Add Player to depth chart, add details for Tom Brady:

![image](https://github.com/user-attachments/assets/3a04fe50-9198-42a0-abfe-080adf7bca78)


To get full depth chart for a given team:

![image](https://github.com/user-attachments/assets/bffc89e8-ecdd-4ce6-8c51-877e69b601fd)





### 4. Running Tests

Automated unit tests are included in the solution to verify the correctness of the code. To run the tests, use the following command:

```bash
dotnet test
```

It can also be run from visual studio by selecting from menu Test -> Run All Tests
