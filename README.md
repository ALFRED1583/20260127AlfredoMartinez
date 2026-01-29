# 🧪 Prueba Técnica de Programación
# Prueba este codigo aqui [LiveDemo](http://pruebabsci.appsrv.store:29312/swagger/index.html). 

## 📌 HU-001: Registro de Incidencias Internas

**Descripción:**  
Como administrador del sistema de gestión interna, quiero registrar nuevas incidencias con información detallada, para que puedan ser analizadas, clasificadas y solucionadas de forma adecuada.

**Criterios de Aceptación:**
- El sistema debe permitir registrar una incidencia con los siguientes campos:
  - Código interno (autogenerado)
  - Título
  - Descripción
  - Categoría (Mantenimiento, TI, Red, Seguridad, etc.)
  - Nivel de severidad (Baja, Media, Alta, Crítica)
  - Fecha de registro
  - Estado inicial: Pendiente
  - Bitácora inicial (opcional)
- Validaciones requeridas:
  - El título no puede estar vacío
  - La severidad debe ser un valor válido
  - La categoría debe existir en catálogo
  - La descripción debe tener mínimo 10 caracteres
- El sistema debe retornar respuestas controladas por cada acción

---

## 🔄 HU-002: Actualización y Seguimiento del Estado de Incidencias

**Descripción:**  
Como administrador del sistema, quiero actualizar el estado y agregar comentarios a una incidencia registrada, para mantener un historial claro del avance y facilitar su resolución.

**Criterios de Aceptación:**
- El sistema debe permitir actualizar los estados:
  - Pendiente
  - En Proceso
  - En Espera
  - Resuelto
  - Cerrado
- Registro de acciones en bitácora:
  - Fecha y hora
  - Acción realizada
  - Cambio de estado
  - Comentario u observación (opcional)
  - Usuario interno que ejecutó el cambio

---

## ⚙️ Consideraciones Técnicas

1. Crear una API con arquitectura en .NET Core.
2. Utilizar consultas eficientes hacia la base de datos y al menos un procedimiento almacenado en alguna HU.
3. Aplicar buenas prácticas de desarrollo: principios SOLID, patrones de diseño, seguridad, código limpio.
4. Crear el proyecto y ejecutar pruebas unitarias correspondientes.
5. No se requiere una interfaz de usuario estructurada; se evaluará el cumplimiento funcional.
6. Crear una rama en GitHub con el formato: `YYYYMMDDNombreApellido` (ejemplo: `20230612JuanPerez`).  
   Enviar el enlace público del repositorio como respuesta al correo.

---

**📂 Repositorio esperado:**  
El repositorio debe contener:
- Carpeta del proyecto API
- Documentación técnica (README.md)
- Scripts de base de datos (procedimientos almacenados, catálogos)
- Pruebas unitarias
