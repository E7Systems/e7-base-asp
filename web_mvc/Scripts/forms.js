
    //script used by Delete Action.  Converts http get to post with a Confirm() dialog.
        $(document).ready(                                      //after document is ready
            function ()
            {
                $("a[data-confirm]").                           //look for all anchor tags that have our custom data-post attribute
                    click(function (e)                          //when clicked
                    {
                        e.preventDefault();                     //prevent navigating to original href

                        var $this = $(this);
                        var message = $this.data("confirm");    //extract confirmation message

                        if (message && !confirm(message))       //if no confirm, return
                            return;

                        $("<form>")                                //create form
                       .attr("method", "post")                     //to be posted
                       .attr("action", $this.attr("href"))         //to original href
                       .appendTo(document.body)                    //append it to existing document
                       .submit();                                  //and post it
                    })
            });
