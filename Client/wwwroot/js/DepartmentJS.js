var table = null;

//fungsi yang akan otomatis terload
$(document).ready(function () {
    table = $('#TB_Department').DataTable({
        "order": [], // menghilangkan sort asc/desc pada tabel head yng disable sort
        // draw atau re-draw datatables
        "drawCallback": function (settings) {
            // mengatur nomor urut berdasarkan halaman dan halaman terakhir
            var api = this.api();
            var startIndex = api.context[0]._iDisplayStart;
            var counter = startIndex + 1;
            api.column(0, { page: 'current' }).nodes().each(function (cell, i) {
                cell.innerHTML = counter;
                counter++;
            });
        },

        responsive: true,

        "ajax": {
            url: "http://localhost:8086/api/Departments",
            type: "GET",
            "datatype": "json",
            "dataSrc": "data",
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('token')
            },
            //success: function (result) {
            //    console.log(result)
            //}
        },
        "columns": [
            {
                // Menampilkan kolom "No." dengan fungsi increment berdasarkan data "name" dari API
                "data": null,
                "orderable": false, // kolom No. tidak bisa di-sort
                "render": function (data, type, row, meta) {
                    return meta.row + 1 + ".";
                }
            },
            {
                "data": "name",
                "orderable": true,
                "orderData": [1]
            },
            {
                // Menambahkan kolom "Action" berisi tombol "Edit" dan "Delete" dengan Bootstrap
                "data": null,
                "orderable": false, // kolom Action tidak bisa di-sort
                "render": function (data, type, row) {
                    var modalId = "modal-edit-" + data.id;
                    var deleteId = "modal-delete-" + data.id;
                    return '<button class="btn btn-warning " data-placement="left" data-toggle="modal" data-animation="false" title="Edit" data-target="#myModal"  onclick="return GetById(' + row.id + ')"><i class="fa fa-pen"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="modal" data-animation="false" title="Delete" onclick="return Delete(' + row.id + ')"><i class="fa fa-trash"></i></button >'
                }
            }
        ]
    });

});

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#UpdateBtn').hide();
    $('#SaveBtn').show();
}

function GetById(id) {
    //debugger;
    $.ajax({
        url: 'http://localhost:8086/api/Departments/' + id,
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //token jwt
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        },
        success: function (result) {
            //debugger;
            //console.log(result)
            var obj = result.data; //data yang didapat dari API
            $('#Id').val(obj.id);
            $('#Name').val(obj.name);
            $('#myModal').modal('show');
            $('#SaveBtn').hide();
            $('#UpdateBtn').show();
        },
        error: function (errorMessage) {
            alert(errorMessage.responseText);
        }
    });
}


function Save() {
    var Department = new Object();
    Department.Name = $('#Name').val();
    $.ajax({
        type: 'POST',
        url: 'http://localhost:8086/api/Departments',
        data: JSON.stringify(Department),
        contentType: "application/json; charset=utf-8",
        //token jwt
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('token')
        },
        success: function (result) {
            //debugger;
            if (result.status == 201 || result.status == 204 || result.status == 200) {
                Swal.fire({
                    icon: 'success',
                    title: 'Your work has been saved',
                    showConfirmButton: false,
                    timer: 1500
                })
                //reload
                /*var table = $('#TB_Department').DataTable();*/
                table.ajax.reload(null, false);
            }
        },
        error: function (errorMessage) {
            Swal.fire(errorMessage.responseText, '', 'error');
        }
    });
}

function Delete(id) {
    //debugger;
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "http://localhost:8086/api/Departments/" + id,
                type: "DELETE",
                dataType: "json",
                //token jwt
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
                },
            }).then((result) => {
                //debugger;
                if (result.status == 200) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    // Reload tabel secara otomatis setelah menghapus data
                    var table = $('#TB_Department').DataTable();
                    table.ajax.reload(null, false);
                } else {
                    Swal.fire(result.responseText, '', 'error');
                }
            });
        }
    })
}

//update > ketika klik edit > akan panggil get byid
// ketika klik update > function Update
function Update() {
    //debugger;
    var Department = new Object();
    Department.Id = $('#Id').val();
    Department.Name = $('#Name').val();

    Swal.fire({
        title: 'Do you want to save the changes?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Save',
        denyButtonText: `Don't save`,
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            $.ajax({
                url: "http://localhost:8086/api/Departments",
                type: "PUT",
                data: JSON.stringify(Department), //kirim data ke API, maka itu harus di convert ke JSON
                contentType: "application/json; charset=utf-8",
                //token jwt
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('token')
                },
                success: function (result) {
                    //debugger;
                    if (result.status == 200) {
                        Swal.fire('Saved!', '', 'success')

                        // Reload tabel secara otomatis setelah update data
                        var table = $('#TB_Department').DataTable();
                        table.ajax.reload(null, false);
                    }
                },
                error: function (errorMessage) {
                    Swal.fire(errorMessage.responseText, '', 'error');
                }
            });
        } else if (result.isDenied) {
            Swal.fire('Changes are not saved', '', 'info')
        }
    });
    closeModal();
}

function closeModal() {
    $('#myModal').modal('hide')
}
