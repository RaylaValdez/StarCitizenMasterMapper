using System;
using System.Collections.Generic;
using SCBXML2TXT;

namespace Star_Shitizen_Master_Mapping;

/*
Sub Categories

On Foot 
        Movement
        Combat
        Equipment
        Quick Keys

EVA & Zero G Traversal
        Movement
Flight
        Movement
        Combat
        Cockpit
        Systems
Vehicles
        Movement
        Combat
Turrets
        Movement
        Combat
Social, Quick Keys, Lights
        Mobiglass
        VOIP & FOIP
        HeadTracking
        Social
        Emotes
        Lights
        Interactions
Spectator
        Movement
        Camera
Camera
        Movement
        Focus
*/

public class BindingsList
{
    public static Dictionary<string, string[]> CategoryOrder = new() {
        {
            "On Foot",
            ["Movement", "Combat", "Equipment", "Quick Keys"]
        },
        {
            "Flight",
            ["Movement", "Combat", "Cockpit", "Systems"]
        },
        {
            "EVA & Zero G Traversal",
            ["Movement"]
        },
        {
            "Vehicles",
            ["Movement", "Combat"]
        },
        {
            "Turrets",
            ["Movement", "Combat"]
        },
        {
            "Social, Quick Keys, Lights",
            ["Mobiglass", "VOIP & VOIP", "HeadTracking", "Social", "Emotes", "Lights", "Interactions"]
        },
        {
            "Spectator",
            ["Movement", "Camera"]
        },
        {
            "Camera",
            ["Movement", "Focus"]
        },
    };

    public static List<MyBinds> Bindings = new()
    {
#region spaceship_general
        new MyBinds("spaceship_general", "v_eject", "Flight", "Cockpit", "Eject"),
        new MyBinds("spaceship_general", "v_eject_cinematic", "Flight", "Cockpit", "Cinematic Eject"),
        new MyBinds("spaceship_general", "v_exit", "Flight", "Cockpit", "Exit"),
        new MyBinds("spaceship_general", "v_self_destruct", "Flight", "Cockpit", "Self Destruct (ON/Off)"),
        new MyBinds("spaceship_general", "v_starmap", "Social, Quick Keys, Lights", "Mobiglass", "Starmap"),
        new MyBinds("spaceship_general", "v_cooler_throttle_up", "Flight", "Systems", "Increase Cooler Rate"),
        new MyBinds("spaceship_general", "v_cooler_throttle_down", "Flight", "Systems", "Decrease Cooler Rate"),
        new MyBinds("spaceship_general", "v_flightready", "Flight", "Cockpit", "Flight Ready"),
        new MyBinds("spaceship_general", "v_toggle_all_doors", "Flight", "Cockpit", "Toggle All Doors"),
        new MyBinds("spaceship_general", "v_open_all_doors", "Flight", "Cockpit", "Open All Doors"),
        new MyBinds("spaceship_general", "v_close_all_doors", "Flight", "Cockpit", "Close All Doors"),
        new MyBinds("spaceship_general", "v_toggle_all_doorlocks", "Flight", "Cockpit", "Toggle All Door Locks"),
        new MyBinds("spaceship_general", "v_lock_all_doors", "Flight", "Cockpit", "Lock All Doors"),
        new MyBinds("spaceship_general", "v_unlock_all_doors", "Flight", "Cockpit", "Unlock All Doors"),
        new MyBinds("spaceship_general", "v_toggle_all_portlocks", "Flight", "Cockpit", "Toggle All Port Locks"),
        new MyBinds("spaceship_general", "v_lock_all_ports", "Flight", "Cockpit", "Lock All Ports"),
        new MyBinds("spaceship_general", "v_unlock_all_ports", "Flight", "Cockpit", "Unlock All Ports"),
#endregion

#region spaceship_view
        new MyBinds("spaceship_view", "v_view_yaw_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_yaw_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_yaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_yaw_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_yaw_absolute", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_pitch_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_pitch_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_pitch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_pitch_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_pitch_absolute", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_cycle_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_cycle_internal_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_option", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_zoom_in", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_zoom_out", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_interact", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_freelook_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_dynamic_zoom_rel", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_dynamic_zoom_rel_in", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_dynamic_zoom_rel_out", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_dynamic_zoom_abs", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_dynamic_zoom_abs_toggle", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_look_behind", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_screen_focus_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_screen_focus_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_screen_focus_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_view", "v_view_screen_focus_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spaceship_movement
        new MyBinds("spaceship_movement", "v_pitch_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_pitch_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_pitch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_pitch_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_yaw_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_yaw_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_yaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_yaw_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_toggle_relative_mouse_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_roll_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_roll_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_roll", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_toggle_yaw_roll_swap", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_target_match_vel", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_brake", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_accel_scale_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_accel_scale_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_accel_scale_rel", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_accel_scale_abs", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_speed_range_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_speed_range_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_speed_range_rel", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_speed_range_abs", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_longitudinal_invert", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_ifcs_toggle_vector_decoupling", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_vertical", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_lateral", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_forward_abs", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_back_abs", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_longitudinal_abs", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_forward_rel", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_back_rel", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_strafe_longitudinal_rel", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_ifcs_toggle_speed_limiter", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_ifcs_toggle_gforce_safety", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_ifcs_toggle_esp", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_ifcs_toggle_cruise_control", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_afterburner", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_boost", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_toggle_landing_system", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_autoland", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_toggle_qdrive_spooling", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_movement", "v_toggle_qdrive_engagement", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spaceship_targeting
        new MyBinds("spaceship_targeting", "v_look_ahead_enable", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_toggle_weapon_gimbal_lock", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_toggle_lock_selected", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_reset_lock", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_nearest_hostile", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_toggle_pin_selected", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_pinned_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_pinned_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_toggle_lock_index_1", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_toggle_lock_index_2", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_toggle_lock_index_3", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_toggle_pin_index_1", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_toggle_pin_index_2", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_toggle_pin_index_3", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_remove_all_pins", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_selection_hostile_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_selection_hostile_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_selection_friendly_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_selection_friendly_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_selection_all_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_selection_all_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_reset_selection", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_subitem_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_subitem_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_subitem_category_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_subitem_category_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_reset_subtargeting", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_target_cycle_reticle_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_look_ahead_start_target_tracking", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_targeting", "v_invoke_ping", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spaceship_scanning
        new MyBinds("spaceship_scanning", "v_toggle_scan_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_scanning", "v_scanning_trigger_scan", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spaceship_mining
        new MyBinds("spaceship_mining", "v_toggle_mining_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_mining", "v_toggle_mining_laser_type", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_mining", "v_toggle_mining_laser_fire", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_mining", "v_increase_mining_throttle", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_mining", "v_decrease_mining_throttle", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_mining", "v_mining_throttle", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spaceship_turret
        new MyBinds("spaceship_turret", "v_toggle_weapon_gimbal_lock", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_aim_yaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_aim_yaw_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_aim_yaw_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_aim_yaw_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_aim_pitch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_aim_pitch_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_aim_pitch_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_aim_pitch_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_aim_snap", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_toggle_lock_selected", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_toggle_reset_lock", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_cycle_selection_all_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_cycle_selection_all_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_cycle_selection_friendly_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_cycle_selection_friendly_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_toggle_pin_selected", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_cycle_selection_hostile_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_cycle_selection_hostile_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_nearest_hostile", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_nearest_friendly", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_cycle_subitem_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_turret", "v_target_cycle_subitem_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spaceship_weapons
        new MyBinds("spaceship_weapons", "v_attack_all", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_weapons", "v_attack_group1", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_weapons", "v_attack_group2", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_weapons", "v_weapon_cycle_ammo_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_weapons", "v_weapon_cycle_ammo_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_weapons", "v_turret_gyromode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spaceship_missiles
        new MyBinds("spaceship_missiles", "v_weapon_launch_missile", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_missiles", "v_weapon_toggle_missile_lock", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spaceship_defensive
        new MyBinds("spaceship_defensive", "v_weapon_countermeasure_cinematic", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_defensive", "v_weapon_countermeasure_launch_decoy", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_defensive", "v_weapon_countermeasure_launch_noise", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_defensive", "v_weapon_countermeasure_launch_all", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_defensive", "v_shield_raise_level_forward", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_defensive", "v_shield_raise_level_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_defensive", "v_shield_raise_level_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_defensive", "v_shield_raise_level_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_defensive", "v_shield_raise_level_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_defensive", "v_shield_raise_level_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_defensive", "v_shield_reset_level", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spaceship_power
        new MyBinds("spaceship_power", "v_power_focus_weapons", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_power", "v_power_focus_shields", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_power", "v_power_focus_thrusters", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_power", "v_power_reset_focus", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_power", "v_power_throttle_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_power", "v_power_throttle_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_power", "v_power_throttle_max", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_power", "v_power_throttle_min", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_power", "v_power_toggle_weapons", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_power", "v_power_toggle_shields", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_power", "v_power_toggle_thrusters", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_power", "v_power_toggle", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spaceship_radar
        new MyBinds("spaceship_radar", "v_radar_toggle_onoff", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_radar", "v_radar_toggle_active_or_passive", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_radar", "v_radar_cycle_mode_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_radar", "v_radar_cycle_mode_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_radar", "v_radar_cycle_zoom_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_radar", "v_radar_cycle_zoom_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_radar", "v_radar_cycle_focus_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_radar", "v_radar_cycle_focus_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_radar", "v_radar_toggle_view_focus", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spaceship_hud
        new MyBinds("spaceship_hud", "mobiglas", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "toggle_ar_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_open_scoreboard", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_interact_toggle", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_cycle_mode_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_cycle_mode_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_focused_cycle_mode_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_focused_cycle_mode_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_left_panel_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_left_panel_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_left_panel_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_left_panel_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_confirm", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_cancel", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_stick_x", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_hud_stick_y", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_comm_open_chat", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_comm_show_chat", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_comm_open_precanned", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_comm_select_precanned_1", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_comm_select_precanned_2", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_comm_select_precanned_3", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_comm_select_precanned_4", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_comm_select_precanned_5", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spaceship_hud", "v_starmap", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region lights_controller
        new MyBinds("lights_controller", "v_lights", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("lights_controller", "v_toggle_cabin_lights", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("lights_controller", "v_toggle_running_lights", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region player
        new MyBinds("player", "moveleft", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "moveright", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "moveforward", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "moveback", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "rotateyaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "rotatepitch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "gp_movex", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "gp_movey", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "gp_rotateyaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "gp_rotatepitch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "jump", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "gp_jump", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "crouch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "gp_crouch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "prone", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "sprint", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "walk", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "leanleft", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "leanright", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "toggle_lowered", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "attack1", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "weapon_melee", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "grenade", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "zoom", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "zoom_out", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "zoom_in", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "zoom_in_out", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "decelerate", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "selectpistol", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "selectprimary", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "selectsecondary", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "selectgadget", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "nextweapon", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "prevweapon", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "nextitem", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "prevItem", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "reload", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "holster", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "drop", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "inspect", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "stabilize", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "weapon_change_firemode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "fixed_speed_increment", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "fixed_speed_decrement", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "use", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "interact", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "useAttachmentBottom", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "useAttachmentTop", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "toggle_flashlight", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "combathealtarget", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "visor_next_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "visor_prev_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "selectitem", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "cancelselect", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "thirdperson", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "toggle_cursor_input", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "free_thirdperson_camera", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "pan_thirdperson_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "pan_thirdperson_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "break_conversation_effects", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "hmd_rotateyaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "hmd_rotatepitch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "hmd_rotateroll", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "mobiglas", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "pl_hud_open_scoreboard", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "pl_hud_confirm", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "toggle_ar_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "ar_mode_scroll_action_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "ar_mode_scroll_action_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "shop_camera_zoom_in", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "shop_camera_zoom_out", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "v_eject", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "v_eject_cinematic", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "spectate_enterpuremode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "port_modification_select", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "v_starmap", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player", "force_respawn", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region prone
        new MyBinds("prone", "prone_rollleft", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("prone", "prone_rollright", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region zero_gravity_eva
        new MyBinds("zero_gravity_eva", "eva_view_yaw_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_view_yaw_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_view_yaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_view_yaw_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_view_pitch_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_view_pitch_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_view_pitch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_view_pitch_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_pitch_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_pitch_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_pitch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_yaw_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_yaw_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_yaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_roll_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_roll_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_roll", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_strafe_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_strafe_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_strafe_vertical", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_strafe_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_strafe_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_strafe_lateral", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_strafe_forward", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_strafe_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_strafe_longitudinal", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_brake", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_boost", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("zero_gravity_eva", "eva_toggle_headlook_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region vehicle_general
        new MyBinds("vehicle_general", "v_exit", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_horn", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_lights", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_cycle_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_option", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_zoom_in", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_zoom_out", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_yaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_yaw_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_pitch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_pitch_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_look_behind", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_toggle_cursor_input", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_target_cycle_all_fwd", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_yaw_absolute", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_pitch_absolute", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_view_roll_absolute", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "mobiglas", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_self_destruct", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_eject", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_attack_all", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_attack_group1", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_attack_group2", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_general", "v_starmap", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region vehicle_driver
        new MyBinds("vehicle_driver", "v_yaw_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_yaw_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_yaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_roll_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_roll_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_move_forward", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_move_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_brake", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_attack_all", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_attack_group1", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_attack_group2", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_view_dynamic_zoom_rel", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_view_dynamic_zoom_rel_in", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_view_dynamic_zoom_rel_out", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_view_dynamic_zoom_abs", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_view_dynamic_zoom_abs_toggle", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_driver", "v_boost", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region vehicle_gunner
        new MyBinds("vehicle_gunner", "v_attack_all", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_gunner", "v_attack_group1", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("vehicle_gunner", "v_attack_group2", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region multiplayer
        new MyBinds("multiplayer", "respawn", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("multiplayer", "force_respawn", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("multiplayer", "retry", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("multiplayer", "ready", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region spectator
        new MyBinds("spectator", "spectate_next_target", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_prev_target", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_toggle_lock_target", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_zoom", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_zoom_in", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_zoom_out", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_rotateyaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_rotateyaw_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_rotatepitch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_rotatepitch_mouse", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_toggle_hud", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_gen_nextcamera", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_gen_nextmode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_gen_prevmode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_moveleft", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_moveright", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_moveforward", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_moveback", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_moveup", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_movedown", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_freecam_sprint", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("spectator", "spectate_toggle_freecam", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region default
        new MyBinds("default", "toggle_contact", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("default", "toggle_chat", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("default", "focus_on_chat_textinput", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("default", "foip_pushtotalk", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("default", "foip_pushtotalk_proximity", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("default", "foip_pushtoheadtrack", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("default", "foip_viewownplayer", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("default", "foip_recalibrate", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("default", "foip_cyclechannel", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region ui_notification
        new MyBinds("ui_notification", "ui_notification_accept", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("ui_notification", "ui_notification_decline", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("ui_notification", "ui_notification_ignore", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region player_emotes
        new MyBinds("player_emotes", "emote_cs_forward", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_cs_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_cs_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_cs_stop", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_cs_yes", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_cs_no", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_agree", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_angry", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_atease", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_attention", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_blah", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_bored", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_bow", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_burp", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_cheer", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_chicken", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_clap", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_come", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_cry", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_dance", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_disagree", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_failure", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_flex", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_flirt", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_gasp", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_gloat", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_greet", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_laugh", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_point", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_rude", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_salute", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_sit", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_sleep", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_smell", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_taunt", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_threaten", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_wait", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_wave", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_emotes", "emote_whistle", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region player_choice
        new MyBinds("player_choice", "pc_primary_interaction", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_secondary_interactions", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_radial_xaxis", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_radial_yaxis", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_throw_decrease", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_zoom_in", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_zoom_out", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_interaction_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_select", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_focus", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_yaw", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_pitch", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_screen_focus_left", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_screen_focus_right", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_screen_focus_up", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_screen_focus_down", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_personal_thought", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_personal_index1", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_personal_index2", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_personal_index3", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_personal_index4", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_personal_index5", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_personal_index6", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_personal_back", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("player_choice", "pc_interaction_quick", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

#region view_director_mode
        new MyBinds("view_director_mode", "view_enable_camview_mode", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_switch_to_alternative", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_save_view_1", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_save_view_2", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_save_view_3", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_save_view_4", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_save_view_5", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_save_view_6", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_save_view_7", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_save_view_8", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_save_view_9", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_load_view_1", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_load_view_2", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_load_view_3", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_load_view_4", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_load_view_5", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_load_view_6", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_load_view_7", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_load_view_8", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_load_view_9", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_reset_saved", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_move_target_X_pos", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_move_target_X_neg", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_move_target_Y_pos", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_move_target_Y_neg", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_move_target_Z_pos", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_move_target_Z_neg", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_fov_in", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_fov_out", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_fstop_in", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_fstop_out", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
        new MyBinds("view_director_mode", "view_restore_defaults", "PH_displayCategory", "PH_subcategory", "PH_displayName"),
#endregion

    };
}