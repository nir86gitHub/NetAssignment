var MessageManager = {
    ShowMessage: function (Msg, MsgType, divCntrlId, spanCntrlId) {
        var MsgDivCntrlId = "divMsg"; var SpanDivCntrlId = "SysMsg";
        if (divCntrlId != undefined && divCntrlId != "") {
            MsgDivCntrlId = divCntrlId;
        }
        if (spanCntrlId != undefined && spanCntrlId != "") {
            SpanDivCntrlId = spanCntrlId;
        }
        if (MsgType == "Error") {
            $("#" + MsgDivCntrlId).css({
                "display": "block"
            });
            $("#" + MsgDivCntrlId).removeClass("alert alert-success");
            $("#" + MsgDivCntrlId).addClass("alert alert-warning");
            $("#" + SpanDivCntrlId).html(Msg);
            $("#" + MsgDivCntrlId).insertAfter(".page-header position-relative");
        }
        else {
            $("#" + MsgDivCntrlId).css({
                "display": "block"
            });
            $("#" + MsgDivCntrlId).removeClass("alert alert-warning");
            $("#" + MsgDivCntrlId).addClass("alert alert-success");
            $("#" + SpanDivCntrlId).html(Msg);
            $("#" + MsgDivCntrlId).insertAfter(".page-header position-relative");
        }
    },
    ClearMessage: function (divCntrlId) {
        var MsgDivCntrlId = "divMsg";
        if (divCntrlId != undefined && divCntrlId != "") {
            MsgDivCntrlId = divCntrlId;
        }
        $("#" + MsgDivCntrlId).css({
            "display": "none"
        });
    },
};

var DataTableManager = {
    DataTable: function () {
        $('#dt_basic').dataTable({
            "aaSorting": []
        });
        //DataTableManager.AppendExportControl();
    },
    AppendExportControl: function () {
        $(".dataTables_filter").append("<a  onclick=Export.Excel() > <img src='../Content/assets/images/ExcelSymbol.png' style='height:36px' /></a>");
        $("#dt_basic_wrapper").css({ 'width': "100%", 'overflow': 'auto' });
    },
    LoadDataTable: function () {
        $('#dt_basic').dataTable({
            "aaSorting": [],
            "bDestroy": true,
            "bRetrieve ": true
        });
        //$("#dt_basic_wrapper").addClass("form-control input-sm input-xsmall input-inline");
        $("#dt_basic_wrapper").css({ 'width': "100%", 'overflow': 'auto' });
    },
}