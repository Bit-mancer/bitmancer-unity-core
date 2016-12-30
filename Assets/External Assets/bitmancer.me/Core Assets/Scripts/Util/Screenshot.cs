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
        public static void saveScreenshot() {
            
            string outputPath;

            if ( Application.isMobilePlatform ) {
                // roots at persistent path on mobile
                outputPath = kScreenshotDirectory;
            } else {
                outputPath = System.IO.Path.Combine( Application.persistentDataPath, kScreenshotDirectory );
            }

            System.IO.Directory.CreateDirectory( outputPath );

            var timestamp = DateTime.Now;
            // Build the timestamp piece-by-piece (my feeling is that this is less error-prone during refactoring/changes than trying to do it all via DateTime.ToString)
            string filename = string.Format( "Screenshot {0} at {1}.png", timestamp.ToString( "yyyy-MM-dd" ), timestamp.ToString( "hh.mm.ss.fff" ) );
            string screenshotFile = System.IO.Path.Combine( outputPath, filename );

            Application.CaptureScreenshot( screenshotFile );

            Bitmancer.Core.Util.Log.info( "Saved screenshot to {0}", screenshotFile );
        }
    }
}
