using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bitmancer.Core.UI {

    [DisallowMultipleComponent]
    public sealed class StrobingButton : BaseBehavior, IPointerEnterHandler, IPointerExitHandler {

        [Tooltip( "The non-scaled seconds to strobe in one direction (e.g. from the starting alpha to endAlpha)." )]
        public float realtimeSeconds = 2;

        [Tooltip( "The strobe is the starting alpha cross-faded to this end value." )]
        public float endAlpha = 0.1f;


        private Coroutine _coroutine;
        private Color _startColor;
        private Button _button;

        private bool _isFadingPaused;


        private IEnumerator strobeCoroutine() {

            yield return null; // wait for next Update (give Start a chance to run)

            while ( true ) {
                _button.image.CrossFadeAlpha( endAlpha, realtimeSeconds, true );
                yield return new WaitForSecondsRealtime( realtimeSeconds ); // triggers "during" (just after) Update
                _button.image.CrossFadeAlpha( _startColor.a, realtimeSeconds, true );
                yield return new WaitForSecondsRealtime( realtimeSeconds ); // triggers "during" (just after) Update
            }
        }


        private void startStrobing() {
            if ( _coroutine == null ) {
                _coroutine = StartCoroutine( strobeCoroutine() );
            }
        }


        private void stopStrobing() {
            if ( _coroutine != null ) {
                StopCoroutine( _coroutine );
                _coroutine = null;
            }
        }


        #region IPointerEnterHandler implementation

        public void OnPointerEnter( PointerEventData eventData ) {
            // Stop strobing when the button is highlighted (otherwise we'll strobe the highlight, too)
            stopStrobing();
        }

        #endregion


        #region IPointerExitHandler implementation

        public void OnPointerExit( PointerEventData eventData ) {

            // Resume strobing once the button is no longer highlighted

            // Snap the color back to a midpoint before resuming the strobe (avoids flashing if using a high end alpha)
            float alpha = (_startColor.a + endAlpha) / 2f;
            Color c = new Color( _startColor.r, _startColor.g, _startColor.b, alpha );
            _button.image.CrossFadeColor( c, 0, true, true );

            startStrobing();
        }

        #endregion


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
            _button = GetRequiredComponent<Button>();
        }


        void OnEnable() {
            startStrobing();
        }


        void OnDisable() {
            stopStrobing();
        }


        void Start() {

            _startColor = _button.colors.normalColor;

            // Whatever CrossFadeAlpha does (adjusts CanvasRenderer??), it starts at alpha 1.0 rather than use what the image is set to,
            // so immediately set the alpha to match the image (this allows the user to set the desired starting alpha in the Unity Editor
            // and have it show up visually in the scene)
            _button.image.CrossFadeColor( _startColor, 0, true, true );
        }
    }
}
