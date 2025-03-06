# Aplicacion de Eventos

## **Problematica**

En la actualidad, organizar eventos, como lo son: las bodas, cumpleaños, conferencias, actividades deportivas, etc., 
todas estas enfrentan un desafío común, como la gestión de invitaciones y confirmaciones. La mayoría recurrimos a grupos de WhatsApp o aplicaciones de mensajería
, pero estas herramientas generan desorganización, así como mensajes repetidos y dificultades para registrar las respuestas de manera claras, teniendo una situación 
que termina complicando aquellos eventos sencillos
Surgiendo así, una solución práctica, es decir, una plataforma web diseñada específicamente para poder gestionar eventos de manera profesional y accesible. 
La idea es tener, un espacio donde el organizador pueda crear un evento con detalles precisos (como: la fecha, ubicación, requisitos) y enviar invitaciones personalizadas. 
Los invitados, por su parte, confirmarían su asistencia de forma rápida y ordenada, sin depender de los chats masivos o respuestas perdidas entre las notificaciones

## **Integrantes**
- Aguilar Chuc Noely Monserrat
- Gonzalez Romero Joshua
- Martinez Lopez Irving
- Laines Cupul Evelin Yasmin
- Sánchez Martínez Jesús Alexander Abad

## **Librerias**
- BCrypt.Net-Next
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

## **Como correr el proyecto**

**Paso 1**:
Una vez clonado el proyecto y ejecutado en Visual Studio, abre una terminal en la carpeta del proyecto y ejecuta el comando:
```
dotnet restore

```
**Paso 2**:
Crea tu cadena de conexion en el archivo **appsettings.json**
````
"ConnectionStrings": {
  "DefaultConnection": "Server=Tu_Servidor_de_SQL;Database=AppEventos;Trusted_Connection=True;Encrypt=False"
}
````

**Paso 3**:
Abre la **Consola de Administrador de Paquetes** y ejecuta:
````
update-database

````
Esto correra las migraciones.

**Una vez configurado el proyecto puede correr**
