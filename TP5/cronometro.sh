#!/bin/sh

#-- variables globales
inicio=0
fin=0
t_tiempo=0
play=0

#-- mostrar menu
mostrar_menu() {
    echo "Cronometro. por favor, seleccione una opcion:"
    echo "1. Iniciar"
    echo "2. Detener"
    echo "3. Reiniciar"
    echo "4. Salir"
}

#-- inicia cronometro
iniciar() {
    if [ $play -eq 1 ]; then
        echo "El cronómetro esta activo."
    else
        inicio=$(date +%s)
        play=1
        echo "Cronómetro iniciado."
    fi
}

#-- detener cronometro
detener() {
    if [ $play -eq 0 ]; then
        echo "El cronómetro no se encuentra en marcha."
    else
        fin=$(date +%s)
        t_tiempo=$((fin - inicio))
        play=0
        echo "OK. Tiempo transcurrido: ${t_tiempo} segundos."
    fi
}

#-- reiniciar cronometro
reiniciar() {
    inicio=0
    fin=0
    t_tiempo=0
    play=0
    echo "Cronómetro reiniciado."
}

#-- mostrar menu
while true; do
    mostrar_menu
    read -p "Seleccione una opción: " opcion

    #-- validación
    if ! [ "$opcion" -ge 1 -a "$opcion" -le 4 ] 2>/dev/null; then
        echo "Por favor, seleccione una opcion valida."
        continue
    fi

    #--- procesa información
    case $opcion in
        1) iniciar ;;
        2) detener ;;
        3) reiniciar ;;
        4) echo "Saliendo..." ; exit 0 ;;
        *) echo "Opción no válida." ;;
    esac
done
