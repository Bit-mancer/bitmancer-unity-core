'use strict';

const path = require( 'path' );
const gulp = require( 'gulp' );
const indents = require( 'gulp-indent-checker' );
const exec = require( 'exec-chainable' );
const del = require( 'del' );

const pkg = require( './package.json' );


const paths = {
    unityApp: process.env.UNITY_APP_PATH || '/Applications/Unity/Unity.app/Contents/MacOS/Unity',
    source: [ './Assets/**/*.cs', './Assets/**/*.shader', '!./Assets/Standard Assets/**/*' ], // gulp should handle these path styles on Windows
    versionFile: path.join( 'Assets', 'External Assets', 'bitmancer.me', 'Core Assets', 'VERSION' ),
    packageRoot: path.join( 'Assets', 'External Assets', 'bitmancer.me', 'Core Assets' ),
    outputPath: '_output',
    packagePrefix: 'Bitmancer-Unity-Core',
    packageSuffix: '.unityPackage'
};



gulp.task( 'lint', () => {
    // Check for annoying mixed-tabs-spaces issue that can occur in MonoDevelop (because the generated solution
    // will use the default MD configuration, and the user has to remember to change it when moving to a new
    // machine, re-generating the solution, etc.)
    return gulp.src( paths.source )
        .pipe( indents( {
            type: 'spaces',
            throwAtEnd: true
        }));
});


gulp.task( 'build-libraries', () => {

    const projectPath = __dirname;
    const projectFilePath = path.join( projectPath, 'Assembly-CSharp.csproj' );
    const editorProjectFilePath = path.join( projectPath, 'Assembly-CSharp-Editor.csproj' );

    const tempProjectFilename = '_BitmancerUnityCore_build.csproj';
    const tempProjectFilePath = path.join( projectPath, tempProjectFilename );
    const tempEditorProjectFilePath = path.join( projectPath, '_BitmancerUnityCore-Editor_build.csproj' );

    const outputPath = path.join( projectPath, paths.outputPath );
    const debugPath = path.join( outputPath, 'bin', 'Debug' );
    const releasePath = path.join( outputPath, 'bin', 'Release' );

    const assemblyName = 'BitmancerUnityCore';
    const editorAssemblyName = 'BitmancerUnityCore-Editor';

    return exec(
        `/bin/mkdir -p "${outputPath}"`
    ).then( () => {
        return del( [ path.join( outputPath, '*.dll' ), path.join( outputPath, '*.mdb' ), path.join( outputPath, 'bin' ) ] );
    }).then( () => {
        return exec( `/usr/bin/sed "s/<AssemblyName>Assembly-CSharp<\\/AssemblyName>/<AssemblyName>${assemblyName}<\\/AssemblyName>/;s/<OutputPath>Temp[\\]bin[\\]Debug[\\]<\\/OutputPath>/<OutputPath>_output\\\\\\bin\\\\\\Debug\\\\\\<\\/OutputPath>\\\r\\\n\\\t<AssemblyName>${assemblyName}_d<\\/AssemblyName>/;s/<OutputPath>Temp[\\]bin[\\]Release[\\]<\\/OutputPath>/<OutputPath>_output\\\\\\bin\\\\\\Release\\\\\\<\\/OutputPath>/;s/<BaseDirectory>Assets<\\/BaseDirectory>/<BaseDirectory>Assets<\\/BaseDirectory>\\\r\\\n\\\t<ReleaseVersion>${pkg.version}<\\/ReleaseVersion>\\\r\\\n\\\t<SynchReleaseVersion>false<\\/SynchReleaseVersion>/" "${projectFilePath}" > "${tempProjectFilePath}"` );
    }).then( () => {
        return exec( `/usr/bin/sed "s/<AssemblyName>Assembly-CSharp-Editor<\\/AssemblyName>/<AssemblyName>${editorAssemblyName}<\\/AssemblyName>/;s/Include=\\\"Assembly-CSharp.csproj\\\"/Include=\\\"${tempProjectFilename}\\\"/;s/<Name>Assembly-CSharp<\\/Name>/<Name>${assemblyName}<\\/Name>/;s/<OutputPath>Temp[\\]bin[\\]Debug[\\]<\\/OutputPath>/<OutputPath>_output\\\\\\bin\\\\\\Debug\\\\\\<\\/OutputPath>\\\r\\\n\\\t<AssemblyName>${editorAssemblyName}_d<\\/AssemblyName>/;s/<OutputPath>Temp[\\]bin[\\]Release[\\]<\\/OutputPath>/<OutputPath>_output\\\\\\bin\\\\\\Release\\\\\\<\\/OutputPath>/;s/<BaseDirectory>Assets<\\/BaseDirectory>/<BaseDirectory>Assets<\\/BaseDirectory>\\\r\\\n\\\t<ReleaseVersion>${pkg.version}<\\/ReleaseVersion>\\\r\\\n\\\t<SynchReleaseVersion>false<\\/SynchReleaseVersion>/" "${editorProjectFilePath}" > "${tempEditorProjectFilePath}"` );
    }).then( () => {
        return exec( `xbuild /p:Configuration=Debug ${tempProjectFilePath}` );
    }).then( () => {
        return exec( `xbuild /p:Configuration=Release ${tempProjectFilePath}` );
    }).then( () => {
        return exec( `xbuild /p:Configuration=Debug ${tempEditorProjectFilePath}` );
    }).then( () => {
        return exec( `xbuild /p:Configuration=Release ${tempEditorProjectFilePath}` );
    }).then( () => {
        return exec( `/bin/cp "${path.join( releasePath, assemblyName )}"*.dll* "${outputPath}"` );
    }).then( () => {
        return exec( `/bin/cp "${path.join( debugPath, assemblyName )}"*.dll* "${outputPath}"` );
    }).then( () => {
        return del( [ path.join( outputPath, 'bin' ), tempProjectFilePath, tempEditorProjectFilePath ] );
    }).catch( err => {
        console.error( `\n*** BUILD ERROR ***\n\n${err}` );
        process.exit( 1 );
    });
});


gulp.task( 'export-packages', () => {

    // Unity commandline stuff doesn't handle POSIX path semantics very well, best to use absolute paths...

    const projectPath = __dirname;
    const logPath = path.join( projectPath, 'UnityBuild.log' );
    const versionPath = path.join( projectPath, paths.versionFile );
    const outputPath = path.join( projectPath, paths.outputPath );

    const materialsPath = path.join( paths.packageRoot, 'Materials' );
    const meshesPath = path.join( paths.packageRoot, 'Meshes' );
    // TODO once library is out of active dev/beta: const shadersPath = path.join( paths.packageRoot, 'Meshes' );
    const texturesPath = path.join( paths.packageRoot, 'Textures' );

    const sourcePackagePath = path.join(
        outputPath,
        `${paths.packagePrefix}-Source-v${pkg.version}${paths.packageSuffix}` );

    const assetsPackagePath = path.join(
        outputPath,
        `${paths.packagePrefix}-Assets-v${pkg.version}${paths.packageSuffix}` );

    return exec(
        // write the version file
        `/bin/echo -n "${pkg.version}" > "${versionPath}"`
    ).then( () => {
        return exec( `/bin/mkdir -p "${outputPath}"` ); // Unity fails silenty if the output path doesn't exist
    }).then( () => {
        return del( [ sourcePackagePath, assetsPackagePath ] );
    }).then( () => {
        // "Source" is raw source + assets (it's refered to as "source" to distinguish from the "root" package name, "Bitmancer-Unity-Core", which is the recommended method of consuming the library)
        // TODO consider making "source" just the source code, and leave assets in a separate package since these can likely be kept completely independent of one another

        // TODO need to actually add the "root" package once the library is out of active/beta dev...

        return exec( `"${paths.unityApp}" -batchmode -nographics -silent-crashes -logFile "${logPath}" -force-free -projectPath "${projectPath}" -exportPackage "${paths.packageRoot}" "${sourcePackagePath}" -quit` );
    }).then( () => {
        return exec( `"${paths.unityApp}" -batchmode -nographics -silent-crashes -logFile "${logPath}" -force-free -projectPath "${projectPath}" -exportPackage "${materialsPath}" "${meshesPath}" "${texturesPath}" "${assetsPackagePath}" -quit` );
    }).catch( err => {
        console.error( `\n*** BUILD ERROR ***\n\n${err}\nSee UnityBuild.log for details.` );
        process.exit( 1 );
    });
});


// Preflight check -- run before pushing commits, filing a PR, etc.
gulp.task( 'preflight', ['lint'] ); // TODO tests (if we can run them outside of Unity env), coverage, docs

gulp.task( 'ci', ['preflight'] ); // this is what continuous integration (e.g. Travis) runs

gulp.task( 'build', cb => {
    require( 'run-sequence' )( 'preflight', 'build-libraries', 'export-packages', cb );
});
