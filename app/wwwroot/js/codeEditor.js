// Thin JS-interop wrapper around CodeMirror 5 (vendored in wwwroot/lib/codemirror —
// not Monaco, deliberately: this app already ships a heavy .NET+Roslyn WASM
// payload, so the editor itself stays a lightweight ~200KB rather than adding
// several more MB). One instance per DOM element id; Blazor owns lifecycle via
// initEditor/disposeEditor.

window.codeEditorInterop = {
    _instances: {},

    initEditor: function (elementId, dotNetRef, initialValue) {
        const host = document.getElementById(elementId);
        if (!host) {
            console.error("codeEditorInterop.initEditor: no element with id", elementId);
            return;
        }

        const cm = CodeMirror(host, {
            value: initialValue || "",
            mode: "text/x-csharp",
            theme: "dracula",
            lineNumbers: true,
            indentUnit: 4,
            tabSize: 4,
            indentWithTabs: false,
            matchBrackets: true,
            viewportMargin: Infinity,
            // Without this, a long single line (a starter-code comment, a LINQ
            // chain, ...) forces CodeMirror's internal scroller wider than its
            // host element — and since nothing upstream constrains that width,
            // it drags the whole page into horizontal scroll instead of just
            // scrolling inside the editor. Especially bad on mobile.
            lineWrapping: true,
        });

        cm.on("change", () => {
            const value = cm.getValue();
            dotNetRef.invokeMethodAsync("OnCodeChanged", value);
        });

        this._instances[elementId] = cm;

        // CodeMirror measures itself on init; if the host was hidden/zero-sized
        // at that point (e.g. inside a not-yet-visible tab), a refresh once
        // it's actually visible avoids a collapsed/blank-looking editor.
        requestAnimationFrame(() => cm.refresh());
    },

    getValue: function (elementId) {
        const cm = this._instances[elementId];
        return cm ? cm.getValue() : "";
    },

    setValue: function (elementId, value) {
        const cm = this._instances[elementId];
        if (cm) cm.setValue(value || "");
    },

    disposeEditor: function (elementId) {
        // CodeMirror 5 has no formal teardown API; dropping the reference and
        // letting the host element be removed by Blazor is sufficient.
        delete this._instances[elementId];
    },
};
