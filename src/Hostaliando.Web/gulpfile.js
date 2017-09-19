var gulp = require("gulp");

var cssmin = require('gulp-cssmin'),
    replace = require('gulp-replace'),
    cssConcat = require('gulp-concat-css'),
    concat = require('gulp-concat')
    uglify = require('gulp-uglify')
    gutil = require('gulp-util')
    flatten = require('gulp-flatten');


    gulp.task('fonts', function () {
        var filesToMove = [
            './bower_components/gentelella/vendors/bootstrap/dist/fonts/*.*',
            './bower_components/gentelella/vendors/font-awesome/fonts/*.*'
        ];
        return gulp.src(filesToMove, { base: '.' })
            .pipe(flatten())
            .pipe(gulp.dest('./wwwroot/fonts'));
    })

    gulp.task('css', ['fonts'], function () {

        var files = [
            './bower_components/gentelella/vendors/bootstrap/dist/css/bootstrap.min.css',
            './bower_components/gentelella/vendors/font-awesome/css/font-awesome.min.css',
            './bower_components/gentelella/build/css/custom.min.css',
            './bower_components/angucomplete-alt/angucomplete-alt.css',
            './bower_components/textAngular/dist/textAngular.css',
            './bower_components/gentelella/vendors/fullcalendar/dist/fullcalendar.min.css',
            './bower_components/pikaday/css/pikaday.css',
            './wwwroot/css/hostaliando.css'
        ];

        console.log('Inicia tarea de css con ', files);

        return gulp.src(files, { base: '.' })
            .pipe(cssConcat("./wwwroot/css/styles.css"))
            .pipe(cssmin({ keepSpecialComments: 0 }))
            .pipe(replace(/\.\.\/\.\.\/bower_components\/\gentelella\/vendors\/bootstrap\/dist\/fonts/g, '/fonts'))
            .pipe(replace(/\.\.\/\.\.\/bower_components\/\gentelella\/vendors\/font-awesome\/fonts/g, '/fonts'))
            .pipe(gulp.dest('.'));
    });

    gulp.task('resources', function () {
        var files = [
            './bower_components/gentelella/vendors/jquery/dist/jquery.js',
            './bower_components/gentelella/vendors/bootstrap/dist/js/bootstrap.js',
            './bower_components/angular/angular.js',
            './bower_components/ngstorage/ngStorage.js',
            './bower_components/angular-route/angular-route.js',
            './bower_components/underscore/underscore.js',
            './bower_components/angucomplete-alt/angucomplete-alt.js',
            './bower_components/textAngular/dist/textAngular-rangy.min.js',
            './bower_components/textAngular/dist/textAngular.min.js',
            './bower_components/moment/moment.js',
            './bower_components/pikaday/pikaday.js',
            './bower_components/textAngular/dist/textAngular-sanitize.min.js',
            './bower_components/angular-bootstrap-contextmenu/contextMenu.js'
        ];
    
        return gulp.src(files, { base: '.' })
            .pipe(concat("./wwwroot/js/resources.min.js"))
            .pipe(uglify())
            .pipe(gulp.dest('.'));
    });

    gulp.task('app', ['resources'], function () {
        return gulp.src([''], { base: '.' })
            .pipe(concat('./wwwroot/js/app.min.js'))
            .pipe(gulp.dest('.'));
    });

    gulp.task('release', ['resources', 'css'], function () {
        return gulp.src(['./wwwroot/app/**/*.js'], { base: '.' })
            .pipe(concat('./wwwroot/js/app.min.js'))
            .pipe(uglify())
            .on('error', function (err) { gutil.log(gutil.colors.red('[Error]'), err.toString()); })
            .pipe(gulp.dest('.'));
    });