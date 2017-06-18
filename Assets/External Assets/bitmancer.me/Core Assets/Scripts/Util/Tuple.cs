using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;
using Bitmancer.Core;

namespace Bitmancer.Core.Util {

    public struct Tuple<T1> {

        private T1 _first;

        public T1 First {
            get { return _first; }
        }


        public Tuple( T1 first ) {
            _first = first;
        }
    }


    public struct Tuple<T1, T2> {

        private T1 _first;

        public T1 First {
            get { return _first; }
        }


        private T2 _second;

        public T2 Second {
            get { return _second; }
        }


        public Tuple( T1 first, T2 second ) {
            _first = first;
            _second = second;
        }
    }


    public struct Tuple<T1, T2, T3> {

        private T1 _first;

        public T1 First {
            get { return _first; }
        }


        private T2 _second;

        public T2 Second {
            get { return _second; }
        }


        private T3 _third;

        public T3 Third {
            get { return _third; }
        }


        public Tuple( T1 first, T2 second, T3 third ) {
            _first = first;
            _second = second;
            _third = third;
        }
    }


    public struct Tuple<T1, T2, T3, T4> {

        private T1 _first;

        public T1 First {
            get { return _first; }
        }


        private T2 _second;

        public T2 Second {
            get { return _second; }
        }


        private T3 _third;

        public T3 Third {
            get { return _third; }
        }


        private T4 _fourth;

        public T4 Fourth {
            get { return _fourth; }
        }


        public Tuple( T1 first, T2 second, T3 third, T4 fourth ) {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
        }
    }


    public struct Tuple<T1, T2, T3, T4, T5> {

        private T1 _first;

        public T1 First {
            get { return _first; }
        }


        private T2 _second;

        public T2 Second {
            get { return _second; }
        }


        private T3 _third;

        public T3 Third {
            get { return _third; }
        }


        private T4 _fourth;

        public T4 Fourth {
            get { return _fourth; }
        }


        private T5 _fifth;

        public T5 Fifth {
            get { return _fifth; }
        }

        public Tuple( T1 first, T2 second, T3 third, T4 fourth, T5 fifth ) {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
            _fifth = fifth;
        }
    }

}
