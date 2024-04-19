#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.Entities.Characters.Primary;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Player : PlayerBase {
        public interface IContext {
            ValueTask<Character> SpawnCharacterAsync(PlayerSpawnPoint point, CharacterEnum character, CancellationToken cancellationToken);
        }
        public record Arguments(IContext Context);

        // Args
        private Arguments Args { get; set; } = default!;
        // Character
        public Character? Character { get; set; }

        // Awake
        public void Awake() {
            Args = Context.Get<Player, Arguments>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
        }

        // SpawnAsync
        public async ValueTask SpawnAsync(PlayerSpawnPoint point, CharacterEnum character, CancellationToken cancellationToken) {
            Character = await Args.Context.SpawnCharacterAsync( point, character, cancellationToken );
        }

    }
}
