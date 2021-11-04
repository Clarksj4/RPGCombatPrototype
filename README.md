# RPG Combat Prototype
___Synopsis:__ Teams of pawns do battle on a grid, each team confined to their own 3x3 formation. Pawns act in turn, using abilities to move, deal damage, or apply status effects to pawns on the opposing formation. First to eliminate all pawns on the opposing team wins._

### Prototype
This project is a WIP prototype / learning space for me to explore the mechanics of an RPG combat system; consequently, parts of it have been reinvented multiple times and lots of things don't work. It's not yet in a fun, playable state, but some of the functionality of the action system is beginning to emerge.

#### Anti-patterns:
Many aspects of the codebase exist to ease the prototyping process:
- Singletons for ease of access instead of dependency injection
- Linq for readability and speed of implementation despite the [cost on performance](https://www.jacksondunstan.com/articles/4819)

#### Things that aren't mine:
- [Odin Inspector / Serializer](https://odininspector.com/): Used to allow for serialization of generics within the editor (and the formating of fields on components is pretty nice too).
- [Fluent Behaviour Tree](https://github.com/ashleydavis/Fluent-Behaviour-Tree/commits?author=ashleydavis): SimpleBehaviour tree is adapted from fluent behaviour trees to allow for inheritence of my interfaces and to pass around a blackboard object at each tick.
- [Singleton Generics](https://gist.github.com/mstevenson/4325117): used to simplify the repeated implementation of the singleton pattern (there are many versions of this online, the one linked is just an example).
- Extension Methods: lots of the extension methods used are sourced from the internet.
- Art assets: most are sourced from [opengameart.org](https://opengameart.org/)

## Action System Overview:
The ActionManager is responsible for assembling actions, which is a multiple step process involving: selecting an actor, action, and target.

## Battle Actions:
A battle action is a complex behaviour performed by a pawn during their turn (think of the kinds of things your D&D character could do during their turn). Battle actions require certain conditions to be met in order to use them (think of the casting cost associated with a card from MTG). Some examples include: 
- __Move:__ moves the pawn to another cell within range on the grid.
- __Firebot:__ deals damage to a single enemy in the opposing formation. Costs 1 mana.
- __Bless:__ increases the power of the actor and adjacent allies temporarily. Costs health to cast.

#### Usage
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
Battle actions perform any combination of behaviours on the actor and / or the affected cells.
- `selfActions`: the collection of things the battle action will do to the actor, e.g. remove mana, or health, push the actor.
- `targetedActions`: the collection of things the battle action will do to each of the affected cells, e.g. deal damage, or apply a status.

__Definition:__ Self and Targeted actions are each defined as a behaviour tree to allow for complex branching behaviours, e.g. the Bull Rush battle action pushes the actor forwards, and if there is an enemy in the opposing cell of the enemy formation then it deals damage and pushes the enemy backwards.

#### Serialization:
Battle actions are stored as ScriptableObjects. This allows for serialization of the data associated with each action. Battle actions inherit from `SerializedScriptableObject` a class defined by the Odin Inspector plugin for Unity - this allows for serializing and displaying generic classes and interfaces in the editor.


## Action Nodes:
An action node is a small, reusable piece of functionality that can be executed in a behaviour tree. They are the units of functionality that make up what a battle action does. Each one could be a check that passes data to other nodes in the tree (via the blackboard) or a behaviour that affects a targeted pawn. Examples include:

- `GetStatus`: gets the statuses of the pawn at the targeted cell and adds them to the blackboard. Succeeds if there was a pawn that had a status
- `PushNode`: pushes the pawn at the targeted cell in the given direction. Succeeds if there was a pawn in the targeted cell. 

#### Behaviour Tree:
The order of execution of nodes is determined by the structure of the tree they exist in: each node’s `Do()` action returns whether the action was successfully executed or not. The parent node uses this data to determine how to proceed in execution of the remaining tree nodes.

#### Blackboard:
Nodes in the behaviour tree are not aware of other nodes in the tree (so they can be reassembled in any order). To pass data between nodes in the tree a blackboard is used. This is, at its core, a dictionary for associating names with values. Nodes adding data to the blackboard is independent of nodes retrieving data and so consumers are free to act on the data in any way they see fit.
