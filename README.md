# RPG Combat Prototype
A WIP prototype / learning space for me to explore the mechanics of an RPG combat system.

### Design Philosophies
At present there is no concrete design for this prototype (it exists so I can learn how to craft a production quality combat system); consequently, parts of it have been reinvented multiple times, but at it's core it adheres to the following:
- __Turn-based__: one actor acts at a time,
- __Grid-based:__ actors move and act on a grid,
- __Team-based:__ each player controls multiple actos,
- __Multiple actions:__ actors spend points from a limited resource pool to cast actions, potentially casting multiple per turn,
- __Action variety:__ overall the game contains a large number of unique actions (large enough so I can figure out the bounds of the action system).

### Prototype Anti-patterns:
Many aspects of the codebase exist to ease the prototyping process:
- Singletons for ease of access instead of dependency injection
- Linq for readability and speed of implementation despite the [cost on performance](https://www.jacksondunstan.com/articles/4819)

### Things that aren't mine:
- [Odin Inspector / Serializer](https://odininspector.com/): Used to allow for serialization of generics within the editor (and the formating of fields on components is pretty nice too).
- [Fluent Behaviour Tree](https://github.com/ashleydavis/Fluent-Behaviour-Tree/commits?author=ashleydavis): SimpleBehaviour tree is adapted from fluent behaviour trees to allow for inheritence of my interfaces and to pass around a blackboard object at each tick.
- [Singleton Generics](https://gist.github.com/mstevenson/4325117): used to simplify the repeated implementation of the singleton pattern (there are many versions of this online, the one linked is just an example).
- Extension Methods: lots of the extension methods used are sourced from the internet.
- Art assets: most are sourced from [opengameart.org](https://opengameart.org/)

## Action System Overview:
The ActionManager is responsible for assembling actions, which is a multiple step process involving: selecting an actor, action, and target.

### Battle Action Anatomy:
During gameplay, a battle action needs two things in order to work: an actor, and a target cell. 
- `Actor`: the pawn that this action originates from
- `TargetCell`: the cell this action targets.

#### Restrictions
Battle actions have restrictions on which actor or cell is suitable for use, based upon the current state of the game:
- `actorRestrictions`: conditions the actor must satisfy in order to use the action, e.g. mana cost, grid position.
- `targetRestrictions`: the conditions that a cell must satisfy in order to be considered targetable by the action, e.g. within a certain range, or on a certain rank of the grid.

__Definition:__ Actor and targeting restrictions are each defined as a collection, where all conditions must be satisfied for the actor or cell to be usable.

#### Area of Effect
An action can affect more than one cell. 
- `areaOfEffect`: determines which cells, originating from the targeted cell, will be affected by the action, e.g. all cells within a certain range, or all cells in the same rank.
- `areaOfEffectRestrictions`: restrictions can be applied to further limit the collection of cells in `areaOfEffect`

#### Behaviour
Battle actions perform any combination of behaviours on the caster and / or the affected cells.
- `selfActions`: the collection of things the battle action will do to the caster, e.g. remove mana, or health, push the caster.
- `targetedActions`: the collection of things the battle action will do to each of the affected cells, e.g. deal damage, or apply a status.

__Definition:__ Self and Targeted actions are each defined as a behaviour tree to allow for complex branching behaviours, e.g. the Bull Rush battle action pushes the caster forwards, and if there is an enemy in the opposing cell of the enemy formation then it deals damage and pushes the enemy backwards.
