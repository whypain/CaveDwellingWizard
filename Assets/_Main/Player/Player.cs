using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData Data => playerData;
    private PlayerData playerData;


    public void Initialize(PlayerData playerData)
    {
        this.playerData = playerData;
    }
}

public class PlayerData
{
    public int Milk => milk;
    public int Time => time;
    public int Cookies => cookies;
    
    private int milk;
    private int cookies;
    private int time;

    public PlayerData(int milk, int cookies, int time, Vector2 position)
    {
        this.milk = milk;
        this.cookies = cookies;
        this.time = time;
    }

    public PlayerData()
    {
        milk = 0;
        cookies = 0;
        time = 0;
    }
}
