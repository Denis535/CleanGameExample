#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;

    public static class InitializationContext {
        private class Scope<T> : IDisposable where T : notnull, Component {

            public static object? Arguments { get; internal set; }

            public Scope(object arguments) {
                Arguments = arguments;
            }
            public void Dispose() {
                Arguments = null;
            }

        }

        public static IDisposable Begin(Type componentType, object arguments) {
            var type_context = typeof( Scope<> ).MakeGenericType( componentType );
            return (IDisposable) Activator.CreateInstance( type_context, arguments );
        }
        public static IDisposable Begin<T>(object arguments) where T : notnull, Component {
            return new Scope<T>( arguments );
        }

        public static object GetArguments(Type componentType) {
            var type_context = typeof( Scope<> ).MakeGenericType( componentType );
            var property_arguments = type_context.GetProperty( "Arguments", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static );
            var result = property_arguments.GetValue( null );
            return result ?? throw Exceptions.Internal.Exception( $"InitializationContext {type_context} has no arguments" );
        }
        public static object GetArguments<T>() where T : Component {
            var result = Scope<T>.Arguments;
            return result ?? throw Exceptions.Internal.Exception( $"InitializationContext {typeof( Scope<T> )} has no arguments" );
        }

    }
    public static class GameObjectExtensions {

        public static T AddComponent<T>(this GameObject gameObject, object arguments) where T : Component {
            using (InitializationContext.Begin<T>( arguments )) {
                return gameObject.AddComponent<T>();
            }
        }
        public static Component AddComponent(this GameObject gameObject, Type componentType, object arguments) {
            using (InitializationContext.Begin( componentType, arguments )) {
                return gameObject.AddComponent( componentType );
            }
        }

    }
}
