using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private GameObject _player;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _multiplText;
    [SerializeField] private float highestDist;
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _score = 0;
            _multiplText.text = "x 0";
        }

        Vector3 playerVel = _player.GetComponent<Rigidbody>().velocity;
        float velX = Math.Abs(playerVel.x) < 1 ? 1 : Math.Abs(playerVel.x);
        float velY = Math.Abs(playerVel.y) < 1 ? 1 : Math.Abs(playerVel.y);
        float velZ = Math.Abs(playerVel.z) < 1 ? 1 : Math.Abs(playerVel.z);

        float avgVel = (velX + velY + velZ) / 3;
        float playerY = _player.transform.position.y < 1 ? 1 : _player.transform.position.y;

        float flyMultip = _player.GetComponent<PlayerController>().isGrounded ? 1 : 4;


        float multipSum = (playerY / 50) + avgVel + flyMultip / 5;


        float currentDist = Vector3.Distance(_player.transform.position, new Vector3(0, 0, 0));
        
        
        if (_player.GetComponent<PlayerController>().wasGroundedeOnce && currentDist > highestDist)
        {
            highestDist = currentDist;
            _score += (int)(highestDist * multipSum / 10);
            _multiplText.text = "x" + (int)multipSum;
        }

        _scoreText.text = _score.ToString();
        
    }
}
