using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using Bitmancer.Core.Util;

namespace Bitmancer.Core.Controllers {

    public static class SceneController {

        /// <summary>
        /// Asynchronously additively loads the specified scene.
        /// </summary>
        /// <param name="scenePath">The path to the scene to load (e.g. "Scenes/Main" to load Scenes/Main.unity).</param>
        /// <param name="callback">Called when the async operation is finished. The callback receives the scene path as the first parameter, and an activation <c>Action</c> as the second parameter.</param>
        /// <remarks>
        /// The caller is expected to finalize the scene activation at some point by invoking the activate action provided as the second parameter to the callback.
        /// </remarks>
        /// <example>
        /// loadAsync( "Main/Scenes", (scenePath, load) => {
        ///     // activate the loaded scene
        ///     load();
        /// } );
        /// </example>
        public static IEnumerator loadAsync( string scenePath, Action<string, Action> callback ) {

            float startTime = Time.realtimeSinceStartup;

            Log.info( "Additively loading scene \"{0}\"", scenePath );

            var loadOp = SceneManager.LoadSceneAsync( scenePath, LoadSceneMode.Additive );
            loadOp.allowSceneActivation = false; // pause activation until we set this back to true (this also affects isDone, etc.)

            yield return new WaitWhile( () => loadOp.progress < 0.9f ); // via Unity docs, 0.9+ is a magic number that indicates loading is complete when allowSceneActivation is false

            Log.info( "Finished additively loading scene \"{0}\" in {1:F3} seconds", scenePath, Time.realtimeSinceStartup - startTime );

            callback( scenePath, () => { loadOp.allowSceneActivation = true; } );
        }
    }
}
