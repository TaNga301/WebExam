(function ($) {
    "use strict";

	/*----------------------------
	 jQuery MeanMenu
	------------------------------ */
    jQuery('nav#dropdown').meanmenu();
	/*----------------------------
	 jQuery myTab
	------------------------------ */
    $('#myTab a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    });
    $('#myTab3 a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    });
    $('#myTab4 a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    });

    $('#single-product-tab a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    });

    $('[data-toggle="tooltip"]').tooltip();

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
    // Collapse ibox function
    $('#sidebar ul li').on('click', function () {
        var button = $(this).find('i.fa.indicator-mn');
        button.toggleClass('fa-plus').toggleClass('fa-minus');

    });
    /*-----------------------------
    Menu Stick
    ---------------------------------*/
    $(".sicker-menu").sticky({ topSpacing: 0 });

    $('#sidebarCollapse').on('click', function () {
        $("body").toggleClass("mini-navbar");
    });
    $(document).on('click', '.header-right-menu .dropdown-menu', function (e) {
        e.stopPropagation();
    });

    /*--------------------------
     scrollUp
    ---------------------------- */
    $.scrollUp({
        scrollText: '<i class="fa fa-angle-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    });

    $('#btnAddAnswer').on('click', function () {
        var answerTitles = ["A", "B", "C", "D"]
        var count = $('#answerNum').val();
        if (count >= answerTitles.length) return false;
        var answerTitle = answerTitles[count];
        var row = $("#anwserRow .first").clone();

        row.addClass("answer");
        $(row).find('.AnswerContent').attr('id', 'Answers_' + count + '__AnswerContent');
        $(row).find('.AnswerContent').attr('name', 'Answers[' + count + '].AnswerContent');
        $(row).find('.AnswerID').attr('id', 'Answers_' + count + '__AnswerID');
        $(row).find('.AnswerID').attr('name', 'Answers[' + count + '].AnswerID');
        $(row).find('.AnswerTitle').attr('id', 'Answers_' + count + '__AnswerTitle');
        $(row).find('.AnswerTitle').attr('name', 'Answers[' + count + '].AnswerTitle');
        $(row).find('.DelFg').attr('id', 'Answers_' + count + '__DelFg');
        $(row).find('.DelFg').attr('name', 'Answers[' + count + '].DelFg');
        $(row).find('.NewFg').attr('id', 'Answers_' + count + '__NewFg');
        $(row).find('.NewFg').attr('name', 'Answers[' + count + '].NewFg');
        $(row).find('.IsCorrect').attr('id', 'Answers_' + count + '__IsCorrect');
        $(row).find('.IsCorrect').attr('name', 'Answers[' + count + '].IsCorrect');
        $(row).find('input[name="IsCorrect"]').attr('name', 'Answers[' + count + '].IsCorrect');
        $(row).find('.AnswerTitle').val(answerTitle);
        $(row).find('.AnswerID').val(0);

        $(row).find('.answerTitle').html("<i class='icon nalika-delete-button btnDelRow' style='font-size: 17px; margin-right: 24px'></i>" + answerTitle);
        $('.answer:last').after(row);
        $('#answerNum').val(++count);

        $('.btnDelRow').unbind('click');
        $('.btnDelRow').click(function () {
            var row = $(this).closest(".answer");
            $(row).remove();
            $(row).find('.AnswerContent').val("abc");
            $(row).find('.DelFg').val(true);

            return false;
        });
    });

    $('.btnDelRow').click(function () {
        var row = $(this).closest(".answer");
        $(row).find('.AnswerContent').val("abc");
        $(row).remove();
        $(row).find('.DelFg').val(true);

        return false;
    });;
})(jQuery);