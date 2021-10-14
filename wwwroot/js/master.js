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