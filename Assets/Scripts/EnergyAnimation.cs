using UnityEngine;
using System.Collections;

public class EnergyAnimation : MonoBehaviour
{
    public GameObject energyDuplicatePlus1;
    public EnergyLoader energyLoader;

    void Start()
    {
        energyDuplicatePlus1.SetActive(false);
    }

    public void StartAnimation()
    {
        if (!energyDuplicatePlus1.activeSelf)
        {
            energyDuplicatePlus1.SetActive(true);

            Animator anim = energyDuplicatePlus1.GetComponent<Animator>();

            StartCoroutine(PlayAnimationAndWait(anim));

        }
    }

    private IEnumerator PlayAnimationAndWait(Animator animator)
    {
        animator.enabled = true;
        animator.Play("EnergyAnim");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        energyDuplicatePlus1.SetActive(false);

        energyLoader.StartTimerIfNeeded();
    }
}
