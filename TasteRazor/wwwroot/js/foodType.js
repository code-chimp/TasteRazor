let dataTable;

function loadList() {
  dataTable = $('#DT_load').DataTable({
    ajax: {
      type: 'GET',
      url: '/api/foodtype',
      datatype: 'json'
    },
    columns: [
      { data: 'name', width: '70%' },
      {
        data: 'id',
        render: data => {
          return `<div class="text-center">
                    <a href="/Admin/FoodType/Upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer;width:100px">
                      <i class="far fa-edit"></i> Edit
                    </a>
                    <a class="btn btn-danger text-white" style="cursor:pointer;width:100px" onclick="Delete('/api/foodtype/${data}')">
                      <i class="far fa-trash-alt"></i> Delete
                    </a>
                  </div>`;
        },
        width: '30%'
      }
    ],
    language: {
      emptyTable: 'no data found'
    },
    width: '100%'
  });
}

function Delete(url) {
  swal({
    title: 'Are you sure you want to Delete?',
    text: 'You will not be able to restore the data!',
    icon: 'warning',
    buttons: true,
    dangerMode: true
  }).then(deleteYes => {
    if (deleteYes) {
      $.ajax({
        type: 'DELETE',
        url: url,
        success: data => {
          if (data.success) {
            toastr.success(data.message);
            dataTable.ajax.reload();
          } else {
            toastr.error(data.message);
          }
        }
      });
    }
  });
}

$(document).ready(() => {
  loadList();
});
