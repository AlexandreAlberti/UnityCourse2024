using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterCounterVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CuttingCounter cutting;

    private const string CUT = "Cut";

    private void Start() {
        cutting.OnPlayerCut += PlayCutAnimation;
    }

    public void PlayCutAnimation(object sender, EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
