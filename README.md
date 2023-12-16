# Unity3D Industrial Robotics: Universal Robots UR3

<p align="center">
<img src="https://github.com/rparak/Unity3D_Robotics_UR/blob/main/images/ur_background.png" width="800" height="500">
</p>

## Requirements:

**Software:**
```bash
Universal Robots Polyscope, Blender, Unity3D 2022.3.2f1, Visual Studio 2017/2019
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

The project was realized at the Institute of Automation and Computer Science, Brno University of Technology, Faculty of Mechanical Engineering (NETME Centre - Cybernetics and Robotics Division).

**Appendix:**

Example of a simple data processing application:

[UR Robot - Data Processing](https://github.com/rparak/UR_Robot_data_processing/)

<p align="center">
<img src="https://github.com/rparak/Unity3D_Robotics_UR/blob/main/images/ur_1.PNG" width="800" height="500">
</p>

## Project Hierarchy:

**Repositary [/Unity3D_Robotics_UR/Universal_Robots_Unity_App/Assets/]:**
```bash
[ UI + Main Control           ] /Script/UI/
[ Data Processing             ] /Script/UR3/
[ Individual objects (.blend) ] /Object/Blender/
[ Images (UI)                 ] /Image/
[ Scene of the Application    ] /Scenes/
```

<p align="center">
<img src="https://github.com/rparak/Unity3D_Robotics_UR/blob/main/images/ur_h.PNG" width="800" height="500">
</p>

## Digital-Twin Application:

<p align="center">
<img src="https://github.com/rparak/Unity3D_Robotics_UR/blob/main/images/ur_dt_1.png" width="800" height="500">
<img src="https://github.com/rparak/Unity3D_Robotics_UR/blob/main/images/ur_dt_2.png" width="800" height="500">
<img src="https://github.com/rparak/Unity3D_Robotics_UR/blob/main/images/ur_dt_3.png" width="800" height="500">
<img src="https://github.com/rparak/Unity3D_Robotics_UR/blob/main/images/ur_dt_4.png" width="800" height="500">
<img src="https://github.com/rparak/Unity3D_Robotics_UR/blob/main/images/ur_rh_1.png" width="800" height="500">
</p>

## Result:

Youtube: https://www.youtube.com/watch?v=kReuJdESdz0&t=182s

## Contact Info:
Roman.Parak@outlook.com

## Citation (BibTex)

```bash
@misc{RomanParak_Unity3D,
  author = {Roman Parak},
  title = {A digital-twins in the field of industrial robotics integrated into the unity3d development platform},
  year = {2020-2021},
  publisher = {GitHub},
  journal = {GitHub repository},
  howpublished = {\url{https://github.com/rparak/Unity3D_Robotics_Overview}}
}
```

## License
[MIT](https://choosealicense.com/licenses/mit/)
