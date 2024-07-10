using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetDashScript : MonoBehaviour
{
    public UnityEvent<DashTargetBundle> OnHit = new UnityEvent<DashTargetBundle>();
    
    public void LaunchEvent(float score)
    {
        DashTargetBundle bundle = new DashTargetBundle(score, this);
        OnHit.Invoke(bundle);
    }

    public class DashTargetBundle
    {
        public float score;
        public TargetDashScript target;

        public DashTargetBundle(float score, TargetDashScript target)
        {
            this.score = score;
            this.target = target;
        }
    }
}
