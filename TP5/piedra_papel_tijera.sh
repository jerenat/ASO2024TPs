#!/bin/bash

# -- variables
PIEDRA=1
PAPEL=2
TIJERAS=3

# -- menu principal
main() {
    echo "Elige una opcion:"
    echo "1. Piedra"
    echo "2. Papel"
    echo "3. Tijeras"
}

#-- eleccion del usuario
eleccion_usuario() {
    echo "Introduce un numero a eleccion"
    echo "1. Piedra"
    echo "2. Papel"
    echo "3. Tijeras"
    read -p ">>" eleccion usuario

    while [[ ! "$eleccion_usuario" =~ ^[1-3]$ ]]; do
        echo "Opcion invalida. Por favor, elige 1, 2 o 3."
        read -p "Introduce un numero a eleccion: " eleccion_usuario
    done
}

#-- ordenador elije aleatoriamente
eleccion_computadora() {
    eleccion_computadora=$(( RANDOM % 3 + 1 ))
}

#-- convierte eleccion a texto
convertir_a_texto() {
    case $1 in
        $PIEDRA) echo "Piedra" ;;
        $PAPEL) echo "Papel" ;;
        $TIJERAS) echo "Tijeras" ;;
    esac
}

#-- determina el ganador
determinar_ganador() {
    if [[ $eleccion_usuario -eq $eleccion_computadora ]]; then
        echo "Es un empate."
    elif [[ $eleccion_usuario -eq $PIEDRA && $eleccion_computadora -eq $TIJERAS ]] || \
         [[ $eleccion_usuario -eq $PAPEL && $eleccion_computadora -eq $PIEDRA ]] || \
         [[ $eleccion_usuario -eq $TIJERAS && $eleccion_computadora -eq $PAPEL ]]; then
        echo "Ganaste tu"
    else
        echo "Ordenador gana."
    fi
}

#-- menu principal
echo "Bienvenido! <<Piedra, papel o tijeras>>"

main
eleccion_usuario
eleccion_computadora

echo "Tu elegiste: $(convertir_a_texto $eleccion_usuario)"
echo "Ordenador eligio: $(convertir_a_texto $eleccion_computadora)"

determinar_ganador