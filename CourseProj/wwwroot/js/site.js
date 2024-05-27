// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

    document.addEventListener('DOMContentLoaded', function () {
    const themeToggle = document.getElementById('theme-toggle');
    const themeIcon = document.getElementById('theme-icon');
        var selectedLanguage = Cookies.get("SelectedCulture");

        // Установка текущего языка, если он был сохранен в куки
        if (selectedLanguage) {
            // Устанавливаем выбранный язык в элементе select
            $('select[name="culture"]').val(selectedLanguage);
        }
    // Проверка сохраненной темы при загрузке страницы
    const savedTheme = Cookies.get("SelectedTheme");
    if (savedTheme) {
    document.documentElement.classList.add(savedTheme);
    if (savedTheme === 'dark-theme') {
    themeIcon.classList.remove('bi-moon');
    themeIcon.classList.add('bi-sun');
}
}

    themeToggle.addEventListener('click', function () {
    document.documentElement.classList.toggle('dark-theme');
    if (document.documentElement.classList.contains('dark-theme')) {
    themeIcon.classList.remove('bi-moon');
    themeIcon.classList.add('bi-sun');
        Cookies.set("SelectedTheme", 'dark-theme', { expires: 365 });
} else {
    themeIcon.classList.remove('bi-sun');
    themeIcon.classList.add('bi-moon');
        Cookies.set("SelectedTheme", '', { expires: 365 });
}
});
});
