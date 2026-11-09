# Minimum Input Combo Attack

A Unity combat prototype that maps sword attacks and energy abilities to two mouse buttons. An action menu changes the selected attack so different sequences can be tested without adding more combat buttons.

https://github.com/user-attachments/assets/8dc5aefa-78fd-44c2-8b01-53957730d21c

## Try it

Open the repository in **Unity 6000.0.27f1**. Load [SampleScene](Assets/Scenes/SampleScene.unity) and enter Play Mode.

| Input | Action |
| --- | --- |
| Left mouse button | Sword attack |
| Right mouse button | Energy attack |
| Q | Open the action menu to change the next attack |

## Code tour

- [PlayerCombatDynamic.cs](Assets/Scripts/PlayerCharacter/PlayerCombatDynamic.cs) and [PlayerAnimationHandler.cs](Assets/Scripts/PlayerCharacter/PlayerAnimationHandler.cs) cover combat sequencing and animation.
- [AttackSO.cs](Assets/Scripts/Player/AttackSO.cs) and [EnergySO.cs](Assets/Scripts/Player/EnergySO.cs) define configurable attack and energy data.
- [ActionMenuHandler.cs](Assets/Scripts/Player/Menu/ActionMenuHandler.cs) drives the attack selection UI.
