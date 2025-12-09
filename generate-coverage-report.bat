del /S /Q ./Coverage

dotnet list package
dotnet test ./Sources/Commons.ExternalClients.Tests --collect:"XPlat Code Coverage" --results-directory ./Coverage/TestResults
dotnet test ./Sources/WebApi.Tests --collect:"XPlat Code Coverage" --results-directory ./Coverage/TestResults
dotnet test ./Sources/Rooms.Tests --collect:"XPlat Code Coverage" --results-directory ./Coverage/TestResults
dotnet test ./Sources/Rooms.Migration.Tests --collect:"XPlat Code Coverage" --results-directory ./Coverage/TestResults
dotnet test ./Sources/Booking.Tests --collect:"XPlat Code Coverage" --results-directory ./Coverage/TestResults
dotnet test ./Sources/Booking.Migration.Tests --collect:"XPlat Code Coverage" --results-directory ./Coverage/TestResults

reportgenerator -reports:./Coverage/TestResults/*/*.xml -targetdir:"./Coverage/Report"
start ./Coverage/Report/index.html

