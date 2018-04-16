using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    // Static variables
    public static bool isPlayer1Turn = false; // To check if player 1 has next turn
    public static bool isPlayer2Turn = false; // To check if player 2 has next turn
    public static bool isChopping = false; // To check if chopping is in-progress
    public static int noOfVegetablesPicked = 0; // To check no of vegetables picked by player
    public static int player1TotalScore = 0; // To count player 1 score
    public static int player2TotalScore = 0; // To count player 2 score
    public static int noOfCustomersServed = 0; // To check no of customers served

    // Public variables
    public string[] menu; // It will have all the available dishes
    public string[] vegetablesNames; // It will have all available vegetables
    public GameObject[] Customers; // All customers
    public GameObject[] customersOrdersText; // All customer's orders
    public GameObject[] CustomersWaitingTimeText; // All customers's waiting time text
    public GameObject[] plates; // Plates of players(Assuming each player has 1 plate)
    public GameObject[] vegetables; // All vegetable's gameobject
    public Sprite[] vegetablesNonSelectedImages; // All vegetable's sprites
    public Sprite[] vegetablesSelectedImages; // All vegetable's sprites if it is picked by player
    public GameObject[] vegetablesOnChoopingBoard1; // Vegetable's of chopping board1
    public GameObject[] vegetablesOnChoopingBoard2; // Vegetable's of chopping board2
    public GameObject choppingAnim; // Chopping Animation or loading animation
    public GameObject choppingText; // Chopping Text
    public GameObject servedText; // To display if the customer is served correctly or dissatified or angry
    public GameObject player1TurnText; // Text to display if player 1 has next turn
    public GameObject player2TurnText; // Text to display if player 2 has next turn
    public GameObject player1Timer; // Player 1 Timer Text
    public GameObject player2Timer; // Player 2 Timer Text
    public GameObject player1Score; // Player 1 Score Text
    public GameObject player2Score; // Player 2 Score Text
    public GameObject winnerText; // To display the winner
    public bool[] canPickVegetable; // To check if particular vegetable can be picked by player or not
    public float[] CustomersWaitingTime; // All customer's waiting time. Each will varies according to the arrival time and amount of indegrients

    // Private variables
    int noOfCustomers;
    int veg1Index;
    Text player1TimerText;
    Text player2TimerText;
    Text player1ScoreText;
    Text player2ScoreText;
    float time = 0;

    private void Awake()
    {
        noOfCustomers = Random.Range(2, 5); // To get the random no from 2 to 5
        RandomOrders(); // To arrange the customers and orders in random order (min no of customers = 2, can be changed according to the gameplay)
    }
    // Use this for initialization
    void Start()
    {

        // To get Text component from the gameobject
        player1TimerText = player1Timer.GetComponent<Text>();
        player2TimerText = player2Timer.GetComponent<Text>();
        player1ScoreText = player1Score.GetComponent<Text>();
        player2ScoreText = player2Score.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {

        if (noOfCustomersServed == noOfCustomers)
        {
            FindWinner(); // To find out the winner only if no of served customers is equal to total no of customers arrived
        }

        for (int i = 0; i < noOfCustomers; i++)
        {
            CustomersWaitingTime[i] -= Time.deltaTime;
            if (CustomersWaitingTime[i] >= 0)
            {
                // if customer is waiting less than the expected waiting time
                CustomersWaitingTimeText[i].GetComponent<Text>().text = "WaitingTime:" + Mathf.Round(CustomersWaitingTime[i]) + "s";
            }
            else
            {
                // If customer is waiting more than the expected waiting time
                CustomersWaitingTimeText[i].GetComponent<Text>().color = Color.red;
                CustomersWaitingTimeText[i].GetComponent<Text>().text = "ExtraWaitingTime:" + Mathf.Round(CustomersWaitingTime[i]) + "s";
            }

        }

        time += Time.deltaTime;

        if (isPlayer1Turn && noOfCustomers != noOfCustomersServed)
        {
            player1TurnText.SetActive(true);
            player2TurnText.SetActive(false);
            player1TimerText.text = "Time: " + Mathf.RoundToInt(time);

        }
        else if (isPlayer2Turn && noOfCustomers != noOfCustomersServed)
        {
            player2TurnText.SetActive(true);
            player1TurnText.SetActive(false);
            player2TimerText.text = "Time: " + Mathf.RoundToInt(time);
        }
        else
        {
            player2TurnText.SetActive(false);
            player1TurnText.SetActive(false);
        }

        if (isChopping == false)
        {
            if (Input.GetKeyDown(KeyCode.A) && canPickVegetable[0])
            {
                PickVegetable(0);
            }

            if (Input.GetKeyDown(KeyCode.B) && canPickVegetable[1])
            {
                PickVegetable(1);
            }

            if (Input.GetKeyDown(KeyCode.C) && canPickVegetable[2])
            {
                PickVegetable(2);
            }

            if (Input.GetKeyDown(KeyCode.D) && canPickVegetable[3])
            {
                PickVegetable(3);
            }

            if (Input.GetKeyDown(KeyCode.E) && canPickVegetable[4])
            {
                PickVegetable(4);
            }

            if (Input.GetKeyDown(KeyCode.F) && canPickVegetable[5])
            {
                PickVegetable(5);
            }


        }

    }

    // To find out the winner between two
    public void FindWinner()
    {
        winnerText.SetActive(true);
        if (player1TotalScore > player2TotalScore)
        {
            winnerText.GetComponent<Text>().text = "PLAYER 1 WON !!";
        }
        else if (player1TotalScore < player2TotalScore)
        {
            winnerText.GetComponent<Text>().text = "PLAYER 2 WON !!";
        }
        else
        {
            winnerText.GetComponent<Text>().text = "TIE !!";
        }

    }

    // To arrange the customers and orders in random order. It will be called only once i.e at the time of loading
    public void RandomOrders()
    {
        for (int i = 0; i < noOfCustomers; i++)
        {
            if (Customers[i] != null && customersOrdersText[i] != null)
            {
                Customers[i].SetActive(true);
                customersOrdersText[i].SetActive(true);
                customersOrdersText[i].GetComponent<Text>().text = menu[Random.Range(0, menu.Length)];
                CustomersWaitingTimeText[i].GetComponent<Text>().text = "WaitingTime: " + (i + 1) * 30 + "s";
                CustomersWaitingTime[i] = (i + 1) * 30;
                CustomersWaitingTimeText[i].SetActive(true);
            }
        }
        isPlayer1Turn = true;

    }

    // To pick the particular vegetable
    public void PickVegetable(int position)
    {
        Debug.Log("PickVegetable");
        noOfVegetablesPicked += 1;
        if ((isPlayer1Turn || isPlayer2Turn) && noOfVegetablesPicked <= 2 && canPickVegetable[position])
        {
            if (isPlayer1Turn)
            {
                Debug.Log("Player1Turn");
                vegetablesOnChoopingBoard1[position].SetActive(true);
            }
            else if (isPlayer2Turn)
            {
                Debug.Log("Player2Turn");
                if (noOfVegetablesPicked > 1)
                {
                    Vector3 pos = vegetablesOnChoopingBoard2[position].transform.position;
                    vegetablesOnChoopingBoard2[position].transform.position = new Vector3(pos.x + 50, pos.y, pos.z);

                }
                vegetablesOnChoopingBoard2[position].SetActive(true);
            }
            isChopping = true;
            vegetables[position].GetComponent<Image>().sprite = vegetablesSelectedImages[position];
            canPickVegetable[position] = false;
            StartCoroutine(StartChopping(position));


        }

    }

    // To start chopping of vegetables 
    IEnumerator StartChopping(int index)
    {
        choppingText.GetComponent<Text>().text = "Chopping";
        choppingAnim.SetActive(true);
        choppingText.SetActive(true);
        yield return new WaitForSeconds(5);
        choppingAnim.SetActive(false);
        choppingText.GetComponent<Text>().text = "Chopped";
        yield return new WaitForSeconds(1);

        if (isPlayer1Turn)
        {

            Vector3 pos = vegetablesOnChoopingBoard1[index].transform.position;
            if (noOfVegetablesPicked == 1)
            {
                veg1Index = index;
                vegetablesOnChoopingBoard1[index].transform.position = new Vector3(pos.x - 120, pos.y, pos.z);

            }
            else
            {
                vegetablesOnChoopingBoard1[index].transform.position = new Vector3(pos.x - 90, pos.y, pos.z);
                yield return new WaitForSeconds(1);
                plates[0].transform.position = new Vector3(plates[0].transform.position.x, plates[0].transform.position.y + 470, 0);
                vegetablesOnChoopingBoard1[index].transform.position = new Vector3(pos.x - 90, pos.y + 470, pos.z);
                vegetablesOnChoopingBoard1[veg1Index].transform.position = new Vector3(pos.x - 120, pos.y + 470, pos.z);
                StartCoroutine(ResetPositions(pos, veg1Index, index));
            }

        }
        if (isPlayer2Turn)
        {
            Vector3 pos = vegetablesOnChoopingBoard2[index].transform.position;
            if (noOfVegetablesPicked == 1)
            {
                veg1Index = index;
                vegetablesOnChoopingBoard2[index].transform.position = new Vector3(pos.x + 150, pos.y, pos.z);

            }
            else
            {
                vegetablesOnChoopingBoard2[index].transform.position = new Vector3(pos.x + 120, pos.y, pos.z);
                yield return new WaitForSeconds(1);
                plates[1].transform.position = new Vector3(plates[0].transform.position.x, plates[1].transform.position.y + 470, 0);
                vegetablesOnChoopingBoard2[index].transform.position = new Vector3(pos.x - 340, pos.y + 470, pos.z);
                vegetablesOnChoopingBoard2[veg1Index].transform.position = new Vector3(pos.x - 370, pos.y + 470, pos.z);
                StartCoroutine(ResetPositions(pos, veg1Index, index));
            }

        }

        isChopping = false;
        choppingText.SetActive(false);
    }

    // To reset position of vegetables,plates and customers and to find out if customer is satisfied or not
    public IEnumerator ResetPositions(Vector3 choppingBoardVegetablesPosition, int veg1Index, int veg2Index)
    {
        if (IsCorrectlyServed(veg1Index, veg2Index))
        {
            if (CustomersWaitingTime[noOfCustomersServed] > 0)
            {
                servedText.GetComponent<Text>().text = "SERVED";
            }
            else
            {
                servedText.GetComponent<Text>().text = "DISSATISFIED";
            }
            servedText.SetActive(true);
            yield return new WaitForSeconds(2);
            CalculateScore(veg1Index, veg2Index, time);
            noOfCustomersServed += 1;
            time = 0;
            servedText.SetActive(false);

            for (int i = noOfCustomers - 1; i >= 0; i--)
            {
                if (Customers[i].transform.position.x == Customers[0].transform.position.x)
                {
                    Customers[i].SetActive(false);
                    customersOrdersText[i].SetActive(false);
                    CustomersWaitingTimeText[i].SetActive(false);
                }
                else
                {
                    Customers[i].transform.position = new Vector3(Customers[i - 1].transform.position.x, Customers[i].transform.position.y, 0);
                    customersOrdersText[i].transform.position = new Vector3(customersOrdersText[i - 1].transform.position.x, customersOrdersText[i].transform.position.y, 0);
                    CustomersWaitingTimeText[i].transform.position = new Vector3(CustomersWaitingTimeText[i - 1].transform.position.x, CustomersWaitingTimeText[i].transform.position.y, 0);
                }

            }
        }
        else
        {
            servedText.GetComponent<Text>().text = "Angry Customer";
            servedText.SetActive(true);
            yield return new WaitForSeconds(2);
            CalculateScore(veg1Index, veg2Index, time);
            time = 0;
            servedText.SetActive(false);

        }

        if (noOfCustomers != noOfCustomersServed)
        {
            noOfVegetablesPicked = 0;
        }

        if (isPlayer1Turn)
        {
            isPlayer1Turn = false;
            isPlayer2Turn = true;
            plates[0].transform.position = new Vector3(plates[0].transform.position.x, plates[0].transform.position.y - 470, 0);
            vegetablesOnChoopingBoard1[veg2Index].transform.position = choppingBoardVegetablesPosition;
            vegetablesOnChoopingBoard1[veg2Index].SetActive(false);
            vegetablesOnChoopingBoard1[veg1Index].transform.position = choppingBoardVegetablesPosition;
            vegetablesOnChoopingBoard1[veg1Index].SetActive(false);

        }
        else
        {
            isPlayer1Turn = true;
            isPlayer2Turn = false;
            plates[1].transform.position = new Vector3(plates[1].transform.position.x + 440, plates[1].transform.position.y - 470, 0);
            vegetablesOnChoopingBoard2[veg2Index].transform.position = choppingBoardVegetablesPosition;
            vegetablesOnChoopingBoard2[veg2Index].SetActive(false);
            vegetablesOnChoopingBoard2[veg1Index].transform.position = choppingBoardVegetablesPosition;
            vegetablesOnChoopingBoard2[veg1Index].SetActive(false);
        }

        vegetables[veg2Index].GetComponent<Image>().sprite = vegetablesNonSelectedImages[veg2Index];
        vegetables[veg1Index].GetComponent<Image>().sprite = vegetablesNonSelectedImages[veg1Index];
        canPickVegetable[veg1Index] = true;
        canPickVegetable[veg2Index] = true;

    }

    // To calculate score : a. if customer did not wait more than the expected waiting time then score will be -> Total score + (100 - Time taken by player to serve)
    // b. if customer is served wrong dish then score will be -> Total Score - 100(Penalty)
    // c. if customer waited more than the expected time but served right dish then score will be -> Total score - Extra Time wait(No extra points will be given )
    public void CalculateScore(int veg1Index, int veg2Index, float timeTaken)
    {

        int netScore = 100 - Mathf.RoundToInt(timeTaken);
        if (isPlayer1Turn)
        {
            if (IsCorrectlyServed(veg1Index, veg2Index) && CustomersWaitingTime[noOfCustomersServed] > 0)
            {
                player1TotalScore += netScore;

            }
            else if (!IsCorrectlyServed(veg1Index, veg2Index))
            {
                player1TotalScore -= 100;
            }
            else
            {
                player1TotalScore += Mathf.RoundToInt(CustomersWaitingTime[noOfCustomersServed]);
            }
            player1ScoreText.text = "Score: " + player1TotalScore;
        }
        else if (isPlayer2Turn)
        {
            if (IsCorrectlyServed(veg1Index, veg2Index) && CustomersWaitingTime[noOfCustomersServed] > 0)
            {
                player2TotalScore += netScore;

            }
            else if (!IsCorrectlyServed(veg1Index, veg2Index))
            {
                player2TotalScore -= 100;
            }
            else
            {
                player2TotalScore += Mathf.RoundToInt(CustomersWaitingTime[noOfCustomersServed]);
            }
            player2ScoreText.text = "Score: " + player2TotalScore;
        }

    }

    // To check if player served right dish or not
    public bool IsCorrectlyServed(int veg1Index, int veg2Index)
    {
        string dishServed = vegetablesNames[veg1Index] + " " + vegetablesNames[veg2Index];
        Debug.Log(dishServed);
        string reverseOfDishServed = vegetablesNames[veg2Index] + " " + vegetablesNames[veg1Index];
        if (customersOrdersText[noOfCustomersServed].GetComponent<Text>().text == dishServed || customersOrdersText[noOfCustomersServed].GetComponent<Text>().text == reverseOfDishServed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
