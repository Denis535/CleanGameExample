# Заголовок
========================================
Architecture of Game Projects Using Third Person Shooter as an Example
Архитектура Игровых Проектов на Примере Шутера от Третьего Лица

# Подзаголовок
========================================
Shooter Game Tutorial | Game Architecture | Modular Architecture | Clean Architecture | Domain-Driven Design | State-Driven UI | Best Practices
Учебное пособие по игре-шутеру | Архитектура игр | Модульная архитектура | Чистая архитектура | Domain-Driven Дизайн | State-Driven Пользовательский Интерфейс | Лучшие Практики

# Обзор
========================================
В этом видео курсе мы подробно рассмотрим пример шутера, в котором я постарался воплотить все свои лучшие идеи и наработки, накопленные за годы работы в игровой индустрии.
Заметьте, я не буду углубляться в детали устройства движка Unity, так как предполагается, что у вас уже есть базовые знания об этом движке.
Вместо этого, я сосредоточусь на главных принципах разработки качественных проектов.
Эти знания будут вам полезны не только для работы с Unity, но и в других сферах программирования.
Также мы рассмотрим некоторые дополнительные пакеты и инструменты, которые значительно упростили разработку основного проекта.

В этом курсе:
- Мы подробно рассмотрим наш пример шутера и все дополнительные пакеты.
- Мы рассмотрим самые важные принципы разработки качественных проектов.
- Вы узнаете как организовать удобную структуру проекта.
- Вы узнаете как спроектировать качественную и модульную архитектуру проекта.
- Вы узнаете что такое Clean Architecture, Domain-Driven Design, View-Driven и State-Driven UI.
- Вы узнаете как написать для UIToolkit красивую тему стилей.
- Вы узнаете как расширить редактор Unity, чтобы повысить удобство и продуктивность рабочего процесса.
- Вы получите доступ к проекту и сможете использовать его в качестве шаблона для ваших проектов.
- Вы получите доступ ко всем дополнительным пакетам, которые вы сможете использовать в ваших проектах.

# Справка
========================================
Проект состоит из модулей:
- Project
- Project.UI
- Project.UI.Internal
- Project.App
- Project.Entities
- Project.Entities.Actors
- Project.Entities.Things
- Project.Entities.Worlds
- Project.Infrastructure

А так же проект имеет несколько зависимостей:
- Clean Architecture Game Framework - Архитектурный фреймворк.
- Addressables Extensions           - Библиотека для работы с ассетами.
- Addressables Source Generator     - Инструмент для генерации всех адресов и меток ассетов.
- Colorful Project Window           - Расширения для окна проекта, делающее рабочий процесс более удобным.
- UIToolkit Theme Style Sheet       - Тема стилей для UI Toolkit.

# Ссылки
========================================
- https://github.com/Denis535/UnityShooterExample
- https://openupm.com/packages/?sort=downloads&q=denis535
- https://assetstore.unity.com/packages/tools/gui/uitoolkit-theme-style-sheet-273463


########## ########## ########## ########## ########## ########## ########## ########## ########## ##########
########## ########## ########## ########## ########## ########## ########## ########## ########## ##########
########## ########## ########## ########## ########## ########## ########## ########## ########## ##########

# 0. Превью
========================================

# 1. Введение
========================================
Всем привет!
В этом видео курсе мы подробно рассмотрим пример шутера, в котором я постарался воплотить все свои лучшие идеи и наработки, накопленные за годы работы в игровой индустрии.
Заметьте, я не буду углубляться в детали устройства движка Unity, так как предполагается, что у вас уже есть базовые знания об этом движке.
Вместо этого, я сосредоточусь на главных принципах разработки качественных проектов.
Эти знания будут вам полезны не только для работы с Unity, но и в других сферах программирования.
Также мы рассмотрим некоторые дополнительные пакеты и инструменты, которые значительно упростили разработку основного проекта.

После просмотра данного курса:
- Вы узнаете все детали устройства нашего пример шутера и дополнительных пакетов.
- Вы узнаете самые важные принципы разработки качественных проектов.
- Вы узнаете как организовать удобную структуру проекта.
- Вы узнаете как спроектировать качественную и модульную архитектуру проекта.
- Вы узнаете что такое Clean Architecture, Domain-Driven Design, View-Driven и State-Driven UI.
- Вы узнаете как написать для UIToolkit красивую тему стилей.
- Вы узнаете как расширить редактор Unity, чтобы повысить удобство и продуктивность рабочего процесса.
- Вы получите доступ к проекту и сможете использовать его в качестве шаблона для ваших проектов.
- Вы получите доступ ко всем дополнительным пакетам, которые помогут вам в разработке ваших проектов.

Приятного просмотра!

# 2. Организация качественной архитектуры проекта
========================================
В этом видео мы подробно рассмотрим принципы организации качественной архитектуры проекта.

**Проблема**
Любой достаточно сложный проект имеет большое количество разных модулей, неймспейсов и типов.
В идеале каждая единица проекта должна иметь одно конкретное назначение и минимальное количество зависимостей.
Но в реальности, проект легко превращается в кучу файлов с непонятными назначениями и запутанными зависимостями.
Поэтому нужны парадигмы, которые будут однозначно и понятно определять архитектуру проектов.

**Решение**
Для организации архитектуры проекта можно использовать следующие идеи:
- Первая идея — это декомпозиция.
  Это значит, что сложный проект дробиться на более простые модули, неймспейсы и типы.
  Вы должны декомпозировать проект так, чтобы другие разработчики могли быстро разобраться в вашем проекте.
  Дробление должно проводится так, чтобы каждая модуль, неймспейс или тип имел единственную и понятную ответственность.
  Или так, чтобы отделить все самое важное от всего менее важного.
  Так же желательно, чтобы каждый модуль имел минимальное количество зависимостей.

- Вторая идея — это чистая архитектура.
  Это значит, что проект декомпозируется на следующие модули: точка входа, презентация, приложение и предметная области.
  А все зависимости направлены от первых модулей к последним.
  Так же проект может иметь дополнительные модули инфраструктуры.
  В итоге получается проект состоящий из простых модулей, каждый из которых имеет понятную ответственность и минимальные зависимости.
  А разработка такого проекта становится более удобной.

- Третья идея — это domain-driven дизайн.
  Это значит, что модули предметной области являются самыми главными в проекте.
  Предметная область — это множество сущностей, выполняющих бизнес задачи приложения и отражающие объекты реального или придуманного мира.
  То есть модули предметной области выполняют все бизнес задачи.
  А все остальные модули являются техническими и лишь обеспечивают функционирование модулей предметной области.
  В итоге получается, что предметная область полностью выделена в специальные модули.
  А разработка такой предметной области становится более простой и удобной.

- Четвертая идея - инверсия зависимостей.
  Это значит, что модули предметной области не должны иметь зависимостей на технические модули.
  Вместо этого, технические модули могут иметь зависимости на модули предметной области.
  Таким образом получается, что предметная область может неявно использовать инфраструктуру через свои собственные абстракции.
  Но только при условии, что инфраструктура имеет реализации для этих абстракций.
  В итоге получается, что предметная область полностью очищена от всех технических деталей.
  А разработка такой предметной области становится полностью независимой от всего прочего.

- Пятая идея — это view-driven UI.
  Это значит, что пользовательский интерфейс представляется деревом визуальных элементов.
  Такая архитектура хорошо подходит для обычных оконных приложений.
  В таких приложениях, все элементы пользовательского интерфейса расположены в главном окне.
  А если что-то не уместилось в главном окне, то пользователь может открывать дополнительные окна.
  Но для некоторых случаев этого недостаточно...

- Шестая идея — это state-driven UI.
  Это значит, что пользовательский интерфейс представляется деревом состояний и деревом визуальных элементов.
  Причем дерево состояний полностью независимо от дерева визуальных элементов.
  Вы можем добавлять новые ноды в дерево состояний или удалять старые ноды и даже целые ветки.
  А каждое состояние, в свою очередь, может иметь (или не иметь) свое визуальное представление.
  Таким образом вы можем делать сложные пользовательские интерфейсы, состоящие из разных состояний и их комбинаций.
  А главное вы можете элегантно переключать пользовательский интерфейс между разными состояниями.
  Такая архитектура хорошо подходит для полноэкранных приложений, таких как мобильные приложения или игры.
  В таких приложениях, в один момент времени, пользовательский интерфейс может находиться лишь в одном или нескольких состояниях.
  А пользователь может перемещаться по пользовательскому интерфейсу, переключая эти состояния одни на другие.

**Вывод**
Благодаря этим идеям, проекты становятся достаточно логичными и структурированными.
А их внутренние устройства получаются очень схожими даже между разными проектами.
В итоге команда разработчиков может сосредоточить свое внимание на решении бизнес задач и меньше думать о технических проблемах.
Что в свою очередь повышает эффективность разработки и поддержки проекта.

# 3. Организация удобной структуры проекта
========================================
В этом видео мы подробно рассмотрим принципы организации удобной структуры проекта.

**Проблема**
При разработке достаточно сложных проектов, количество файлов и папок быстро вырастает до огромных количеств.
Без продуманной структуры, ориентироваться в таких проектах становится очень тяжело.
Поэтому нужно организовывать проекты таким образом, чтобы с ними было удобнее работать.

**Решение**
Для лучшей организации проекта можно использовать следующие идеи:
- Первая идея — разделяйте первостепенное и второстепенного.
  Это значит, что все самое важное всегда должно быть у вас под рукой, а все второстепенное не должно мешать вам.
  Давайте посмотрим на наш проект.
  Все основные модули я храню в папках Project и Project.Content.
  А дополнительный модуль инфраструктуры я спрятал в пакете Project.Infrastructure.
  Так же обратите внимание, что в папке Plugins я спрятал библиотеки, которые не имеют прямого отношения к нашему проекту.

- Вторая идея — разделяйте проект и контент.
  Это значит, что проект должен содержать только системные файлы, а все исходники и ассеты должны содержаться отдельно.
  Давайте посмотрим на наш проект.
  Папка Project содержит только файлы *.asmdef и некоторые другие файлы.
  А все исходники и ассеты содержаться в Project.Content.
  Заметьте, что эти исходники и ассеты не перемешаны друг с другом, а содержатся в специальных папках.
  Эти папки привязаны к соответствующим модулям или адресуемым группам (addressables groups).
  А названия этих папок отражают названия соответствующих модулей или групп.
  При необходимости, вы даже можем привязать несколько разных папок к одному модулю или группе.

- Третья идея — избегайте больших вложенностей папок в проекте.
  Большие иерархии папок усложняют работу с проектом так как всегда нужно тратить некоторое время, чтобы найти нужные файлы в глубинах проекта.
  Но большое количество файлов и папок в одной директории точно так же усложняют работу с проектом.
  К счастью, можно кастомизировать окно проекта и подсветить специальные элементы в соответствующие им цвета, и таким образом сильно облегчить работу с проектом.
  Давайте посмотрим на наш проект.
  Я подсвечиваю пакеты в синие цвета, модули в красные, ассеты в желтые, а исходники в зеленные цвета.
  Благодаря такому подходу, я практически мгновенно нахожу все необходимые мне элементы в проекте.
  В теории, можно было бы сделать еще лучше и полностью избавиться от вложенных папок, но к сожалению Unity предоставляет очень слабые возможности по кастомизации окна проекта.

**Вывод**
Благодаря этим идеям, большие проекты получаются более удобными.
А команде разработчиков гораздо проще работать с такими проектами. 
Что в свою очередь так же повышает эффективность разработки и поддержки проекта.

# 4. Модуль Project
========================================
В этом видео мы рассмотрим модуль Project.
Это самый главный модуль, который отвечает за запуск нашего приложения, обработку событий, разрешение зависимостей, а так же содержит некоторые инструменты.
Так же этот модуль содержит все сцены, кроме уровней.

**Исходники**
Теперь давайте рассмотрим исходный код:
- Launcher — это скрипт, который просто загружает из бандлов главную сцену Startup.
- Program — это главный класс, который отвечает за инициализацию, обработку событий и разрешение зависимостей.
- DebugScreen — это отладочный экран, который показывает некоторую полезную информацию.
- ProjectToolbar — это набор инструментов, которые вы можете использовать на панели инструментов.
- ProjectWindow — это продвинутое окно проекта, которое подсвечивает специальные файлы и папки в соответствующие им цвета.
  Мы поговорим об этом подробнее в следующих видео.

**Ассеты**
Теперь давайте рассмотрим ассеты:
- Launcher — это дефолтная сцена, которая загружается самой первой, и в свою очередь, загружает главную сцену Startup.
  Обратите внимание, что движок требует, чтобы дефолтная сцена была встроена в сам билд проекта.
  Поэтому вы должны сначала загружать дефолтную сцену Launcher. 
  А потом загружать из бандлов любые другие сцены.
- Startup — это главная сцена, которая загружена на протяжении всего жизненного цикла приложения.
  Она содержит все singleton объекты приложения.
- MainScene — это сцена, которая загружается вместе с главным меню.
  В нашем случае эта сцена абсолютно пустая.
- GameScene — это сцена, которая загружается вместе с игрой.
  В нашем случае эта сцена так же абсолютно пустая.

# 5. Модуль Project.UI
========================================
В этом видео мы рассмотрим модуль Project.UI.
Данный модуль отвечает за пользовательский интерфейс нашего приложения.
Обратите внимание, что наш пользовательский интерфейс состоит из нескольких неймспейсов:
- Project.UI — содержит тему, экран и роутер.
- Project.UI.MainScreen — содержит все виджеты главного экрана.
- Project.UI.GameScreen — содержит все виджеты игрового экрана.
- Project.UI.Common — содержит все общие виджеты.
А так же, обратите внимание, что модуль разделен на две части:
- Project.UI — содержит тему, экран, роутер и виджеты.
- Project.UI.Internal — содержит вьюшки.
Благодаря такому подходу, вьюшки не имеют зависимостей на другие модули.

**Исходники**
Теперь давайте рассмотрим исходный код:
- UITheme — это аудио тема приложения.
  Она содержит плейлист и проигрывает его.
- MainPlayList и GamePlayList — это плейлисты темы.
  Они содержат списки аудио клипов и проигрывают их.
  А в некоторых случаях они добавляют некоторые аудио эффекты.
- UIScreen — это графический пользовательский интерфейс приложения.
  Он содержит дерево виджетов и показывает на экран все видимые виджеты.
- RootWidget — это корневой виджет.
  Он показывает на экран другие виджеты.
  А так же обрабатывает некоторые события.
- UIRouter — это менеджер приложения.
  Он может загружать, перезагружать, выгружать игру, а так же выходить из приложения.
- MainWidget — это виджет главного экран.
  Он показывает главное меню.
  Или если произошла ошибка, то он показывает сообщение об этой ошибки.
- MenuWidget — это виджет главного меню.
  Главное меню состоит из нескольких подменю, которые добавляются и удаляются, когда пользователь взаимодействует с ними.
- GameWidget — это виджет игрового экрана.
  Он показывает информацию об игре.
  Или если игрок нажал на паузу, то он показывает игровое меню.
- MenuWidget — это виджет игрового меню.
- TotalsWidget — это виджет итогов игры.
  Итоги могут быть трех типов: уровень проигран, уровень пройден и игра пройдена.
- LoadingWidget — это виджет экрана загрузки и перезагрузки игры.
- UnloadingWidget — это виджет экрана выгрузки игры.
- SettingsWidget — это виджет настроек.
  Точнее это лишь контейнер для других виджетов, содержащих сами настройки.
- ProfileSettingsWidget — это виджет настроек профиля.
- VideoSettingsWidget — это виджет настроек графики.
- AudioSettingsWidget — это виджет настроек звука.
- DialogWidget, InfoDialogWidget, WarningDialogWidget, ErrorDialogWidget — это виджеты диалогов разных стилей.
- Вьюшки — это просто визуальные элементы.

**Ассеты**
Теперь давайте рассмотрим ассеты:
- PanelSettings — это настройки UItoolkit.
- ThemeStyleSheet — это таблица стилей темы.
- Так же модуль содержит и другие ресурсы необходимые для стилей.

# 6. Модуль Project.App
========================================
В этом видео мы рассмотрим модуль Project.App.
Данный модуль отвечает за все необходимое для работы нашего приложения.

**Исходники**
Теперь давайте рассмотрим исходный код:
- Application — это сущность приложения.
  Она содержит другие объекты, сущности и сервисы.
- Storage, ProfileSettings, VideoSettings, AudioSettings — это объекты, содержащие разные значения.
  Эти значения могут быть загружены из хранилища или получены из аргументов окружения.

# 7. Модуль Project.Entities
========================================
В этом видео мы рассмотрим модуль Project.Entities.
Данный модуль отвечает за нашу предметную область, то есть содержит все сущности нашей игры.
Обратите внимание, что наша предметная область состоит из нескольких модулей:
- Project.Entities — содержит базовые сущности, такие как игра, игрок, камера и игровой дисплей (HUD).
- Project.Entities.Actors — содержит все сущности, которые могут действовать самостоятельно или под управлением игрока.
- Project.Entities.Things — содержит все вещи, которыми актеры могут владеть.
- Project.Entities.Worlds — содержит все объекты окружения и сами миры.
Так же, при необходимости, вы можете добавить модуль для транспортных средств.

**Исходники**
Теперь давайте рассмотрим исходный код:
- GameBase и Game — это сущность игры.
  Она содержит информацию, состояния, правила и игроков.
- PlayerBase и Player — это сущность реального игрока.
  Она содержит информацию, состояния, достижения и другие сущности принадлежащие игроку.
- Camera — это сущность камеры.
  Она всегда следует за персонажем и снимает его от третьего лица.
  Заметьте, что камера работает в разных режимах для живого и мертвого персонажа.
- CharacterBase — это сущность персонажа.
  Она может перемещаться, использовать оружие, атаковать и быть атакованной.
  Заметьте, что вся низкоуровневая логика находится в фасаде.
- PlayableCharacterBase — это сущность играбельного персонажа.
- NonPlayableCharacterBase — это сущность неиграбельного персонажа.
- PlayerCharacter — это сущность персонажа игрока.
  Она является аватаром игрока и управляется игроком.
- EnemyCharacter — это сущность вражеского персонажа.
  Она управляется искусственным интеллектом и пытается убить персонажа игрока.
- WeaponBase — это сущность оружия.
- Gun — это сущность пистолета.
  Она может стрелять и наносить урон другим сущностям.
- Bullet — это сущность пули.
  При столкновении с разными сущностями, она наносит им урон.
- World — это сущность мира.
  Она может иметь некоторую логику, делающую мир более интерактивным. 

**Ассеты**
Теперь давайте рассмотрим ассеты:
- Camera — это префаб камеры.
- PlayerCharacter — это префаб игрового персонажа.
- EnemyCharacter — это префаб вражеского персонажа.

# 8. Модуль Project.Infrastructure
========================================
В этом видео мы рассмотрим модуль Project.Infrastructure.
Данный модуль содержит все общее и весь мусор, который я хотел скрыть от моих глаз.
Благодаря такому подходу, другие модули получились гораздо проще и чище, что делает работу с проектом более простой и приятной.

**Исходники**
Теперь давайте рассмотрим исходный код.
- Lock - это объект, запрещающий многочисленный доступ в заданную область кода.
- Utils - это набор разных полезных методов.
- GameObjectExtensions - это набор расширений для GameObject.
- VisualElementExtensions - это набор расширений для VisualElement.
- VisualElementFactory - это набор методы для создания разных визуальных элементов.
- Point - это объект, определяющий какую-либо позицию в мире.
- Socket - это объект, способный содержать какой-либо другой объект.
- R - это список всех адресов ассетов.
- L - это список всех меток ассетов.
- IDamageable - это интерфейс для нанесения урона разным сущностям.
- ActorBase - это базовая сущность актера.
  Она действует под управлением игрока или искусственного интеллекта.
- MovableBody - это компонент реализующий простейшую физику тела персонажа.
- ThingBase - это базовая сущность вещи.
  Которой могут владеть другие актеры.

# 9. Пакет Clean Architecture Game Framework
========================================
В этом видео мы рассмотрим пакет Clean Architecture Game Framework.
Данный пакет предоставляет вам архитектурный фреймворк и помогает вам организовать качественную архитектуру вашего проекта.
Заметьте, что для большего удобства, пакет состоит из трех модулей: Основной, Сore и Internal.

## 9.1 Модуль Core
Давайте рассмотрим модуль Core.
Данный модуль содержит фреймворк определяющий архитектуру вашего проекта.
Фреймворк состоит из нескольких частей: Framework, Framework.UI, Framework.App, Framework.Entities.

**Исходники**
Теперь давайте рассмотрим исходный код:
- ProgramBase - это главный класс приложения.
  Он содержит точку входа, обрабатывает все события и разрешает зависимости.
  Так же он содержит метод OnInspectorGUI, который может вывести в инспекторе некоторую информацию.
- UIThemeBase - это аудио тема приложения.
  Она содержит плейлист и проигрывает его.
  Заметьте, что я использовал паттерн состояние, что значит, что тема может находиться в разных состояниях.
  Это позволяет вам создавать тему из разных состояний и легко переключаться между ними.
- UIPlayListBase - это состояние темы.
  Оно содержит список аудио клипов и проигрывает их.
  А в некоторых случаях оно добавляет некоторые аудио эффекты.
- UIScreenBase - это графический пользовательский интерфейс приложения.
  Он содержит дерево состояний и показывает на экран все видимые состояния.
  Заметьте, что я использовал state-driven архитектуру, что значит, что экран состоит из дерева состояний и дерева визуальных элементов.
  Это позволяет вам создавать экран из множества разных состояний, комбинировать их и легко переключаться между ними.
  Я рассказывал об этой архитектуре в прошлых видео.
- UIWidgetBase - это состояние экрана.
  Оно может иметь (или не иметь) визуальное представление.
  А так же оно обрабатывает все события и содержит всю высокоуровневую логику.
- UIViewBase - это визуальное представление виджета.
  Это просто визуальный элемент, содержащий свои элементы.
  В некоторых случаях вьюшка может быть контейнером для других вьюшек.
- UIRouterBase - это менеджер приложения.
  Он может загружать, перезагружать, выгружать сцены, а так же выходить из приложения.
- ApplicationBase - это сущность приложения.
  Она содержит другие объекты, сущности и сервисы.
- StorageBase - это объект, содержащий разные значения.
  Эти значения могут быть загружены из хранилища или получены из аргументов окружения.
- GameBase - это сущность игры.
  Она содержит информацию, состояния, правила и игроков.
- PlayerBase - это сущность реального игрока.
  Она содержит информацию, состояния, достижения и другие сущности принадлежащие игроку.
- EntityBase - это высокоуровневая единица предметной области.
  То есть это все, что может существовать в реальном или виртуальном мире.
  Например: актер, камера, вещь, транспортное средство, объект окружения и любые другие объекты.
  И сам мир это тоже сущность.

## 9.2 Основной модуль
Давайте рассмотрим основной модуль.
Данный модуль содержит некоторые дополнения к модулю Core.

**Исходники**
Теперь давайте рассмотрим исходный код:
- IDependencyContainer - это локатор служб, предоставляющий вам необходимые зависимости.
- UIRootWidgetBase - это корневой виджет.
  Он добавляет другие виджеты в дерево визуальных элементов.
  А так же он управляет фокусом и обрабатывает события навигации.

## 9.3 Модуль Internal
Давайте рассмотрим модуль Internal.
Данный модуль содержит разные низкоуровневые утилиты и хелперы.

**Исходники**
Теперь давайте рассмотрим исходный код:
- CSharp - это набор методов для конвейерной обработки значений и списков.
- Array2 - это набор расширений для массива.
- Enum2 - это набор расширений для enum.
- TypeExtensions - это набор расширений для типа.
- StringExtensions - это набор расширений для строки.
- Assert - это набор утверждений с удобным текучим интерфейсом.
- Option - это Nullable c поддержкой ссылочных типов.
- IStateful и StateBase - это классы помогающие реализовать шаблон состояния.
- ITree и NodeBase - это классы помогающие реализовать древовидную структуру.
- CLI - это набор методов для работы с аргументами окружения.
- DisposableBase - это базовый класс для всех disposable классов.
- TaskExtensions - это набор расширений для класса Task.
- VisualElementExtensions - это набор расширений для класса VisualElement.
- VisualTreeAssetExtensions - это набор расширений для класса VisualTreeAsset.
- EventExtensions - это набор расширений для класса EventBase.
