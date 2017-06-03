jQuery(document).ready(function () {

    $('[data-yourtooltipctrl]').each(function (index) {

        var toolTipType = '';

        //get tooltiptype
        if ($(this).data('yourtooltiptype')) {
            toolTipType = $(this).data('yourtooltiptype');
        }

        var container = $("<a href='#' class='yourToolTipLink' data-yourtooltiptype='" + toolTipType
            + "' data-yourtooltipid='" + $(this).data('yourtooltipctrl')
            + "'><img alt='Click for detail' src='/content/info_16x16.png'/></a>")
            .css({
                cursor: 'help',
                'padding-left': '2px'
            });

        $(this).css("display", "inline-block");

        if ($(this).is("label")) {
            $(this).append(container);
        }
        else {
            $(this).after(container);
        }
    });

    $("#yourTooltipPanel").dialog({
        autoOpen: false,
        resizable: true,
        closeOnEscape: true,
        modal: false,
        draggable: true,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close").show();
        }
    });

    //close tge tolltip on x button click
    $(document).on("click", ".popover-title .close", function () {
        $(this).parents(".popover").popover('hide');
    });

    $(".yourToolTipLink").on('click', function (e) {
        e.stopPropagation();
        e.preventDefault();

        var o = $(this);
        var toolTipTypeSpecified = $(this).attr('data-yourtooltiptype');

        //close the dialog or tooltip, to make sure only one at a time
        $(".yourToolTipLink").not(this).popover('hide');

        if ($("#yourTooltipPanel").dialog('isOpen') === true) {
            $("#yourTooltipPanel").not(this).dialog('close');
        }

        //var Dummy = {
        //    "Key": $(this).data('yourtooltipid')
        //};

        jQuery.ajax({
            type: "POST",
            url: "http://localhost:47503/api/tooltip/GetWithPost",
            data: JSON.stringify({ Key: $(this).data('yourtooltipid') }),
            //data: JSON.stringify(Dummy),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            beforeSend: function (xhr) { xhr.setRequestHeader('RequestVerificationToken', $("#antiForgeryToken").val()); },
            headers: {
                Accept: "application/json;charset=utf-8",
                "Content-Type": "application/json;charset=utf-8"
            },
            accepts: {
                text: "application/json"
            },
            success: function (data) {

                if (toolTipTypeSpecified) {
                    o.popover({
                        placement: 'right',
                        html: true,
                        trigger: 'manual',
                        title: data.Title + '<a href="#" class="close" data-dismiss="alert">×</a>',
                        content: '<div class="media"><div class="media-body"><p>' + data.Description + '</p></div></div>'
                    }).popover('toggle');
                    $('.popover').css({ 'width': '100%' });
                }
                else {
                    $("#yourTooltipPanel p").html(data.Description);
                    $("span.ui-dialog-title").text(data.Title);
                    $("#yourTooltipPanel").dialog("option", "position", {
                        my: "left top",
                        at: "left top",
                        of: o
                    }).dialog("open");
                }
            }
        });
    });
});