exports.config = function(weyland) {
    weyland.build('main')
        .task.jshint({
            include: 'Scripts/**/*.js'
        })
        .task.uglifyjs({
            include: ['Scripts/**/*.js', 'Scripts/lib/durandal/**/*.js']
        })
        .task.rjs({
            include: ['Scripts/**/*.{js}', 'Scripts/lib/durandal/**/*.js'],
            loaderPluginExtensionMaps:{
                '.html':'text'
            },
            rjs:{
                name:'../Scripts/lib/almond-custom', //to deploy with require.js, use the build's name here instead
                insertRequire:['main'], //not needed for require
                baseUrl: 'Scripts',
                wrap:true, //not needed for require
                paths : {
                    'text': '../Scripts/lib/durandal/text',
                    'durandal': '../Scripts/lib/durandal',
                    'plugins': '../Scripts/lib/durandal/plugins',
                    'transitions': '../Scripts/lib/durandal/transitions',
                    'knockout': 'empty:',
                    'bootstrap': 'empty:',
                    'jquery': 'empty:'
                },
                inlineText: true,
                optimize : 'none',
                pragmas: {
                    build: true
                },
                stubModules : ['text'],
                keepBuildDir: true,
                out: 'Scripts/main-built.js'
            }
        });
}