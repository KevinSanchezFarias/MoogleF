#!/bin/bash

function ayuda
{

echo  " Estas son las opciones con las que cuenta  el script " 
echo -e "\n1.run  -- Ejecuta el proyecto "
echo "2.report -- Compila y genera el informe del proyecto " 
echo "3.slides -- Compila y genera la presentación del proyecto  " 
echo "4.show_report -- Permite visualizar el informe del proyecto "
echo "5.show-slides -- Permite visualizar la presentacion del proyecto "
echo "6.clear-- Elimina los archivos auxiliares que no forman parte del contenido original del repositorio "
echo "7.interaccion -- Ofrece un menu interactivo que permite , como indica su nombre , interactuar con todas las opciones anteriores " 



}

function run 
{

cd ../Moogle
dotnet watch run --project MoogleServer

}

function report {

cd ../Informe
pdflatex -jobname=InformeMoogle InformeMoogle.tex
pdflatex -jobname=InformeMoogle InformeMoogle.tex

}

function slides
{

cd ../Presentacion

pdflatex document.tex
pdflatex document.tex

}

function show_report 
{
    parametro=$1
    cd ../Informe
    nombre="InformeMoogle.pdf"

    if [ -f  "$nombre" ];
    then

    if [ -z "$parametro" ]
    then 
    start InformeMoogle.pdf
    

    else 
    "$parametro" InformeMoogle.pdf
   

    fi

    else 

if [ -z "$parametro" ];
then
echo "El pdf no se ha generado , espero unos segundos a que lo haga" 

report

 start InformeMoogle.pdf


else 

echo "El pdf no se ha generado , espero unos segundos a que lo haga" 
report
"$parametro" InformeMoogle.pdf


fi

fi

}

function show_slides
{

 parametro=$1
    cd ../Presentacion
    nombre="document.pdf"

    if [ -f  "$nombre" ];
    then

    if [ -z "$parametro" ]
    then 
    start document.pdf
    

    else 
    "$parametro" document.pdf
     

    fi

    else 

if [ -z "$parametro" ];
then
echo "El pdf no se ha generado , espero unos segundos a que lo haga" 

slides

 start document.pdf


else 

echo "El pdf no se ha generado , espero unos segundos a que lo haga" 
slides
"$parametro" document.pdf

fi

fi


}

function clear
{

cd ../Informe 

find . -type f ! -name "InformeMoogle.tex" ! -name "matcom.jpg" -delete


cd ../Presentacion

find . -type f ! -name "document.tex" ! -name "matcom.jpg" -delete 

}

function interaccion 
{

echo -e "\nBienvenido al script de bash "

while true 

do

echo -e "\nSeleccione una opción"
echo -e "\n1.run "
echo "2.report"
echo "3.slides"
echo "4.show_report"
echo "5.show_slides"
echo "6.clear"
echo "7.Salir"

read -r option 


case $option in 

1)

run 
;;

2)

report
;;

3)

slides
;;

4)

parametro="start" 
read -p "Desea ingresar una herramienta de visualización o prefiere utilizar una por defecto  (s/n) : " respuesta 

if [ $respuesta = "s" ] ;
then 

read -p " Ingrese el valor del parametro " valor_parametro 

else

valor_parametro=""

fi 

if [ -n "$valor_parametro" ];
then 

parametro="$valor_parametro"

fi 

cd ../Informe 

if [ -f InformeMoogle.pdf ];

then
"$parametro" InformeMoogle.pdf
else
report
"$parametro" InformeMoogle.pdf

fi

;;

5) 

parametro="start" 
read -r -p "Desea ingresar una herramienta de visualización o prefiere utilizar una por defecto  (s/n) : " respuesta 

if [ "$respuesta" = "s" ] ;
then 

read -p " Ingrese el valor del parametro " valor_parametro 

else

valor_parametro=""

fi 

if [ -n "$valor_parametro" ];
then 

parametro="$valor_parametro"

fi 

cd ../Presentacion


if [ -f document.pdf ];

then
"$parametro" document.pdf
else
slides
"$parametro" document.pdf

fi

;;

6)

clear

;;

7)

exit

;;


esac 


done

}



if [[ $# -eq 0 ]];
then 
echo " Bienvenido  al script " 
echo " Si escribe ./test.sh ayuda se desplegara una lista con las opciones disponibles " 
echo -e "\n1.Salir"  
echo  "2.Continuar"

read -r a 

if [  "$a" = "1" ];
then 
exit
else
interaccion
fi

else 

case "$1" in 

"run")

run 
;;

"report")

report

;;

"slides")

slides

;;

"show_report")

show_report "$2"

;;

"show_slides")

show_slides "$2"
;;

"clear")

clear
;;

"interaccion")

interaccion

;;

"ayuda")

ayuda
;;

esac 

fi








