using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeRangeBrain : MonoBehaviour
{
    // All the enemies in this level
    public GameObject[] Enemies;
    // A required deathToll to open a door on the 1st floor
    public int FirstFloorDeathToll1 = 8;
    [SerializeField]
    private bool firstFloorCleared = false;
    // An Array of all the locked doors in the level
    public GameObject[] LockedDoors;
    // The Player for faster finding
    public GameObject Player;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if we met the 1ts floor advance condition
        // If we did, move on to next area logic (as to not have additional calls)
        if(firstFloorCleared == false)
        FirstFloorLogic();
        

    }


    //
    //  Takes count of how many dummies are killed
    //  * if all enemies are killed, Unlock the Key Door
    //  
    void FirstFloorLogic()
    {
        int counter = 0;
        for (int i = 0; i < FirstFloorDeathToll1; i++)
        {
            if (Enemies[i].GetComponent<Target>().GetDeathStatus() == true)
                counter++;
        }
        if (counter == FirstFloorDeathToll1)
        {
            LockedDoors[0].GetComponent<KeyDoor>().setLockFalse();
            firstFloorCleared = true;
        }
    }
}
