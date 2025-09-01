var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/putAway/getall' },
        "columns": [
            { data: 'code', className: "text-start", "width": "15%" },
            { data: 'customer.name', "width": "35%" },
            { data: 'status', "width": "15%" },

            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                     <a href="/admin/putAway/edit?id=${data}" class="btn btn-outline-secondary mx-2"> <i class="bi bi-list-task"></i> View</a>
                     <a onClick=Reject('/admin/putAway/reject/${data}') class="btn btn-outline-danger mx-2"> <i class="bi bi-x-circle"></i> Reject</a>
                     <a onClick=Approve('/admin/putAway/approve/${data}') class="btn btn-outline-success mx-2"> <i class="bi bi-check-circle"></i> Approve</a>
                    </div>`
                },
                "width": "35%"
            }
        ]
    });
}

function Reject(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, reject it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'POST',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function () {
                    toastr.error("An error occurred while rejecting the GRN.");
                }
            });
        }
    });
}
function Approve(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You are about to approve this GRN.",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#28a745',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, approve it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'POST',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function () {
                    toastr.error("An error occurred while approving the GRN.");
                }
            });
        }
    });
}
