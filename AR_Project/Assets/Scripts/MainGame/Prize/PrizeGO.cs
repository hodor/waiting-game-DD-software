using System.Collections;
using AR_Project.MainGame.UI;
using AR_Project.Savers;
using UnityEngine;

namespace AR_Project.MainGame.Prize
{
    public class PrizeGO : MonoBehaviour
    {
        private Animator animator;
        public Vector3 finalDestination;
        public bool isFinished;
        private bool stopSecondButton = false;


        public int timer;

        public void StartMoving(bool isTutorial)
        {
            animator = GetComponent<Animator>();
            var isImaginarium = PlayerPrefsSaver.instance.isImaginarium;
            if (isImaginarium && !isTutorial)
            {
                FinishedRun();
            }
            else
            {
                animator.SetBool("isRunning", timer < 10);
                StartCoroutine(timer == 0
                    ? MoveToPosition(gameObject.transform, finalDestination, 0.5f)
                    : MoveToPosition(gameObject.transform, finalDestination, timer));
            }
        }

        private IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
        {
            var currentPos = transform.position;
            var t = 0f;
            if (ARDebug.Debugging)
                timeToMove = ARDebug.TimeToFill;
            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                transform.position = Vector3.Lerp(currentPos, position, t);
                yield return null;
            }

            animator.SetBool("stoped", true);
            FinishedRun();
        }

        private void FinishedRun()
        {
            PrizeButtons.instance.FinishedExperiment();
            PrizeButtons.instance.AnimateTotalPointsPoints();
        }
    }
}