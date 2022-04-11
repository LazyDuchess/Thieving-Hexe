/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_CAMPFIRE = 4000411161U;
        static const AkUniqueID PLAY_CAULDRONBUBBLES = 210028557U;
        static const AkUniqueID PLAY_CRYSTAL__PICKUP = 1152667966U;
        static const AkUniqueID PLAY_CRYSTAL_EMPTY = 288321370U;
        static const AkUniqueID PLAY_CRYSTAL_LARGE_USE = 1263063560U;
        static const AkUniqueID PLAY_CRYSTAL_SMALL_USE = 429735360U;
        static const AkUniqueID PLAY_ENEM_ATK_BSC_CHARGED = 2157278666U;
        static const AkUniqueID PLAY_ENEM_ATK_BSC_LTE = 2634303667U;
        static const AkUniqueID PLAY_ENEM_DEFEATED = 279773806U;
        static const AkUniqueID PLAY_ENEM_MIN_ON_FIRE = 3897903487U;
        static const AkUniqueID PLAY_ENEM_REC_GEN_DMG_HIT = 1064632258U;
        static const AkUniqueID PLAY_ENEM_RECV_FIRE_DMG = 924170866U;
        static const AkUniqueID PLAY_ENEM_SKEL_FS_MATERIAL = 2027687623U;
        static const AkUniqueID PLAY_ENEM_SKELLE_DEFEAT = 3332044750U;
        static const AkUniqueID PLAY_ENEM_SKELLE_FS_MATERIAL = 432265666U;
        static const AkUniqueID PLAY_FIREGEM_FLIGHT = 4040184904U;
        static const AkUniqueID PLAY_GUARDBLURB = 784653928U;
        static const AkUniqueID PLAY_ITM_DRINK_POTION = 2003385513U;
        static const AkUniqueID PLAY_ITM_PU_KEY = 2258692236U;
        static const AkUniqueID PLAY_ITM_PU_POTION = 873733766U;
        static const AkUniqueID PLAY_MUSIC = 2932040671U;
        static const AkUniqueID PLAY_OBJ_DOOR_CLOSE = 675557581U;
        static const AkUniqueID PLAY_OBJ_DOOR_OPEN = 1417455751U;
        static const AkUniqueID PLAY_PLAYERFSMATERIAL = 3383738331U;
        static const AkUniqueID PLAY_PLYR_ATK_BSC_LTE = 2754623237U;
        static const AkUniqueID PLAY_PLYR_ATK_CHRG_LOOP = 2613753308U;
        static const AkUniqueID PLAY_PLYR_ATK_CHRG_RELEASE = 843314701U;
        static const AkUniqueID PLAY_PLYR_ATK_CHRG_WU = 2731356750U;
        static const AkUniqueID PLAY_PLYR_DEFEATED = 2464356432U;
        static const AkUniqueID PLAY_PLYR_ELCTY_WTR = 856894529U;
        static const AkUniqueID PLAY_PLYR_PO_FIRE_W_WTR = 2930802274U;
        static const AkUniqueID PLAY_PLYR_REC_GEN_DMG = 3674195162U;
        static const AkUniqueID PLAY_PLYR_RECV_FIRE_DMG = 149588724U;
        static const AkUniqueID PLAY_TORCH = 2025845440U;
        static const AkUniqueID PLAY_UI_MENUSELECT = 1999625262U;
        static const AkUniqueID PLAY_UI_ROLLOVER = 4038800916U;
        static const AkUniqueID STOP_PLAYERFSMATERIAL = 2251837801U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace ENVIRO
        {
            static const AkUniqueID GROUP = 1995189622U;

            namespace STATE
            {
                static const AkUniqueID CASTLE = 129146593U;
                static const AkUniqueID FOREST = 491961918U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace ENVIRO

        namespace GAMEPLAY
        {
            static const AkUniqueID GROUP = 89505537U;

            namespace STATE
            {
                static const AkUniqueID BOSS = 1560169506U;
                static const AkUniqueID COMBAT = 2764240573U;
                static const AkUniqueID DEFEAT = 1593864692U;
                static const AkUniqueID EXPLORE = 579523862U;
                static const AkUniqueID MENU = 2607556080U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID VICTORY = 2716678721U;
            } // namespace STATE
        } // namespace GAMEPLAY

    } // namespace STATES

    namespace SWITCHES
    {
        namespace CRYSTALTYPE
        {
            static const AkUniqueID GROUP = 2418394769U;

            namespace SWITCH
            {
                static const AkUniqueID ELECTRICITY = 2917121896U;
                static const AkUniqueID FIRE = 2678880713U;
                static const AkUniqueID GENERIC = 4294388576U;
                static const AkUniqueID WATER = 2654748154U;
            } // namespace SWITCH
        } // namespace CRYSTALTYPE

        namespace ENEMRUNWALK
        {
            static const AkUniqueID GROUP = 2680486766U;

            namespace SWITCH
            {
                static const AkUniqueID ENMSKRUN = 3672549870U;
                static const AkUniqueID ENMSKWALK = 2293070800U;
            } // namespace SWITCH
        } // namespace ENEMRUNWALK

        namespace PLAYERRUNWALK
        {
            static const AkUniqueID GROUP = 3176616166U;

            namespace SWITCH
            {
                static const AkUniqueID RUN = 712161704U;
                static const AkUniqueID WALK = 2108779966U;
            } // namespace SWITCH
        } // namespace PLAYERRUNWALK

        namespace PLAYERSTATUS
        {
            static const AkUniqueID GROUP = 3800848640U;

            namespace SWITCH
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID DEFEATED = 2791675679U;
                static const AkUniqueID HLFHEALTH = 2673177675U;
                static const AkUniqueID QRTHEALTH = 1834963222U;
                static const AkUniqueID TQHEALTH = 3546734882U;
            } // namespace SWITCH
        } // namespace PLAYERSTATUS

        namespace SURFACE
        {
            static const AkUniqueID GROUP = 1834394558U;

            namespace SWITCH
            {
                static const AkUniqueID COBBLE = 3135525842U;
                static const AkUniqueID DIRT = 2195636714U;
                static const AkUniqueID GRASS = 4248645337U;
            } // namespace SWITCH
        } // namespace SURFACE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID MUSICVOLUME = 2346531308U;
        static const AkUniqueID PLAYERHEALTH = 151362964U;
        static const AkUniqueID PLAYERSPEED = 1493153371U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIENCE = 85412153U;
        static const AkUniqueID BOSS = 1560169506U;
        static const AkUniqueID C_COMBAT = 2588151145U;
        static const AkUniqueID C_EXPLORE = 1348890866U;
        static const AkUniqueID CAMPFIRE = 1931646578U;
        static const AkUniqueID CASTLEBUS = 1239215153U;
        static const AkUniqueID DEFEAT = 1593864692U;
        static const AkUniqueID ENEMIES = 2242381963U;
        static const AkUniqueID F_COMBAT = 1367030554U;
        static const AkUniqueID F_EXPLORE = 1325345903U;
        static const AkUniqueID FORESTBUS = 199201492U;
        static const AkUniqueID ITEMS = 2151963051U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MENU = 2607556080U;
        static const AkUniqueID MUSICGRP = 3272928167U;
        static const AkUniqueID MUSICRTPC = 2038898903U;
        static const AkUniqueID OBJECTS = 1695690031U;
        static const AkUniqueID PLATFORMS = 1764819763U;
        static const AkUniqueID PLAYER = 1069431850U;
        static const AkUniqueID REVERBS = 3545700988U;
        static const AkUniqueID UI = 1551306167U;
        static const AkUniqueID VICTORY = 2716678721U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID REVERBCASTLE = 3128068529U;
        static const AkUniqueID REVERBCASTLEHALLWAY = 865895389U;
        static const AkUniqueID REVERBCASTLEOUTDOORS = 2895998378U;
        static const AkUniqueID REVERBFOREST = 2493188974U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
