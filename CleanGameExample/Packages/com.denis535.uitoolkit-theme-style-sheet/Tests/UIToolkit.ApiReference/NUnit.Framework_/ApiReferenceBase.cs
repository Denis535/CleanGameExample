#nullable enable
namespace NUnit.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class ApiReferenceBase {

        // GetActualTypes
        public abstract object[] GetActualTypes();

        // GetExpectedTypes
        public abstract object[] GetExpectedTypes();

        // Heleprs
        protected static void AssertThatTypesAreEqual(Type[] actual, Type[] expected) {
            AssertThatTypesAreEqual( actual, expected, out var missing, out var extra );
            if (missing.Any()) {
                TestContext.WriteLine( "Missing: " );
                foreach (var missing_ in missing) {
                    TestContext.WriteLine( missing_ );
                }
                Assert.Fail( "Actual types has '{0}' missing types", missing.Length );
            }
            if (extra.Any()) {
                TestContext.WriteLine( "Extra: " );
                foreach (var extra_ in extra) {
                    TestContext.WriteLine( extra_ );
                }
                Assert.Fail( "Actual types has '{0}' extra types", extra.Length );
            }
        }
        protected static void AssertThatTypesAreEqual(Type[] actual, Type[] expected, out Type[] missing, out Type[] extra) {
            missing = expected.Except( actual ).ToArray();
            extra = actual.Except( expected ).ToArray();
        }

        // Heleprs
        protected static bool IsObsolete(Type type) {
            if (type.GetCustomAttribute<ObsoleteAttribute>() != null) return true;
            if (type.DeclaringType != null) return IsObsolete( type.DeclaringType );
            return false;
        }
        protected static bool IsCompilerGenerated(Type type) {
            if (type.GetCustomAttribute<CompilerGeneratedAttribute>() != null) return true;
            if (type.DeclaringType != null) return IsCompilerGenerated( type.DeclaringType );
            return false;
        }

    }
}
