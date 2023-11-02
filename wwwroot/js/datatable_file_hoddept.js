$(document).ready(function () {
    GetEmployee();

});

function GetEmployee() {
    $.ajax({
        url: '/Departmentalshod/GetList',
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
                data: 'Department',
                render: function (data, type, row, meta) {
                    return row.department
                }

            },

            {
                data: 'LocationIncident',
                render: function (data, type, row, meta) {
                    return row.locationIncident
                }

            },

            {
                data: 'NamePersonAffected',
                render: function (data, type, row, meta) {
                    return row.namePersonAffected
                }

            },

            {
                data: 'AddressPerson',
                render: function (data, type, row, meta) {
                    return row.addressPerson
                }

            },

            {
                data: 'Designation',
                render: function (data, type, row, meta) {
                    return row.designation
                }

            },

            {
                data: 'Age',
                render: function (data, type, row, meta) {
                    return row.age
                }

            },
            {
                data: 'Sex',
                render: function (data, type, row, meta) {
                    return row.sex
                }

            },

            {
                data: 'NatureInjury',
                render: function (data, type, row, meta) {
                    return row.natureInjury
                }

            },





            {
                data: 'CauseIncident',
                render: function (data, type, row, meta) {
                    return row.causeIncident
                }

            },


            {
                data: 'NatureofDuty',
                render: function (data, type, row, meta) {
                    return row.natureofDuty
                }

            },


            {
                data: 'ServiceLength',
                render: function (data, type, row, meta) {
                    return row.serviceLength
                }

            },

            {
                data: 'EmpPosture',
                render: function (data, type, row, meta) {
                    return row.empPosture
                }

            },


            {
                data: 'NameEyeWitness',
                render: function (data, type, row, meta) {
                    return row.nameEyeWitness
                }

            },


            {
                data: 'EyeWitnessDivision',
                render: function (data, type, row, meta) {
                    return row.eyeWitnessDivision
                }

            },

            {
                data: 'EmployerName',
                render: function (data, type, row, meta) {
                    return row.employerName
                }

            },

            {
                data: 'ExpDisablement',
                render: function (data, type, row, meta) {
                    return row.expDisablement
                }

            },

            {
                data: 'Remark',
                render: function (data, type, row, meta) {
                    return row.remark
                }

            },
            {
                data: 'StatusDepartmental',
                render: function (data, type, row, meta) {
                    return row.statusDepartmental
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


