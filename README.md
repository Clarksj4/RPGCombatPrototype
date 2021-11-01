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

## Action System Overview:
The ActionManager is responsible for assembling actions, which is a multiple step process involving: selecting an actor, action, and target.
