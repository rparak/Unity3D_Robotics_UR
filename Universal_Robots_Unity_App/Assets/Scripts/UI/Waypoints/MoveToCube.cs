using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCube : MonoBehaviour
{
    public bool move;
    public bool moveCobyJoin;
    public bool moveCobyCart;
    public bool showLog;

    public Transform cobot;


    private void Update()
    {
        if (!move) return;

        Vector3 diff = cobot.transform.position - transform.position;

        Vector3 cobotDiff = diff.ToRobotPos();

        if(moveCobyJoin) Robot.CMD.SpeedL(cobotDiff, Vector3.zero, .2f);
        if(moveCobyCart)
        {
            moveCobyCart = false;
            Vector3 calcPos = transform.position.ToRobotPos();
            Pose pose = new Pose(calcPos.x, calcPos.z, calcPos.y, 2.221f, 2.221f, 0);
            Debug.Log(calcPos);
            Robot.CMD.MoveJ(pose);
        }

        Vector3 cobyPos = new Vector3(Data.Current.position.x, Data.Current.position.z, Data.Current.position.y);
        //if(showLog) Debug.Log($"Coby: {cobot.transform.position} Me: {transform.position} Diff: {transform.position - cobot.transform.position} \n" +
        //    $"Cobot| Coby {cobyPos} Me: {transform.position} Diff: {Data.Current.position - cobot.transform.position}"
        //    );


        if (showLog) Debug.Log($"Unity: {cobot.transform.position} Coby: {cobyPos} Fake C/U: {cobyPos.ToUnityPos()} Fake U/C: {cobot.transform.position.ToRobotPos()}");
    }


    
}
