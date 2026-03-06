# Accounting.Api

Este repositorio contiene una API REST construida con .NET 10
 A continuación se detallan los pasos para configurar, ejecutar migraciones y utilizar los endpoints.

---

## 📦 Prerrequisitos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (u otro servidor SQL accesible)
- Visual Studio 2022/2023 o VS Code


## ⚙️ Configuración inicial

1. Clona el repositorio:
   ```bash
   git clone https://github.com/alexandraCaballero7/Accounting.Api.git Accounting.Api
   cd Accounting.Api/Accounting.Api
   ```
2. Restaura paquetes NuGet:
   ```bash
   dotnet restore
   ```
3. Ajusta la cadena de conexión en `appsettings.json` si es necesario. Ejemplo:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AccountingDb;Trusted_Connection=True;"
   }
   ```
4. Construye la solución:
   ```bash
   dotnet build Accounting.Api.slnx
   ```

## 🗃️ Migraciones de Entity Framework Core

Desde NuGet Package Manager Console (Visual Studio)
Abrir Package Manager Console:
Tools → NuGet Package Manager → Package Manager Console
Seleccionar como Default Project:
Accounting.Infrastructure
Crear nueva migración:
# Comandos
Add-Migracion NombreMigracion
Update-Database
----------

```bash
# generar una nueva migración desde el proyecto de infraestructura
dotnet ef migrations add NombreDeMigracion --project Accounting.Infraestructure

# aplicar migraciones al servidor configurado
dotnet ef database update
```

Las migraciones existentes se encuentran en la carpeta `Accounting.Infraestructure/Migrations`.


## 🚀 Ejecutar la API

Desde la carpeta `Accounting.Api/Accounting.Api`:

```bash
dotnet run
```

La aplicación escuchará en `https://localhost:5186` (configuración por defecto).


## 📖 Documentación de Endpoints

La API está expuesta bajo la ruta base `/api`.

| Recurso | Método | Ruta | Descripción |
|---------|--------|------|-------------|
| Employees | GET | `/api/employees` | Obtener lista de empleados |
| Employees | GET | `/api/employees/{employeeId}` | Obtener empleado por id |
| Employees | POST | `/api/employees` | Agregar empleado |
| Employees | PUT | `/api/employees/{employeeId}` | Actualizar empleado |
| Employees | DELETE | `/api/employees/{employeeId}` | Eliminar empleado |
| Vouchers | GET | `/api/vouchers` | Obtener todos los vouchers |
| Vouchers | GET | `/api/vouchers/{voucherId}` | Obtener voucher por id |
| Vouchers | GET | `/api/vouchers/employee/{employeeId}` | Obtener vouchers de empleado |
| Vouchers | POST | `/api/vouchers` | Agregar voucher |
| Vouchers | PUT | `/api/vouchers/{voucherId}` | Actualizar voucher |
| Vouchers | DELETE | `/api/vouchers/{voucherId}` | Eliminar voucher |

**Nota:** la documentación completa está disponible en Swagger cuando la aplicación se ejecuta (normalmente en `/swagger/index.html`).


## 📬 Ejemplos de Request/Response

### Crear empleado

**Request**
```http
POST /api/employees HTTP/1.1
Content-Type: application/json

{
  "firstName": "Juan",
  "lastName": "Pérez",
  "email": "juan.perez@example.com",
  "phone": "+123456789",
  "hireDate": "2024-01-15T00:00:00Z",
  "salary": 55000.00
}
```

**Response**
```http
HTTP/1.1 201 Created
Content-Type: application/json

{
  "employeeId": 1,
  "firstName": "Juan",
  "lastName": "Pérez",
  "email": "juan.perez@example.com",
  "phone": "+123456789",
  "hireDate": "2024-01-15T00:00:00Z",
  "salary": 55000.00
}
```


### Obtener voucher por id (ejemplo de respuesta 404)

```http
GET /api/vouchers/99 HTTP/1.1
```

```http
HTTP/1.1 404 Not Found
Content-Type: application/json

{
  "status": 404,
  "error": "Voucher with Id 99 not found."
}
```