<?php
header('Content-Type: application/json; charset=utf-8');

echo "Iniciando <br>";

$json = file_get_contents('php://input');

echo "Json: $json <br>";

$obj = json_decode($json);

//echo "Obj: $obj <br>";

print_r($obj);