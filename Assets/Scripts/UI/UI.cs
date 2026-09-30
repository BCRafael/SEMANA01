using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    public static UI instance;

    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Player player;

    void Update()
    {
        lifeText.text = player.life.ToString();
        coinText.text = player.coins.ToString();
    }
}