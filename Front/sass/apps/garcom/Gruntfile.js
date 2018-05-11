module.exports = function (grunt) {

    // Project configuration
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        path: '../../../app/',
        sass: {
            dist: { files: [{ src: 'main.scss', dest: '_dist/style.min.css' }] }
        },
        cssmin: {
            options: { keepSpecialComments: 0 },
            dist: { files: [{ src: '_dist/style.min.css', dest: '_dist/style.min.css' }] }
        },
        copy: {
            dist: { files: [
                { src: '_dist/style.min.css', dest: '<%= path %>src/style.min.css' }
                //{ src: '_dist/style.min.css.map', dest: '<%= path %>src/css/style.min.css.map' }
            ]}
        },
        watch: {
            dist: {
                files: ['abstracts/**/*', 'base/**/*', 'components/**/*', 'pages/**/*', 'themes/**/*', 'main.scss', '../../base/**/*'],
                tasks: ['default'], options: { spawn: false }
            }
        }
    });

    // Load the plugins that provides the tasks
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-watch');

    // Tasks
    grunt.registerTask('default', ['sass', 'cssmin', 'copy']);
};
