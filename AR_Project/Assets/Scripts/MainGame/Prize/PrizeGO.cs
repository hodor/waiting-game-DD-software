﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AR_Project.MainGame.Prize
{
    public class PrizeGO : MonoBehaviour {
    
        public int timer;
        public Vector3 finalDestination;
        public bool isFinished = false;

        public void StartMoving()
        {
            if (timer == 0)
                StartCoroutine(MoveToPosition(this.gameObject.transform, finalDestination, 0.5f));
            else
                StartCoroutine(MoveToPosition(this.gameObject.transform, finalDestination, timer));
        }

        IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
        {
            var currentPos = transform.position;
            var t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                transform.position = Vector3.Lerp(currentPos, position, t);
                yield return null;
            }
        }

    }

}