let table = new DataTable('#DT_load', {
    "ajax": {
        "url": "/api/MenuItem",
        "type": "GET",
        "datatype": "json"
    },
    "columns": [
        { "data": "name", "width": "25%" },
        { "data": "price", "width": "25%" },
        { "data": "category.name", "width": "15%" },
        { "data": "foodType.name", "width": "15%" },
        {
            "data": "id",
            "render": function (data) {
                return `<div class="w-75 btn-group">
                            <a href="/Admin/MenuItems/upsert?id=${data}" 
                            class="btn btn-success text-white mx-2">
                                <i class="bi bi-pencil-square"></i> Edit 
                            </a>
                            <a onClick=Delete('/api/MenuItem/'+${data})
                            class="btn btn-danger text-white mx-2">
                                <i class="bi bi-trash-fill"></i> Delete 
                            </a>
                        </div>`
            }
        }
    ],
    "width": "100%"
});

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
                    console.log(url);
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        table.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    });
}