using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bitmancer.Core.Util {

    /// <summary>
    /// Screenshot utilities.
    /// </summary>
    public static class Screenshot {

        /// <summary>
        /// The relative path where screenshots are written.
        /// </summary>
        public const string kScreenshotDirectory = "Screenshots";


        /// <summary>
        /// Saves a screenshot.
        /// </summary>
        /// <param name="prefix">Prefix (e.g. the name of the Application or similar).</param>
        /// <remarks>
        /// Screenshots are saved as PNGs.
        /// Screenshots made in the editor will be saved to the desktop, with the <c>prefix</c> used as a filename prefix.
        /// Screenshots in standalone desktop builds will be saved to the "My Pictures" folder, with the <c>prefix</c> used as a subfolder name.
        /// Screenshots on mobile will be stored in the application persistent data path, under a <c>kScreenshotDirectory</c> folder (<c>prefix</c> is ignored in this case).
        /// </remarks>
        public static void saveScreenshot( string prefix ) {

            var timestamp = DateTime.Now;
            string filename = string.Format( "{0} at {1}.png", timestamp.ToString( "yyyy-MM-dd" ), timestamp.ToString( "hh.mm.ss.fff" ) );

            string outputPath;

            if ( Application.isMobilePlatform ) {
                // roots at persistent path on mobile
                outputPath = kScreenshotDirectory;

            } else {
                
#if UNITY_EDITOR_OSX || UNITY_EDITOR_WIN
                outputPath = System.Environment.GetFolderPath( System.Environment.SpecialFolder.Desktop );

                // adjust the filename since we're saving to the desktop
                filename = string.Format( "{0} Screenshot {1}", prefix, filename );

#elif UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
                outputPath = System.IO.Path.Combine( System.Environment.GetFolderPath( System.Environment.SpecialFolder.MyPictures ), prefix );
#else
                outputPath = System.IO.Path.Combine( Application.persistentDataPath, kScreenshotDirectory );
#endif
            }

            if ( ! System.IO.Directory.Exists( outputPath ) ) {
                System.IO.Directory.CreateDirectory( outputPath );
            }

            string screenshotFile = System.IO.Path.Combine( outputPath, filename );

            Application.CaptureScreenshot( screenshotFile );

            Bitmancer.Core.Util.Log.info( "Saved screenshot to {0}", screenshotFile );
        }
    }
}
