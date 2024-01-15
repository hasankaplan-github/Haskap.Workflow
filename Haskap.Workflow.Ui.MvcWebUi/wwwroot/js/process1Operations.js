

function showProgressDataModal(requestId, commandId, viewName) {
    $.ajax({
        type: "GET",
        url: '/Process1/Load' + viewName + 'ViewComponent', //'@Url.Action("LoadCreateReservationViewComponent", "Schedule")',
        data: {
            requestId: requestId,
            commandId: commandId
        }
    }).done(function (result, status, xhr) {
        $("#process1ProgressDataModalContent").html(result);
    });

    process1ProgressDataModal = new bootstrap.Modal(document.getElementById(modal.process1ProgressDataModalId), wizardModalOptions);
    process1ProgressDataModal.show();
}
