/**
 * This file is based on Subtitles.cs:
 *     Author: Lindsey Bieda
 *     URL: https://github.com/LindseyB/Windows/blob/master/Assets/Scripts/Subtitles.cs
 *     License: Public Domain
 *     License Source: https://github.com/LindseyB/Windows/blob/master/LICENSE
 */
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Bitmancer.Core.UI {

    [RequireComponent( typeof(AudioSource) )]
    [DisallowMultipleComponent]
    public sealed class SubtitlesForAudioSource : BaseBehavior {

        private class Subtitle {
            public string line;
            public double startTime;
            public double duration;  
        }


        [Tooltip( "The scaled seconds to fade the subtitle in, and then out." )]
        public float fadeInOutSeconds = 0.7f;

        [Tooltip( "The SRT-based subtitles, parsed via regex that allows comments." )]
        public TextAsset subtitlesFile;

        [Tooltip( "The Text element which will display the subtitles" )]
        public Text text;

        [Tooltip( "The parent CanvasGroup of the Text component. The group is what is used to fade the subtitles in and out." )]
        public CanvasGroup group;


        private AudioSource _source;
        private List<Subtitle> _subtitles;


        /// <summary>
        /// Gets the length in seconds of the audio clip.
        /// </summary>
        /// <value>The length in seconds.</value>
        /// <remarks>An analogue for AudioSource.clip.length.</remarks>
        public float Length {
            get {
                return _source.clip.length;
            }
        }


        public IEnumerator playWithSubtitlesCoroutine() {

            _source.Play();

            double deltaStart;

            for(int i = 0; i < _subtitles.Count; i++) {
                deltaStart = _subtitles[i].startTime;
                if (i > 0) {
                    // We want to start playing at the delta between the the subtitles
                    deltaStart = _subtitles[i].startTime - (_subtitles[i - 1].startTime + _subtitles[i - 1].duration);
                }

                yield return new WaitForSeconds((float)deltaStart);

                text.text = _subtitles[ i ].line;
                var co = StartCoroutine( Bitmancer.Core.Util.Coroutines.linearLerpInUpdateCoroutine( group.alpha, 1, fadeInOutSeconds, alpha => group.alpha = alpha ) );

                yield return new WaitForSeconds((float)_subtitles[i].duration);

                StopCoroutine( co );
                StartCoroutine( Bitmancer.Core.Util.Coroutines.linearLerpInUpdateCoroutine( group.alpha, 0, fadeInOutSeconds, alpha => group.alpha = alpha ) );
            }
        }


        /**
         * Execution Order:
         * 
         *  Awake()
         *  OnEnable() / OnDisable()
         *  Start()
         *  FixedUpdate() [0..n times]
         *  Update()
         *  LateUpdate()
         * */

        void Awake() {

            Assert.IsNotNull( subtitlesFile );
            Assert.IsNotNull( text );
            Assert.IsNotNull( group );

            _source = GetRequiredComponent<AudioSource>();


            _subtitles = new List<Subtitle>();

            // 00:00:00,000 --> 00:00:00,000 sfv format for subtitles
            Regex rgx = new Regex(@"(\d{2}:\d{2}:\d{2},\d{3}) --> (\d{2}:\d{2}:\d{2},\d{3})");

            bool found_duration = false;
            DateTime startTime, endTime;
            int current_index = 0;
            StringBuilder sb = new StringBuilder();

            // Parse the SFV subtitle file assuming wellformed
            foreach (string line in subtitlesFile.text.Split('\n')) {

                if (found_duration) {
                    // handle multiple lines of text; terminated by empty string
                    if ( line.Length > 0 ) {
                        // This line is the text
                        if ( sb.Length > 0 ) {
                            sb.Append( '\n' );
                        }
                        sb.Append( line );
                    } else {
                        _subtitles[current_index].line = sb.ToString();
                        sb.Length = 0;
                        current_index++;
                        found_duration = false;
                    }
                }

                if (rgx.IsMatch(line)) {
                    found_duration = true;
                    MatchCollection matches = rgx.Matches(line);
                    string start = matches[0].Groups[1].Value;
                    string end = matches[0].Groups[2].Value;

                    // Let DateTime do the heavy lifting of parsing since we will need to do some math
                    startTime = DateTime.ParseExact(start, "hh:mm:ss,fff", null);

                    Subtitle sub = new Subtitle();

                    sub.startTime = (3600 * startTime.Hour) + (60 * startTime.Minute) + startTime.Second + (0.001 * startTime.Millisecond);
                    endTime = DateTime.ParseExact(end, "hh:mm:ss,fff", null);
                    sub.duration = (endTime - startTime).TotalSeconds;

                    _subtitles.Add(sub);
                }
            }
        }
    }
}
