# Unity3D Industrial Robotics: Universal Robots UR3

## Requirements:

**Software:**
```bash
Universal Robots Polyscope, Blender, Unity3D 2020.1.8f1, Visual Studio 2017/2019
```

**Supported on the following operating systems:**
```bash
Universal Windows Platform, Android
```

| Software/Package           | Link                                                                                  |
| -------------------------- | ------------------------------------------------------------------------------------- |
| Blender                    | https://www.blender.org/download/                                                     |
| Unity3D                    | https://unity3d.com/get-unity/download/archive                                        |
| Unity HDRI Pack            | https://assetstore.unity.com/packages/2d/textures-materials/sky/unity-hdri-pack-72511 |
| Universal Robots Polyscope | https://www.universal-robots.com/download/                                            |
| Visual Studio              | https://visualstudio.microsoft.com/downloads/                                         |

## Project Description:

The project is focused on a simple demonstration of client-server communication via TCP / IP, which is implemented in Unity3D. The project demonstrates the Digital-Twin of the UR3 robot with some additional functions. The application uses performance optimization using multi-threaded programming.

This solution can be used to control a real robot or to simulate it (using VMware <-> UR Polyscope in Windows), E and CB series. The Unity3D Digital-Twin application was tested on the UR3 robot, both on real hardware and on simulation.

Main functions of the UR3 Digital-Twin model:
- Camera Control
- Connect/Disconnect -> Real HW or Simulation
- Read Data (Cartesian / Joint Position diagnostics)
- Write Data (Speed control of the robot (X,Y,Z and EA{RX, RY, RZ}) using the joystick)

The application can be installed on a mobile phone, tablet or computer, but for communication with the robot it is necessary to be in the same network

The project was realized at Institute of Automation and Computer Science, Brno University of Technology, Faculty of Mechanical Engineering (NETME Centre - Cybernetics and Robotics Division).

**Appendix:**

Example of a simple data processing application:

[UR Robot - Data Processing](https://github.com/rparak/UR_Robot_data_processing/)

<p align="center">
<img src="https://github.com/rparak/Unity3D_Robotics_UR/blob/main/images/ur_1.PNG" width="800" height="500">
</p>

## License
[MIT](https://choosealicense.com/licenses/mit/)
