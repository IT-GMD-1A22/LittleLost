using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/*
 * In case some obstacles is too annoying to get past.
 * This scripts allows a new spawn beacon to be set and
 * teleports the player to that beacon. Just a little help :)
 *
 * JH
 * Note: inspiration drawn from here: https://answers.unity.com/questions/553597/how-to-implement-cheat-codes.html
 */
public class CheatCode : MonoBehaviour
{
    [SerializeField] private KeyCode[] _code;
    [SerializeField] private List<Transform> orderedBeacons = new();

    private SpawnManager _spawnManager;
    private int _index;
    private float _inputTimer = 5f;

    private void Awake()
    {
        _spawnManager = GetComponent<SpawnManager>();
    }

    private void Update()
    {
        if (_inputTimer > 0f && _index > 0)
            _inputTimer -= Time.deltaTime;
        
        if (Input.GetKeyDown(_code[_index]))
        {
            _index++;
        }
        else if (_inputTimer <= 0f)
        {
            _index = 0;
        }


        if (_index == _code.Length && orderedBeacons.Count > 0 && _inputTimer > 0f)
        {
            Debug.Log("Cheat activated");
            var player = _spawnManager.currentPlayer.GetComponent<PlayerController>();
            player.TeleportPlayerToVector(orderedBeacons[0].position);
            orderedBeacons.RemoveAt(0);

            _index = 0;
        }
    }
    
    
}
