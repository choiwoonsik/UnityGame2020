using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotMachine : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;

    public GameObject panel;

    private int result1;
    private int result2;
    private int result3;

    private MiniGameOnOff theMinigame;

    private void Start()
    {
        theMinigame = FindObjectOfType<MiniGameOnOff>();
    }

    public void StopSlotMachine()
    {
        StartCoroutine(SlotMachineCoroutine());
    }

    IEnumerator SlotMachineCoroutine()
    {
        result1 = Random.Range(0, 5);
        animator1.SetInteger("result", result1);
        yield return new WaitForSeconds(1.0f);

        result2 = Random.Range(0, 5);
        animator2.SetInteger("result", result2);
        yield return new WaitForSeconds(1.0f);

        result3 = Random.Range(0, 5);
        animator3.SetInteger("result", result3);

        theMinigame.AfterSlotMachine(result1, result2, result3);

        yield return new WaitForSeconds(1.0f);

        animator1.SetInteger("result", -1);
        animator2.SetInteger("result", -1);
        animator3.SetInteger("result", -1);
        animator1.SetTrigger("start");
        animator2.SetTrigger("start");
        animator3.SetTrigger("start");

        panel.SetActive(false);
    }
}
