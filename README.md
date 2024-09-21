# pTask - Aplicación web para la gestión de tareas

## 1. Nombre de la Aplicación
**pTask** - Aplicación web para la gestión de tareas.

## 2. Descripción General
**pTask** es una aplicación CRUD (Crear, Leer, Actualizar, Eliminar) diseñada para la gestión de tareas personales o profesionales. Permite al usuario organizar sus tareas diarias de manera eficiente, agregando, editando y eliminando tareas. La aplicación ofrece autenticación mediante JWT, lo que garantiza que solo los usuarios autorizados puedan acceder. Además, es completamente responsiva, lo que significa que puede ser utilizada tanto en dispositivos móviles como en pantallas grandes, adaptándose a cualquier tamaño de pantalla.

## 3. Características Principales
- **Autenticación segura:** Utiliza autenticación basada en tokens JWT para proteger el acceso.
- **CRUD de tareas:** Crear, editar, eliminar y listar tareas.
- **Interfaz responsiva:** La aplicación está diseñada para ser usada en dispositivos móviles y de escritorio.
- **Edición de tareas:** Las tareas pueden ser actualizadas, marcadas como completadas o pendientes.

## 4. Requisitos Previos
Para ejecutar la aplicación **pTask**, es necesario cumplir con los siguientes requisitos:
- **Backend:** El servidor debe estar configurado con .NET Core y una base de datos PostgreSQL.
- **Frontend:** El usuario debe tener instalado Node.js y npm para ejecutar el proyecto Vue.
- **Herramientas necesarias:**
  - Node.js v20.10.0 o superior.
  - PostgreSQL para la gestión de la base de datos.
  - Navegador web actualizado (Chrome, Firefox, Edge, Safari, etc.).
- **Acceso al repositorio GitHub** donde se encuentra el código fuente y las instrucciones de despliegue.

## 5. Interacción y Uso

La interfaz de la aplicación pTask es intuitiva y fácil de usar, permitiendo a los usuarios gestionar sus tareas rápidamente. A continuación se describe cómo interactuar con las principales funcionalidades:

### Crear una Tarea
1. Haz clic en el botón azul con el símbolo `+` en la parte inferior derecha para desplegar el formulario de creación de tareas.
   ![image](https://github.com/user-attachments/assets/8c271fec-a3a7-413f-913a-79eb9a88cb9b)

2. Completa los campos requeridos como el título y la descripción de la tarea.![image](https://github.com/user-attachments/assets/f619118a-f300-4dec-9204-050d476cb748)

3. Presiona el botón `Agregar Tarea` para guardar la nueva tarea en la lista.![image](https://github.com/user-attachments/assets/98bc1cbb-4f22-451f-9f13-140d65d2ee0b)

4. Si desea cancelar la creacion de la tarea Haz clic en el botón rojo con el símbolo `X` en la parte inferior derecha, el menu se cerrara sin conservar los cambios.

### Ver la Lista de Tareas
- Todas las tareas creadas se muestran en formato de tarjetas. Cada tarjeta contiene el título, la descripción, y las opciones para editar o eliminar la tarea.
  
  ![image](https://github.com/user-attachments/assets/013b7137-09e4-4048-bffc-f9e559a8b71f)


### Editar una Tarea
1. Haz clic en el boton azul `Editar` en la tarjeta de la tarea que deseas modificar.
2. El formulario de edición se desplegará con los campos previamente rellenados. alli tambien puedes marcar una tarea como *Completada*.  
   ![image](https://github.com/user-attachments/assets/d6c76527-caa6-4c16-8f0a-786f5150a9d9)


4. Realiza los cambios y haz clic en `Guardar Cambios`.

### Eliminar una Tarea
1. Haz clic en el boton rojo `Eliminar` en la tarjeta de la tarea que deseas eliminar.
   
   ![image](https://github.com/user-attachments/assets/6d54804d-0977-4de1-b0fb-5fd9b2dded56)

3. Se eliminara instantaneamente

### Autenticación
- La aplicación requiere iniciar sesión con un token JWT para poder acceder a la lista de tareas. Si no se ha iniciado sesión, no podrás ver ni gestionar las tareas.  
![image](https://github.com/user-attachments/assets/3bfa0ad0-34cd-42cb-bdc1-36fe943a307e)


## Responsividad
La aplicacion es responsiva, de ve bien y comodo en dispositivos mobiles:  
![image](https://github.com/user-attachments/assets/fdaaf0e1-2e04-4034-85e8-e4138ae87d02) ![image](https://github.com/user-attachments/assets/edadf437-ba6d-4c2c-87a6-4fac5f7045e8) 




