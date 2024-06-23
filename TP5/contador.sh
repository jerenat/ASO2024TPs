#!/bin/bash

# -- verifica si se proporciona el archivo como argumento
if [ $# -ne 1 ]; then
    echo "Uso: $0 archivo.txt"
    exit 1
fi

archivo="$1"

#-- verifica si el archivo existe
if [ ! -f "$archivo" ]; then
    echo "Error: El archivo $archivo no existe."
    exit 1
fi

# -- cuenta cada elemento del archivo
num_lineas=$(wc -l < "$archivo")
num_palabras=$(wc -w < "$archivo")
num_caracteres=$(wc -m < "$archivo")

#-- muestra los resultados
echo "Número de líneas: $num_lineas"
echo "Número de palabras: $num_palabras"
echo "Número de caracteres: $num_caracteres"