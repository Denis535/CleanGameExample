# Overview
This project is well designed game template allowing you to quickly and efficiently start developing your project with the best practices (like Domain Driven Design, Clean Architecture and Uber Ribs).

# Reference
## Assemblies
The project consists of the following modules:
- Project - root of project (entry point, global logic, scenes).
- Project.UI - presentation-level (audio theme, screens, widgets and views).
- Project.App - application-level (application, global variables).
- Project.Entities - domain-level (game, players, characters, entities).
- Project.Common - everything common.

## Namespaces
The project consists of the following namespaces:
- Project
- Project.UI
- Project.UI.MainScreen
- Project.UI.GameScreen
- Project.UI.Common
- Project.App
- Project.Entities.MainScene
- Project.Entities.GameScene
- Project.Entities.WorldScene
- Project.Entities.Common

# Clean Architecture Game Framework
This package contains classes that define the entire architecture of your game project and some other utilities.

## Assemblies
- CleanArchitectureGameFramework - additions.
- CleanArchitectureGameFramework.Core - main.
- CleanArchitectureGameFramework.Internal - utilities and helpers.

## Namespaces
- Framework - This namespace represents the root module.
  - IDependencyContainer - This interface allows you to resolve your dependencies.
  - Program - this class is responsible for the startup and global logic.
- Framework.UI - this namespace represents the presentation (user interface) module.
  - UIAudioTheme - this class is responsible for the audio theme.
  - UIScreen - this class is responsible for the user interface. The user interface consists of the hierarchy of logical (business) units and the hierarchy of visual units.
  - UIWidget - this class is responsible for the business logic of ui unit. This may contain (or not contain) the view.
  - UIView - this class is responsible for the visual (view) logic of ui unit. This just contains the VisualElement, so it's essentially a wrapper for VisualElement.
  - UIRouter - This class is responsible for the application state.
- Framework.App - this namespace represents the application module.
  - Application - this class is responsible for the application logic.
  - Globals - this class provides you with the global values.
- Framework.Entities - this namespace represents the domain (entities) module.
  - Game - this class is responsible for the game rules and states.
  - Player - this class is responsible for the player rules and states.
  - World - This class is responsible for the world.
  - WorldView - this class is responsible for the world's visual and audible aspects.
  - Entity - this class is responsible for the scene's entity (player's avatar, AI agent or any other object).
  - EntityView - this class is responsible for the entity's visual and audible aspects.
  - EntityBody - this class is responsible for the entity's physical aspects.

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
- https://youtu.be/SJ8aB4fxgUo?feature=shared
- https://youtu.be/JQobAqfakJQ?feature=shared
- ![CleanArchitectureGameTemplate-638466226109772156](https://github.com/Denis535/CleanArchitectureGameTemplate/assets/7755015/5c045c2b-657c-4952-a2a1-765ad2dc8a9f)
- ![CleanArchitectureGameTemplate-638466226606827588](https://github.com/Denis535/CleanArchitectureGameTemplate/assets/7755015/fe6e5179-1325-42c2-bb40-9b7200bab31e)
- ![CleanArchitectureGameTemplate-638466226681423464](https://github.com/Denis535/CleanArchitectureGameTemplate/assets/7755015/50d92312-4007-43d6-87ec-efb67db10c48)
- ![CleanArchitectureGameTemplate-638466226738518227](https://github.com/Denis535/CleanArchitectureGameTemplate/assets/7755015/aa2b3099-c431-4919-9997-b76991affc10)
- ![CleanArchitectureGameTemplate-638466226828680706](https://github.com/Denis535/CleanArchitectureGameTemplate/assets/7755015/9ff54663-d273-48b4-96f4-0bd7765b0dda)
- ![CleanArchitectureGameTemplate-638466226975639952](https://github.com/Denis535/CleanArchitectureGameTemplate/assets/7755015/11ace5df-da1e-4e69-b20d-93d77f7aaef5)
- ![CleanArchitectureGameTemplate-638466227067777986](https://github.com/Denis535/CleanArchitectureGameTemplate/assets/7755015/154f1bfd-eb4a-4153-8ece-2a966adb44ac)
- ![CleanArchitectureGameTemplate-638466227132727553](https://github.com/Denis535/CleanArchitectureGameTemplate/assets/7755015/12911cc9-c2f7-4739-9a70-4162b28829a4)
- ![CleanArchitectureGameTemplate-638466227192218633](https://github.com/Denis535/CleanArchitectureGameTemplate/assets/7755015/170c8271-1524-468a-b7e9-9a79fe78cdd9)
- ![CleanArchitectureGameTemplate-638466227287023775](https://github.com/Denis535/CleanArchitectureGameTemplate/assets/7755015/0cfd347b-2b68-4dfd-9823-c9283f9e67c0)
- ![CleanArchitectureGameTemplate-638466228400428880](https://github.com/Denis535/CleanArchitectureGameTemplate/assets/7755015/a059dcfd-f9a0-4cf5-a577-b8bc00e7ee95)

# Links
- https://denis535.github.io
- https://assetstore.unity.com/publishers/90787
- https://denis535.itch.io/
- https://openupm.com/packages/?sort=downloads&q=denis535
- https://www.youtube.com/channel/UCLFdZl0pFkCkHpDWmodBUFg
- https://github.com/Denis535/CleanArchitectureGameTemplate/

# If you want to support me
If you want to support me, please rate my packages, subscribe to my YouTube channel and like my videos.
