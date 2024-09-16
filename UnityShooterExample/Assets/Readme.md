# Overview
This project is example of a shooter game with high-quality modular architecture (inspired by Clean Architecture, Domain-Driven Design and State-Driven UI).

# Reference
The project consists of several modules (each module consists of sources and assets):
- Project                     - The root
- Project.UI                  - The presentation
- Project.UI.Internal         - The presentation
- Project.App                 - The application-level entities and services
- Project.Entities            - The domain-level entities
- Project.Entities.Actors     - The domain-level entities
- Project.Entities.Things     - The domain-level entities
- Project.Entities.Worlds     - The domain-level entities
- Project.Infrastructure      - Everything common and low-level

The project has several dependencies:
- Clean Architecture Game Framework - The architecture game framework that helping you to develop your project following the best practices.
- Addressables Extensions           - This package is addition to Addressables giving you the ability to manage your assets in more convenient way.
- Addressables Source Generator     - The addition to Addressables giving you the ability to reference assets in a very convenient way with compile time checking.
- Colorful Project Window           - The more convenient project window.
- UIToolkit Theme Style Sheet       - The beautiful UIToolkit theme style sheets and some additions and tools.
- FC Game Audio Pack 1 [Lite]       - The audio themes

The project also consists of the following source codes:
- Project
  - Assets/Project/Project.00/Launcher.cs
  - Assets/Project/Project.00/Program.cs
  - Assets/Project/Project.00/DebugScreen.cs
  - Assets/Project/Project.00/Tools/ProjectToolbar.cs
  - Assets/Project/Project.00/Tools/ProjectWindow.cs
- Project.UI
  - Assets/Project/Project.01.UI/UITheme.cs
  - Assets/Project/Project.01.UI/UIScreen.cs
  - Assets/Project/Project.01.UI/UIRouter.cs
  - Assets/Project/Project.01.UI/MainScreen/MainWidget.cs
  - Assets/Project/Project.01.UI/MainScreen/MenuWidget.cs
  - Assets/Project/Project.01.UI/GameScreen/GameWidget.cs
  - Assets/Project/Project.01.UI/GameScreen/TotalsWidget.cs
  - Assets/Project/Project.01.UI/GameScreen/MenuWidget.cs
  - Assets/Project/Project.01.UI/Common/LoadingWidget.cs
  - Assets/Project/Project.01.UI/Common/UnloadingWidget.cs
  - Assets/Project/Project.01.UI/Common/SettingsWidget.cs
  - Assets/Project/Project.01.UI/Common/ProfileSettingsWidget.cs
  - Assets/Project/Project.01.UI/Common/VideoSettingsWidget.cs
  - Assets/Project/Project.01.UI/Common/AudioSettingsWidget.cs
  - Assets/Project/Project.01.UI/Common/DialogWidget.cs
- Project.UI.Internal
  - Assets/Project/Project.01.UI.Internal/MainScreen/MainWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/MainScreen/MenuWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/GameScreen/GameWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/GameScreen/TotalsWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/GameScreen/MenuWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/LoadingWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/UnloadingWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/SettingsWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/ProfileSettingsWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/VideoSettingsWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/AudioSettingsWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/DialogWidgetView.cs
- Project.App
  - Assets/Project/Project.02.App/Application2.cs
  - Assets/Project/Project.02.App/Storage.cs
  - Assets/Project/Project.02.App/Storage.ProfileSettings.cs
  - Assets/Project/Project.02.App/Storage.VideoSettings.cs
  - Assets/Project/Project.02.App/Storage.AudioSettings.cs
  - Assets/Project/Project.02.App/Storage.Preferences.cs
- Project.Entities
  - Assets/Project/Project.03.Entities/Game.cs
  - Assets/Project/Project.03.Entities/Player.cs
  - Assets/Project/Project.03.Entities/Camera2.cs
- Project.Entities.Actors
  - Assets/Project/Project.03.Entities.Characters/Character.cs
  - Assets/Project/Project.03.Entities.Characters/PlayableCharacter.cs
  - Assets/Project/Project.03.Entities.Characters/NonPlayableCharacter.cs
  - Assets/Project/Project.03.Entities.Characters/PlayerCharacter.cs
  - Assets/Project/Project.03.Entities.Characters/EnemyCharacter.cs
- Project.Entities.Things
  - Assets/Project/Project.03.Entities.Things/Gun.cs
  - Assets/Project/Project.03.Entities.Things/Bullet.cs
- Project.Entities.Worlds
  - Assets/Project/Project.03.Entities.Worlds/World.cs

# Setup
- Install the "UIToolkit Theme Style Sheet" package (https://denis535.github.io/#uitoolkit-theme-style-sheet).
- Embed "UIToolkit Theme Style Sheet" package (press Toolbar/Tools/UIToolkit Theme Style Sheet/Embed Package).
- Link project with Unity Gaming Services.

# Build
- Prepare your project for build (Toolbar/Project/Pre Build).
- Build your project (Toolbar/Project/Build).

# FAQ
- Why can not I compile the stylus files?
    - You need to install the node.js and stylus.
- Why is the "ThemeStyleSheet.styl" compiled with errors?
    - The "UIToolkit Theme Style Sheet" package must be embedded.
- Why is the UI broken?
    - Sometimes you need to reimport the "UIToolkit Theme Style Sheet" package.

# Media
- https://youtu.be/WmLJHRg0EI4

# Links
- https://denis535.github.io
- https://github.com/denis535/CleanGameExample
- https://www.youtube.com/channel/UCLFdZl0pFkCkHpDWmodBUFg
- https://www.nuget.org/profiles/Denis535
- https://openupm.com/packages/?sort=downloads&q=denis535
- https://assetstore.unity.com/publishers/90787
- https://denis535.itch.io/

# If you want to support me
If you want to support me, please rate my packages, subscribe to my YouTube channel and like my videos.
