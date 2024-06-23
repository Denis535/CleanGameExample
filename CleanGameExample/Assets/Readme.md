# Overview
This is example of a project (shooter) with advanced (professional) architecture (inspired by Clean Architecture, Domain Driven Design and Uber Ribs).

# Reference
The project consists of several modules (each module consists of sources and assets):
- Project                     - entry point.
- Project.UI                  - presentation entities and services.
- Project.UI.Internal         - presentation entities and services.
- Project.App                 - application entities and services.
- Project.Entities            - domain entities.
- Project.Entities.Characters - domain entities.
- Project.Entities.Things     - domain entities.
- Project.Entities.Worlds     - domain entities.
- Project.Common              - utils.

## Project
- Assets/Project/Project.00/Launcher.cs
- Assets/Project/Project.00/Program.cs
- Assets/Project/Project.00/DebugScreen.cs
- Assets/Project/Project.00/Tools/ProjectToolbar.cs
- Assets/Project/Project.00/Tools/ProjectBuilder.cs
- Assets/Project/Project.00/Tools/ProjectWindow.cs
## Project.UI
- Assets/Project/Project.01.UI/UITheme.cs
- Assets/Project/Project.01.UI/UIScreen.cs
- Assets/Project/Project.01.UI/UIRouter.cs
- Assets/Project/Project.01.UI/MainScreen/MainWidget.cs
- Assets/Project/Project.01.UI/MainScreen/MenuWidget.cs
- Assets/Project/Project.01.UI/GameScreen/GameWidget.cs
- Assets/Project/Project.01.UI/GameScreen/TotalsWidget.cs
- Assets/Project/Project.01.UI/GameScreen/MenuWidget.cs
- Assets/Project/Project.01.UI/Common/DialogWidgetBase.cs
- Assets/Project/Project.01.UI/Common/SettingsWidget.cs
- Assets/Project/Project.01.UI/Common/ProfileSettingsWidget.cs
- Assets/Project/Project.01.UI/Common/VideoSettingsWidget.cs
- Assets/Project/Project.01.UI/Common/AudioSettingsWidget.cs
- Assets/Project/Project.01.UI/Common/LoadingWidget.cs
- Assets/Project/Project.01.UI/Common/UnloadingWidget.cs
## Project.UI.Internal
- Assets/Project/Project.01.UI.Internal/MainScreen/MainWidgetView.cs
- Assets/Project/Project.01.UI.Internal/MainScreen/MenuWidgetView.cs
- Assets/Project/Project.01.UI.Internal/GameScreen/GameWidgetView.cs
- Assets/Project/Project.01.UI.Internal/GameScreen/TotalsWidgetView.cs
- Assets/Project/Project.01.UI.Internal/GameScreen/MenuWidgetView.cs
- Assets/Project/Project.01.UI.Internal/Common/DialogWidgetViewBase.cs
- Assets/Project/Project.01.UI.Internal/Common/SettingsWidgetView.cs
- Assets/Project/Project.01.UI.Internal/Common/ProfileSettingsWidgetView.cs
- Assets/Project/Project.01.UI.Internal/Common/VideoSettingsWidgetView.cs
- Assets/Project/Project.01.UI.Internal/Common/AudioSettingsWidgetView.cs
- Assets/Project/Project.01.UI.Internal/Common/LoadingWidgetView.cs
- Assets/Project/Project.01.UI.Internal/Common/UnloadingWidgetView.cs
- Assets/Project/Project.01.UI.Internal/VisualElementFactory.cs
- Assets/Project/Project.01.UI.Internal/VisualElementFactory_Main.cs
- Assets/Project/Project.01.UI.Internal/VisualElementFactory_Game.cs
- Assets/Project/Project.01.UI.Internal/VisualElementFactory_Common.cs
## Project.App
- Assets/Project/Project.02.App/Application2.cs
- Assets/Project/Project.02.App/Storage.cs
- Assets/Project/Project.02.App/Storage.ProfileSettings.cs
- Assets/Project/Project.02.App/Storage.VideoSettings.cs
- Assets/Project/Project.02.App/Storage.AudioSettings.cs
- Assets/Project/Project.02.App/Storage.Preferences.cs
## Project.Entities
- Assets/Project/Project.03.Entities/Game.cs
- Assets/Project/Project.03.Entities/Player.cs
- Assets/Project/Project.03.Entities/Camera2.cs
- Assets/Project/Project.03.Entities/Camera2Editor.cs
## Project.Entities.Characters
- Assets/Project/Project.03.Entities.Characters/Character.cs
- Assets/Project/Project.03.Entities.Characters/PlayerCharacter.cs
- Assets/Project/Project.03.Entities.Characters/EnemyCharacter.cs
- Assets/Project/Project.03.Entities.Characters/IGame.cs
- Assets/Project/Project.03.Entities.Characters/IPlayer.cs
## Project.Entities.Things
- Assets/Project/Project.03.Entities.Things/Thing.cs
- Assets/Project/Project.03.Entities.Things/Gun.cs
- Assets/Project/Project.03.Entities.Things/Bullet.cs
- Assets/Project/Project.03.Entities.Things/IDamageable.cs
- Assets/Project/Project.03.Entities.Things/IDamager.cs
- Assets/Project/Project.03.Entities.Things/IWeapon.cs
## Project.Entities.Worlds
- Assets/Project/Project.03.Entities.Worlds/World.cs

# Setup
- Install the "UIToolkit Theme Style Sheet" package (https://denis535.github.io/#uitoolkit-theme-style-sheet).
- Embed "UIToolkit Theme Style Sheet" package (press Tools/UIToolkit Theme Style Sheet/Embed Package).
- Link project with Unity Gaming Services.

# Build
- Prepare your project for build (Toolbar / Project / Pre Build).
- Build your project (Toolbar / Project / Build).

# FAQ
- Why can not I compile the stylus files?
    - You need to install the node.js and stylus.
- Why is the "ThemeStyleSheet.styl" compiled with errors?
    - The "UIToolkit Theme Style Sheet" package must be embedded.
- Why is the UI broken?
    - Sometimes you need to reimport the "UIToolkit Theme Style Sheet" package.

# Media


# Links
- https://github.com/denis535/CleanGameExample
- https://denis535.github.io
- https://www.nuget.org/profiles/Denis535
- https://openupm.com/packages/?sort=downloads&q=denis535
- https://assetstore.unity.com/publishers/90787
- https://denis535.itch.io/
- https://www.youtube.com/channel/UCLFdZl0pFkCkHpDWmodBUFg

# If you want to support me
If you want to support me, please rate my packages, subscribe to my YouTube channel and like my videos.
