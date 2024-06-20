#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.App;

    // https://docs.unity3d.com/Manual/CommandLineArguments.html
    // https://docs.unity3d.com/Manual/EditorCommandLineArguments.html
    // https://docs.unity3d.com/Manual/PlayerCommandLineArguments.html
    public partial class Storage : StorageBase {

        // Profile
        public string? Profile { get; }

        // Constructor
        internal Storage() {
            //foreach (var (key, values) in CLI.GetKeyValues( Environment.GetCommandLineArgs() )) {
            //    if (key != null) {
            //        Debug.Log( key + ": " + string.Join( ", ", values ) );
            //    } else {
            //        Debug.Log( string.Join( ", ", values ) );
            //    }
            //}
            Profile = CLI.GetValue( Environment.GetCommandLineArgs(), "--profile" );
        }

    }
}
