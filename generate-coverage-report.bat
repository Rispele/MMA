del /S /Q ./Coverage

dotnet list package
dotnet test ./Sources/Rooms.Tests --collect:"XPlat Code Coverage" --results-directory ./Coverage/TestResults
dotnet test ./Sources/Commons.ExternalClients.Tests --collect:"XPlat Code Coverage" --results-directory ./Coverage/TestResults
dotnet test ./WebApi.Tests --collect:"XPlat Code Coverage" --results-directory ./Coverage/TestResults

@REM dotnet test /Bookings.Tests --collect:"XPlat Code Coverage"

reportgenerator -reports:./Coverage/TestResults/*/*.xml -targetdir:"./Coverage/Report"
start ./Coverage/Report/index.html

