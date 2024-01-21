﻿using System.Text.RegularExpressions;
using Dalamud.Interface;
using Dalamud.Interface.Components;
using Dalamud.Interface.GameFonts;
using Dalamud.Interface.ManagedFontAtlas;
using Dalamud.Interface.Utility;
using DeeperDeepDungeonDex.Storage;
using ImGuiNET;

namespace DeeperDeepDungeonDex.System;

public partial interface IDrawableFloorSet {
    public string? Title { get; set; }
    public DeepDungeonType DungeonType { get; set; }
    public string? MimicType { get; set; }
    public string? Rooms { get; set; }
    public string? Chests { get; set; }
    public string? Enemies { get; set; }
    public string? KillsNeeded { get; set; }
    public string? RespawnRate { get; set; }
    public string? Reward { get; set; }
    public string? Notes { get; set; }

    private static IFontHandle? LargeFont;

    public void Draw() {
        LargeFont ??= Services.PluginInterface.UiBuilder.FontAtlas.NewGameFontHandle(new GameFontStyle(GameFontFamilyAndSize.Axis36));
        
        if (ImGui.BeginTable("##FloorsetTable", 3, ImGuiTableFlags.NoClip)) {
            ImGui.TableNextColumn();
            using (LargeFont.Push()) {
                ImGui.TextUnformatted(Title);
            }

            ImGui.TableNextColumn();
            ImGui.TableNextColumn();
            ImGui.SetCursorPosX(ImGui.GetContentRegionMax().X - 23.0f * ImGuiHelpers.GlobalScale);
            if (ImGuiComponents.IconButton("Button", FontAwesomeIcon.Times)) {
                Plugin.Controller.WindowController.RemoveWindowForFloor(this);
            }
            
            DrawSpacingRow();

            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            DrawIconString(FontAwesomeIcon.Box, MimicType, "Mimic Type");
            
            ImGui.TableNextColumn();
            DrawIconString(FontAwesomeIcon.Home, Rooms, "Number of Rooms per floor");
            
            ImGui.TableNextColumn();
            DrawIconString(FontAwesomeIcon.Gem, Chests, "Number of Chests per floor");
            
            ImGui.TableNextColumn();
            DrawIconString(FontAwesomeIcon.Bugs, Enemies, "Number of Enemies per floor");

            ImGui.TableNextColumn();
            DrawIconString(FontAwesomeIcon.SkullCrossbones, KillsNeeded, "Kills Needed per floor");
            
            ImGui.TableNextColumn();
            DrawIconString(FontAwesomeIcon.Recycle, RespawnRate, "Enemy Respawn Rate");

            ImGui.TableNextColumn();
            DrawIconString(FontAwesomeIcon.Coins, Reward, "Hoard Reward");
            
            ImGui.EndTable();
        }
        
        ImGui.Separator();
        
        if (Notes is not null) {
            Notes = Notes.TrimStart();
            Notes = Notes.Replace("\r\n\r\n", "ABC");
            Notes = Notes.Replace("\r\n", " ");
            Notes = Notes.Replace("ABC", "\n");
            Notes = MarkdownLinks().Replace(Notes, "$1");
            
            foreach (var line in Notes.Split("\n")) {
                ImGuiHelpers.ScaledDummy(5.0f);
                ImGuiHelpers.SafeTextWrapped(line);
            }
        }
    }
    
    private static void DrawSpacingRow() {
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Separator();
        ImGuiHelpers.ScaledDummy(3.0f);

        ImGui.TableNextColumn();
        ImGui.Separator();
        ImGuiHelpers.ScaledDummy(3.0f);

        ImGui.TableNextColumn();
        ImGui.Separator();
        ImGuiHelpers.ScaledDummy(3.0f);
    }

    private static void DrawIconString(FontAwesomeIcon icon, string? label, string tooltip) {
        ImGui.BeginGroup();
        ImGui.PushFont(UiBuilder.IconFont);
        ImGui.TextUnformatted(icon.ToIconString());
        ImGui.PopFont();
        if (label is not null) {
            ImGui.AlignTextToFramePadding();
            ImGui.SameLine();
            ImGui.TextUnformatted(label);
        }
        ImGui.EndGroup();
        
        if (ImGui.IsItemHovered() && label is not null) {
            ImGui.SetTooltip(tooltip);
        }
        
        ImGuiHelpers.ScaledDummy(3.0f);
    }

    [GeneratedRegex("\\[(.*?)\\]\\((.*?)\\)")]
    private static partial Regex MarkdownLinks();
}
