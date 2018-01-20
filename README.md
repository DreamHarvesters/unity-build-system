A modular build system for Unity.

Motivation: Having a build library which will allow pre-build and post-build modifications in the project like moving some assets to different folders, etc.

Project was first started for a project which has the same codebase but different assets and builds. All the assets wasstaying in the same project and irrelavant files needed to be removed from Resources and StreamedAssets folders. We first started with project specific build scripts. Then decided having a more scalable and flexible build system which canbe used in any kind of projects.

The system hasn' t been used in any projects. We aim using this system in both editor builds and cloud builds(if possible)
