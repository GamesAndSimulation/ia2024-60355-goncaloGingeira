using UnityEngine;
using DG.Tweening;

namespace Platformer
{
    public class PlatformMover : MonoBehaviour
    {

        [SerializeField] Vector3 moveTo = Vector3.zero;
        [SerializeField] float moveTime = 5f;
        [SerializeField] Ease ease = Ease.InOutQuad; 
        
        Vector3 originalPosition;
        
        void Start() {
            originalPosition = transform.position;
            Move();
        }


        void Move()
        {
            transform.DOMove(originalPosition + moveTo, moveTime)
                .SetEase(ease)
                .SetLoops(-1, LoopType.Yoyo);
            
        }
    }
}