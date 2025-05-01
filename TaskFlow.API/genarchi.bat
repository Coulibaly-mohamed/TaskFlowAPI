@echo off
cd %~dp0

REM Création des dossiers
mkdir Controllers
mkdir Models
mkdir DTOs
mkdir Services\Interfaces
mkdir Services\Implementations
mkdir Data
mkdir Middleware
mkdir Helpers

REM Création des fichiers (vides)
echo. > Controllers\UsersController.cs
echo. > Controllers\ProjectsController.cs
echo. > Controllers\TasksController.cs

echo. > Models\User.cs
echo. > Models\Project.cs
echo. > Models\TaskItem.cs

echo. > DTOs\RegisterUserDto.cs
echo. > DTOs\LoginUserDto.cs
echo. > DTOs\ProjectDto.cs
echo. > DTOs\TaskDto.cs

echo. > Services\Interfaces\IUserService.cs
echo. > Services\Interfaces\IProjectService.cs
echo. > Services\Interfaces\ITaskService.cs

echo. > Services\Implementations\UserService.cs
echo. > Services\Implementations\ProjectService.cs
echo. > Services\Implementations\TaskService.cs

echo. > Data\AppDbContext.cs
echo. > Middleware\ExceptionMiddleware.cs
echo. > Helpers\JwtHelper.cs

echo Architecture créée avec succès.
pause
