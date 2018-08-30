/// <binding BeforeBuild='build' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

var paths = {
    webroot: "./wwwroot/",
    StaticFiles: "./StaticFiles/",
    NodeLibs: "./node_modules/",
    WebFonts: "./node_modules/@fortawesome/fontawesome-free/webfonts/*.*"
};

paths.js = [paths.NodeLibs + "jquery/dist/jquery.js",
    paths.NodeLibs + "popper.js/dist/umd/popper.js",
    paths.NodeLibs + "bootstrap/dist/js/bootstrap.js",
    paths.StaticFiles + "js/**/*.js"];
paths.css = [paths.NodeLibs + "bootstrap/dist/css/bootstrap.css",
    paths.NodeLibs + "@fortawesome/fontawesome-free/css/all.css",
    paths.StaticFiles + "css/**/*.css"];
paths.concatJsDest = paths.webroot + "dist/site.min.js";
paths.concatCssDest = paths.webroot + "dist/site.min.css";
paths.webfontsDest = paths.webroot + "webfonts/";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean:webfonts", function(cb) {
    rimraf(paths.webfontsDest, cb);
})

gulp.task("clean", ["clean:js", "clean:css", "clean:webfonts"]);

gulp.task("min:js", function () {
    return gulp.src(paths.js)
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src(paths.css)
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("move-webfonts", function() {
    return gulp.src(paths.WebFonts).pipe(gulp.dest(paths.webfontsDest));
})

gulp.task("min", ["min:js", "min:css"]);

gulp.task("build", ["clean", "min", "move-webfonts"]);
