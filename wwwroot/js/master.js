$(function () {
    $("#StandardMaster").change(function () {        
        var stdId = $("#StandardMaster").val();
        if (stdId > 0) {
            GetSubjects(stdId);
        }
    });
});

function GetSubjects(id) {
    var url = "/Admin/Chapter/GetSubject/" + id;  
    $.getJSON(url, function (data) {        
        var items = '<option>Select a Subject</option>';
        $.each(data, function (i) {
            
            items += "<option value='" + data[i].subjectID + "'>" + data[i].subjectName + "</option>";
        });
        $('#SubjectMaster').html(items);
    });
}

$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});