### Build Instructions

1. clone the project and run
```shell
./download.sh
```
BC data for 31 days will be downloaded inside folder data (default May 2016)

2. Create neo4j image from Dockerfile, run a container
```shell
docker build -t myneo4j . --no-cache
docker container run -p 7474:7474 -p 7687:7687 -v data:/data -v plugins:/var/lib/neo4j/plugins -v conf:/var/lib/neo4j/conf --env NEO4J_AUTH=neo4j/test myneo4j
```

3. Download .net 6.0 sdk, build and run inserter app
```shell
cd inserter
dotnet build -c Release -o app
cd app
./inserter
```

4. After successful data insertion, initiate the web API
```shell
cd server
dotnet build -c Release -o app
source .env && app/server
```


Make http requests to 12 endpoints