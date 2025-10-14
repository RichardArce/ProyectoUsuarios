// Aislar funciones 
(() => {
    const Usuarios = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },
        inicializarTabla() {
            this.tabla = $('#tablaUsuarios').DataTable({
                ajax: {
                    url: '/Usuario/ObtenerUsuarios',
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'id', title: 'ID' },
                    { data: 'nombre', title: 'Nombre' },
                    { data: 'apellido', title: 'Apellido' },
                    { data: 'edad', title: 'Edad' },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-sm btn-primary editar" data-id="${row.id}">Editar</button>
                                <button class="btn btn-sm btn-danger eliminar" data-id="${row.id}">Eliminar</button>
                            `;
                        }
                    }
                ],
                responsive: true,
                processing: true,
                pageLength:10
            });
        },
        registrarEventos() {

            $('#tablaUsuarios').on('click', '.editar', function () {
                const id = $(this).data('id');
                Usuarios.CargaDatosUsuario(id);
            });

            $('#tablaUsuarios').on('click', '.eliminar', function () {
                const id = $(this).data('id');
                Usuarios.EliminarUsuario(id);
            });


            $('#btnGuardarCambios').on('click', function () {
                Usuarios.GuardarUsuario();
            });

            $('#btnEditarCambios').on('click', function () {
                Usuarios.EditarUsuario();
            });
        },
        GuardarUsuario: function () {
            let form = $('#formCrearUsuario'); 

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'), //ACTION DEL CONTROLADOR QUE DEBE EJECUTARSE, ESTA EN EL FORMULARIO
                type: 'POST', //TIPO DE REQUEST
                data: form.serialize(), //SERIALIZAR EL FORMULARIO
                success: function (response) { //MANEJO DE RESPUESTA
                    if (!response.esError) {
                        $('#modalCrearUsuario').modal('hide'); // Ocultar el modal
                        Usuarios.tabla.ajax.reload(); //Recargar la tabla
                        form[0].reset(); //Borrar el formulario

                        Swal.fire({
                            title: 'Éxito',
                            text: response.mensaje,
                            icon: 'success',
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Error',
                            text: response.mensaje,
                            icon: 'error',
                        });
                    }

                }


            });

        },

        CargaDatosUsuario: function (id) {
            $.get(`/Usuario/ObtenerUsuarioPorId/${id}`, function (response) {
                if (!response.esError) {
                    const usuario = response.data;
                    $('#UsuarioId').val(usuario.id);
                    $('#Nombre').val(usuario.nombre);
                    $('#Apellido').val(usuario.apellido);
                    $('#Edad').val(usuario.edad);
                    $('#modalEditarUsuario').modal('show');
                } else {
                    Swal.fire({
                        title: 'Error',
                        text: response.mensaje,
                        icon: 'error',
                    });
                }
            });

        },

        EditarUsuario: function () {
            let form = $('#formEditarUsuario');


            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (!response.esError) {
                        $('#modalEditarUsuario').modal('hide');
                        Usuarios.tabla.ajax.reload();
                        Swal.fire({
                            title: 'Éxito',
                            text: response.mensaje,
                            icon: 'success',
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Error',
                            text: response.mensaje,
                            icon: 'error',
                        });
                    }
                }
            });
        },
        EliminarUsuario: function (id) {
            Swal.fire({
                title: "Estas seguro?",
                text: "No podras revertir esta acción",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText:"Si, borrar"
            }).then((result) => {

                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Usuario/EliminarUsuario',
                        type: 'POST',
                        data: {id:id},
                        success: function (response) {
                            if (!response.esError) {
                                Usuarios.tabla.ajax.reload();
                                Swal.fire({
                                    title: 'Éxito',
                                    text: response.mensaje,
                                    icon: 'success',
                                });
                            }
                            else {
                                Swal.fire({
                                    title: 'Error',
                                    text: response.mensaje,
                                    icon: 'error',
                                });
                            }
                        }
                    });
                }
            });
        }

    }
    $(document).ready(() => Usuarios.init());
})();


