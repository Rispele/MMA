del /S /Q TestResults
dotnet list package
dotnet test ./Rooms.Tests --collect:"XPlat Code Coverage" --results-directory ./TestResults
dotnet test ./Commons.ExternalClients.Tests --collect:"XPlat Code Coverage" --results-directory ./TestResults
@REM dotnet test /Bookings.Tests --collect:"XPlat Code Coverage"
dotnet test ./WebApi.Tests --collect:"XPlat Code Coverage" --results-directory ./TestResults
reportgenerator -reports:./TestResults/*/*.xml -targetdir:"report"
./report/index.html

