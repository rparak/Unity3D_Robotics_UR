# Universal Robots UR3 - Unity3D Robotics

## Requirements:

**Software:**
```bash
Universal Robots Polyscope, Unity3D 2019 and later, Visual Studio 2017/2019
```

**Supported on the following operating systems:**
```bash
Universal Windows Platform, Android
```

Unity3D: https://unity3d.com/get-unity/download/archive

Universal Robots: https://www.universal-robots.com/download/

Visual Studio: https://visualstudio.microsoft.com/downloads/

## Project Description:

The project is focused on a simple demonstration of client / server communication via TCP / IP, which is implemented in Unity3D. The project demonstrates the digital-twin of the UR3 robot with some additional functions. The application uses performance optimization using multi-threaded programming.

This solution can be used to control a real robot or to simulate it (using VMware <-> UR Polyscope in Windows), E and CB series. The Unity3D Digital-Twin application was tested on a UR3 robot, on real hardware and on simulation. The results of this example will be published on youtube.

A simple example of a data processing application:

[UR Robot - Data Processing](https://github.com/rparak/UR_Robot_data_processing/edit/main/README.md)

Main functions of the UR3 Digital-Twin model:
- Camera Control
- Connect/Disconnect -> Real HW or Simulation
- Read Data (Cartesian / Joint Position diagnostics)
- Write Data (Speed control of the robot (X,Y,Z and EA{RX, RY, RZ}) using the joystick)

The application can be installed on a mobile phone, tablet or computer, but for communication with the robot it is necessary to be in the same network

The project was realized at Institute of Automation and Computer Science, Brno University of Technology, Faculty of Mechanical Engineering (NETME Centre - Cybernetics and Robotics Division).

<p align="center">
<img src="https://github.com/rparak/Unity3D_Robotics_UR/blob/main/images/ur_1.PNG" width="800" height="500">
</p>

## Project Hierarchy:

**Repositary [/Unity3D_Robotics_UR/Universal_Robots_UR3/Universal_Robots_Unity_App/Assets/]:**
```bash
[ UI + Main Control           ] /Script/UI/
[ Data Processing             ] /Script/UR3/
[ Individual objects (.blend) ] /Object/Blender/
[ Images (UI)                 ] /Image/
[ Animation of the objects    ] /Animation/
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

Youtube: coming soon

## Contact Info:
Roman.Parak@outlook.com

## License
[MIT](https://choosealicense.com/licenses/mit/)
