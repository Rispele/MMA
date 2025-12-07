del /S /Q TestResults
dotnet list package
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:TestResults\*\*.xml -targetdir:"report"
..\report\index.html

