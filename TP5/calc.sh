#!/bin/bash

#-- dialogo de menu principal
main() {
    echo "Bienvenido, seleccione la operación a realizar:"
    echo "1. Suma"
    echo "2. Resta"
    echo "3. Multiplicación"
    echo "4. División"
    read -p ">> " operacion
}

#-- valida si la entrada es un número
es_numero_valido() {
    [[ "$1" =~ ^-?[0-9]+(\.[0-9]+)?$ ]]
}

#-- lee los numeros y los valida
leer_numero() {
    local prompt="$1"
    local num
    while true; do
        read -p "$prompt" num
        if es_numero_valido "$num"; then
            echo "$num"
            return
        else
            echo "Por favor, introduce un número válido."
        fi
    done
}

#-- operar condicionales
options() {
    case $operacion in
        1) # Suma
            resultado=$(echo "$num1 + $num2" | bc)
            echo "Resultado: $num1 + $num2 = $resultado"
            ;;
        2) # Resta
            resultado=$(echo "$num1 - $num2" | bc)
            echo "Resultado: $num1 - $num2 = $resultado"
            ;;
        3) # Multiplicación
            resultado=$(echo "$num1 * $num2" | bc)
            echo "Resultado: $num1 * $num2 = $resultado"
            ;;
        4) # División
            if [ "$num2" == "0" ]; then
                echo "Error: División por cero"
            else
                resultado=$(echo "scale=2; $num1 / $num2" | bc)
                echo "Resultado: $num1 / $num2 = $resultado"
            fi
            ;;
        *)
            echo "Operación no válida"
            ;;
    esac
}

#-- leer y validar números
num1=$(leer_numero "Ingrese el primer número: ")
num2=$(leer_numero "Ingrese el segundo número: ")

#-- menu principal
main
options