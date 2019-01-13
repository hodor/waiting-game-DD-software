using System.Collections;
using System.Collections.Generic;
using AR_Project.MainGame;
using AR_Project.MainGame.Prize;
using UnityEngine;

namespace AR_Project.MainGame.GameObjects
{
    public class FinishLine : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.name.Contains("Prize"))
                col.gameObject.GetComponent<PrizeGO>().isFinished = true;
        }
    }

}
