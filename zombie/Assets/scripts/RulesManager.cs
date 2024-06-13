using Mirror;
using UnityEngine;

public class RulesManager : NetworkBehaviour
{

    public readonly SyncList<GameObject> Players = new SyncList<GameObject>();
    public readonly SyncList<GameObject> ZPlayers = new SyncList<GameObject>();

    public GameObject winWindow;

    private Transform ZPoint;
    public Transform[] PPoints;
    [SyncVar]
    bool GameStarted;

    [SyncVar]
    [HideInInspector]public bool GameEnd = false;

    float time;
    private void Start()
    {
        ZPoint=transform.GetChild(0);   
    }
    private void Update()
    {
        if (Players.Count>0&&ZPlayers.Count>0)
        {
            GameStarted = true;
        }
        if (GameStarted)
        {
            if (Players.Count == 0)
            {
                time += Time.deltaTime;
                winWindow.SetActive(true);
                if (time >= 2)
                {
                    GameEnd = true;
                } 
            }
        }   
    }
    // Start is called before the first frame update
    public void AddZombie(GameObject gameObject)
    {
        ZPlayers.Add(gameObject);
        gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        gameObject.transform.position = ZPoint.position;
        gameObject.GetComponent<RulesHelper>().isZombie = true;
    }
    public void AddPlayer(GameObject gameObject)
    {
        Players.Add(gameObject);
        int randomIndex = Random.Range(0, PPoints.Length-1);
        gameObject.transform.position = PPoints[randomIndex].position; 
        
    }


}
