function Sucesso(data) {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: data.msg,
        showConfirmButton: false,
        timer: 1500
    })
}

function Falha(data) {
    Swal.fire({
        position: 'top-end',
        icon: 'error',
        title: 'Falhou',
        showConfirmButton: false,
        timer: 1500
    })
}