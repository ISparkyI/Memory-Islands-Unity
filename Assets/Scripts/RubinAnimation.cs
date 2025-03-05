using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubinAnimation : MonoBehaviour
{
    public GameObject rubinDuplicatePlus1;

    void Start()
    {
        rubinDuplicatePlus1.SetActive(false);
    }

    public void StartAnimation()
    {
        if (!rubinDuplicatePlus1.activeSelf)
        {
            rubinDuplicatePlus1.SetActive(true);

            Animator anim = rubinDuplicatePlus1.GetComponent<Animator>();

            StartCoroutine(PlayAnimationAndWait(anim));

        }
    }

    private IEnumerator PlayAnimationAndWait(Animator animator)
    {
        animator.enabled = true;
        animator.Play("RubinAnim");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        rubinDuplicatePlus1.SetActive(false);

    }
}
