'use strict';

const path = require( 'path' );
const gulp = require( 'gulp' );
const indents = require( 'gulp-indent-checker' );
const exec = require( 'exec-chainable' );
const del = require( 'del' );

const pkg = require( './package.json' );


const paths = {
    unityApp: process.env.UNITY_APP_PATH || '/Applications/Unity/Unity.app/Contents/MacOS/Unity',
    source: [ './Assets/**/*.cs' ], // gulp should handle this on Windows
    versionFile: path.join( 'Assets', 'External Assets', 'bitmancer.me', 'Core Assets', 'VERSION' ),
    packageRoot: path.join( 'Assets', 'External Assets', 'bitmancer.me', 'Core Assets' ),
    outputPath: '_output',
    packagePrefix: 'Bitmancer-Unity-Core-v',
    packageSuffix: '.unityPackage'
};



gulp.task( 'lint', () => {
    return gulp.src( paths.source )
        .pipe( indents( {
            type: 'spaces',
            throwAtEnd: true
        }));
});


gulp.task( 'export-packages', () => {

    // Unity commandline stuff doesn't handle POSIX path semantics very well, best to use absolute paths...

    const projectPath = __dirname;
    const logPath = path.join( projectPath, 'UnityBuild.log' );
    const versionPath = path.join( projectPath, paths.versionFile );
    const outputPath = path.join( projectPath, paths.outputPath );
    const packagePath = path.join( outputPath, `${paths.packagePrefix}${pkg.version}${paths.packageSuffix}` );

    return exec( `/bin/echo -n "${pkg.version}" > "${versionPath}"` ) // write the version file
    .then( () => {
        return exec( `/bin/mkdir -p "${outputPath}"` ); // Unity fails silenty if the output path doesn't exist
    })
    .then( () => {
        return del( [ path.join( outputPath, '*' ) ] );
    })
    .then( () => {
        return exec( `"${paths.unityApp}" -batchmode -nographics -silent-crashes -logFile "${logPath}" -force-free -projectPath "${projectPath}" -exportPackage "${paths.packageRoot}" "${packagePath}" -quit` );
    })
    .catch( err => {
        console.error( `\n*** BUILD ERROR ***\n\n${err}\nSee UnityBuild.log for details.` );
        process.exit( 1 );
    });
});


// Preflight check -- run before pushing commits, filing a PR, etc.
gulp.task( 'preflight', ['lint'] );

gulp.task( 'build', ['preflight', 'export-packages'] );
