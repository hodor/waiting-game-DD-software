using System.Collections;
using System.Collections.Generic;
using AR_Project.MainGame;
using UnityEngine;

namespace AR_Project.MainGame
{
    public class FinishLine : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.name.Contains("Prize"))
                col.gameObject.GetComponent<Prize>().isFinished = true;
        }
    }

}
