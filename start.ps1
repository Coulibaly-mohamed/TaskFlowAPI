# Aller dans le dossier du projet
cd TaskFlow.API

Write-Host " Installation des packages NuGet..."
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Swashbuckle.AspNetCore
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design

Write-Host " Lancement de la base de données via Docker..."
try {
    ../dockerDB.ps1
} catch {
    Write-Warning "Échec du lancement de Docker DB : si vous avez déjà un conteneur en cours d exécution, vous pouvez l ignorer. Si vous n utilisez pas Docker, pensez à modifier le TaskFlow.API\\appsettings.json pour pointer vers votre base de données."
}


Write-Host " Attente de 10 secondes pour le démarrage de la base de données..."
Start-Sleep -Seconds 10

Write-Host " Application des migrations..."
dotnet ef database update

# ouvre l'url dans le navigateur par défaut https://localhost:7055/swagger/index.html
Start-Process "https://localhost:7055/swagger/index.html" -WindowStyle Normal

Write-Host " Lancement du projet"
dotnet run

