#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.Entities.Characters.Primary;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework;
    using UnityEngine.Framework.Entities;
    using UnityEngine.InputSystem;

    public class Game : GameBase, Player.IContext, Character.IContext {
        public record Arguments(LevelEnum Level, CharacterEnum Character);
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
                if (isPlaying) {
                    Actions.Enable();
                } else {
                    Actions.Disable();
                }
            }
        }
        // Instances
        private List<InstanceHandle<Character>> Characters { get; } = new List<InstanceHandle<Character>>();
        private List<InstanceHandle<Transform>> Enemies { get; } = new List<InstanceHandle<Transform>>();
        private List<InstanceHandle<Transform>> Plunders { get; } = new List<InstanceHandle<Transform>>();
        // Actions
        private InputActions Actions { get; set; } = default!;

        // Awake
        public void Awake() {
            Args = Context.Get<Game, Arguments>();
            Camera = this.GetDependencyContainer().RequireDependency<Camera2>( null );
            World = this.GetDependencyContainer().RequireDependency<World>( null );
            using (Context.Begin<Player, Player.Arguments>( new Player.Arguments( this ) )) {
                Player = gameObject.AddComponent<Player>();
            }
            Actions = new InputActions();
            Actions.Enable();
        }
        public void OnDestroy() {
            foreach (var instance in Characters) {
                instance.ReleaseSafe();
            }
            foreach (var instance in Enemies) {
                instance.ReleaseSafe();
            }
            foreach (var instance in Plunders) {
                instance.ReleaseSafe();
            }
            Actions.Disable();
            Actions.Dispose();
        }

        // Start
        public async void Start() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                    var tasks = new List<Task>();
                    {
                        tasks.Add( Player.SpawnAsync( World.PlayerSpawnPoints.First(), Args.Character, destroyCancellationToken ).AsTask() );
                    }
                    foreach (var enemySpawnPoint in World.EnemySpawnPoints) {
                        tasks.Add( SpawnEnemyAsync( enemySpawnPoint, destroyCancellationToken ).AsTask() );
                    }
                    foreach (var lootSpawnPoint in World.LootSpawnPoints) {
                        tasks.Add( SpawnLootAsync( lootSpawnPoint, destroyCancellationToken ).AsTask() );
                    }
                    await Task.WhenAll( tasks );
                }
            }
        }
        public void Update() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                }
            }
            if (Player.Character != null) {
                Camera.SetTarget( Player.Character.transform, Vector3.up * 2 );
                Camera.Rotate( Actions.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Actions.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply();
            } else {
                Camera.SetTarget( Vector3.up * 1024 );
                Camera.Rotate( Actions.Game.Look.ReadValue<Vector2>() );
                Camera.Zoom( Actions.Game.Zoom.ReadValue<Vector2>().y );
                Camera.Apply();
            }
        }
        public void LateUpdate() {
            if (@lock.CanEnter) {
                using (@lock.Enter()) {
                }
            }
        }

        // Player.IContext
        ValueTask<Character> Player.IContext.SpawnCharacterAsync(PlayerSpawnPoint point, CharacterEnum character, CancellationToken cancellationToken) {
            return SpawnCharacterAsync( point, character, cancellationToken );
        }
        // Character.IContext
        bool Character.IContext.IsFirePressed() {
            return Actions.Game.Fire.IsPressed();
        }
        bool Character.IContext.IsAimPressed() {
            return Actions.Game.Aim.IsPressed();
        }
        bool Character.IContext.IsInteractPressed() {
            return Actions.Game.Interact.WasPressedThisFrame();
        }
        // CharacterBody.IContext
        Vector3? CharacterBody.IContext.GetMoveVector(CharacterBody character) {
            var vector2 = Actions.Game.Move.ReadValue<Vector2>();
            var vector3 = Camera.transform.TransformDirection( vector2.x, 0, vector2.y );
            return new Vector3( vector3.x, 0, vector3.z ).normalized * vector2.magnitude;
        }
        Vector3? CharacterBody.IContext.GetLookTarget(CharacterBody character) {
            if (Actions.Game.Fire.IsPressed() || Actions.Game.Aim.IsPressed() || Actions.Game.Interact.IsPressed()) {
                return Camera.Hit?.Point ?? Camera.transform.TransformPoint( Vector3.forward * 128 + Vector3.up * 1.75f );
            }
            var vector2 = Actions.Game.Move.ReadValue<Vector2>();
            if (vector2 != default) {
                var vector3 = Camera.transform.TransformDirection( vector2.x, 0, vector2.y );
                vector3 = new Vector3( vector3.x, 0, vector3.z ).normalized * vector2.magnitude;
                return character.transform.position + vector3 * 128f;
            }
            return null;
        }
        bool CharacterBody.IContext.IsJumpPressed(CharacterBody character, out float duration) {
            duration = 0;
            return Actions.Game.Jump.IsPressed();
        }
        bool CharacterBody.IContext.IsCrouchPressed(CharacterBody character) {
            return Actions.Game.Crouch.IsPressed();
        }
        bool CharacterBody.IContext.IsAcceleratePressed(CharacterBody character) {
            return Actions.Game.Accelerate.IsPressed();
        }

        // SpawnCharacterAsync
        private async ValueTask<Character> SpawnCharacterAsync(PlayerSpawnPoint point, CharacterEnum character, CancellationToken cancellationToken) {
            using (Context.Begin<Character, Character.Arguments>( new Character.Arguments( this ) )) {
                using (Context.Begin<CharacterBody, CharacterBody.Arguments>( new CharacterBody.Arguments( this ) )) {
                    var instance = new InstanceHandle<Character>( GetCharacterAddress( character ) );
                    Characters.Add( instance );
                    return await instance.InstantiateAsync( point.transform.position, point.transform.rotation, transform, cancellationToken );
                }
            }
        }
        // SpawnEnemyAsync
        private async ValueTask<Transform> SpawnEnemyAsync(EnemySpawnPoint point, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Transform>( R.Project.Entities.Characters.Secondary.EnemyCharacter_Gray_Value );
            Enemies.Add( instance );
            return await instance.InstantiateAsync( point.transform.position, point.transform.rotation, transform, cancellationToken );
        }
        // SpawnLootAsync
        private async ValueTask<Transform> SpawnLootAsync(LootSpawnPoint point, CancellationToken cancellationToken) {
            var instance = new InstanceHandle<Transform>( R.Project.Entities.Characters.Inventory.Gun_Gray_Value );
            Plunders.Add( instance );
            return await instance.InstantiateAsync( point.transform.position, point.transform.rotation, transform, cancellationToken );
        }

        // Heleprs
        private static string GetCharacterAddress(CharacterEnum character) {
            switch (character) {
                case CharacterEnum.Gray: return R.Project.Entities.Characters.Primary.PlayerCharacter_Gray_Value;
                case CharacterEnum.Red: return R.Project.Entities.Characters.Primary.PlayerCharacter_Red_Value;
                case CharacterEnum.Green: return R.Project.Entities.Characters.Primary.PlayerCharacter_Green_Value;
                case CharacterEnum.Blue: return R.Project.Entities.Characters.Primary.PlayerCharacter_Blue_Value;
                default: throw Exceptions.Internal.NotSupported( $"Character {character} is not supported" );
            }
        }

    }
    // Level
    public enum LevelEnum {
        Level1,
        Level2,
        Level3
    }
    // Character
    public enum CharacterEnum {
        Gray,
        Red,
        Green,
        Blue
    }
}
