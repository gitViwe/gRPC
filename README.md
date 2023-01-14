<!-- ABOUT THE PROJECT -->
# gRPC

gRPC is based around the idea of defining a service, specifying the methods that can be called remotely with their parameters and return types.


<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple example steps.

### Prerequisites

Things you need to use the software and how to install them.
* [Visual Studio / Visual Studio Code](https://visualstudio.microsoft.com/)
* [.NET 7](https://devblogs.microsoft.com/dotnet/announcing-dotnet-7/)
* [Docker](https://www.docker.com/)

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/gitViwe/gRPC.git
   ```
2. Run via Docker
   ```sh
   cd grpc
   docker compose up -d
   ```
3. Run via Docker hub
   ```
   docker run -d -p 5007:80 --env ASPNETCORE_ENVIRONMENT=Development hubviwe/grpc.server:1.0.0
   ```

Then can make gRPC unary calls to [http://localhost:5007](http://localhost:5007), using a client of your choice such as [Insomnia](https://insomnia.rest/download) or [Postman](https://www.postman.com/downloads/).

