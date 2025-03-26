# Aplicacion de Eventos

## **Problematica**
En la actualidad, organizar eventos como bodas, cumpleaños, conferencias, actividades deportivas, entre otros, enfrenta un desafío común: la gestión eficiente de invitaciones y confirmaciones.
La mayoría de las personas recurren a grupos de WhatsApp o aplicaciones de mensajería, pero estas herramientas presentan diversas limitaciones, como:
-Desorganización: La información relevante del evento (fecha, hora, ubicación) puede perderse fácilmente entre la cantidad de mensajes no relacionados.
-Mensajes repetidos o confusos: La constante repetición de datos o preguntas genera redundancia y dificulta la obtención de información clara.
-Falta de confirmación estructurada: No existe un mecanismo claro para registrar las confirmaciones o rechazos, lo que complica la gestión de la lista de asistentes.
-Poca personalización: No hay forma de personalizar las invitaciones ni de enviar recordatorios automáticos.
 ## **Consecuencias**
-Dificultad para llevar un control preciso del número de asistentes.

-Confusión en la comunicación, lo que puede afectar la correcta organización del evento.

-Problemas logísticos, como falta de planeación adecuada del catering, la disposición de espacios o el material necesario.

## **Solucion**
Una plataforma web diseñada específicamente para gestionar eventos de manera profesional y accesible.
-Organización centralizada: Crear eventos con detalles específicos (fecha, ubicación, requisitos, descripción).

-Invitaciones personalizadas: Enviar invitaciones únicas a cada invitado, incluyendo la opción de confirmar o rechazar la asistencia.

-Gestión de respuestas automatizada: Registrar de forma clara y estructurada las confirmaciones, rechazos y comentarios adicionales.

-Panel de control para el organizador: Visualizar en tiempo real el estado de las confirmaciones y administrar fácilmente la lista de invitados.

-Notificaciones automáticas: Enviar recordatorios y actualizaciones a los invitados de forma automática.
## **Organizadores**
-Creación de eventos: Formulario intuitivo para agregar detalles del evento (nombre, fecha, lugar, descripción).

-Gestión de invitados: Subida masiva de contactos (por CSV) o ingreso manual.

-Generación de invitaciones personalizadas: Cada invitación incluirá un enlace único para confirmar o rechazar la asistencia.

-Seguimiento en tiempo real: Panel para visualizar la cantidad de confirmaciones, rechazos y pendientes.

-Notificaciones automáticas: Envío de recordatorios a los invitados por correo o SMS.

-Exportación de lista de asistentes: Generar un reporte en formato CSV o PDF con la lista final de confirmados.

## **Invitados**
-Recepción de invitaciones personalizadas: Los invitados recibirán un correo con un enlace único para acceder al evento.

-Confirmación sencilla: Opción para confirmar o rechazar la asistencia con un solo clic.

-Formulario de comentarios: Posibilidad de añadir comentarios adicionales o especificar requerimientos especiales (alergias, dietas, etc.).

-Recordatorios automáticos: Notificaciones previas al evento para evitar olvidos.

## **Integrantes**
- Aguilar Chuc Noely Monserrat - 22393209@utcancun.edu.mx - **Git**: Acmonse11
- Gonzalez Romero Joshua - 22393217@utcancun.edu.mx - **Git**: secondglitter
- Martinez Lopez Irving - 22393236@utcancun.edu.mx - **Git**: XxIrvingositoxX
- Laines Cupul Evelin Yasmin - 22394139@utcancun.edu.mx - **Git**: Eveyas
- Sánchez Martínez Jesús Alexander Abad - 22393250@utcancun.edu.mx - **Git**: JAASM1

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
  "DefaultConnection": "Server=Tu_Servidor_SQL;Database=AppEventos;Trusted_Connection=True;Encrypt=False"
}
````
**Paso 3**:
Abre la **Consola de Administrador de Paquetes** y ejecuta:
````
Update-Database
````
**Paso 4**:
Una vez configurado el proyecto puede correr el proyecto con:
````
dotnet run
````

