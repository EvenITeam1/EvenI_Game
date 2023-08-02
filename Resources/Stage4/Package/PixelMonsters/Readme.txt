Thanks for purchasing Pixel Monsters!

Each monster contains:
1) PNG spritesheet
2) Sprite Library Asset that defines a list of animations and a list of frames for each animation
3) Prefab

The full list on animations available:
1) Idle
2) Ready
3) Walk/Run/Jump
4) Attack
5) Death

All monsters use the same unified animation controller. Animation transitions are managed by animation parameters (please refer to the Animation window).

Each monster has the [Monster] component, you can use it to play animation by calling SetState(). Please refer to the AnimationState enum.
Each monster also comes with the [MonsterControls] component that deals with [CharacterController].
You can use the binding of [MonsterControls] and [CharacterController] as an example, or replace it by your own implementation.

In case if you need more info, or if you have any questions, please contact me on Discord using the link https://discord.gg/4ht2AhW