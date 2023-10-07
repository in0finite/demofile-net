﻿using System.Diagnostics;
using DemoFile;
using DemoFile.Sdk;
using Spectre.Console;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var path = args.SingleOrDefault() ?? throw new Exception("Expected a single argument: <path to .dem>");

        var demo = new DemoParser();

        demo.Source1GameEvents.PlayerDeath += e =>
        {
            var attacker = demo.GetEntityByIndex<CCSPlayerController>(e.Attacker);
            var victim = demo.GetEntityByIndex<CCSPlayerController>(e.Userid);

            // Write attacker name in the colour of their team
            AnsiConsole.Markup($"[{TeamNumberToString(attacker?.CSTeamNumber)}]{attacker?.m_iszPlayerName}[/]");

            // Write the weapon
            AnsiConsole.Markup(" <");
            AnsiConsole.Markup(e.Weapon);
            if (e.Headshot)
                AnsiConsole.Markup(" HS");
            AnsiConsole.Markup("> ");

            // Write the victim's name in the colour of their team
            AnsiConsole.MarkupLine($"[{TeamNumberToString(victim?.CSTeamNumber)}]{victim?.m_iszPlayerName}[/]");
        };

        demo.Source1GameEvents.RoundEnd += e =>
        {
            var roundEndReason = (CSRoundEndReason) e.Reason;
            var winningTeam = (CSTeamNumber) e.Winner switch
            {
                CSTeamNumber.Terrorist => demo.TeamTerrorist,
                CSTeamNumber.CounterTerrorist => demo.TeamCounterTerrorist,
                _ => null
            };

            AnsiConsole.MarkupLine($"\n[white]>>> Round end: {roundEndReason}[/]");
            AnsiConsole.MarkupLine($"  Winner: [{TeamNumberToString((CSTeamNumber) e.Winner)}]{winningTeam?.m_szClanTeamname}[/]");
            AnsiConsole.MarkupLine($"  {demo.GameRules.m_nRoundsPlayedThisPhase} rounds played in {demo.GameRules.CSGamePhase}");
            AnsiConsole.MarkupLine($"  Scores: [red]{demo.TeamTerrorist.m_szClanTeamname}[/] {demo.TeamTerrorist.m_iScore} - {demo.TeamCounterTerrorist.m_iScore} [blue]{demo.TeamCounterTerrorist.m_szClanTeamname}[/]");
            AnsiConsole.WriteLine("");
        };

        // Now that we've attached the event listeners, start reading the demo
        var sw = Stopwatch.StartNew();
        await demo.Start(File.OpenRead(path));
        sw.Stop();

        var ticks = demo.CurrentDemoTick.Value;
        Console.WriteLine($"\nFinished! Parsed {ticks:N0} ticks ({demo.CurrentTime.Value:N1} game secs) in {sw.Elapsed.TotalSeconds:0.000} secs ({ticks * 1000 / sw.Elapsed.TotalMilliseconds:N1} ticks/sec)");
    }

    private static string TeamNumberToString(CSTeamNumber? csTeamNumber) => csTeamNumber switch
    {
        CSTeamNumber.Terrorist => "red",
        CSTeamNumber.CounterTerrorist => "blue",
        _ => "white",
    };
}