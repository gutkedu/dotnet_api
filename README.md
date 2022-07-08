# Catalog API - A study of the .NET stack

This API implements a simple CRUD on items to demonstrate how to create a API
in .NET using principles of clean architecture.

## Technologies

- .NET 6
- MongoDB
- XUnit
- Moq]
- Docker
- Minikube

The application was containerized with Docker with the following commands:

### mongodb docker container:

```
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=root -e MONGO_INITDB_ROOT_PASSWORD=root --network=net6 mongo
```

### catalog docker container:

```
 docker run -it --rm -p 8080:80 -e MongoDbSettings:Host=mongo -e MongoDbSettings:Password=root --network=net6 catalog:v1
```

Then, the catalog container was uploaded to DockerHub to allow the use of Kubernetes. For this project, I utilized a Kubernetes application that runs locally on my computer, called Minikube.

The application Unit tests were done utilizing the Xunit framework.
