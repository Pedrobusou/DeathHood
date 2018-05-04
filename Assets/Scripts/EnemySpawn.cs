using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawn : NetworkBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool RTInUse = false;

    // Update is called once per frame
    private void Update()
    {
        if (!isLocalPlayer) { return; }
        rtButton();
    }

    /// <summary>
    /// Makes the RT button to work as a digital input instead of as an axis
    /// </summary>
    private void rtButton()
    {
        if (!RTInUse)
        {
            if (InputManager.RTTrigger() == 1f)
            {
                print("RT Value: " + Input.GetAxis("RT_Button"));

                if (RTInUse == false)
                {
                    RTInUse = !RTInUse;
                    print("RT Trigger pressed");
                    CmdSpawn();
                }
            }
        }

        if (RTInUse)
        {
            if (InputManager.RTTrigger() <= 0.5f)
            {
                RTInUse = !RTInUse;
                print("RT Trigger Released");
            }
        }
    }

    [Command]
    private void CmdSpawn()
    {
        GameObject go = Instantiate(objectToSpawn, spawnPoint);
        NetworkServer.Spawn(go);
    }
}