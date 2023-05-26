$(document).ready(function () {
    $('#login-form').submit(function (e) {
        e.preventDefault();
        var Account = new Object();
        Account.Email = $('#Email').val();
        Account.Password = $('#Password').val();
        $.ajax({
            type: 'POST',
            url: 'http://localhost:8086/Login',
            data: JSON.stringify(Account), //convert json
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                //debugger;
                Swal.fire({
                    icon: 'success',
                    title: result.message,
                    showConfirmButton: false,
                    timer: 1500
                }).then((successAllert) => {
                    if (successAllert) {
                        //debugger;
                        //set token to session
                        var getToken = result.token;
                        sessionStorage.setItem('token', getToken); // jwt token to session
                        location.replace("/Departments");
                    } else {
                        location.replace("/Departments");
                    }

                });
                //$.post("/Home/Login", { email: Account.Email })
                //    .done(function () {
                //        Swal.fire({
                //            icon: 'success',
                //            title: result.message,
                //            showConfirmButton: false,   
                //            timer: 1500
                //        }).then((successAllert) => {
                //            if (successAllert) {
                //                debugger;
                //                //set token to session
                //                var getToken = result.token;
                //                sessionStorage.setItem('token', getToken); // jwt token to session

                //                location.replace("/Departments");
                //            } else {
                //                location.replace("/Departments");
                //            }

                //        });
                //    })
                //    .fail(function () {
                //        alert("Fail!, Gagal Login");
                //    })
                //    .always(function () {
                //        //alert
                //    });
                //if (result.status == 201 || result.status == 204 || result.status == 200) {

                //    // simpan email di session
                //    sessionStorage.setItem('email', Account.Email);
                //    //window.location.href =
                //    window.location = '/Departments';
                //}
            },
            error: function (errorMessage) {
                Swal.fire('Gagal Login', errorMessage.message, 'error');
            }
        });
    })
});

$("#logoutClick").on('click', function () {
    sessionStorage.removeItem("token");
    location.replace("/Home/Login");
});
