#!/bin/bash

#-- menu principal
echo "Clima: Bienvenido."
read -p "Ingrese el nombre de la ciudad: " ciudad

#-- api key y enlace
API_KEY="859f23ca0e0a4550ba232446242306"
URL="http://api.weatherapi.com/v1/current.json?key=${API_KEY}&q=${ciudad}"

#-- solicitud api
response=$(curl -s "$URL")

#-- verifica si la solicitud fue exitosa
if [ $? -ne 0 ]; then
  echo "Error: No se pudo obtener la información del clima para la ciudad especificada."
  exit 1
fi

#-- extrae los datos json y los muestra en pantalla
location=$(echo "$response" | jq -r '.location | "\(.name), \(.region), \(.country)"')
temp_c=$(echo "$response" | jq -r '.current.temp_c')
condition=$(echo "$response" | jq -r '.current.condition.text')

#--verifica si los datos necesarios fueron obtenidos correctamente
if [ -z "$location" ] || [ -z "$temp_c" ] || [ -z "$condition" ]; then
  echo "Error: No se pudieron extraer algunos datos del clima."
  exit 1
fi

#-- muestra los resultados obtenidos
echo "Ciudad: $location"
echo "Temperatura actual: $temp_c°C"
echo "Condición: $condition"
