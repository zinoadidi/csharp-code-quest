// Forces the PWA onto the latest deployed version. Blazor's default service
// worker (service-worker.published.js) caches assets under a version-hashed
// name and only swaps the active worker once every tab of the site is
// closed — so an installed/pinned player can sit on a stale cached build
// indefinitely. This unregisters the service worker and clears its Cache
// Storage entries (both scoped to this origin, separate from localStorage),
// then reloads — the next load re-registers the worker and fetches
// everything fresh. GameState lives in localStorage, which none of this
// touches, so progress survives untouched.
window.appUpdate = {
    forceRefresh: async function () {
        try {
            if ("serviceWorker" in navigator) {
                const registrations = await navigator.serviceWorker.getRegistrations();
                await Promise.all(registrations.map(r => r.unregister()));
            }
            if ("caches" in window) {
                const keys = await caches.keys();
                await Promise.all(keys.map(k => caches.delete(k)));
            }
        } catch {
            // Best-effort — fall through to reload regardless, a plain
            // network reload still picks up the latest index.html/assets.
        }
        window.location.reload();
    },
};
