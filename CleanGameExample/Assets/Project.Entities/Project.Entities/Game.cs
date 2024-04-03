#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase {
        public record Arguments(Level Level, Character Character);
        private bool isPlaying = true;

        // Args
        private Arguments Args { get; set; } = default!;
        // Globals
        public World World { get; private set; } = default!;
        public Camera2 Camera { get; private set; } = default!;
        // Player
        public Player Player { get; private set; } = default!;
        // IsPlaying
        public bool IsPlaying {
            get => isPlaying;
            set {
                isPlaying = value;
            }
        }

        // Awake
        public void Awake() {
            Args = InitializationContext.GetArguments<Game, Arguments>();
            World = this.GetDependencyContainer().Resolve<World>( null );
            Camera = this.GetDependencyContainer().Resolve<Camera2>( null );
            Player = gameObject.AddComponent<Player>( new Player.Arguments( Args.Character ) );
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
            Player.Spawn( World.PlayerStarts.First() );
        }
        public void Update() {
        }

    }
    // Level
    public enum Level {
        Level1,
        Level2,
        Level3
    }
}
