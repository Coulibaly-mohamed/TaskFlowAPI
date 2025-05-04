
# 📌 TaskFlow API

Bienvenue dans le projet **TaskFlow API** 
Une API REST développée en C# pour gérer des utilisateurs, projets et tâches. 
Ce projet a été réalisé dans le cadre d’un exercice pédagogique autour des bonnes pratiques de développement backend avec ASP.NET Core, Entity Framework et l'authentification JWT.

---

## 🚀 Objectifs

- Proposer une API REST permettant de gérer des utilisateurs, des projets, et des tâches.
- Implémenter une architecture claire, sécurisée et modulaire.
- Utiliser Entity Framework pour la persistance des données avec SQL Server.
- Sécuriser les endpoints via des tokens JWT.
- Documenter l’API avec Swagger.

---

## 🧱 Technologies utilisées

- ASP.NET Core 7+
- Entity Framework Core
- SQL Server / SQLite (optionnel pour tests)
- Swagger / Swashbuckle
- JWT (JSON Web Token)
- C#

---

## 🗂️ Architecture du projet

- `Controllers/` — Gère les endpoints REST.
- `Models/` — Contient les entités (User, Project, Task).
- `DTOs/` — Objets de transfert de données.
- `Services/` — Logique métier (via injection de dépendances).
- `Data/` — Contexte EF Core et configuration de la base.
- `Middlewares/` — Gestion centralisée des erreurs.

---

Voici une version claire et structurée de la section **Installation** pour ton `README.md`, adaptée à ton projet TaskFlowAPI :

---

## 🛠️ Installation

### Prérequis

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
* [Docker](https://www.docker.com/)
* (Facultatif) [Visual Studio 2022+](https://visualstudio.microsoft.com/) ou VS Code

---

### 1. Cloner le dépôt

```bash
git clone https://github.com/Coulibaly-mohamed/TaskFlowAPI.git
cd TaskFlowAPI
```



### 2. Démarrer la base de données avec Docker

Un script PowerShell `dockerDB.ps1` est fourni pour lancer un conteneur SQL Server local :

```powershell
.\dockerDB.ps1
```

> ⚠️ Assurez-vous que Docker est installé et en cours d'exécution.
> Le mot de passe SA par défaut est défini dans le script (`YourStrong@Passw0rd`).
> Se scripte est à titre indicatif, il est lancé dans Start.ps1 !
> <!> Nous avons fais le choix de ne pas mettre de volume persistant pour le docker <!>


### 3. Lancer le projet

Le script `start.ps1` automatise l'installation des packages, le lancement de la base de données, les migrations EF et l'exécution de l'API :

```powershell
.\start.ps1
```

Ce script effectue :

* L'installation des packages NuGet nécessaires
* Le lancement de la base SQL avec Docker (si nécessaire)
* L'application des migrations Entity Framework
* Le démarrage du projet via `dotnet run`
* L'ouverture automatique de la documentation Swagger à l'adresse :

  [https://localhost:7055/swagger/index.html](https://localhost:7055/swagger/index.html)

---

### 4. ⚙️ Configuration manuelle (optionnel)

Si vous ne souhaitez pas utiliser Docker, vous pouvez modifier le fichier `TaskFlow.API/appsettings.json` pour y configurer votre propre base de données SQL Server.


## 5. 📖 Documentation Swagger
Swagger est accessible à l'adresse suivante :

https://localhost:7055/swagger/index.html

Elle documente tous les endpoints, objets, schémas et exemples de requêtes/réponses.

![Aperçu du projet](assets/routes.png) <!-- Ajout de l'image -->



## 6. ❌ Gestion des erreurs

Le projet intègre une gestion centralisée des erreurs via un **middleware personnalisé** (`ExceptionMiddleware`). Cela permet de retourner des messages d'erreur cohérents au format JSON en fonction du type d'exception levée.

### ✅ Exemples d’erreurs gérées :

| Type d'erreur                       | Exception levée               | Code HTTP | Message renvoyé                                                   |
| ----------------------------------- | ----------------------------- | --------- | ----------------------------------------------------------------- |
| Email déjà utilisé                  | `DbUpdateException`           | 409       | "Erreur de base de données (conflit ou violation de contrainte)." |
| Ressource introuvable               | `KeyNotFoundException`        | 404       | "La ressource demandée est introuvable."                          |
| Projet appartenant à un autre user  | `UnauthorizedAccessException` | 403       | "Ce projet ne vous appartient pas." / "Accès refusé."             |
| Requête invalide (paramètres, etc.) | `ArgumentException`           | 400       | "Requête invalide."                                               |
| Erreur non gérée                    | `Exception`                   | 500       | "Une erreur interne est survenue sur le serveur."                 |

---

### 🛡️ Exemple : suppression d’un projet non autorisé

Dans `ProjectService.cs` :

```csharp
if (project.UserId != userId)
    throw new UnauthorizedAccessException("Ce projet ne vous appartient pas.");
```

Résultat : l’API renvoie une erreur 403 - **"Accès refusé."**

---

### 🖼️ Aperçu d’erreurs dans Swagger

#### Erreur : email déjà utilisée


![Erreur : email déjà utilisée](assets/Email_deja_utilise.png)

> Cette erreur est déclenchée lors de la tentative de création d’un compte avec un email déjà existant.






## 6. Bonus
✅ Injection de dépendance
La logique métier est séparée dans des services injectés via les interfaces dans les contrôleurs. Cela facilite les tests et la maintenabilité.
🗃️ Contenu du livrable
🧩 Fichier de migration
📚 Swagger généré automatiquement
📄 Ce fichier README.md
💻 Site Web de démo `DEMO-HTML.html`
🦅 Scriptes de lancement rapide!
👨‍💻 Développé par:
* Denisot Jimmy
* Coulibaly mohamed


----
## 📬 Contact
Pour toute question, contactez-nous !

----
## 📜 Licence
Projet pédagogique — tous droits réservés.

