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
    public partial class Globals : GlobalsBase {

        public string? Profile { get; }

        // Constructor
        public Globals() {
            //var args = Environment.GetCommandLineArgs();
            //Debug.Log( "Arguments: " + args.Join( ", " ) );
            Profile = GetCommandLineArgument( "--profile" );
        }

    }
}
