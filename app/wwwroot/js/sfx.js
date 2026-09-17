// Small synthesized sound-effect engine (Web Audio API, no asset files) for
// game moments: task complete, quiz passed, achievement unlocked, level
// complete. Whether to actually play is decided on the C# side (see
// GameStateService.PlaySoundAsync / GameState.SoundEnabled) — this module
// just knows how to make each sound when asked. The keys in `sequences`
// below are the JS-side half of the catalog in Services/SoundEffect.cs —
// keep the two in sync (every C# constant's value must have a matching key
// here, and vice versa).
window.sfx = (function () {
    let ctx = null;

    function ensureCtx() {
        if (!ctx) {
            const AudioCtor = window.AudioContext || window.webkitAudioContext;
            if (!AudioCtor) return null;
            ctx = new AudioCtor();
        }
        if (ctx.state === "suspended") {
            ctx.resume();
        }
        return ctx;
    }

    function tone(c, freq, start, duration, type, gainPeak) {
        const osc = c.createOscillator();
        const gain = c.createGain();
        osc.type = type;
        osc.frequency.setValueAtTime(freq, c.currentTime + start);
        gain.gain.setValueAtTime(0, c.currentTime + start);
        gain.gain.linearRampToValueAtTime(gainPeak, c.currentTime + start + 0.02);
        gain.gain.exponentialRampToValueAtTime(0.001, c.currentTime + start + duration);
        osc.connect(gain);
        gain.connect(c.destination);
        osc.start(c.currentTime + start);
        osc.stop(c.currentTime + start + duration + 0.05);
    }

    const sequences = {
        // Passing a task — quick, satisfying two-note "ding-ding" up-chirp.
        taskComplete: (c) => {
            tone(c, 523.25, 0, 0.12, "triangle", 0.18);
            tone(c, 783.99, 0.09, 0.18, "triangle", 0.18);
        },
        // A failed Run Code attempt — short, low, non-punishing "bonk" (not
        // meant to feel bad, just a clear "not yet" cue).
        taskFailed: (c) => {
            tone(c, 220, 0, 0.1, "sine", 0.14);
            tone(c, 174.61, 0.08, 0.16, "sine", 0.12);
        },
        // Opening a level from the sidebar — a light, quick "whoosh in" blip.
        levelOpen: (c) => {
            tone(c, 392, 0, 0.08, "sine", 0.1);
            tone(c, 523.25, 0.05, 0.12, "sine", 0.12);
        },
        // Switching pages (Quest <-> Your Journey) — a very light, subtle
        // click/swish, quieter and shorter than levelOpen so it doesn't
        // compete with it when both fire in quick succession.
        navigate: (c) => {
            tone(c, 440, 0, 0.06, "sine", 0.07);
            tone(c, 587.33, 0.04, 0.08, "sine", 0.06);
        },
        // One quiz card answered right / wrong — tiny, snappy so it doesn't
        // overstay between rapid-fire cards.
        quizCorrect: (c) => {
            tone(c, 698.46, 0, 0.09, "triangle", 0.14);
            tone(c, 987.77, 0.06, 0.12, "triangle", 0.14);
        },
        quizIncorrect: (c) => {
            tone(c, 233.08, 0, 0.14, "sawtooth", 0.08);
        },
        // A whole quiz stage passed / failed (end of the card set).
        quizPass: (c) => {
            tone(c, 587.33, 0, 0.12, "triangle", 0.16);
            tone(c, 880, 0.1, 0.2, "triangle", 0.16);
        },
        quizFail: (c) => {
            tone(c, 293.66, 0, 0.12, "sine", 0.14);
            tone(c, 246.94, 0.1, 0.22, "sine", 0.14);
        },
        achievement: (c) => {
            tone(c, 659.25, 0, 0.1, "square", 0.12);
            tone(c, 880, 0.09, 0.1, "square", 0.12);
            tone(c, 1174.66, 0.18, 0.22, "square", 0.14);
        },
        // Level complete — the "biggest" moment short of finishing the whole
        // quest, so this is a longer mini-fanfare: a rising four-note run
        // followed by a sustained major chord.
        levelComplete: (c) => {
            tone(c, 523.25, 0, 0.14, "triangle", 0.2);
            tone(c, 659.25, 0.12, 0.14, "triangle", 0.2);
            tone(c, 783.99, 0.24, 0.14, "triangle", 0.2);
            tone(c, 1046.5, 0.36, 0.3, "triangle", 0.22);
            tone(c, 1046.5, 0.7, 0.5, "triangle", 0.16);
            tone(c, 1318.51, 0.7, 0.5, "triangle", 0.14);
            tone(c, 1568, 0.7, 0.6, "triangle", 0.12);
        },
        // Finishing the WHOLE quest (every level) — the biggest moment in the
        // game, so this is the longest sound: a full ascending run into a
        // held major chord with a sparkly high octave on top.
        questComplete: (c) => {
            tone(c, 392, 0, 0.12, "triangle", 0.18);
            tone(c, 523.25, 0.1, 0.12, "triangle", 0.18);
            tone(c, 659.25, 0.2, 0.12, "triangle", 0.18);
            tone(c, 783.99, 0.3, 0.12, "triangle", 0.18);
            tone(c, 1046.5, 0.4, 0.16, "triangle", 0.2);
            tone(c, 1046.5, 0.62, 0.9, "triangle", 0.18);
            tone(c, 1318.51, 0.62, 0.9, "triangle", 0.15);
            tone(c, 1568, 0.62, 1.0, "triangle", 0.13);
            tone(c, 2093, 0.62, 1.1, "sine", 0.08);
        },
    };

    return {
        play(name) {
            const seq = sequences[name];
            const c = ensureCtx();
            if (!seq || !c) return;
            try {
                seq(c);
            } catch {
                // Web Audio blocked/unsupported — non-fatal, sound is a nicety.
            }
        },
    };
})();
