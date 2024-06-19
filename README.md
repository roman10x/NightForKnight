# NightForKnight
# Simple Game Core Project

This project features a simple game core with a custom Dependency Injection (DI) container and a state machine.

## Features

### State Machine

The state machine serves as the main controller for application states. Each state can be extended with tasks, making this approach beneficial for projects with a large number of controllers and mediators. Note that the `MainMenuState` is not included since there is no main menu. For step-based games, a additional game state machine can be added that operating in GameLoopState.

### Custom DI Container

For service delivery, this project uses a custom DI container - `AllServices`.

## Getting Started

Use the `Initial` scene to start the game.