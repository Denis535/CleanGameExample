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
        public record Arguments(LevelEnum Level);
        private readonly Lock @lock = new Lock();
        private bool isPlaying = true;

        // Args
        private Arguments Args { get; set; } = default!;
        // Deps
        public Camera2 Camera { get; private set; } = default!;
        public World World { get; private set; } = default!;
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
            Args = Context.Get<Game, Arguments>();
            Camera = this.GetDependencyContainer().RequireDependency<Camera2>( null );
            World = this.GetDependencyContainer().RequireDependency<World>( null );
            Player = gameObject.RequireComponent<Player>();
        }
        public void OnDestroy() {
        }

        // Start
        public async void Start() {
            if (@lock.IsLocked) return;
            using (@lock.Enter()) {
                await Player.SpawnAsync( World.PlayerSpawnPoints.First(), Camera );
            }
        }
        public void Update() {
            if (@lock.IsLocked) return;
            using (@lock.Enter()) {
            }
        }

    }
    // Level
    public enum LevelEnum {
        Level1,
        Level2,
        Level3
    }
}
