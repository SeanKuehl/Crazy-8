using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameScript : MonoBehaviour
{


    public float numberChangeTimer = 3.0f;  //meant to be 4 seconds

    public float computerReactionTimer = 2.0f;

    public float computerReactionReset = 2.0f;

    public int playerPoints = 0;

    public int computerPoints = 0;

    private int winPointThreshold = 5;

    // Start is called before the first frame update
    void Start()
    {
        
        SetNumber(1);    //set the starting values to something that's not 8
    }

    void SetPointsText(){

        

        var mainNum = GameObject.Find("Score");
        string playerText = "Player has: ";
        string afterPlayer = ", CPU has: ";
        string secondLine = "\nhit space to claim the 8 to get a point";
        string thirdLine = "\nFirst to 5 wins!";
        string playerScore = playerPoints.ToString();
        string cpuScore = computerPoints.ToString();
        string textToInsert = string.Concat(playerText, playerScore, afterPlayer, cpuScore, secondLine, thirdLine);
        mainNum.GetComponent<Text>().text = textToInsert;
    }


    void SetNumber(int numberToSet){
        var mainNum = GameObject.Find("Number");
        mainNum.GetComponent<Text>().text = numberToSet.ToString();
    }

    int GetNumber(){
        var mainNum = GameObject.Find("Number");
        int toReturn = Int32.Parse(mainNum.GetComponent<Text>().text);
        return toReturn;
    }



    // Update is called once per frame
    void Update()
    {

        numberChangeTimer -= Time.deltaTime;
        
        if (GetNumber() == 8){
            computerReactionTimer -= Time.deltaTime;
        }


        if (computerReactionTimer <= 0.0f){
            CheckEight();
        }

        if (numberChangeTimer <= 0.0f)
        {
            ChangeNumber();
        }

        if (Input.GetKey(KeyCode.Space) && GetNumber() == 8) {
            //if it's an 8, get a point
            //make computer harder to beat for next time
            playerPoints += 1;
            computerReactionReset -= 0.1f;
            SetPointsText();
            ChangeNumber();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && GetNumber() != 8){
            computerPoints += 1;
            SetPointsText();
            ChangeNumber();
        }


        if (playerPoints >= winPointThreshold){
            //load scene winning screen
            SceneManager.LoadScene("WinScreen");
        }
        else if (computerPoints >= winPointThreshold){
            //load scene losing screen
            SceneManager.LoadScene("LoseScreen");
        }
        
    }


    void ChangeNumber()
    {
        //change the number randomly to one between 1 and 10
        int randomNumber = UnityEngine.Random.Range(6, 10);
        SetNumber(randomNumber);
        numberChangeTimer = 3.0f;
        computerReactionTimer = computerReactionReset;
    }

    void CheckEight(){
        if (GetNumber() == 8){
            computerPoints += 1;
            ChangeNumber();
            computerReactionTimer = computerReactionReset;
            numberChangeTimer = 3.0f;
        }
        else if (GetNumber() != 8) {
            computerReactionTimer = computerReactionReset;
        }
    }


}
