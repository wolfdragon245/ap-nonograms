using System;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Godot;

namespace APNonograms.Scripts;

public static class Universal
{
    public static ArchipelagoSession Session;
    public static bool Connected;

    public static void Connect(String ip, String slot, String password)
    {
        Session = ArchipelagoSessionFactory.CreateSession(ip);
        Session.MessageLog.OnMessageReceived += message =>
        {
            GD.Print(message);
        };
        
        LoginResult result = Session.TryConnectAndLogin(
            "",
            slot,
            ItemsHandlingFlags.NoItems,
            new Version(0, 5, 0),
            new[] {"TextOnly", "AP_Nonograms"},
            requestSlotData: false,
            password: password
        );

        GD.Print(result.Successful);
        Connected = result.Successful;
    }

    public static void Disconnect()
    {
        Session.Socket.DisconnectAsync();
        Connected = false;
    }
}