// Applies the player's theme choice ("light" | "dark" | "system") by setting
// (or clearing) documentElement's data-theme attribute — app.css keys its
// light/dark variable overrides off that attribute, falling back to
// prefers-color-scheme when it's absent ("system"). Kept as one small,
// dependency-free function so index.html's pre-boot inline script (which
// must run before Blazor loads, to avoid a flash of the wrong theme) and
// GameStateService.SetThemeAsync (after the player changes it mid-session)
// can both call the exact same logic.
window.themeManager = {
    apply(theme) {
        var root = document.documentElement;
        if (theme === "light" || theme === "dark") {
            root.setAttribute("data-theme", theme);
        } else {
            root.removeAttribute("data-theme");
        }
    },
};
