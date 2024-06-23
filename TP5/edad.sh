#!/bin/bash

#-- obtiene el nombre ingresado por el usuario
read -p "Por favor, ingrese un nombre: " nombre

#-- validacion de datos
if [ -z "$nombre" ]; then
    echo "Error, no se ha ingresado ningun nombre"
    exit 1
fi

URL="https://api.agify.io/?name=${nombre}"


response=$(curl -s "$URL")

#-- verifica si la operacion fue exitosa
if [ $? -ne 0 ]; then
    echo "Error, no se pudo obtener informacion."
    exit 1
fi

#-- extrae los datos y los almacena en una variable
name=$(echo "$response" | jq -r '.name')
age=$(echo "$response" | jq -r '.age')
count=$(echo "$response" | jq -r '.count')

#-- verifica si se obtuvieron correctamente los datos necesarios
if [ -z "$name" ] || [ -z "$age" ] || [ -z "$count" ]; then
    echo "Error: No se pudieron extraer algunos datos."
    exit 1
fi

#-- muestra los resultados obtenidos
echo "Nombre: $name"
echo "Edad probable: $age"
echo "Probabilidad: $count"
