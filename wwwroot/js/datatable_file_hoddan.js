$(document).ready(function () {
    GetEmployee();

});

function GetEmployee() {
    $.ajax({
        url: '/Dangeroushod/GetList',
        type: 'Get',
        dataType: 'json',
        success: OnSuccess
    })

}



function OnSuccess(response) {

    $('#employeeTable').DataTable({


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
                data: 'NatureOcc',
                render: function (data, type, row, meta) {
                    return row.natureOcc
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
                data: 'NameEquip',
                render: function (data, type, row, meta) {
                    return row.nameEquip
                }

            },

            {
                data: 'Manufacturer',
                render: function (data, type, row, meta) {
                    return row.manufacturer
                }

            },
            {
                data: 'PurposeUsed',
                render: function (data, type, row, meta) {
                    return row.purposeUsed
                }

            },

            {
                data: 'DateOfManufacture',
                render: function (data, type, row, meta) {
                    return row.dateOfManufacture
                }

            },





            {
                data: 'DateOfInstallation',
                render: function (data, type, row, meta) {
                    return row.dateOfInstallation
                }

            },


            {
                data: 'LastDateOfMaintenance',
                render: function (data, type, row, meta) {
                    return row.lastDateOfMaintenance
                }

            },


            {
                data: 'LastDateTest',
                render: function (data, type, row, meta) {
                    return row.lastDateTest
                }

            },

            {
                data: 'NatureDamage',
                render: function (data, type, row, meta) {
                    return row.natureDamage
                }

            },


            {
                data: 'ReasonOccurence',
                render: function (data, type, row, meta) {
                    return row.reasonOccurence
                }

            },


            {
                data: 'EyeWitnessPerson',
                render: function (data, type, row, meta) {
                    return row.eyeWitnessPerson
                }

            },

            {
                data: 'DescByWitness',
                render: function (data, type, row, meta) {
                    return row.descByWitness
                }

            },

            {
                data: 'PrvAction',
                render: function (data, type, row, meta) {
                    return row.prvAction
                }

            },

            {
                data: 'Remark',
                render: function (data, type, row, meta) {
                    return row.remark
                }

            },
            {
                data: 'StatusDangerous',
                render: function (data, type, row, meta) {
                    return row.statusDangerous
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
                pageSize: 'A1'
            },
            'copy', 'csv', 'excel', 'print'
        ]

    });

}


