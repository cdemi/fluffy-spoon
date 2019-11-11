# fluffy_spoon

## Build and Run

To run the services using docker, use the following command:

```
docker-compose -f fluffy-spoon/docker/docker-compose.yml -f fluffy-spoon-profile/docker/docker-compose.yml -f fluffy-spoon-userverification/docker/docker-compose.yml up profile registration userverification
```