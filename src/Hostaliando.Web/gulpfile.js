var gulp = require("gulp");

var cssmin = require('gulp-cssmin'),
    replace = require('gulp-replace'),
    cssConcat = require('gulp-concat-css'),
    flatten = require('gulp-flatten');


gulp.task('moveResources', function () {
    var filesToMove = [
        './bower_components/gentelella/vendors/bootstrap/dist/fonts/*.*',
        './bower_components/gentelella/vendors/font-awesome/fonts/*.*'
    ];
    return gulp.src(filesToMove, { base: '.' })
        .pipe(flatten())
        .pipe(gulp.dest('./wwwroot/fonts'));
})

gulp.task('css', ['moveResources'], function () {

    var files = [
        './bower_components/gentelella/vendors/bootstrap/dist/css/bootstrap.min.css',
        './bower_components/gentelella/vendors/font-awesome/css/font-awesome.min.css',
        './bower_components/gentelella/build/css/custom.min.css'
    ];

    console.log('Inicia tarea de css con ', files);

    return gulp.src(files, { base: '.' })
        .pipe(cssConcat("./wwwroot/css/styles.css"))
        .pipe(cssmin({ keepSpecialComments: 0 }))
        .pipe(replace(/\.\.\/\.\.\/bower_components\/\gentelella\/vendors\/bootstrap\/dist\/fonts/g, '/fonts'))
        .pipe(replace(/\.\.\/\.\.\/bower_components\/\gentelella\/vendors\/font-awesome\/fonts/g, '/fonts'))
        .pipe(gulp.dest('.'));
});