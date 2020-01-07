# __fluffyspoon__

The purpose of this project is to demo microservice architecture using .NET Core and Microsoft Orleans

[![Build status](https://img.shields.io/azure-devops/build/christopherdemicoli/8c7d1a1e-f368-46cf-bad7-1f2ed587335d/16)](https://img.shields.io/azure-devops/build/christopherdemicoli/8c7d1a1e-f368-46cf-bad7-1f2ed587335d/16) 
[![NuGet](https://img.shields.io/nuget/v/demofluffyspoon.contracts.svg)](https://nuget.org/packages/demofluffyspoon.contracts)

## List of Services

All the below services are based on the [fluffy-spoon microservice template](https://github.com/cdemi/fluffy-spoon-template)!

* Registration: [Source Code](https://github.com/cdemi/fluffy-spoon)
* Profile: [Source Code](https://github.com/cdemi/fluffy-spoon-profile)
* User Verification: [Source Code](https://github.com/cdemi/fluffy-spoon-userverification)
* Email: [Source Code](https://github.com/cdemi/fluffy-spoon-email)

## __Build and Run__

To build and run via Docker.
```sh
git clone git@github.com:cdemi/fluffy-spoon.git
cd docker
docker-compose up --build
```

To run all the services together:

```sh
# Download source code
git clone https://github.com/cdemi/fluffy-spoon.git
git clone https://github.com/cdemi/fluffy-spoon-profile.git
git clone https://github.com/cdemi/fluffy-spoon-userverification.git
git clone https://github.com/cdemi/fluffy-spoon-email.git

# Build docker images
docker-compose -f fluffy-spoon/docker/docker-compose.yml build
docker-compose -f fluffy-spoon-profile/docker/docker-compose.yml build
docker-compose -f fluffy-spoon-userverification/docker/docker-compose.yml build
docker-compose -f fluffy-spoon-email/docker/docker-compose.yml build

# Run Containers
docker-compose -f fluffy-spoon/docker/docker-compose.yml \
 -f fluffy-spoon-profile/docker/docker-compose.yml \
 -f fluffy-spoon-userverification/docker/docker-compose.yml \
 -f fluffy-spoon-email/docker/docker-compose.yml up
```