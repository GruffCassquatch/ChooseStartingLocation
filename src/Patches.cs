using System;
using HarmonyLib;
using UnityEngine;
using MelonLoader;
using System.Collections.Generic;

namespace ChooseStartingLocation
{
    class Patches
    {
        [HarmonyPatch(typeof(GameManager), "LaunchSandbox")]
        internal class OverridePlayerSpawn
        {
            private static bool Prefix()
            {
                if (Settings.settings.modFunction == ModFunction.Disabled) return true;
                ExperienceModeType xpMode = ExperienceModeManager.GetCurrentExperienceModeType();

                if (xpMode == ExperienceModeType.Pilgrim || xpMode == ExperienceModeType.Voyageur || xpMode == ExperienceModeType.Stalker || xpMode == ExperienceModeType.Interloper || xpMode == ExperienceModeType.Custom)
                {
                    SaveGameSlots.ClearAutoSave();
                    SaveGameSlotHelper.ClearSaveSlotsLists();

                    if (Settings.settings.modFunction == ModFunction.LocationList)
                    {
                        Implementation.startLocation = LocationList.GetLocation(Settings.settings.region);
                    }
                    else if (Settings.settings.modFunction == ModFunction.CustomCoords)
                    {
                        Implementation.startLocation = new Location("Custom Coords", (Region)Enum.Parse(typeof(CustomRegion), Settings.settings.customRegion.ToString()), Settings.settings.x, Settings.settings.y, Settings.settings.z, Settings.settings.rotationX, Settings.settings.rotationY);
                    }

                    GameManager.m_StartRegion = (GameRegion) Enum.Parse(typeof(Region), Implementation.startLocation.region.ToString());

                    GameManager.InstantiateSandboxManager();
                    string commandToRunAfterLoad = "mission_jump null false";

                    bool flag = SpecialEvent.MatchesExperienceMode(xpMode);

                    InterfaceManager.m_Panel_Loading.m_HoldScreenAfterLoad = true;
                    InterfaceManager.m_Panel_Loading.m_ShowQuoteAfterLoad = Utils.XPModeIsSandbox(xpMode);
                    InterfaceManager.m_Panel_Loading.m_CommandToRunAfterLoad = commandToRunAfterLoad;
                    InterfaceManager.m_Panel_Loading.m_SaveAfterLoad = flag;
                    UIInput.selection = null;
                    InterfaceManager.m_Panel_Inventory.ResetFilter();
                    StatsManager.Reset();
                    InterfaceManager.m_Panel_Log.Reset();
                    InterfaceManager.m_Panel_SprayPaint.Reset();
                    InterfaceManager.m_Panel_HUD.CleanupDamageEventTable();
                    InterfaceManager.m_Panel_OptionsMenu.m_State.m_NumGamesPlayed++;
                    GameManager.FadeOutSceneAudio();
                    GameManager.m_SceneTransitionData = new SceneTransitionData();
                    GameManager.m_SceneTransitionData.m_GameRandomSeed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
                    GameManager.m_PendingSave = !flag;

                    GameManager.LoadSceneWithLoadingScreen(Implementation.startLocation.scene);

                    //MelonLogger.Msg("LAUNCH SANDBOX PREFIX: NEW START SCENE: " + Implementation.startLocation.scene);
                    return false;
                }
                return true;
            }
        }
        
        [HarmonyPatch(typeof(StartGear), "AddAllToInventory")]
        internal class CustomStartingLocation
        {
            private static void Postfix()
            {
                if (GameManager.m_ActiveScene == "MainMenu") return;
                if (Settings.settings.modFunction == ModFunction.Disabled) return;

                if (!Implementation.startLocation.teleport)
                {
                    //MelonLogger.Msg("Teleport flag FALSE for " + Implementation.startLocation.name);
                    return;
                }

                //MelonLogger.Msg("TELEPORTING TO: " + Implementation.startLocation.name);

                if (Implementation.startLocation.position == Vector3.zero)
                {
                    MelonLogger.Msg("ERROR: Location " + Implementation.startLocation.name + " has no coordinates!");
                    return;
                }
                TeleportToSpawnPoint(Implementation.startLocation.position, Implementation.startLocation.rotation);
                GameManager.GetPlayerManagerComponent().StickPlayerToGround();
            }
        }

        public static void TeleportToSpawnPoint(Vector3 position, Quaternion rotation)
        {
            ExperienceModeType xpMode = ExperienceModeManager.GetCurrentExperienceModeType();

            GameManager.GetPlayerManagerComponent().TeleportPlayer(position, rotation);

            if (xpMode == ExperienceModeType.Custom)
            {
                if (GameManager.GetCustomMode().m_StartWeather == CustomExperienceModeManager.CustomTunableWeather.Blizzard)
                {
                    GameManager.GetWeatherTransitionComponent().ActivateWeatherSetAtFrac(WeatherStage.Blizzard, 0.5f);
                }
                else if (GameManager.GetCustomMode().m_StartWeather == CustomExperienceModeManager.CustomTunableWeather.Clear)
                {
                    GameManager.GetWeatherTransitionComponent().ActivateWeatherSetAtFrac(WeatherStage.Clear, 0.5f);
                }
                else if (GameManager.GetCustomMode().m_StartWeather == CustomExperienceModeManager.CustomTunableWeather.DenseFog)
                {
                    GameManager.GetWeatherTransitionComponent().ActivateWeatherSetAtFrac(WeatherStage.DenseFog, 0.5f);
                }
                else if (GameManager.GetCustomMode().m_StartWeather == CustomExperienceModeManager.CustomTunableWeather.HeavySnow)
                {
                    GameManager.GetWeatherTransitionComponent().ActivateWeatherSetAtFrac(WeatherStage.HeavySnow, 0.5f);
                }
                else if (GameManager.GetCustomMode().m_StartWeather == CustomExperienceModeManager.CustomTunableWeather.LightFog)
                {
                    GameManager.GetWeatherTransitionComponent().ActivateWeatherSetAtFrac(WeatherStage.LightFog, 0.5f);
                }
                else if (GameManager.GetCustomMode().m_StartWeather == CustomExperienceModeManager.CustomTunableWeather.LightSnow)
                {
                    GameManager.GetWeatherTransitionComponent().ActivateWeatherSetAtFrac(WeatherStage.LightSnow, 0.5f);
                }
                else if (GameManager.GetCustomMode().m_StartWeather == CustomExperienceModeManager.CustomTunableWeather.Random)
                {
                    GameManager.GetStartSettingsComponent().SetWeather();
                }
                if (GameManager.GetCustomMode().m_StartTimeOfDay == CustomExperienceModeManager.CustomTunableTimeOfDay.Dawn)
                {
                    GameManager.GetTimeOfDayComponent().SetNormalizedTime(0.28f);
                }
                else if (GameManager.GetCustomMode().m_StartTimeOfDay == CustomExperienceModeManager.CustomTunableTimeOfDay.Noon)
                {
                    GameManager.GetTimeOfDayComponent().SetNormalizedTime(0.5f);
                }
                else if (GameManager.GetCustomMode().m_StartTimeOfDay == CustomExperienceModeManager.CustomTunableTimeOfDay.Dusk)
                {
                    GameManager.GetTimeOfDayComponent().SetNormalizedTime(0.78f);
                }
                else if (GameManager.GetCustomMode().m_StartTimeOfDay == CustomExperienceModeManager.CustomTunableTimeOfDay.Midnight)
                {
                    GameManager.GetTimeOfDayComponent().SetNormalizedTime(0f);
                }
                else if (GameManager.GetCustomMode().m_StartTimeOfDay == CustomExperienceModeManager.CustomTunableTimeOfDay.Random)
                {
                    GameManager.GetStartSettingsComponent().SetRandomTime();
                }
            }
            else
            {
                GameManager.GetStartSettingsComponent().SetTime();
                GameManager.GetStartSettingsComponent().SetWeather();
            }
        }
    }
}

