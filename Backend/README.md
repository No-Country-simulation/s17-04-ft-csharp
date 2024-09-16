# 🌟 JuniorHub API

Esta es una API creada en ASP.NET Core Web API utilizando Clean Architecture.

## 🚀 Tecnologías Utilizadas

- **ASP.NET Core Web API**
- **Identity**: Gestión de usuarios y roles.
- **FluentValidation**: Validaciones.
- **AutoMapper**: Mapeos con las entidades.
- **Cloudinary** :Almacenamiento de imagenes.
- **SendGrid**: Envío de notificaciones cuando un employer selecciona un freelancer para hacer el trabajo.

## 🏗️ Estructura del Proyecto

- **API**: Contiene los controladores y las configuraciones específicas de la API.
- **Core**: 
  - **Application**: Contiene la lógica de negocio, servicios, interfaces y DTOs.
  - **Domain**: Contiene las entidades del dominio y enums.
  - **Mappings**: Contiene los mapeos de la aplicación.
- **Infrastructure**:
  - **Persistence**: Contiene la implementación de la persistencia de datos y Identity.
  - **Cloudinary**: Contiene la implementación de Cloudinary para guardar las imagenes de los perfiles de freelancer y employers.
  - **SendGrid**: Contiene la implementación de SendGrid para notificaciones.

## 🔧 Configuración del Entorno de Desarrollo

### Prerrequisitos

- .NET 8.0 o superior
- Visual Studio 2022 o superior / Visual Studio Code
- SQL Server
- Cloudinary (tener una ApiKey)
- SendGrid (tener una ApiKey)

### Instalación

1. Clona el repositorio:
```bash
    git clone https://github.com/No-Country-simulation/s17-04-ft-csharp.git
    cd justina-api
```

2. Configura las variables de entorno necesarias en el archivo `appsettings.json` y `appsettings.Development.json`:
```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=JuniorHub;Trusted_Connection=True;"
      },
      "JwtConfiguration": {
        "Key": "your-key",
        "Issuer": "https://localhost:5001/",
        "Audience": "https://localhost:5001/",
        "DurationInHours": 24,
        "ValidateLifeTime": true,
        "ValidateIssuer": true,
        "ValidateAudience": true,
        "ValidateIssuerSigningKey": true
      },
      "CloudinaryConfiguration": {
        "Cloud": "",
        "ApiKey": "your-api-key",
        "ApiSecret": "your-api-secret"
      },
      "SendGridConfiguration": {
        "ApiKey": "your-api-key"
      }
    }
```

3. Restaura las dependencias e inicializa la base de datos:
```bash
    dotnet restore
    dotnet ef database update
```

### ▶️ Ejecutar la API

1. Ejecuta el proyecto:
```bash
    dotnet run
```

2. La API estará disponible en `https://localhost:5001` (o `http://localhost:5000`).

### 🛠️ Probar la API Localmente

Puedes utilizar herramientas como [Insomnia](https://insomnia.rest/download) o [Swagger](https://swagger.io/) para probar los endpoints de la API.

1. Abre tu navegador y navega a `https://localhost:5001/swagger` para ver la documentación de la API generada automáticamente por Swagger.

## 🤝 Contribuir

Si deseas contribuir a este proyecto, por favor sigue los pasos a continuación:

1. Haz un fork del proyecto.
2. Crea una nueva rama (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza tus cambios y haz commit (`git commit -am 'Agregar nueva funcionalidad'`).
4. Sube tu rama (`git push origin feature/nueva-funcionalidad`).
5. Abre un Pull Request.

## 📜 Licencia

Este proyecto está licenciado bajo los términos de la licencia MIT. Consulta el archivo [LICENSE](LICENSE) para obtener más detalles.

---
