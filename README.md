# __fluffyspoon__

The purpose of this project is to demo microservice architecture using .NET Core and Microsoft Orleans

[![Build status](https://img.shields.io/azure-devops/build/christopherdemicoli/8c7d1a1e-f368-46cf-bad7-1f2ed587335d/16)](https://img.shields.io/azure-devops/build/christopherdemicoli/8c7d1a1e-f368-46cf-bad7-1f2ed587335d/16) 
[![NuGet](https://img.shields.io/nuget/v/demofluffyspoon.contracts.svg)](https://nuget.org/packages/demofluffyspoon.contracts)

## List of Services

* Registration: [https://github.com/cdemi/fluffy-spoon]
* Profile: [https://github.com/cdemi/fluffy-spoon-profile]
* User Verification: [https://github.com/cdemi/fluffy-spoon-userverification]
* Email: [https://github.com/cdemi/fluffy-spoon-email]

## __Build and Run__

To build and run via Docker.
```sh
cd docker
docker-compose up --build
```

To run all the services together:

```sh
docker-compose -f fluffy-spoon/docker/docker-compose.yml \
 -f fluffy-spoon-profile/docker/docker-compose.yml \
 -f fluffy-spoon-userverification/docker/docker-compose.yml \
 -f fluffy-spoon-email/docker/docker-compose.yml up
```