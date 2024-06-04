#nullable enable
namespace UnityEngine.Framework {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.App;
    using UnityEngine.Framework.Entities;
    using UnityEngine.Framework.UI;

    [DefaultExecutionOrder( 1000 )]
    public abstract class ProgramBase2 : ProgramBase, IDependencyContainer {

        // Awake
        protected override void Awake() {
            Application.wantsToQuit += OnQuit;
        }
        protected override void OnDestroy() {
        }

        // Start
        protected abstract void Start();
        protected abstract void FixedUpdate();
        protected abstract void Update();
        protected abstract void LateUpdate();

        // OnQuit
        protected abstract bool OnQuit();

        // IDependencyContainer
        Option<object?> IDependencyContainer.GetValue(Type type, object? argument) {
            return GetValue( type, argument );
        }
        protected abstract Option<object?> GetValue(Type type, object? argument);

    }
    public abstract class ProgramBase2<TTheme, TScreen, TRouter, TApplication, TGame> : ProgramBase2
        where TTheme : UIThemeBase2
        where TScreen : UIScreenBase2
        where TRouter : UIRouterBase2
        where TApplication : ApplicationBase2<TGame>
        where TGame : GameBase2 {

        // UI
        protected abstract TTheme Theme { get; set; }
        protected abstract TScreen Screen { get; set; }
        protected abstract TRouter Router { get; set; }
        // App
        protected abstract TApplication Application { get; set; }
        // Entities
        protected abstract TGame? Game { get; }

        // Awake
        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

    }
}
