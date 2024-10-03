# paynau-tech-assessment
proceso de selección para la posición de Desarrollador, te invitamos a realizar un examen técnico que nos permitirá conocer más a fondo tus habilidades y experiencia. A continuación, te compartimos los detalles para acceder y completar la evaluación




Guía para Restablecer la Contraseña de MySQL usando Docker

Esta guía te ayudará a restablecer la contraseña de root en MySQL si no puedes acceder a tu base de datos debido a problemas de configuración.
Pasos para Restablecer la Contraseña

1. Modificar el archivo docker-compose.yml

Agrega la línea command: --skip-grant-tables en la sección del servicio de tu contenedor de MySQL. A continuación se muestra un ejemplo de cómo debe verse tu archivo:

yaml

version: '3.8'

services:
  personCatalogdb:
    container_name: mysql_container
    restart: always
    environment:
      - MYSQL_ROOT_PASSWORD=admin123
      - MYSQL_DATABASE=PersonCatalogDb
      - MYSQL_USER=admin
      - MYSQL_PASSWORD=admin123
    ports:
      - "3306:3306"
    volumes:
      - mysql_data:/var/lib/mysql
    command: --skip-grant-tables  # Agrega esta línea

2. Iniciar el Contenedor

Ejecuta el siguiente comando para iniciar el contenedor:

bash

docker-compose up -d

3. Acceder a MySQL

Intenta acceder a MySQL sin contraseña utilizando el siguiente comando:

bash

docker exec -it mysql_container mysql -u root

4. Restablecer la Contraseña del Root

Dentro de la consola de MySQL, ejecuta los siguientes comandos para restablecer la contraseña de root:

sql

FLUSH PRIVILEGES;
ALTER USER 'root'@'localhost' IDENTIFIED BY 'admin123';  -- Restablece la contraseña a lo que desees

Si deseas permitir el acceso desde otros hosts, ejecuta:

sql

ALTER USER 'root'@'%' IDENTIFIED BY 'admin123';  -- Asegúrate de que el usuario root pueda acceder desde cualquier host

5. Otorgar Privilegios de Administrador al Usuario admin

Para otorgar privilegios de administrador al usuario admin, ejecuta el siguiente comando:

sql

GRANT ALL PRIVILEGES ON *.* TO 'admin'@'%' WITH GRANT OPTION;  -- Otorga privilegios de administrador a admin

6. Salir de MySQL

Para salir de la consola de MySQL, ejecuta:

sql

EXIT;

7. Detener el Contenedor

Detén el contenedor utilizando el siguiente comando:

bash

docker-compose down

8. Modificar el Archivo docker-compose.yml de Nuevo

Elimina la línea command: --skip-grant-tables de tu archivo docker-compose.yml para que MySQL se ejecute con la configuración de seguridad normal.
9. Volver a Iniciar el Contenedor

Inicia el contenedor nuevamente con:

bash

docker-compose up -d

10. Verificar Acceso

Finalmente, verifica si puedes acceder a MySQL con la nueva contraseña:

bash

docker exec -it mysql_container mysql -u root -p

Introduce admin123 como contraseña y verifica si ahora puedes acceder correctamente. Además, verifica que el usuario admin tenga los privilegios necesarios.



Guía para Crear la Estructura de la Tabla en MySQL usando Entity Framework Core

Una vez que hayas restablecido la contraseña de MySQL y verificado el acceso, puedes proceder a crear la estructura de la tabla para tu base de datos PersonCatalog. A continuación se indican los pasos para agregar migraciones y aplicar cambios en tu proyecto utilizando la Package Manager Console.

1. Agregar una Migración

Abre la Package Manager Console en Visual Studio. Para agregar una nueva migración que cree la tabla, utiliza el siguiente comando:

powershell

Add-Migration InitialCreate -OutputDir Data/Migrations

    InitialCreate es el nombre de la migración; puedes cambiarlo según tu preferencia.
    -OutputDir Data/Migrations especifica el directorio donde se generarán los archivos de migración.

2. Aplicar la Migración

Una vez que hayas creado la migración, debes aplicarla para que se cree la tabla en la base de datos. Ejecuta el siguiente comando en la Package Manager Console:

powershell

Update-Database

Este comando aplicará todas las migraciones pendientes a la base de datos configurada en tu DbContext.

3. Verificar la Estructura de la Tabla

Después de aplicar la migración, puedes verificar la estructura de la tabla en tu base de datos MySQL. Puedes hacerlo utilizando cualquier cliente de MySQL, como MySQL Workbench o phpMyAdmin. Asegúrate de que la tabla Persons se haya creado con las columnas correctas.