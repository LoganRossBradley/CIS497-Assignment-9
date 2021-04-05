/* * Logan Ross
 * * SlotMachine.cs
 * * Assignment 9
 * * Tracks money in machine, spins the machine and calculates output
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public bool spinning;
    public int moneyInMachine;

    public GameManager gm;

    public Text[] txtFiles = new Text[3];
    public Text txtSpinStop;

    public string[] possibleOutputs = { "A", "B", "C"};

    void Start()
    {
        spinning = false;
        moneyInMachine = 0;
        gm.updateMoney(moneyInMachine);
        txtSpinStop.text = "Spin";
    }

    public void addMoneyClick()
    {
        if (spinning)
        {
            gm.outputText.text = "You cannot add money while the machine is spinning";
        }
        else
        {
            moneyInMachine += 10;
            gm.updateMoney(moneyInMachine);
        }
    }

    public void spinStopClick()
    {
        if(spinning)
        {
            StopCoroutine(spinMachine());
            txtSpinStop.text = "Spin";
            spinning = false;
            StartCoroutine(waitForEvaluate());
        }
        else if(moneyInMachine >= 5)
        {
            moneyInMachine -= 5;
            gm.updateMoney(moneyInMachine);
            txtSpinStop.text = "Stop";
            spinning = true;
            StopCoroutine(spinMachine());
            StartCoroutine(spinMachine());
        }
        else
        {
            gm.outputText.text = "You need $5 in the machine to spin";
        }
    }

    IEnumerator spinMachine()
    {
        while(spinning)
        {
            yield return new WaitForSeconds(0.05f);

            foreach (Text file in txtFiles)
            {
                file.text = possibleOutputs[Random.Range(0, possibleOutputs.Length)];
            }
        }    
    }

    IEnumerator waitForEvaluate()
    {
        yield return new WaitForSeconds(0.1f);
        evaluateSpin();
    }

    public int evaluateSpin()
    {
        int retMoney = 0;
        foreach(Text file in txtFiles)
        {
            Debug.Log(file.text);
        }
        if(txtFiles[0].text == txtFiles[1].text)
        {
            Debug.Log("winner");
            switch(txtFiles[0].text)
            {
                case "A":
                    retMoney = 5;
                    break;
                case "B":
                    retMoney = 3;
                    break;
                case "C":
                    retMoney = 1;
                    break;
                default:
                    Debug.LogError("invalid switch");
                    break;
            }
            if (txtFiles[0].text == txtFiles[2].text)
            {
                retMoney *= 3;
            }
        }

        gm.outputText.text = "You won $" + retMoney;
        moneyInMachine += retMoney;
        gm.updateMoney(moneyInMachine);
        return retMoney;
    }
}
