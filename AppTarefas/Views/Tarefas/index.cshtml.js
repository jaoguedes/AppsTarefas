$(document).ready(function () {
    $('#filtroCategoriaCor').click(function () {
        var selectedCor = $('#categoriaCorSelect').val();
        window.location.href = '/Tarefas/Index?categoriaCor=' + selectedCor;
    });
});