# Sloth 2D Game Engine

This project served as my final year project for my computer science course at the University of Hull. I graduated in 2018 with a 2nd class first division degree.

## About This Project

This is a component-based, 2D game engine, which means that game objects (which are logical entities within a game world, like a car, a character, a camera, etc) are represented as generic objects that have 'components' (or properties) applied to them at runtime. For instance, a car object has the components of position, texture, and velocity. This approach makes the engine much more flexible than a system which 'hard-codes' each game object with assigned properties. In this way, not only is the development of a game much simpler, due to the fact that this approach avoids hierarchical explosion, but also makes the engine reusable for multiple games.

This project - while it is a functioning 2D game engine - is not actually intended to be seriously used to make a computer game - there are many better solutions out there developed by teams of people with actual budgets, over many years rather than months.

### The Engine

The engine is comprised of multiple components and systems. Systems process each component each frame, according to the data in the components, and the set rules for each system. Components do not contain any logic - only data for the logic in the systems to process.

### The Example Game

This project was developed in visual studio, and the solution contains two projects. The engine itself, and an example game using the engine as a class library. A game is made up of a collection of scenes, which are a collection of game objects intended to be used together. The example game includes two scenes, and can be used as a template for making other games.

### The Report

As this was a final year project for university, I had to write three reports. I have only included the final report here, because it encompasses the other two. If you would like to know more about this project and the background of it, I would recommend reading it. It also includes a basic instruction manual in the appendices.

## Dependencies

This project relies on two libraries which can be obtained via NuGet in Visual Studio:

* OpenTK
* OpenAL
* The `wrap_oal.dll` and `OpenAL32.dll` files should be placed in the `MyGame` folder

## Known Issues

This project was developed over a short period of time and therefore is not fully featured, and has several problems. Among them are:

* The collision system doesn't work very well when objects are rotated
* The audio system does not allow for more than one sound to be played at one time
* An incorrectly formatted audio file will crash the engine
* The engine is generally not very well optimised

More information on these issues can be found in the evaluation of the final report.
