using System.Collections;
using System.Collections.Generic;
using AR_Project.MainGame.UI;
using AR_Project.Savers;
using UnityEngine;

namespace AR_Project.MainGame.Prize
{
    public class PrizeGO : MonoBehaviour {
    

        public int timer;
        public Vector3 finalDestination;
        public bool isFinished = false;
        Animator animator;
        bool stopSecondButton = false;

        public void StartMoving()
        {
            animator = GetComponent<Animator>();
            var isImaginarium = PlayerPrefsSaver.instance.isImaginarium;

            if (isImaginarium)
            {
                FinishedRun();
            } else {

                if (timer >= 10)
                    animator.SetBool("isRunning", false);
                else
                    animator.SetBool("isRunning", true);


                if (timer == 0)
                    StartCoroutine(MoveToPosition(this.gameObject.transform, finalDestination, 0.5f));
                else
                    StartCoroutine(MoveToPosition(this.gameObject.transform, finalDestination, timer));
            }


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
            animator.SetBool("stoped", true);
            FinishedRun();
        }

        void FinishedRun ()
        {
            PrizeButtons.instance.FinishedExperiment();
        }

    }

}
