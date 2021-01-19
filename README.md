# About

This is a demo project to introduce myself to Azure services. The frontend is just a simple todo app(https://github.com/Matteeh/react-hooks-todo).
The backend is built with .Net and is hosted on Azure app services. The backend consists of three microservices; Auth:(https://github.com/Matteeh/.Net-authorization-microservice), Users:(https://github.com/Matteeh/.Net-users-microservice) and Todos(https://github.com/Matteeh/.Net-todo-microservice).
The database in use is Azure CosmosDB.


## Table of content

- [Pre-reqs](#Pre-reqs)
- [Installation](#installation)
- [Running The Application](#Runningtheapplication)
- [Deploying](#Deploying)
- [Authors](#Authors)

## Pre-reqs

To build and run this app locally you will need to install the following:

- [.NET](https://dotnet.microsoft.com/)

## Installing dependencies

Install nugget packages

```
dotnet restore 
```

## Running the application

Use the dotnet CLI tool to run the application

```
dotnet run
```

## Deploying

Use the dotnet CLI tool to build the production version of the application

```
dotnet build
```

### Azure
https://docs.microsoft.com/en-us/azure/devops/pipelines/targets/webapp?view=azure-devops&tabs=yaml
Create a resource group
```
az group create --name myResourceGroup --location "West Europe"
```
Create a SQL API Cosmos DB account with session consistency and multi-region writes enabled
```
az cosmosdb create \
    --resource-group $resourceGroupName \
    --name $accountName \
    --kind GlobalDocumentDB \
    --locations regionName="South Central US" failoverPriority=0 --locations regionName="North Central US" failoverPriority=1 \
    --default-consistency-level "Session" \
    --enable-multiple-write-locations true
```


## Authors

- **Mathias Rahikainen** - [Matteeh](https://github.com/matteeh)
