using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    public GameObject coinDuplicatePlus1;

    void Start()
    {
        coinDuplicatePlus1.SetActive(false);
    }

    public void StartAnimation()
    {
        if (!coinDuplicatePlus1.activeSelf)
        {
            coinDuplicatePlus1.SetActive(true);

            Animator anim = coinDuplicatePlus1.GetComponent<Animator>();

            StartCoroutine(PlayAnimationAndWait(anim));

        }
    }

    private IEnumerator PlayAnimationAndWait(Animator animator)
    {
        animator.enabled = true;
        animator.Play("CoinAnim");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        coinDuplicatePlus1.SetActive(false);

    }
}
