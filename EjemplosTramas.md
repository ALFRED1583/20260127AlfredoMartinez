## 📌 Ejemplos de Consumo de la API

---

### 🟢 Registrar una Incidencia

**Método:** `POST`  
**Endpoint:** `/api/Incidencias`

#### Request

```json
{
  "titulo": "Primera Incidencia",
  "descripcion": "Prueba de incidencia",
  "idCategoria": 1,
  "severidad": "baja",
  "bitacoraInicial": null
}

```
### 🔄 Actualizar Estado de una Incidencia
Permite modificar el estado actual de una incidencia específica mediante su ID.

**Método:** `PUT`

**Endpoint:** `/api/Incidencias/{id}/estado`

#### Cuerpo de la petición (Request):

```json
{
  "estado": "En Proceso",
  "comentario": "Incidencia en validación se validara",
  "usuario": "alfredo",
  "accionRealizada": "Se inicia evaluación del caso"
}
```


