using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{
    
    public float curHp;
    public float maxHp;
   
    CharacterScript globalStats;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        globalStats = player.GetComponent<CharacterScript>();
    }

    private void Update()
    {
        UpdatePlayerStats();
    }


    public void UpdatePlayerStats()
    {
        curHp = globalStats.curHP;
        maxHp = globalStats.maxHP;
       
    }
}
