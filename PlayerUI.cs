using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class PlayerUI : MonoBehaviour
{
    public Player player;

    [SerializeField, Tooltip("This is the text for this player.")]
    private Text text;

    private void Awake()
    {
        text = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        text.text = string.Format("Health: {0}%", Mathf.RoundToInt(player.health.Percent * 100f));
    }
}
