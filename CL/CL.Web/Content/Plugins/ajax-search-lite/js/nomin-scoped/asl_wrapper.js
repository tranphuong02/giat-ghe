(function(jQuery, $, window){
    // INIT CODE

    // Use the window to make sure it is in the main scope, I do not trust IE
    window.ASL = window.ASL || {};

    window.ASL.getScope = function() {
        /**
         * Explanation:
         * If the sript is scoped, the first argument is always passed in a localized jQuery
         * variable, while the actual parameter can be aspjQuery or jQuery (or anything) as well.
         */
        if (typeof jQuery !== "undefined") return jQuery;

        // The code should never reach this point, but sometimes magic happens (unloaded or undefined jQuery??)
        // .. I am almost positive at this point this is going to fail anyways, but worth a try.
        if (typeof window[ASL.js_scope] !== "undefined")
            return window[ASL.js_scope];
        else
            return eval(ASL.js_scope);
    };

    // Call this function if you need to initialize an instance that is printed after an AJAX call
    // Calling without an argument initializes all instances found.
    window.ASL.initialize = function(id) {
        // Yeah I could use $ or jQuery as the scope variable, but I like to avoid magical errors..
        var scope = window.ASL.getScope();
        var selector = ".asl_init_data";

        if (typeof id !== 'undefined')
            selector = "div[id*=asl_init_id_" + id + "]";

        /**
         * Getting around inline script declarations with this solution.
         * So these new, invisible divs contains a JSON object with the parameters.
         * Parse all of them and do the declaration.
         */
        scope(selector).each(function(index, value){
            var rid =  scope(this).attr('id').match(/^asl_init_id_(.*)/)[1];
            var jsonData = scope(this).html();
            if (typeof jsonData === "undefined") return false;
            var args = JSON.parse(jsonData);

            return scope("#ajaxsearchlite" + rid).ajaxsearchlite(args);
        });
    };

    window.ASL.ready = function() {
        var scope = window.ASL.getScope();

        scope(document).ready(function () {
            window.ASL.initialize();
        });
    };

    // Do the init here
    window.ASL.ready();

})(asljQuery, asljQuery, window);