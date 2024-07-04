#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class Debug2 {

        // Log
        public static void Log(object message) {
            if (Application.isEditor) return;
            Debug.Log( message );
        }
        public static void LogFormat(string format, params object[] args) {
            if (Application.isEditor) return;
            Debug.LogFormat( format, args );
        }
        // Log
        public static void Log(object message, Object context) {
            if (Application.isEditor) return;
            Debug.Log( message, context );
        }
        public static void LogFormat(Object context, string format, params object[] args) {
            if (Application.isEditor) return;
            Debug.LogFormat( context, format, args );
        }

        // Log/Warning
        public static void LogWarning(object message) {
            if (Application.isEditor) return;
            Debug.LogWarning( message );
        }
        public static void LogWarningFormat(string format, params object[] args) {
            if (Application.isEditor) return;
            Debug.LogWarningFormat( format, args );
        }
        // Log/Warning
        public static void LogWarning(object message, Object context) {
            if (Application.isEditor) return;
            Debug.LogWarning( message, context );
        }
        public static void LogWarningFormat(Object context, string format, params object[] args) {
            if (Application.isEditor) return;
            Debug.LogWarningFormat( context, format, args );
        }

        // Log/Error
        public static void LogError(object message) {
            if (Application.isEditor) return;
            Debug.LogError( message );
        }
        public static void LogErrorFormat(string format, params object[] args) {
            if (Application.isEditor) return;
            Debug.LogErrorFormat( format, args );
        }
        // Log/Error
        public static void LogError(object message, Object context) {
            if (Application.isEditor) return;
            Debug.LogError( message, context );
        }
        public static void LogErrorFormat(Object context, string format, params object[] args) {
            if (Application.isEditor) return;
            Debug.LogErrorFormat( context, format, args );
        }

        // Log/Assertion
        public static void LogAssertion(object message) {
            if (Application.isEditor) return;
            Debug.LogAssertion( message );
        }
        public static void LogAssertionFormat(string format, params object[] args) {
            if (Application.isEditor) return;
            Debug.LogAssertionFormat( format, args );
        }
        // Log/Assertion
        public static void LogAssertion(object message, Object context) {
            if (Application.isEditor) return;
            Debug.LogAssertion( message, context );
        }
        public static void LogAssertionFormat(Object context, string format, params object[] args) {
            if (Application.isEditor) return;
            Debug.LogAssertionFormat( context, format, args );
        }

        // Assert
        public static void Assert(bool condition) {
            if (Application.isEditor) return;
            Debug.Assert( condition );
        }
        public static void Assert(bool condition, object message) {
            if (Application.isEditor) return;
            Debug.Assert( condition, message );
        }
        public static void Assert(bool condition, string message) {
            if (Application.isEditor) return;
            Debug.Assert( condition, message );
        }
        public static void AssertFormat(bool condition, string format, params object[] args) {
            if (Application.isEditor) return;
            Debug.AssertFormat( condition, format, args );
        }
        // Assert
        public static void Assert(bool condition, Object context) {
            if (Application.isEditor) return;
            Debug.Assert( condition, context );
        }
        public static void Assert(bool condition, object message, Object context) {
            if (Application.isEditor) return;
            Debug.Assert( condition, message, context );
        }
        public static void Assert(bool condition, string message, Object context) {
            if (Application.isEditor) return;
            Debug.Assert( condition, message, context );
        }
        public static void AssertFormat(bool condition, Object context, string format, params object[] args) {
            if (Application.isEditor) return;
            Debug.AssertFormat( condition, context, format, args );
        }

    }
}
