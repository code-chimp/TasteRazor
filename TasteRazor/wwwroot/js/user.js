let dataTable;

function loadList() {
  dataTable = $('#DT_load').DataTable({
    ajax: {
      type: 'GET',
      url: '/api/user',
      datatype: 'json'
    },
    columns: [
      { data: 'fullName', width: '25%' },
      { data: 'email', width: '25%' },
      { data: 'phoneNumber', width: '25%' },
      {
        data: { id: 'id', lockoutEnd: 'lockoutEnd' },
        render: data => {
          const today = new Date().getTime();
          const lockout = new Date(data.lockoutEnd).getTime();
          let buttonText, buttonClass, icon;

          if (lockout > today) {
            buttonText = 'Unlock';
            buttonClass = 'danger'
            icon = '';
          } else {
            buttonText = 'Lock';
            buttonClass = 'success';
            icon = '-open';
          }

          return `<div class="text-center">
                    <a class="btn btn-${buttonClass} text-white"
                       style="cursor: pointer;width: 100px"
                       onclick="toggleLock('${data.id}')">
                      <i class="fas fa-lock${icon}"></i> ${buttonText}
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

function toggleLock(id) {
  $.ajax({
    type: 'POST',
    url: '/api/User',
    data: JSON.stringify(id),
    contentType: 'application/json',
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

$(document).ready(() => {
  loadList();
});
