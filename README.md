# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.0.8] - 2024-09-27
### Add
- Countdown (3-2-1-GO!) before start of the race
- Cutscene with overview of map and car from different angles before start of race

## [0.0.7] - 2024-09-27
### Changed
- Improved car crush physics (now falls more realistically)

### Added
- UI scaling depending on screen resolution
- Time counting
- Best time updating and saving to PlayerPrefs
- Endgame screen
- Restart button functionality
- Quit button functionality

## [0.0.6] - 2024-09-27
### Added
- Idle animation for obstacles (orange glowing)
- Hit animation for obstacles (red blinking)
- Explosion effect when car hits obstacles or walls

## [0.0.5] - 2024-09-26
### Added
- Obstacles spawning on Level Blocks with some probability
- Collider to obstacles prefabs
- "Obstacle" tag to obstacles prefabs

### Fixed
- Obstacles lightning and shadows
- Walls lightning and shadows

## [0.0.4] - 2024-09-25
### Added
- Missing walls on LevelBlocks
- "Wall" tags to walls

## [0.0.3] - 2024-09-25
### Changed
- Reduced randomness of floating

### Fixed
- Fixed glitching issue caused by DriveCar function in HoverMotor.cs

### Removed
- DriveCar function from HoverMotor as redundant (caused glitches) from HoverMotor.cs

## [0.0.2] - 2024-09-25
### Added
- Chunks are deleted after OnChunkExit is triggered

## [0.0.1] - 2024-09-25
### Changed
- Using Rigidbody instead of NiceCar in HoverMotor.cs

### Fixed
- Fixed problem with game crashing after tree collision. Removed Utils.ForceCrash(ForcedCrashCategory.Abort) from CarTag.cs and TriggerExit.cs;
- Fixed NullReferenceException error caused by carMain in HoverMotor.cs by getting Rigidbody component from object
