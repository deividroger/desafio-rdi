## **Hello!**

**Some important information:
**

This application was configured to run on ports 8081 and 8082. I put a self-signed certificate inside the project because that could happen when you'll run a message at your browser show up saying 'this is not a trusted website'.

if you feel bothered with this, just remove (or comment) the  variables of docker-compose bellow:

ASPNETCORE_Kestrel__Certificates__Default__Password
ASPNETCORE_Kestrel__Certificates__Default__Path

Doing this the app will run without a certificate.

I installed the swagger package to help me to document the API. Then, you can access by:

- http://localhost:8081/swagger
- https://localhost:8082/swagger

I tried to keep the solution clean as possible.


To run this project, open your preferred terminal and run:

```shell
docker-compose up

```
This app was build with

-  .net core 3.1
-  xunit
-  FluentAssertions
-  Swashbuckle
-  AutoMapper
- Docker
- Docker-compose
- Self-sign certificate

Enjoy.

Thank you so much