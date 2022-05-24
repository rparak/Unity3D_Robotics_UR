# Universal Robots Unity3D App
Control the [Universal Robots](https://www.universal-robots.com/) with Unity3D by using **URScript** and a **TCP/IP** communication.

[<img src="images/screenshot3.jpg" width="60%">](https://youtu.be/4BE_jgSYYJU "UR Digital Twin showcase")

You can find a documentation of more commands URScript commands in the official [Manual](https://www.universal-robots.com/download/manuals-e-series/script/script-manual-e-series-sw-511/).

<br><br>

# Requirements

| Software / Package           | Description / Link                                                                                  |
| -------------------------- | ------------------------------------------------------------------------------------- |
| OS                         | *Linux* or *Windows*
| Blender 2.9.xx             | https://www.blender.org/download/                                                     |
| Unity3D 2020.3.xx          | https://unity3d.com/get-unity/download/archive                                        |
| Unity HDRI Pack            | https://assetstore.unity.com/packages/2d/textures-materials/sky/unity-hdri-pack-72511 |
| Docker                     | https://docs.docker.com/get-docker/
| UR Simulator               | https://github.com/vushu/DockURSim                                            |

<br><br>

# Features
## Virtual UR Twin
Use the game engine Unity3D to directly move your UR robot.

## Game Controller Support
Move the robot with a PS4 game controller

## Waypoints
Create a simple movement set in an intuitive virtual environment without needing any skills in robotic. 

<img src="images/screenshot1.jpg" width="500"/>

<br><br>

# Quick Start

## I) UR Simulator
1. **Docker:** Install and run [Docker Engine](https://docs.docker.com/get-docker/)
2. **URSim**
    ```
   # Create volume
   docker volume create dockursim
   
   # Run container
    docker run -d \
    --name="dockursim" \
    -e ROBOT_MODEL=UR3 \
    -p 8080:8080 \
    -p 29999:29999 \
    -p 30001-30004:30001-30004 \
    -v /path/to/your/local/ursim/programs:/ursim/programs \
    -v dockursim:/ursim \
    --privileged \
    --cpus=1 \
    arranhs/dockursim:latest
    ```
3. Open http://localhost:8080

<br>

## II) Unity3D

### Connect to UR Sim
1. Make sure [URSim](https://github.com/vushu/DockURSim) is running
2. Connect in Unity to *127.0.0.1*

### Connect to real robot
1. Connect you PC by the ethernet with your robot
2. Configurate your network, e.g.:
    ```
    # Polyscope
      IP: 192.168.0.102
      Subnet: 255.255.255.0
      Gateway: 192.168.0.1
   
    # PC
      IP: 192.168.0.101
      Subnet: 255.255.255.0
      Gateway: 192.168.0.1
   
   # Unity
      IP: 192.168.0.102
       ```

<img src="images/screenshot2.jpg" width="500"/>

# Sources
* [Realtime Interface documentation (deprecated)](https://s3-eu-west-1.amazonaws.com/ur-support-site/16496/ClientInterfaces_Realtime.pdf)
* [RTDE documentation](https://s3-eu-west-1.amazonaws.com/ur-support-site/22229/Real_Time_Data_Exchange_(RTDE)_Guide.pdf)
* [UR Script documentation](https://s3-eu-west-1.amazonaws.com/ur-support-site/115824/scriptManual_SW5.11.pdf)

## License
[MIT](https://choosealicense.com/licenses/mit/)