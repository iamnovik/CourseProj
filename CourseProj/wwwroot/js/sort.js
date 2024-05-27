$(document).ready(function() {
    $('th').click(function(){
        var table = $(this).parents('table').eq(0);
        var rows = table.find('tr:gt(0)').toArray().sort(comparer($(this).index()));
        console.log($(this).index())
        console.log($(this))
        console.log(rows)
        this.asc = !this.asc;
        if (!this.asc){
            rows = rows.reverse();
            $(this).find('.arrow').removeClass('arrow-up').addClass('arrow-down');
        } else {
            $(this).find('.arrow').removeClass('arrow-down').addClass('arrow-up');
        }
        for (var i = 0; i < rows.length; i++){table.append(rows[i])}

        // Обновление стрелок
        $('th .arrow').css('opacity', 0.3);
        $(this).find('.arrow').css('opacity', 1);

        // Обновление жирности текста
        $('th').css('font-weight', 'normal');
        $(this).css('font-weight', 'bold');
    });

    function comparer(index) {
        return function(a, b) {
            var valA = getCellValue(a, index), valB = getCellValue(b, index);

            return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.toString().localeCompare(valB);
        }
    }

    function getCellValue(row, index){ return $(row).children('td').eq(index).text() }
})