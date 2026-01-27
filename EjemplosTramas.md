📌 Ejemplos de Consumo de la API

🟢 Registrar una Incidencia

Método: POST

Endpoint: /api/Incidencias

Request

{

  "titulo": "Primera Incidencia",
  
  "descripcion": "Prueba de incidencia",
  
  "idCategoria": 1,
  
  "severidad": "baja",
  
  "bitacoraInicial": null
  
}

🔄 Actualizar Estado de una Incidencia

Método: PUT

Endpoint: /api/Incidencias/1/estado

Request

{

  "estado": "En Proceso",
  
  "comentario": "Incidencia en validación se validara",
  
  "usuario": "alfredo",
  
  "accionRealizada": "Se inicia evaluación del caso"
  
  
}



