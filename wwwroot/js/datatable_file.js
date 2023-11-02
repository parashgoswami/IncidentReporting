$(document).ready(function () {
    GetEmployee();
   
   
});

function GetEmployee() {
    $.ajax({
        url: '/Nearmissesadmin/GetList',
        type: 'Get',
        dataType: 'json',
        success: OnSuccess
       
    })

}



function OnSuccess(response) {

    //var table = $('#employeeTable').DataTable({
    //    scrollX: "100%"
    //});
   
   
   var dataTableInstance= $('#employeeTable').DataTable({

    
        bProcessing: true,
        bLengthChange: true,
        lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
        bfilter: true,
        bPaginate: true,
        data: response,
        columns: [

            {
                data: 'Id',
                render: function (data, type, row, meta) {
                    return row.id
                }

            },

            {
                data: 'RequestId',
                render: function (data, type, row, meta) {
                    return row.requestId
                }

            },
            {
                data: 'Project',
                render: function (data, type, row, meta) {
                    return row.project
                }

            },
            {
                data: 'Period',
                render: function (data, type, row, meta) {
                    return row.period
                }

            },

            {
                data: 'ReleaseDate',
                render: function (data, type, row, meta) {
                    return row.releaseDate
                }

            },

            {
                data: 'LocationIncident',
                render: function (data, type, row, meta) {
                    return row.locationIncident
                }

            },

            {
                data: 'DepartmentDiv',
                render: function (data, type, row, meta) {
                    return row.departmentDiv
                }

            },

            {
                data: 'Description',
                render: function (data, type, row, meta) {
                    return row.description
                }

            },

            {
                data: 'EyeWitness',
                render: function (data, type, row, meta) {
                    return row.eyeWitness
                }

            },

            {
                data: 'Escape',
                render: function (data, type, row, meta) {
                    return row.escape
                }

            },

            {
                data: 'Reason',
                render: function (data, type, row, meta) {
                    return row.reason
                }

            },
            {
                data: 'PrvMeasure',
                render: function (data, type, row, meta) {
                    return row.prvMeasure
                }

            },
           
            {
                data: 'Remark',
                render: function (data, type, row, meta) {
                    return row.remark
                }

            },
            {
                data: 'StatusNearmiss',
                render: function (data, type, row, meta) {
                    return row.statusNearmiss
                }

            },
            {
                data: 'RemarkHod',
                render: function (data, type, row, meta) {
                    return row.remarkHod
                }

            },
          
            {
                data: 'Status',
                render: function (data, type, row, meta) {
                    return row.status
                }

            },
         
           
        ],
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pdfHtml5', 
                orientation: 'landscape',
                pageSize: 'A3'
            },
            'copy', 'csv', 'excel', 'print'
        ]



    });

    

    $('#employeeTable tfoot th').each(function () {
        var title = $('#employeeTable thead th').eq($(this).index()).text();
        $(this).html('<input type="text" placeholder="search ' + title+'"/>');
    });

    
}


