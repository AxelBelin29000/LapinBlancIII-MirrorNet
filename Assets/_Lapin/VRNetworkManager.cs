using Mirror;
using UnityEngine;

public class VRNetworkManager : NetworkManager
{
    [Header("VR")]
    public Transform[] spawnPoints;
    private int _spawnIndex = 0;

    public GameObject cubePrefab;
    public Transform cubeSpawnPoint;

    public override void OnStartServer()
    {
        base.OnStartServer();
        GameObject cube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
        NetworkServer.Spawn(cube);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform spawn = spawnPoints[_spawnIndex % spawnPoints.Length];
        _spawnIndex++;

        GameObject player = Instantiate(playerPrefab, spawn.position, spawn.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}