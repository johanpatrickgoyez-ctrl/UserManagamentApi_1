# UserManagamentApi_1

API REST desarrollada en ASP.NET Core para la gestión de usuarios.
Este proyecto fue creado como parte de mi portafolio para demostrar conocimientos en desarrollo backend con .NET, uso de Entity Framework y buenas prácticas básicas como separación por capas y manejo de DTOs.

---

## Tecnologías utilizadas

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Swagger (OpenAPI)

---

## Estructura del proyecto

El proyecto está organizado en una sola aplicación con separación por carpetas:

* **Models** → Entidades de la base de datos
* **DTOs** → Objetos de transferencia de datos
* **Services** → Lógica de negocio
* **Data** → Configuración del DbContext
* **Controllers** → Endpoints de la API

Esta estructura permite mantener el código limpio y fácil de entender sin añadir complejidad innecesaria.

---

## Seguridad

Las contraseñas no se almacenan en texto plano.

* Se utiliza **HMACSHA512**
* Se genera un **salt único por usuario**
* Se almacena:

  * PasswordHash
  * PasswordSalt

En el login, la contraseña se vuelve a hashear con el mismo salt para validarla.

---

## Funcionalidades

* Registro de usuarios
* Inicio de sesión
* Obtener todos los usuarios
* Obtener usuario por ID
* Actualizar usuario
* Eliminar usuario

---

## Endpoints principales

 Método  Endpoint             Descripción        
 POST    /api/users/register  Registrar usuario
 POST    /api/users/login     Iniciar sesión
 GET     /api/users           Obtener todos
 PUT     /api/users/{id}      Actualizar usuario
 DELETE  /api/users/{id}      Eliminar usuario

---

## Configuración del proyecto

### 1. Clonar repositorio

```bash
git clone https://github.com/tu-usuario/UserManagamentApi_1.git
```

### 2. Configurar conexión a base de datos

Editar `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=UserManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

### 3. Ejecutar migraciones

```bash
Add-Migration InitialCreate
Update-Database
```

---

### 4. Ejecutar el proyecto

Al iniciar, Swagger estará disponible en:

```
https://localhost:{puerto}/swagger
```

---

## Decisiones técnicas

* No se utilizó AutoMapper para mantener control explícito del mapeo
* Se implementó una capa de servicios para separar la lógica del controlador

---

## Autor

**Patrick Goyez**
Estudiante de Ingeniería Informática de la Universidad internacional SEK

---

##  Notas

Este proyecto está enfocado en demostrar fundamentos sólidos de backend, priorizando claridad y comprensión sobre complejidad innecesaria.
