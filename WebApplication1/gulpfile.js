/// <binding AfterBuild='default' />
var gulp = require('gulp');
var ugify = require("gulp-uglify");
var concat = require("gulp-concat");
const browserSync = require('browser-sync').create();

//gulp.task("default", ['js'], function () {
//    return gulp.src("wwwroot/js/**/*.js", { sourcemaps: true })
//    .pipe(ugify())
//    .pipe(concat("webaplication.min.js"))
//        .pipe(gulp.dest("wwwroot/dist", { sourcemaps: true }));
//});

//gulp.task('default', ["minify"]);


















gulp.task('minify', function () {
    return gulp.src("wwwroot/js/**/*.js")
        .pipe(ugify())
        .pipe(concat('webaplication.min.js'))
        .pipe(gulp.dest("wwwroot/dist"))
        .pipe(browserSync.stream());
});
gulp.task('default', gulp.series('minify'));

gulp.task('watch', function () {
    browserSync.init({
        server: {
            baseDir: paths.root.www
        }
    });
    gulp.watch(paths.src.js, gulp.series('default'));
    gulp.watch(paths.src.html).on('change', browserSync.reload);
});

