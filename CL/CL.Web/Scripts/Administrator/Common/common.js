Array.prototype.remove = function () {
    var what, a = arguments, l = a.length, ax;
    while (l && this.length) {
        what = a[--l];
        while ((ax = this.indexOf(what)) !== -1) {
            this.splice(ax, 1);
        }
    }
    return this;
};

function commonVariables() {
    /// <summary>
    /// Define all the common for the front-end.
    /// </summary>
    /// <returns type=""></returns>
    return {
        iDisplayLength: 50,
        aLengthMenu: [10, 30, 50, 100, 200],
        searchLength: 200
    };
}

function combineUrl(baseUrl, url) {
    /// <summary>
    /// Combine two url into one.
    /// </summary>
    /// <returns type=""></returns>
    return baseUrl + url;
}

function responseCode() {
    return {
        /// <summary>
        ///  This code indicates that the server has received and is processing the request, but no response is available yet
        /// </summary>
        Processing: 102,

        /// <summary>
        /// Standard response for successful HTTP requests. The actual response will depend on the request method used.
        /// </summary>
        Success: 200,

        /// <summary>
        /// The server successfully processed the request, but is not returning any content.
        /// </summary>
        NoContent: 204,

        /// <summary>
        /// The server cannot or will not process the request due to something that is perceived to be a client error 
        /// (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).
        /// </summary>
        BadRequest: 400,

        /// <summary>
        /// Similar to 403 Forbidden, but specifically for use when authentication is required and has failed or has not yet been provided. 
        /// The response must include a WWW-Authenticate header field containing a challenge applicable to the requested resource
        /// </summary>
        Unauthorized: 401,

        /// <summary>
        /// The request was a valid request, but the server is refusing to respond to it.
        /// Unlike a 401 Unauthorized response, authenticating will make no difference
        /// </summary>
        Forbidden: 403,

        /// <summary>
        /// The requested resource could not be found but may be available again in the future. Subsequent requests by the client are permissible
        /// </summary>
        NotFound: 404,

        /// <summary>
        /// The server timed out waiting for the request.
        /// According to HTTP specifications: The client did not produce a request within the time that the server was prepared to wait. 
        /// The client MAY repeat the request without modifications at any later time
        /// </summary>
        TimeOut: 408,

        /// <summary>
        /// Indicates that the request could not be processed because of conflict in the request, such as an edit conflict in the case of multiple updates.
        /// </summary>
        Conflict: 409,

        /// <summary>
        /// A generic error message, given when an unexpected condition was encountered and no more specific message is suitable.
        /// </summary>
        InternalError: 500,

        /// <summary>
        /// The server is currently unavailable (because it is overloaded or down for maintenance). Generally, this is a temporary state.
        /// </summary>
        ServiceUnavailable: 503,

        /// <summary>
        /// This status code is not specified in any RFC and is returned by certain services
        /// </summary>
        UnknownError: 520
    };
}

function httpMethod() {
    return {
        POST: "POST",
        GET: "GET"
    };
}

function addListener_Keyboard_Enter($elementSource, $elementDestination, action) {
    /// <summary>
    ///     Add listener when element source press enter make element destination fire a action
    /// </summary>
    /// <param name="$elementSource" type="type">jquery object</param>
    /// <param name="$elementDestination" type="type">jquery object</param>
    /// <param name="action" type="type">action: "click", "dbclick" and so on</param>
    $elementSource.keydown(function (e) {
        if (e.which == 13) {
            $elementDestination.trigger(action);
        }
    });
}

function addListener_Keyboard_Ctrl_Enter($elementSource, $elementDestination, action) {
    /// <summary>
    ///     Add listener when element source press ctrl + enter make element destination fire a action
    /// </summary>
    /// <param name="$elementSource" type="type">jquery object</param>
    /// <param name="$elementDestination" type="type">jquery object</param>
    /// <param name="action" type="type">action: "click", "dbclick" and so on</param>
    $elementSource.keydown(function (e) {
        if (e.ctrlKey && e.keyCode == 13) {
            $elementDestination.trigger(action);
        }
    });
}

function datatableToolkitUrl() {
    return {
        sSwfPath: "/Content/Administrator/js/plugins/dataTables/swf/copy_csv_xls_pdf.swf"
    }
}

function dateTimeFormatConstant() {
    return {
        getFullDateTimeFormat: "dd/mm/yyyy hh:MM:ss"
    }
}

function replaceAll(str, find, replace) {
    /// <summary>
    ///  replay all
    /// </summary>
    /// <param name="str" type="type">source</param>
    /// <param name="find" type="type">string search</param>
    /// <param name="replace" type="type">string replace</param>
    /// <returns type=""></returns>
    return str.replace(new RegExp(find, 'g'), replace);
}

function getNumberOnly(source) {
    /// <summary>
    ///     Get number from string
    /// </summary>
    /// <param name="source" type="type"></param>
    return parseInt(source.replace(/\D/g, ''));
}

function getFullTime(strTime) {
    /// <summary>
    /// Get full time: dd/mm/yyyy hh:mm AM/PM
    /// </summary>
    /// <param name="strTime"></param>
    /// <returns type=""></returns>
    var dateTime = new Date(strTime);

    var date = dateTime.getDate();
    var month = dateTime.getMonth() + 1;
    var year = dateTime.getFullYear();

    var hours = dateTime.getHours();
    var minutes = dateTime.getMinutes();

    var amPM = hours < 12 ? 'AM' : 'PM';

    return date + "/" + month + "/" + year + " " + hours + ":" + minutes + " " + amPM;
}

function getDate(strTime) {
    /// <summary>
    /// Get full time: dd/mm/yyyy
    /// </summary>
    /// <param name="strTime"></param>
    /// <returns type=""></returns>
    var dateTime = new Date(strTime);

    var date = dateTime.getDate();
    var month = dateTime.getMonth() + 1;
    var year = dateTime.getFullYear();

    return date + "/" + month + "/" + year;
}

String.prototype.preventInjection = function preventInjection() {
    return this.replace(/</g, "&lt;").replace(/>/g, "&gt;");
};

String.prototype.genUrl = function changeToSlug() {
    var title, slug;
    title = this;

    slug = title.toLowerCase();

    slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
    slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
    slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
    slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
    slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
    slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
    slug = slug.replace(/đ/gi, 'd');

    slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');

    slug = slug.replace(/ /gi, "-");

    slug = slug.replace(/\-\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-/gi, '-');
    slug = slug.replace(/\-\-/gi, '-');

    slug = '@' + slug + '@';
    slug = slug.replace(/\@\-|\-\@|\@/gi, '');

    return slug;
}

function getDomain() {
    var url = window.location.href;
    var arr = url.split("/");
    var result = arr[0] + "//" + arr[2] + "/";

    return result;

}

function actionCellDecode(nTd, sData, oData, iRow, iCol) {
    var decode = htmlEncode(sData);
    decode = replaceAll(decode, '\n', "<br/>");
    $(nTd).html(decode);
}

function replaceAll(str, text, replace) {
    if (str != null) {
        while (str.indexOf(text) != -1) {
            str = str.replace(text, replace);
        }
    }
    return str;
}