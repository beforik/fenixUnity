using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerInfo
{
    [JsonProperty]
    public int x;
    
    [JsonProperty]
    public int y;
    
    [JsonProperty]
    public string id;
    
    [JsonProperty]
    public string color;
}

public class PlayerInfoList
{
    [JsonProperty("players")] 
    public List<PlayerInfo> Players { get; set; }

    public PlayerInfoList()
    {
        Players = new List<PlayerInfo>();
    }
}
