# NFL Depth Chart Management System

## Overview

This project is a C# .NET application that manages the depth charts of NFL teams. It provides functionality to add, remove, and retrieve players from the depth chart based on their positions.
Currently it only allows to add for NFL team Tampa Bay Buccaneers. However, the solution is supporing additional sports and additional teams and requires slight modification inside Program.cs in TradingSolutionsAPI to add more teams and sports.

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

![image](https://github.com/user-attachments/assets/685f51c6-8bf0-4c78-8c1d-aa0c2d31e6cc)

The swagger API consists of 4 APIS

- AddPlayer
- RemovePlayer
- GetBackups
- GetFullDepthChart

To Add Player to depth chart, add details for Tom Brady:

![image](https://github.com/user-attachments/assets/d22e5fbf-ae53-4097-936a-0be009aa53eb)

To get full depth chart for a given team:

![image](https://github.com/user-attachments/assets/b560615b-6d1b-425b-ab71-d9a2243bc941)




### 4. Running Tests

Automated unit tests are included in the solution to verify the correctness of the code. To run the tests, use the following command:

```bash
dotnet test
```

It can also be run from visual studio by selecting from menu Test -> Run All Tests
