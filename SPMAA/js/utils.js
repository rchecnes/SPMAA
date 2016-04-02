function getJson(url, data) {

    var result;
    $.ajax({
        url: url,
        data: data,
        type: 'GET',
        datatype: 'json',
        cache: false,
        async: false,
        success: function (data) {
            result = data;
        }
    });
    return JSON.parse(result);
    //return result;
}