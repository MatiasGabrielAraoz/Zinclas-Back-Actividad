# Zinclas-Back-Actividad
## **Consigna:**
Para cumplir con este ejercicio tienen que crear un pequeño sistema que permita gestionar cursos, alumnos y asistencias usando ASP.NET Core. La API que armen debe incluir el CRUD (create, read, update, delete) de cursos y alumnos, el registro de las asistencias, la persistencia en una base de datos (puede ser SQL Server Express o PostgreSQL que posiblemente sea al motor que migremos) mediante Entity Framework Core y su arquitectura básica usando servicios y controladores.
## **Capacidades**
- **Cursos**: <br>
    Obtener todos los cursos <br>
    Obtener un curso específico <br>
    Crear cursos <br>
    Eliminar cursos <br>

---

- **Alumnos**: <br>
    Obtener todos los alumnos <br>
    Obtener un alumno específico <br>
    Crear alumnos <br>
    Eliminar alumnos <br>
    Cambiar de información de alumnos <br>

---

- **Asistencias**: <br>
    Obtener las asistencias de un alumno en x día <br>
    Obtener el porcentaje de asistencias de un alumno a lo largo del año junto a su cantidad de presentes y ausentes <br>
    Registrar asistencia de un alumno en x día <br>
    Eliminar la asistencia de un alumno en x día <br>
    Modificar la asistencia de un alumno en x día en caso de un error o justificación <br>

## Estructura
- **GestionApi**: <br>
    Se encarga de manejar la persistencia de los datos en PostgreSQL usando Entity Framework Core <br>
    Utiliza DTOs para enviar información para evitar exponer información sensible de la base de datos <br>
- **GestionApiClient**: <br>
    Es una librería que se encarga de gestionar la interacción con la api creando funciones para facilitar la interacción con la api <br>
- **GestionApiConsole**: <br>
    Es una aplicación de consola que usando la librería del cliente permite la interacción con la api desde una consola con comandos simples

## Como instalarlo
Aclaraciones: debes tener una base de datos en postgreSQL de nombre ZinclasDB, proximamente se le añadirá una imagen de docker que automatice este proceso y elimine dependencias para facilitar la instalación.
```
# 1. Clonar el repo
git clone https://github.com/MatiasGabrielAraoz/Zinclas-Back-Actividad

# 2. Entrar en el directorio del repo
cd Zinclas-Back-Actividad

# 3. Crear las migraciones (Entity Framework)
dotnet ef database update --Project GestionApi

# 4. arrancar la Api
dotnet run --project GestionApi
```


