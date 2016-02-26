var Vagforeningar = (function (my, $) {
    my = {};

    my.ansokningar = {};

    my.hasData = function () {
        if (my.ansokningar && my.ansokningar.ansokningar.length > 0)
            return true;
        else
            return false;
    }

    my.htmlOverview = function (item) {
        var result = '<strong>Namn: </strong>' + item.vaghallare + '<br/>' +
        '<strong>Inkom: </strong>' + item.modified.substr(0, 10) + ' ' + item.modified.substr(11, 8) + '<br/>' +
        '<strong>Uppgiftslämnare: </strong>' + item.uppgiftslamnare + '<br/>';
        if (item.uppgiftslamnare_epost != null)
            '<strong>E-post: </strong>' + item.uppgiftslamnare_epost + '<br/>';
        if (item.uppgiftslamnare_telefon != null)
            result += '<strong>Telefon: </strong>' + item.uppgiftslamnare_telefon + '<br/>';

            return result;
    }

    my.current = function (id, callback) {
        $.ajax({
            url: 'api/Vagforening/' + id,
            type: 'GET',
            dataType: 'json',
            crossDomain: true,
            contentType: 'application/json',
            success: function (data) {
                callback(data);
                //                    alert(decodeURIComponent($.param(data)) + '\n' + data.GetApplicationsResult[0].name + ': ' + data.GetApplicationsResult[0].url);
            },
            error: function (xhr, status, errorThrown) {
                alert(status);
            }
        });
    }

    my.approve = function (item, callback) {
        $.ajax({
            url: 'api/Ansokan?file=' + escape(item.path),
            type: 'PUT',
            dataType: 'json',
            crossDomain: true,
            contentType: 'application/json',
            success: function (data) {
                callback();
                //                    alert(decodeURIComponent($.param(data)) + '\n' + data.GetApplicationsResult[0].name + ': ' + data.GetApplicationsResult[0].url);
            },
            error: function (xhr, status, errorThrown) {
                alert(status);
            }
        });
    }

    my.deny = function (item, callback) {
        $.ajax({
            url: 'api/Ansokan?file=' + escape(item.path),
            type: 'DELETE',
            dataType: 'json',
            crossDomain: true,
            contentType: 'application/json',
            success: function (data) {
                callback();
                //                    alert(decodeURIComponent($.param(data)) + '\n' + data.GetApplicationsResult[0].name + ': ' + data.GetApplicationsResult[0].url);
            },
            error: function (xhr, status, errorThrown) {
                alert("Fel: " + status);
            }
        });
    }

    my.init = function (callback) {
        $.ajax({
            url: 'api/Ansokan',
            type: 'GET',
            dataType: 'json',
            crossDomain: true,
            contentType: 'application/json',
            success: function (data) {
                my.ansokningar = data;
                callback();
                //                    alert(decodeURIComponent($.param(data)) + '\n' + data.GetApplicationsResult[0].name + ': ' + data.GetApplicationsResult[0].url);
            },
            error: function (xhr, status, errorThrown) {
                alert(status);
            }
        });
    }


    return my;
}(Vagforeningar, jQuery));