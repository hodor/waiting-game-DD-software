using AR_Project.MainGame.Prize;
using UnityEngine;

namespace AR_Project.MainGame.GameObjects
{
    public class FinishLine : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.name.Contains("Prize"))
                col.gameObject.GetComponent<PrizeGO>().isFinished = true;
        }
    }
}