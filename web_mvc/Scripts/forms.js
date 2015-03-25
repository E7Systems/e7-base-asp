$(document).ready(function() {
    $("a[data-post]").click(function(e)
    {
        e.preventDefault();                     //prevent navigating to original href

        var $this = $(this);
        var message = $this.data("post");       //extract message

        if (message && !confirm(message))       //if no confirm, return
            return;

        $("<form>")                             //create form
        .attr("method", "post")                 //to be posted
        .attr("action", $this.attr("href"))     //to original href
        .appendTo(document.body)                //and append it to existing view
        .submit();                              //and post it
    })
});