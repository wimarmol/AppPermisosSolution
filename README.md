# AppPermisosSolution

1 - Descargar o clonar el repositorio

2 - Ejecutar sobre una sintacia de SqlServer, el archivo "Create_all.sql", este crea:

    - La Base de datos
    
    - Las tablas y las relaciones
    
    - Un usuario expecífico, para realizar la conexión con los pemriso sobre la base de datos
    
    - Las vistas (una sola)
    
    - Los procedimientos almacenados
    
    
3 - Editar el archivo "appsettings.json", que se encuentra dentro de la carpeta "WebApp", específicamente la cadena de conexión "ConnectionStringPersonal", de acuerdo a  la conexión y configuración de sus instancia de SqlServer

4 - El diagrama de clases 

![Screenshot](DER.jpg)

La tabla "Permiso", esta bien normalizada, pormlo siguiente.

1FN
Todos los atributos son atómicos (sin embargo, según la necesidad de gestionar por separado apellido materno y paterno esta columna debería separarse)

No existen variación de columnas entre las filas, es decir todas las  filas tiene el mismo número de columnas

Todos los campos que no son clave se identifican y acceden por el campo clave Id (llave primaria)

El orden de registro so columnas no afecta el significado del elemento o fila

2FN

Está en 1FN

No existen dependencias parciales y todos los atributos dependen únicamente de la clave prinicipal.


3FN

Está en 2FN

No existen dependencias parciales y todos los atributos dependen únicamente de la clave prinicipal

- Es decir en esta tabla se muestran dos entidades adicionales Empleado y Tipo Permiso, estan han sido separadas y dependen del campo llave de cada una de ellas que es referenciada en la tabla Permiso.


