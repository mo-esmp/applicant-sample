# applicant-sample
To run the API project you need to install [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0). After installing SDK go to `src/Hahn.ApplicationProcess.December2020.Web/` and run `dotnet run` command. The application will be launched on `http://localhost:5000` and `https://localhost:5001` and swagger API documentation can be accessed on `https://localhost:5001/swagger/index.html`.

To run the client application go to `src/ClientApp/` and run `npm install` to install dependecies and `ng serve` to launch applicantion. The application will be launched on `http://localhost:4200`. If you want to run the API on another address or port, you can configure server address on `src\ClientApp\src\environments\environment.ts` and change `serverBaseUrl` value.
