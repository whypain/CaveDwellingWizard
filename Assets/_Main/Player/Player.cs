using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;


    public void Initialize(PlayerData playerData)
    {
        this.playerData = playerData;
    }


    private void Update()
    {
        playerData.UpdatePosition(transform.localPosition);
    }
}

public class PlayerData
{
    public Vector2 Position => position;
    public int Milk => milk;
    public int Time => time;
    public int Cookies => cookies;
    
    private int milk;
    private int cookies;
    private int time;
    private Vector2 position;

    public void UpdatePosition(Vector2 newPosition)
    {
        position = newPosition;
    }

    public PlayerData(int milk, int cookies, int time, Vector2 position)
    {
        this.milk = milk;
        this.cookies = cookies;
        this.time = time;
        this.position = position;
    }

    public PlayerData()
    {
        milk = 0;
        cookies = 0;
        time = 0;
        position = Vector2.zero;
    }

    public string Pack()
    {
        return $"{Milk}|{Cookies}|{Position}|{Time}";
    }

    public static PlayerData Unpack(string packed)
    {
        int unpack_milk;
        int unpack_cookies;
        int unpack_time;
        Vector2 unpack_position;

        List<string> unpacked = new List<string>();
        foreach (string item in packed.Split("|"))
        {
            unpacked.Add(item);
        }

        try
        {
            unpack_milk = int.Parse(unpacked[0]);
            unpack_cookies = int.Parse(unpacked[1]);
            unpack_time = int.Parse(unpacked[2]);
            unpack_position = unpacked[3].ParseToVector2();

            return new PlayerData(unpack_milk, unpack_cookies, unpack_time, unpack_position);
        }
        catch
        {
            throw new ArgumentException("Input is invalid");
        }
    }

}


public static class VectorExtension
{
    public static Vector2 ParseToVector2(this string raw)
    {
        string trimmed = raw.TrimStart('(').TrimEnd(')');
        string[] split = trimmed.Split(",");
        if (split.Length != 2) throw new ArgumentException("Input is invalid");
        try
        {
            float x = float.Parse(split[0]);
            float y = float.Parse(split[1]);

            return new Vector2(x, y);
        }
        catch
        {
            throw new ArgumentException("Input is invalid");
        }
    }
}