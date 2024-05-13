#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.App;

    public partial class Storage {
        public class ProfileSettings : StorageBase {

            private string name = default!;

            // Name
            public string Name {
                get => name;
                set {
                    Assert.Argument.Message( $"Argument 'value' ({value}) is invalid" ).Valid( IsNameValid( value ) );
                    name = value;
                }
            }

            // Constructor
            public ProfileSettings() {
                Load();
            }

            // Save
            public void Save() {
                Save( "ProfileSettings.Name", Name );
            }
            public void Load() {
                Name = Load( "ProfileSettings.Name", "Anonymous" );
            }

            // Utils
            public bool IsNameValid(string? value) {
                return value != null &&
                    value.Length >= 3 &&
                    char.IsLetterOrDigit( value.First() ) &&
                    char.IsLetterOrDigit( value.Last() ) &&
                    value.All( i => char.IsLetterOrDigit( i ) || (i is ' ' or '_' or '-') );
            }

        }
    }
}
