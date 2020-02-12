function ValidateForm() {
    var flag = true;
    $('input,select,textarea').each(function (i, item) {
        if ($("#" + item.id).attr("data-required") !== undefined) {
            if ($("#" + item.id).attr("data-required") == "True" || $("#" + item.id).attr("data-required") == "true") {
                if ($(item).val() == "") {
                    flag = false;
                    $("#span" + item.id).css({
                        "display": "block"
                    });
                    $("#div" + item.id).removeClass("form-group");
                    $("#div" + item.id).addClass("form-group-error");
                    $("#lbl" + item.id).text("Please Enter " + $('label[for=' + item.id + ']').text() == "" ? $('label[for=' + item.id + ']').text() : "Field Is Mandatory");
                }
                else if ($("#" + item.id + " option:selected").text().toUpperCase() == "--SELECT--") {
                    flag = false;
                    $("#span" + item.id).css({
                        "display": "block"
                    });
                    $("#div" + item.id).removeClass("form-group");
                    $("#div" + item.id).addClass("form-group-error");
                    $("#lbl" + item.id).text("Please Select " + $('label[for=' + item.id + ']').text() == "" ? $('label[for=' + item.id + ']').text() : "Field Is Mandatory");
                }
                else { flag = CheckDataType(item, flag); }
            }
        }
        flag = CheckDataType(item, flag);
    });
    return flag;
};

function CheckDataType(item, flag) {
    var status = flag;
    if ($("#" + item.id).val() != "" && $("#" + item.id).attr("data-jsonChar") !== undefined) {
        var specialChar = /^[A-Za-z0-9 ,'@!()_/.-]+$/;

        if (!$(item).val().replace(' ', '').match(specialChar)) {
            status = false;
            $("#span" + item.id).css({
                "display": "block"
            });
            $("#div" + item.id).removeClass("form-group");
            $("#div" + item.id).addClass("form-group-error");
            $("#lbl" + item.id).text("No Special Character Allowed");
        }
    }
    if ($("#" + item.id).val() != "" && status) {
        if ($("#" + item.id).attr("data-alphanumeric") !== undefined) {
            var regEx = /^[a-zA-Z0-9]+$/;

            if (!$(item).val().match(regEx)) {
                status = false;
                $("#span" + item.id).css({
                    "display": "block"
                });
                $("#div" + item.id).removeClass("form-group");
                $("#div" + item.id).addClass("form-group-error");
                $("#lbl" + item.id).text("No Special Character Allowed");
            }
        }
        else if ($("#" + item.id).attr("data-number") !== undefined) {
            var regEx = /(^\d*\.?\d*[0-9]+\d*$)|(^[0-9]+\d*\.\d*$)/;

            if (!$(item).val().match(regEx)) {
                status = false;
                $("#span" + item.id).css({
                    "display": "block"
                });
                $("#div" + item.id).removeClass("form-group");
                $("#div" + item.id).addClass("form-group-error");
                $("#lbl" + item.id).text("Only numeric Allowed");
            }
        }
        else if ($("#" + item.id).attr("data-numberLessOrEqualYr") !== undefined) {
            var regEx = /(^\d*\.?\d*[0-9]+\d*$)|(^[0-9]+\d*\.\d*$)/;
            if (!$(item).val().match(regEx)) {
                status = false;
                $("#span" + item.id).css({
                    "display": "block"
                });
                $("#div" + item.id).removeClass("form-group");
                $("#div" + item.id).addClass("form-group-error");
                $("#lbl" + item.id).text("Only numeric Allowed");
            }
            else {

                if ($("#" + item.id).val() > (new Date).getFullYear()) {
                    status = false;
                    $("#span" + item.id).css({
                        "display": "block"
                    });
                    $("#div" + item.id).removeClass("form-group");
                    $("#div" + item.id).addClass("form-group-error");
                    $("#lbl" + item.id).text("Year Should Be Equal To Or Less Than Current Year");
                }

            }

        }
        else if ($("#" + item.id).attr("data-email") !== undefined) {
            var regEx = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;

            if (!regEx.test(($("#" + item.id).val()))) {
                status = false;
                $("#span" + item.id).css({
                    "display": "block"
                });
                $("#div" + item.id).removeClass("form-group");
                $("#div" + item.id).addClass("form-group-error");
                $("#lbl" + item.id).text("InValid Email");
            }
        }

        else if ($("#" + item.id).attr("data-decimal") !== undefined) {
            if ($("#" + item.id).attr("data-decimal") == "True" || $("#" + item.id).attr("data-decimal") == "true") {
                var decimal = /^[0-9]+\.[0-9]+$/;
                var number = /(^\d*\.?\d*[0-9]+\d*$)|(^[0-9]+\d*\.\d*$)/;

                //  if (!$("#" + item.id).val().match(decimal) && !$("#" + item.id).val().match(number)) {
                if (!$("#" + item.id).val().match(decimal) && !$("#" + item.id).val().match(number)) {

                    //var theVal = "";
                    //if (!regEx.match(($("#" + item.id).val())))
                    status = false;
                    $("#span" + item.id).css({
                        "display": "block"
                    });
                    $("#div" + item.id).removeClass("form-group");
                    $("#div" + item.id).addClass("form-group-error");
                    $("#lbl" + item.id).text("InValid Decimal Format");
                }
            }
        }

        else if ($("#" + item.id).attr("data-phoneNumber") !== undefined) {
            if ($("#" + item.id).attr("data-phoneNumber") == "True" || $("#" + item.id).attr("data-phoneNumber") == "true") {
                var phoneNo = /^([\+]|0)[(\s]{0,1}[2-9][0-9]{0,2}[\s-)]{0,2}[0-9][0-9][0-9\s-]*[0-9]$/;
                //var number = /(^\d*\.?\d*[0-9]+\d*$)|(^[0-9]+\d*\.\d*$)/;

                if (!$("#" + item.id).val().match(phoneNo)) {
                    //var theVal = "";
                    //if (!regEx.match(($("#" + item.id).val())))
                    status = false;
                    $("#span" + item.id).css({
                        "display": "block"
                    });
                    $("#div" + item.id).removeClass("form-group");
                    $("#div" + item.id).addClass("form-group-error");
                    $("#lbl" + item.id).text("InValid Phone Format");
                }
            }
        }
        else if ($("#" + item.id).attr("data-Latitude") !== undefined) {
            if ($("#" + item.id).attr("data-Latitude") == "True" || $("#" + item.id).attr("data-Latitude") == "true") {
                var Latitude = /^-?([1-8]?[1-9]|[1-9]0)\.{1}\d{1,6}$/;
                //var number = /(^\d*\.?\d*[0-9]+\d*$)|(^[0-9]+\d*\.\d*$)/;

                if (!$("#" + item.id).val().match(Latitude)) {
                    //var theVal = "";
                    //if (!regEx.match(($("#" + item.id).val())))
                    status = false;
                    $("#span" + item.id).css({
                        "display": "block"
                    });
                    $("#div" + item.id).removeClass("form-group");
                    $("#div" + item.id).addClass("form-group-error");
                    $("#lbl" + item.id).text("InValid Latitude");
                }
            }
        }
        else if ($("#" + item.id).attr("data-Longitude") !== undefined) {
            if ($("#" + item.id).attr("data-Longitude") == "True" || $("#" + item.id).attr("data-Longitude") == "true") {
                var Longitude = /^-?([1]?[1-7][1-9]|[1]?[1-8][0]|[1-9]?[0-9])\.{1}\d{1,6}$/;
                //var number = /(^\d*\.?\d*[0-9]+\d*$)|(^[0-9]+\d*\.\d*$)/;

                if (!$("#" + item.id).val().match(Longitude)) {
                    //var theVal = "";
                    //if (!regEx.match(($("#" + item.id).val())))
                    status = false;
                    $("#span" + item.id).css({
                        "display": "block"
                    });
                    $("#div" + item.id).removeClass("form-group");
                    $("#div" + item.id).addClass("form-group-error");
                    $("#lbl" + item.id).text("InValid Longitude");
                }
            }
        }
        else if ($("#" + item.id).attr("data-date") !== undefined) {

            var objvalue = s = ($("#" + item.id).val());
            var r = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;
            if (!r.test(objvalue)) {
                status = false;
                $("#span" + item.id).css({
                    "display": "block"
                });
                $("#div" + item.id).removeClass("form-group");
                $("#div" + item.id).addClass("form-group-error");
                $("#lbl" + item.id).text("InValid Date Format");
            }
            else {
                var monthfield = objvalue.split("/")[1]
                var dayfield = objvalue.split("/")[0]
                var yearfield = objvalue.split("/")[2]
                var dayobj = new Date(yearfield, monthfield - 1, dayfield)
                if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield)) {
                    status = false;
                    $("#span" + item.id).css({
                        "display": "block"
                    });
                    $("#div" + item.id).removeClass("form-group");
                    $("#div" + item.id).addClass("form-group-error");
                    $("#lbl" + item.id).text("InValid Date Format");

                }
            }

        }
        else if ($("#" + item.id).attr("data-datetime") !== undefined) {

            var objvalue = s = ($("#" + item.id).val());
            var date = "";
            var time = "";
            var r = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;
            var arr2 = objvalue.split(' ');
            var regtime = /^\d{1,2}:\d{2}([ap]m)?$/;
            if (arr2.length == 2) {
                date = arr2[0];
                time = arr2[1];
            }
            else {
                date = objvalue.substr(0, 10);
                time = objvalue.substr(10, 6);
            }

            if (!r.test(date) || !regtime.test(time)) {
                status = false;
                $("#span" + item.id).css({
                    "display": "block"
                });
                $("#div" + item.id).removeClass("form-group");
                $("#div" + item.id).addClass("form-group-error");
                $("#lbl" + item.id).text("InValid Date Format");
            }
            else {
                var monthfield = date.split("/")[1]
                var dayfield = date.split("/")[0]
                var yearfield = date.split("/")[2]
                var hour = parseInt(time.split(":")[0], 10);
                var minute = parseInt(time.split(":")[1], 10);
                var isValidTime = false;
                if (hour >= 0 && hour <= 23 && minute >= 0 && minute <= 59)
                    isValidTime = true;
                else if (hour == 24 && minute == 0)
                    isValidTime = true;
                var dayobj = new Date(yearfield, monthfield - 1, dayfield)
                if (((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield)) || !isValidTime) {
                    status = false;
                    $("#span" + item.id).css({
                        "display": "block"
                    });
                    $("#div" + item.id).removeClass("form-group");
                    $("#div" + item.id).addClass("form-group-error");
                    $("#lbl" + item.id).text("InValid Date Format");
                }
            }
        }
    }
    return status;
};

function ClearErrorMsg() {
    $('input,select,textarea').each(function (i, item) {
        if ($("#" + item.id).attr("data-required") !== undefined || $("#" + item.id).attr("data-alphanumeric") !== undefined
            || $("#" + item.id).attr("data-number") !== undefined || $("#" + item.id).attr("data-numberLessOrEqualYr") !== undefined
            || $("#" + item.id).attr("data-jsonChar") !== undefined
            || $("#" + item.id).attr("data-decimal") !== undefined || $("#" + item.id).attr("data-date") !== undefined || $("#" + item.id).attr("data-email") !== undefined
            || $("#" + item.id).attr("data-Latitude") !== undefined || $("#" + item.id).attr("data-Longitude") !== undefined) {
            $("#span" + item.id).css({
                "display": "none"
            });
            $("#div" + item.id).removeClass("form-group-error");
            $("#div" + item.id).addClass("form-group");
        }
    });
};

function ClearAttributeErrorMsg(nameAttrValue) {
    $('input' + '[name^="' + nameAttrValue + '"], ' + 'textarea' + '[name^="' + nameAttrValue + '"], ' + 'select' + '[name^="' + nameAttrValue + '"]').each(function (i, item) {
        if ($("#" + item.id).attr("data-required") !== undefined || $("#" + item.id).attr("data-uncondition") !== undefined || $("#" + item.id).attr("data-alphanumeric") !== undefined
            || $("#" + item.id).attr("data-number") !== undefined || $("#" + item.id).attr("data-numberLessOrEqualYr") !== undefined
            || $("#" + item.id).attr("data-jsonChar") !== undefined
            || $("#" + item.id).attr("data-decimal") !== undefined || $("#" + item.id).attr("data-date") !== undefined || $("#" + item.id).attr("data-email") !== undefined
            || $("#" + item.id).attr("data-Latitude") !== undefined || $("#" + item.id).attr("data-Longitude") !== undefined) {
            $("#span" + item.id).css({
                "display": "none"
            });
            $("#div" + item.id).removeClass("form-group-error");
            $("#div" + item.id).addClass("form-group");
        }
    });
};

function ErrorControlMsg (Id, Msg) {
    $("#span" + Id).css({ "display": "block" });
    $("#div" + Id).removeClass("form-group");
    $("#div" + Id).addClass("form-group-error");
    $("#lbl" + Id).text(Msg);
};

function ClearErrorControlMsg (Id) {
    $("#span" + Id).css({ "display": "none" });
    $("#div" + Id).removeClass("form-group-error");
    $("#div" + Id).addClass("form-group");
};
