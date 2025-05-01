# Définir les variables pour les paramètres Docker
$containerName = "taskflow-sql"
$imageName = "mcr.microsoft.com/mssql/server:2022-latest"  # Tag corrigé
$saPassword = "YourStrong@Passw0rd"
$portMapping = "1433:1433"

# Exécuter le conteneur Docker
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$saPassword" `
-p $portMapping --name $containerName `
-d $imageName
