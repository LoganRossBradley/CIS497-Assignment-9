/* * Logan Ross
 * * GameManager.cs
 * * Assignment 9
 * * Manages some basic buttons and ui texts
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text txtMoney;
    public Text outputText;

    public SlotMachine slotMachine;

    public void updateMoney(int newMoney)
    {

            txtMoney.text = "$" + newMoney.ToString();
    }

    public void restartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
