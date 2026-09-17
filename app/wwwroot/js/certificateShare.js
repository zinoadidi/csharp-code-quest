// "Share the certificate" helper for Pages/Certificate.razor. Progress lives
// only in this browser's localStorage (see GameState.cs) — there's no
// backend, so a shared /certificate LINK would just show whoever opens it
// THEIR OWN (probably empty) certificate, not the sharer's. So sharing always
// points at the plain landing page, and the actual proof — the sharer's
// name/points/date — goes out as an IMAGE instead of text.
//
// The image is drawn by hand on a <canvas> (not a DOM screenshot library like
// html2canvas) specifically because this app's certificate card leans on
// CSS custom properties and `background-clip: text` gradients for its look,
// neither of which DOM-to-canvas screenshot tools render reliably — that
// approach was tried first and produced an image with an invisible name/
// title and a blank icon. Drawing directly with the Canvas 2D API sidesteps
// all of that: what you see here is exactly what ends up in the image, with
// no external library/CDN dependency either.
window.certificateShare = {
    async shareImage(cert, title, text, landingUrl) {
        let blob;
        try {
            blob = await renderCertificatePng(cert);
        } catch {
            return this.shareTextFallback(title, text, landingUrl);
        }

        if (!blob) {
            return this.shareTextFallback(title, text, landingUrl);
        }

        const file = new File([blob], "csharp-code-quest-certificate.png", { type: "image/png" });

        if (navigator.canShare && navigator.canShare({ files: [file] })) {
            try {
                await navigator.share({ title, text, url: landingUrl, files: [file] });
                return "shared";
            } catch {
                // User dismissed the share sheet — not an error worth surfacing.
                return "cancelled";
            }
        }

        // No native "share an image" support (most desktop browsers) — save
        // the PNG locally instead so it can still be shared however the
        // player likes, and let them know that's what happened.
        try {
            const link = document.createElement("a");
            link.href = URL.createObjectURL(blob);
            link.download = "csharp-code-quest-certificate.png";
            link.click();
            URL.revokeObjectURL(link.href);
            return "downloaded";
        } catch {
            return this.shareTextFallback(title, text, landingUrl);
        }
    },

    async shareTextFallback(title, text, landingUrl) {
        if (navigator.share) {
            try {
                await navigator.share({ title, text, url: landingUrl });
                return "shared";
            } catch {
                return "cancelled";
            }
        }

        try {
            await navigator.clipboard.writeText(`${text} ${landingUrl}`);
            return "copied";
        } catch {
            return "unavailable";
        }
    },
};

// cert: { username, points, maxStreak, achievementsUnlocked, achievementsTotal, daysToComplete (nullable), dateText }
function renderCertificatePng(cert) {
    const W = 1200, H = 800;
    const canvas = document.createElement("canvas");
    canvas.width = W;
    canvas.height = H;
    const ctx = canvas.getContext("2d");

    // Background — a fixed dark/gold theme regardless of the viewer's own
    // light/dark preference, so a shared certificate always looks the same
    // "official" way no matter who opens the image.
    const bg = ctx.createLinearGradient(0, 0, W, H);
    bg.addColorStop(0, "#1e1b3a");
    bg.addColorStop(0.5, "#2d1b4e");
    bg.addColorStop(1, "#3a1f5c");
    ctx.fillStyle = bg;
    ctx.fillRect(0, 0, W, H);

    // Outer gold border + inset hairline, matching .cert-border in app.css.
    const pad = 28;
    roundRect(ctx, pad, pad, W - pad * 2, H - pad * 2, 18);
    ctx.strokeStyle = "#facc15";
    ctx.lineWidth = 3;
    ctx.stroke();
    roundRect(ctx, pad + 14, pad + 14, W - (pad + 14) * 2, H - (pad + 14) * 2, 10);
    ctx.strokeStyle = "rgba(255,255,255,0.18)";
    ctx.lineWidth = 1;
    ctx.stroke();

    ctx.textAlign = "center";

    // Trophy — drawn as text so no external image asset/CORS concerns.
    ctx.font = "64px 'Apple Color Emoji','Segoe UI Emoji',sans-serif";
    ctx.fillText("🏆", W / 2, 140);

    ctx.fillStyle = "#facc15";
    ctx.font = "bold 20px system-ui, -apple-system, sans-serif";
    ctx.fillText(letterSpaced("CERTIFICATE OF COMPLETION", 3), W / 2, 190);

    const titleGrad = ctx.createLinearGradient(W / 2 - 220, 0, W / 2 + 220, 0);
    titleGrad.addColorStop(0, "#f5f3ff");
    titleGrad.addColorStop(1, "#e9d5ff");
    ctx.fillStyle = titleGrad;
    ctx.font = "bold 52px system-ui, -apple-system, sans-serif";
    ctx.fillText("C# Code Quest", W / 2, 250);

    ctx.fillStyle = "#c9c3ff";
    ctx.font = "24px system-ui, -apple-system, sans-serif";
    ctx.fillText("Fundamentals to Capstone", W / 2, 288);

    ctx.font = "20px system-ui, -apple-system, sans-serif";
    ctx.fillText("This certifies that", W / 2, 344);

    ctx.fillStyle = "#ffffff";
    ctx.font = "bold 46px system-ui, -apple-system, sans-serif";
    ctx.fillText(cert.username || "Player", W / 2, 400);

    ctx.fillStyle = "#c9c3ff";
    ctx.font = "19px system-ui, -apple-system, sans-serif";
    wrapText(
        ctx,
        `has successfully completed all 12 levels and ${cert.totalTasks} tasks, mastering C# from ` +
            "first variables to classes, collections, and error handling.",
        W / 2,
        436,
        860,
        26
    );

    // Divider + stat row.
    ctx.strokeStyle = "rgba(255,255,255,0.14)";
    ctx.lineWidth = 1;
    line(ctx, pad + 60, 512, W - pad - 60, 512);
    line(ctx, pad + 60, 640, W - pad - 60, 640);

    const stats = [
        [String(cert.points), "POINTS EARNED"],
        [String(cert.maxStreak), "BEST STREAK"],
        [`${cert.achievementsUnlocked}/${cert.achievementsTotal}`, "ACHIEVEMENTS"],
    ];
    if (cert.daysToComplete) {
        stats.push([String(cert.daysToComplete), cert.daysToComplete === 1 ? "DAY TO COMPLETE" : "DAYS TO COMPLETE"]);
    }
    const colWidth = (W - pad * 2 - 120) / stats.length;
    stats.forEach(([value, label], i) => {
        const cx = pad + 60 + colWidth * (i + 0.5);
        ctx.fillStyle = "#facc15";
        ctx.font = "bold 34px system-ui, -apple-system, sans-serif";
        ctx.fillText(value, cx, 580);
        ctx.fillStyle = "#c9c3ff";
        ctx.font = "13px system-ui, -apple-system, sans-serif";
        ctx.fillText(letterSpaced(label, 1), cx, 604);
    });

    ctx.textAlign = "left";
    ctx.fillStyle = "#c9c3ff";
    ctx.font = "18px system-ui, -apple-system, sans-serif";
    ctx.fillText(cert.dateText || "", pad + 60, 690);

    ctx.textAlign = "right";
    ctx.fillText("🎮 C# Code Quest", W - pad - 60, 690);

    return new Promise((resolve) => canvas.toBlob(resolve, "image/png"));
}

function roundRect(ctx, x, y, w, h, r) {
    ctx.beginPath();
    ctx.moveTo(x + r, y);
    ctx.arcTo(x + w, y, x + w, y + h, r);
    ctx.arcTo(x + w, y + h, x, y + h, r);
    ctx.arcTo(x, y + h, x, y, r);
    ctx.arcTo(x, y, x + w, y, r);
    ctx.closePath();
}

function line(ctx, x1, y1, x2, y2) {
    ctx.beginPath();
    ctx.moveTo(x1, y1);
    ctx.lineTo(x2, y2);
    ctx.stroke();
}

function letterSpaced(str, px) {
    // Canvas has no letter-spacing API for fillText, so approximate the
    // app's uppercase tracking by joining characters with thin spaces.
    return str.split("").join(" ".repeat(px));
}

function wrapText(ctx, text, cx, y, maxWidth, lineHeight) {
    const words = text.split(" ");
    let line = "";
    const lines = [];
    for (const word of words) {
        const test = line ? `${line} ${word}` : word;
        if (ctx.measureText(test).width > maxWidth && line) {
            lines.push(line);
            line = word;
        } else {
            line = test;
        }
    }
    if (line) lines.push(line);
    lines.forEach((l, i) => ctx.fillText(l, cx, y + i * lineHeight));
}
