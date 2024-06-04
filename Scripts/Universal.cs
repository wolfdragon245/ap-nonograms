using System;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Godot;
using HttpClient = System.Net.Http.HttpClient;

namespace APNonograms.Scripts;

public static class Universal
{
    public static ArchipelagoSession Session;
    public static bool Connected;
    public static String baseURL = "https://commandtm.github.io/ap-nonograms/";
    
    public static HttpClient client = new()
    {
        BaseAddress = new Uri(baseURL)
    };
}