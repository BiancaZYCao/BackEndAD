<div float=right>
  <img src="https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F5382f6b0-53fe-4086-b2a8-0bd47762cf3a%2Flogo.png?table=block&id=2d9d5067-a082-43b4-a538-8e24e63b95b2&width=1670&userId=&cache=v2" width="400" alt="logo"/>
</div>

# Team8-AD Project Stationery Store Inventory System

## Table of Contents
- [Background](#background)
- [Platform](#platform)
- [Installation&Configuration](#Installation&Configuration)
- [Backend Structure](#.NET Core Backend Structure)
- [Contributors](#contributors)
- [License](#license)


## Background
This is an Stationery Store Inventory System Application - Backend Part.
More about project, Pls check: https://www.notion.so/Doc-Shared-Team8-Bianca-64cf8e14b5764679bd9792d59221b6a9

## Platform
- Backend: C# .NET Core Web Application to offer Web API Services (deployed via Azure)
- Databased: MsSQL Server (deployed via Azure)
- Frontend: ReactJS for Web & Android studio for Mobile App
- Machine Learning:Python & Flask

## Installation&Configuration
### [Database on Cloud - Azure SQL]
  Server:          team8-sa50.database.windows.net
  Login:            Bianca
  Password:    !Str0ngPsword
ATTENTION: At first time, u may face prob related to IP Address, pls send Bianca ur screenshot so that we can add in cloud setting. 
          (this IP maybe different from ur IP address)
### [Installation]
1. git clone https://github.com/BiancaZYCao/BackEndAD.git
2. choose Branch Martin
3. open in visual studio check all nuget packages 
4. run as BackendAD mode (will auto open http://localhost:5001)
    test via localhost:5001/api/dept, you should be able to see JSON results in a list.
We also deploy our backend in Azure: click the link to view https://backendad.azurewebsites.net/api

## Contributors
<a href="https://github.com/BiancaZYCao/BackendAD/graphs/contributors">
  <img src="https://contributors-img.web.app/image?repo=BiancaZYCao/BackendAD" />
</a>

Made with [contributors-img](https://contributors-img.web.app).


## .NET Core Backend Structure
### Web Api Controller:
  - Department 
  - Store 
  - Scheduler
  
### Service 
  - Department Service
  - Store Service
  - Manager Service
  - Spuerviser Service
  - email Servcie 
  
### Models
  - check ERD
 
### Other techiques

### External Libraries

  
## License

