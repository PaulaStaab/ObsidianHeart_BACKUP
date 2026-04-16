////using UnityEngine;

////public class SlideWall : MonoBehaviour
////{
////    [SerializeField] private Animator animator;
////    [SerializeField] private string triggerName = "Einfahren";
////    [SerializeField] private bool nurEinmal = true;

////    private bool wurdeBereitsAusgeloest = false;

////    private void Awake()
////    {
////        if (animator == null)
////            animator = GetComponent<Animator>();
////    }

////    public void WandEinfahrenStarten()
////    {
////        if (nurEinmal && wurdeBereitsAusgeloest)
////            return;

////        animator.SetTrigger(triggerName);
////        wurdeBereitsAusgeloest = true;
////    }
////}

//using UnityEngine;

//public class SlideWall : MonoBehaviour
//{
//    [SerializeField] private Animator animator;
//    [SerializeField] private string triggerName = "Einfahren";

//    private void Start()
//    {
//        if (animator == null)
//            animator = GetComponent<Animator>();

//        animator.SetTrigger(triggerName);
//    }
//}

using UnityEngine;

public class SlideWall : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string stateName = "SlidingWall";

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        animator.Play(stateName, 0, 0f);
    }
}