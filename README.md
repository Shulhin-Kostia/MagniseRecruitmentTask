# MagniseRecruitmentTask
This is recruitment task implementation of Kostiantyn Shulhin for .NET Developer position at Magnise.  

## Tech Stack
**ASP.NET Core Minimal API**  
**PostgreSQL**  
**Docker / Docker Compose**

## Some implementation details
**Minimal API** approach was chosen due to small size and simplicity of project. Repositories weren't used because only simple crud operations are performed and they would have cloned existing EF Core features.  
  
CoinAPI REST API was used to periodically retrieve exchange rates of cryptocurrencies to USD. WebSockets were not used because of the limitations of free api key. Only trades info can be obtained via websockets, which is not representing actual prices of coins.  
  
**Note:**  
WebSockets data fetching approach is preferred because CoinAPI subscription has limited REST API requests which can become a financial problem. WebSockets only count connection request, so there're no limitation on amount of supported coins and requests. But, to use Websockets effectively, ```exrate``` endpoint acccess is required.  
  
**Another approach:**  
If there was no requirement to store info in db then using cache is better on my opinion. This will simplify logic, increase performance and stabililty. Using such approach eliminates manual periodic background data fetches and local database write operations.

## Prerequisites
[Docker and Docker Compose](http://www.docker.io/gettingstarted/#h_installation) have to be installed and Docker engine has to be running.  
[.NET 8 ASP.NET Core Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) has to be installed.

## Build and run
Steps to build and run application using Docker Compose:
1. Clone the repo
```
git clone https://github.com/cb372/docker-sample.git
```
2. Navigate to solution folder

3. Build and run images with docker-compose
```
docker-compose up --build -d
```

Now the application can be accessed via ```localhost:5000```  
Swagger UI is available via ```localhost:5000/swagger``` route
