# Angry Animals

An Angry Birds-style game developed in **Godot** using **C#**, following a Udemy course as a guided learning project.

The project is loosely inspired by Angry Birds, with the player controlling a small parrot that must be flung into cups to complete each level.

## Features

- Physics-based parrot movement
- Player-controlled parrot launching
- Multiple levels with increasing numbers of cups
- Collision detection using physics bodies and collision shapes
- Cups disappear when the parrot comes to a stop inside them
- Level completion when all cups have been landed in
- Parrot respawns at the starting point after each attempt
- Persistent high scores for each level
- 2D game development using Godot
- C# scripting

## Technologies

- **Godot Engine**
- **C# / .NET**
- **Git / GitHub**

## Project Structure

```text
.
├── Assets/          # Game assets
├── Classes/         # Data classes
├── Globals/         # Global Singeltons
├── Resources/       # Godot resources
├── Scenes/          # Godot scenes
├── project.godot    # Godot project configuration
└── README.md
```

## Getting Started

### Prerequisites

- [Godot Engine](https://godotengine.org/) with C#/.NET support
- .NET SDK compatible with the version of Godot being used
- Git, if cloning the repository

### Running the Project

1. Clone the repository:

   ```bash
   git clone https://github.com/apaquette/AngryAnimals.git
   ```

2. Open the project in the Godot editor.

3. Allow Godot to import and build the C# project if prompted.

4. Run the project using the **Play** button or the appropriate Godot run command.

## Controls

| Action | Input |
|---|---|
| Launch Parrot | `Left Mouse Button` |
| Quit | `Esc` |

## Learning Context

This project was created while following the **Learn 2D Game Development: Godot 4 & C# from scratch** course on Udemy. It is primarily intended as a learning exercise for becoming familiar with:

- Godot's scene and node architecture
- C# scripting within Godot
- 2D physics
- Static bodies
- Collision polygons
- Collision shapes
- Interactions between physics bodies and collision shapes
- Player input
- Game state management
- Level progression
- Persistent high scores
- Structuring a small game project

## Credits

This project is based on the concepts and implementation taught in the **Learn 2D Game Development: Godot 4 & C# from scratch** Udemy course by Richard Allbert. It is an educational project and is not intended to be a commercial recreation of the original Angry Birds game.

## License

This project is provided for educational and personal learning purposes.
