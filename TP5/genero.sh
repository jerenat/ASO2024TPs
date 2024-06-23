#!/bin/bash

#-- solicita al usuario que ingrese datos
read -p "Por favor, ingrese un nombre " nombre

#-- valida que se haya ingresado un nombre
if [ -z "$nombre" ]; then
    echo "Error, no se ha ingresado un nombre."
    exit 1
fi

URL="https://api.genderize.io?name=${nombre}"

response=$(curl -s "$URL")

#-- verifica si la informacion fue exitosa
if [ $? -ne 0 ]; then
    echo "Error, no se pudo obtener informacion del genero"
    exit 1
fi

#-- extrae los datos y los almacena en una variable
name=$(echo "$response" | jq -r '.name')
gender=$(echo "$response" | jq -r '.gender')
probability=$(echo "$response" | jq -r '.probability')

case "$gender" in
    male)
        gender="Masculino (M)"
        ;;
    female)
        gender="Femenino (F)"
        ;;
    *)
        gender="Desconocido (X)"
        ;;
esac

pc_probabilidad=$(echo "scale=2; $probability * 100" | bc)

# Muestra los resultados obtenidos
echo "Nombre: $name"
echo "Género: $gender"
echo "Probabilidad: ${pc_probabilidad}%"
