window.toggleTheme = (theme) => {
    if (theme === "dark") {
        document.documentElement.classList.add("dark");
    } else {
        document.documentElement.classList.remove("dark");
    }
};

window.initializeTheme = (isDark) => {
    if (isDark) {
        document.documentElement.classList.add("dark");
    } else {
        document.documentElement.classList.remove("dark");
    }
};

window.applySavedTheme = function () {
    var themePreference = localStorage.getItem('themePreference');
    if (themePreference === 'true') {
        document.documentElement.classList.add('dark');
    } else {
        document.documentElement.classList.remove('dark');
    }
};

window.applyThemeOnNavigation = function () {
    setInterval(function () {
        if (window.location.href !== sessionStorage.getItem('previousUrl')) {
            applySavedTheme();
            sessionStorage.setItem('previousUrl', window.location.href);
        }
    }, 500);
};


document.addEventListener('DOMContentLoaded', function () {
    window.applySavedTheme();
    /*applyThemeOnNavigation();*/
});

