using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RulesHelper : NetworkBehaviour
{
    [SyncVar (hook = nameof(OnChangeVar))]
    [HideInInspector] public bool isZombie = false;
    public GameObject Sumbol;
    public TMP_Text rule;


    private RulesManager manager;
    private void Start()
    {
        manager = GameObject.FindObjectOfType<RulesManager>();
    }


    private void OnChangeVar(bool oldvalue, bool newvalue)
    {
        if (isZombie==true)
        {
            NetworkIdentity identity = gameObject.GetComponent<NetworkIdentity>();
            if (!identity.isLocalPlayer)
               Sumbol.SetActive(true);
            gameObject.tag = "ZPlayer";
            rule.text = "ַמלבט";
            rule.color = Color.green;

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ZPlayer" && isZombie==false)
        {

            gameObject.GetComponent<RulesHelper>().isZombie = true;
            gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            gameObject.tag = "ZPlayer";
            player Player = gameObject.GetComponent<player>();
            Player.speed_run = 11f;
            Player.staminaDepletionRate = 10f;
            Player.maxStamina = 90;
            rule.text = "ַמלבט";
            rule.color = Color.green;
            Command(gameObject);
        }
        else if(other.tag!="ZPlayer" && isZombie==true){
            other.gameObject.GetComponent<RulesHelper>().isZombie = true;
            other.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            other.tag = "ZPlayer";
            player Player = other.GetComponent<player>();
            Player.speed_run = 11f;
            Player.staminaDepletionRate = 10f;
            Player.maxStamina = 90;
            rule.text = "ַמלבט";
            rule.color = Color.green;
            Command(other.gameObject);
        }
    }
    private void Command(GameObject gameObject)
    {
        manager.AddZombie(gameObject);
        manager.Players.Remove(gameObject);
    }
}
