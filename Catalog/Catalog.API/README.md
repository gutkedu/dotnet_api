### mongodb docker container:

```
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=root -e MONGO_INITDB_ROOT_PASSWORD=root --network=net6 mongo
```

### catalog docker container:

```
 docker run -it --rm -p 8080:80 -e MongoDbSettings:Host=mongo -e MongoDbSettings:Password=root --network=net6 catalog:v1 #or gutkedu/catalog:v1
```
