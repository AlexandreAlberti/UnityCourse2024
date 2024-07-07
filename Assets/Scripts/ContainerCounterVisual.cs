using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ContainerCounter container;

    private const string OPEN_CLOSE = "OpenClose";

    private void Start() {
        container.OnPlayerGrabbedObject += PlayOpenAnimation;
    }

    public void PlayOpenAnimation(object sender, EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
