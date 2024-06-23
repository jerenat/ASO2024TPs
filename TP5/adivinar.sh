#!/bin/bash

#-- variables
MIN=1         # -- mínimo
MAX=100       # -- máximo
intentos=0    # -- inicializa el contador de intentos

#-- genera un número aleatorio entre 1 y 100
rand_number=$(( RANDOM % MAX + MIN ))

#-- Inicio de dialogo
echo " === Adivinar numero === "
echo "Adivina el numero entre $MIN y $MAX:"


while true; do
  read -p "Introduce tu intento: " intento

  if ! [[ "$intento" =~ ^[0-9]+$ ]]; then
    echo "Por favor, introduce un número válido."
    continue
  fi

  intento=$((intento))
  intentos=$((intentos + 1))  # -- añade +1 al contador de intentos

  #-- si el número es demasiado bajo
  if (( intento < rand_number )); then
    echo "Demasiado bajo. Por favor, intenta de nuevo."

  #-- si el número es demasiado alto
  elif (( intento > rand_number )); then
    echo "Demasiado alto. Por favor, intenta de nuevo."
  else

  #-- si el numero es correcto
    echo "¡Felicidades! Has adivinado el número."
    echo "El número de intentos ha sido $intentos intentos"
    break
  fi
done
