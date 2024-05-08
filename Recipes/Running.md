# How to run in docker
- Download ```docker-desktop``` (It will download docker and provide a nice UI)
- Launch ```docker-desktop```
- Navigate to ./VU-PSK/Recipes in CMD
- Run ```docker-compose up --build```
- If ```http://localhost:8080/swagger``` does not work, send requests via postman

# How to run locally with DB in docker
- Download ```docker-desktop``` (It will download docker and provide a nice UI)
- Launch ```docker-desktop```
- - Comment out the entire ```app``` node. Only leave the ```DB``` node
- Navigate to ./VU-PSK/Recipes in CMD
- Run ```docker-compose up --build```
- Build solution, select ```https``` run configuration and run server

# How to run locally
Fill this in if you decide to build locally :)
