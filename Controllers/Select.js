function cambio() {
    var valorSeleccionado = ORIGEN.value;
    console.log('Respuesta del servidor: ' + valorSeleccionado);
    // Crear la solicitud AJAX
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Transferencia/Saldo', true);
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

    // Enviar los datos del select al controlador
    xhr.send('ORIGEN=' + encodeURIComponent(valorSeleccionado));

    // Opcionalmente, manejar la respuesta del servidor
    xhr.onload = function () {
        if (xhr.status === 200) {
            // Hacer algo con la respuesta del servidor
            console.log('Respuesta del servidor: ' + xhr.responseText);
            document.getElementById("SALDO").value = xhr.responseText;
        } else {
            console.error('Error al enviar el valor: ' + xhr.status);
        }
    };
}
var ORIGEN = document.getElementById('ORIGEN');
    
    ORIGEN.addEventListener('change', function () {
        cambio();
    });
cambio();
