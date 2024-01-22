using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{

    List<long> experienceLevel = new List<long>();
    List<int> healthFromEndurancePointsLevel = new List<int>();
    List<int> manaFromIntelectPointsLevel = new List<int>();
    List<int> manaFromWisdomPointsLevel = new List<int>();
    public long nextLevelExperience;
    public long currentLevelExperience;

    public void Awake()
    {
        // Level Steps
        experienceLevel.Add(0);
        experienceLevel.Add(1000);
        experienceLevel.Add(5700);
        experienceLevel.Add(15900);
        experienceLevel.Add(33000);
        experienceLevel.Add(58100);
        experienceLevel.Add(92600);
        experienceLevel.Add(137400);
        experienceLevel.Add(193700);
        experienceLevel.Add(262450);
        experienceLevel.Add(344700);
        experienceLevel.Add(441442);
        experienceLevel.Add(553700);
        experienceLevel.Add(682500);
        experienceLevel.Add(828700);
        experienceLevel.Add(993480);
        experienceLevel.Add(1177700);
        experienceLevel.Add(1382400);
        experienceLevel.Add(1608300);
        experienceLevel.Add(1857000);
        experienceLevel.Add(2128800);
        experienceLevel.Add(2425300);
        experienceLevel.Add(2746900);
        experienceLevel.Add(3095000);
        experienceLevel.Add(3470800);
        experienceLevel.Add(3875000);
        experienceLevel.Add(4308700);
        experienceLevel.Add(4772900);
        experienceLevel.Add(5268700);
        experienceLevel.Add(5797100);
        experienceLevel.Add(6359000);
        experienceLevel.Add(6956000);
        experienceLevel.Add(7589000);
        experienceLevel.Add(8257800);
        experienceLevel.Add(8965000);
        experienceLevel.Add(9711600);
        experienceLevel.Add(10498000);
        experienceLevel.Add(11325300);
        experienceLevel.Add(12195200);
        experienceLevel.Add(13108000);
        experienceLevel.Add(14066000);
        experienceLevel.Add(15069000);
        experienceLevel.Add(16119300);
        experienceLevel.Add(17217500);
        experienceLevel.Add(18377000);
        experienceLevel.Add(19602000);
        experienceLevel.Add(20896000);
        experienceLevel.Add(22262000);
        experienceLevel.Add(23704000);
        experienceLevel.Add(25227500);
        experienceLevel.Add(26835000);
        experienceLevel.Add(28531000);
        experienceLevel.Add(30320000);
        experienceLevel.Add(32208000);
        experienceLevel.Add(34199000);
        experienceLevel.Add(36298000);
        experienceLevel.Add(38518000);
        experienceLevel.Add(40844000);
        experienceLevel.Add(43297000);
        experienceLevel.Add(45882500);
        experienceLevel.Add(48604500);
        experienceLevel.Add(51471000);
        experienceLevel.Add(54483000);
        experienceLevel.Add(57650500);
        experienceLevel.Add(60981000);
        experienceLevel.Add(64483000);
        experienceLevel.Add(68159000);
        experienceLevel.Add(72020000);
        experienceLevel.Add(76074000);
        experienceLevel.Add(80320000);
        experienceLevel.Add(84788000);
        experienceLevel.Add(89459000);
        experienceLevel.Add(94360000);
        experienceLevel.Add(99492000);
        experienceLevel.Add(104861000);
        experienceLevel.Add(110489000);
        experienceLevel.Add(116389000);
        experienceLevel.Add(122525000);
        experienceLevel.Add(128957800);
        experienceLevel.Add(135683000);
        experienceLevel.Add(142714000);
        experienceLevel.Add(150048000);
        experienceLevel.Add(157700000);
        experienceLevel.Add(165692000);
        experienceLevel.Add(174219000);
        experienceLevel.Add(183316000);
        experienceLevel.Add(193006000);
        experienceLevel.Add(203327000);
        experienceLevel.Add(214304000);
        experienceLevel.Add(225974000);
        experienceLevel.Add(238369000);
        experienceLevel.Add(251523000);
        experienceLevel.Add(265472000);
        experienceLevel.Add(280253000);
        experienceLevel.Add(295900000);
        experienceLevel.Add(312452000);
        experienceLevel.Add(329950000);
        experienceLevel.Add(348435000);
        experienceLevel.Add(367940200);
        experienceLevel.Add(388515000);
        experienceLevel.Add(410200000);
        experienceLevel.Add(433039000);
        experienceLevel.Add(457078000);
        experienceLevel.Add(482360000);
        experienceLevel.Add(508933000);
        experienceLevel.Add(536847000);
        experienceLevel.Add(566146000);
        experienceLevel.Add(596884000);
        experienceLevel.Add(629114000);
        experienceLevel.Add(662878000);
        experienceLevel.Add(698236000);
        experienceLevel.Add(735240000);
        experienceLevel.Add(773954000);
        experienceLevel.Add(814418000);
        experienceLevel.Add(856700000);
        experienceLevel.Add(900856000);
        experienceLevel.Add(946944000);
        experienceLevel.Add(995025000);
        experienceLevel.Add(1045164000);
        experienceLevel.Add(1097414000);
        experienceLevel.Add(1151839000);
        experienceLevel.Add(1208532000);
        experienceLevel.Add(1267520000);
        experienceLevel.Add(1328891000);
        experienceLevel.Add(1394766000);
        experienceLevel.Add(1465353000);
        experienceLevel.Add(1540775000);
        experienceLevel.Add(1621343000);
        experienceLevel.Add(1707202000);
        experienceLevel.Add(1798604000);
        experienceLevel.Add(1895689000);
        experienceLevel.Add(1998700000);
        experienceLevel.Add(2107964000);
        experienceLevel.Add(2223594548);
        experienceLevel.Add(2345859740);
        experienceLevel.Add(2474996700);
        experienceLevel.Add(2611246919);
        experienceLevel.Add(2754856329);
        experienceLevel.Add(2906075326);
        experienceLevel.Add(3065158798);
        experienceLevel.Add(3232366145);
        experienceLevel.Add(3407961306);
        experienceLevel.Add(3592212780);
        experienceLevel.Add(3785393657);
        experienceLevel.Add(3999725569);
        experienceLevel.Add(4236116170);
        experienceLevel.Add(4495490778);
        experienceLevel.Add(4778792472);
        experienceLevel.Add(5086982188);
        experienceLevel.Add(5421038816);
        experienceLevel.Add(5781959301);
        experienceLevel.Add(6170758735);
        experienceLevel.Add(6588470454);
        experienceLevel.Add(7036146133);
        experienceLevel.Add(7514855881);
        experienceLevel.Add(8025688337);
        experienceLevel.Add(8569750762);
        experienceLevel.Add(9148169131);
        experienceLevel.Add(9762088230);
        experienceLevel.Add(10412671747);
        experienceLevel.Add(11101102362);
        experienceLevel.Add(11828581842);
        experienceLevel.Add(12596331130);
        experienceLevel.Add(13405591808);
        experienceLevel.Add(14323751218);
        experienceLevel.Add(15355130652);
        experienceLevel.Add(16504312961);
        experienceLevel.Add(17775189762);
        experienceLevel.Add(19172880438);
        experienceLevel.Add(20701812509);
        experienceLevel.Add(22366681987);
        experienceLevel.Add(24172261748);
        experienceLevel.Add(26123401888);
        experienceLevel.Add(28225030084);
        experienceLevel.Add(30482151954);
        experienceLevel.Add(32899851410);
        experienceLevel.Add(35483291016);
        experienceLevel.Add(38237712340);
        experienceLevel.Add(41168436309);
        experienceLevel.Add(44280863558);
        experienceLevel.Add(47580474776);
        experienceLevel.Add(51072831062);
        experienceLevel.Add(54763574263);
        experienceLevel.Add(58658427326);
        experienceLevel.Add(63115894897);
        experienceLevel.Add(68115894897);
        experienceLevel.Add(73800275488);
        experienceLevel.Add(80068783764);
        experienceLevel.Add(86983099850);
        experienceLevel.Add(94564821930);
        experienceLevel.Add(102835870903);
        experienceLevel.Add(111739655123);
        experienceLevel.Add(121535254947);
        experienceLevel.Add(132009057736);
        experienceLevel.Add(144873495079);
        experienceLevel.Add(160214619412);
        experienceLevel.Add(178119779264);
        experienceLevel.Add(198677624721);
        experienceLevel.Add(221978112873);
        experienceLevel.Add(248292513235);

        // Health gain by Endurance Steps
        healthFromEndurancePointsLevel.Add(7); // < 1-19 Endurance
        healthFromEndurancePointsLevel.Add(8); // < 20-39 Endurance
        healthFromEndurancePointsLevel.Add(9); // < 40-59 Endurance
        healthFromEndurancePointsLevel.Add(10); // < 60-79 Endurance
        healthFromEndurancePointsLevel.Add(11); // < 80-99 Endurance
        healthFromEndurancePointsLevel.Add(12); // < 100-119 Endurance
        healthFromEndurancePointsLevel.Add(13); // < 120-139 Endurance
        healthFromEndurancePointsLevel.Add(14); // < 140-159 Endurance
        healthFromEndurancePointsLevel.Add(15); // < 160-179 Endurance
        healthFromEndurancePointsLevel.Add(16); // < 180-199 Endurance
        healthFromEndurancePointsLevel.Add(17); // < 200-219 Endurance
        healthFromEndurancePointsLevel.Add(18); // < 220-239 Endurance
        healthFromEndurancePointsLevel.Add(19); // < 240-259 Endurance
        healthFromEndurancePointsLevel.Add(20); // < 260-279 Endurance
        healthFromEndurancePointsLevel.Add(21); // < 280-299 Endurance
        healthFromEndurancePointsLevel.Add(22); // < 300-319 Endurance
        healthFromEndurancePointsLevel.Add(23); // < 320-339 Endurance
        healthFromEndurancePointsLevel.Add(24); // < 340-359 Endurance
        healthFromEndurancePointsLevel.Add(25); // < 360-379 Endurance
        healthFromEndurancePointsLevel.Add(26); // < 380-399 Endurance
        healthFromEndurancePointsLevel.Add(27); // < 400-419 Endurance

        // Mana gain by Intelect Steps
        manaFromIntelectPointsLevel.Add(4); // < 1-29 Intelect
        manaFromIntelectPointsLevel.Add(5); // < 30-59 Intelect
        manaFromIntelectPointsLevel.Add(6); // < 60-89 Intelect
        manaFromIntelectPointsLevel.Add(7); // < 90-119 Intelect
        manaFromIntelectPointsLevel.Add(8); // < 120-149 Intelect
        manaFromIntelectPointsLevel.Add(9); // < 150-179 Intelect
        manaFromIntelectPointsLevel.Add(10); // < 180-209 Intelect
        manaFromIntelectPointsLevel.Add(11); // < 210-239 Intelect
        manaFromIntelectPointsLevel.Add(12); // < 240-269 Intelect
        manaFromIntelectPointsLevel.Add(13); // < 270-299 Intelect
        manaFromIntelectPointsLevel.Add(14); // < 300-329 Intelect
        manaFromIntelectPointsLevel.Add(15); // < 330-359 Intelect
        manaFromIntelectPointsLevel.Add(16); // < 360-389 Intelect
        manaFromIntelectPointsLevel.Add(17); // < 390-419 Intelect
        manaFromIntelectPointsLevel.Add(18); // < 420-449 Intelect
        manaFromIntelectPointsLevel.Add(19); // < 450-479 Intelect
        manaFromIntelectPointsLevel.Add(20); // < 480-509 Intelect
        manaFromIntelectPointsLevel.Add(21); // < 510-539 Intelect
        manaFromIntelectPointsLevel.Add(22); // < 540-569 Intelect
        manaFromIntelectPointsLevel.Add(23); // < 570-599 Intelect

        // Mana gain by Wisdom Steps
        manaFromWisdomPointsLevel.Add(0); // < 1-59 Wisdom
        manaFromWisdomPointsLevel.Add(1); // < 60-119 Intelect
        manaFromWisdomPointsLevel.Add(2); // < 120-179 Intelect
        manaFromWisdomPointsLevel.Add(3); // < 180-239 Intelect
        manaFromWisdomPointsLevel.Add(4); // < 240-299 Intelect
        manaFromWisdomPointsLevel.Add(5); // < 300-359 Intelect
        manaFromWisdomPointsLevel.Add(6); // < 360-419 Intelect
        manaFromWisdomPointsLevel.Add(7); // < 420-479 Intelect
        manaFromWisdomPointsLevel.Add(8); // < 480-539 Intelect

    }

    public void Update()
    {
        GetCurrentLevel();
    }

    public void GetCurrentLevel()
    {
        int level = 1;
        foreach (long threshold in experienceLevel)
        {
            if ((long)Player.Instance.experience >= threshold)
            {
                if (level > Player.Instance.level)
                {
                    LevelUp(level);
                }
                currentLevelExperience = threshold;
                nextLevelExperience = experienceLevel[level];
                level++;
            }
        }
    }

    private void LevelUp(int level)
    {
        StartCoroutine(LevelUpMessage());

        // Level Gain
        Player.Instance.level = level;

        // Attributes Points Gain
        Player.Instance.remainingAttributesPoints += 5;

        // Skills Points Gain
        Player.Instance.remainingSkillsPoints += 15;

        // Health Points Gain
        int gainHealth = 0;
        if (Player.Instance.endurance <= 19) gainHealth = healthFromEndurancePointsLevel[0];
        if (Player.Instance.endurance >= 20 && Player.Instance.endurance <= 39) gainHealth = healthFromEndurancePointsLevel[1];
        if (Player.Instance.endurance >= 40 && Player.Instance.endurance <= 69) gainHealth = healthFromEndurancePointsLevel[2];
        if (Player.Instance.endurance >= 60 && Player.Instance.endurance <= 79) gainHealth = healthFromEndurancePointsLevel[3];
        if (Player.Instance.endurance >= 80 && Player.Instance.endurance <= 99) gainHealth = healthFromEndurancePointsLevel[4];
        if (Player.Instance.endurance >= 100 && Player.Instance.endurance <= 119) gainHealth = healthFromEndurancePointsLevel[5];
        if (Player.Instance.endurance >= 120 && Player.Instance.endurance <= 149) gainHealth = healthFromEndurancePointsLevel[6];
        if (Player.Instance.endurance >= 140 && Player.Instance.endurance <= 159) gainHealth = healthFromEndurancePointsLevel[7];
        if (Player.Instance.endurance >= 160 && Player.Instance.endurance <= 179) gainHealth = healthFromEndurancePointsLevel[8];
        if (Player.Instance.endurance >= 180 && Player.Instance.endurance <= 199) gainHealth = healthFromEndurancePointsLevel[9];
        if (Player.Instance.endurance >= 200 && Player.Instance.endurance <= 219) gainHealth = healthFromEndurancePointsLevel[10];
        if (Player.Instance.endurance >= 220 && Player.Instance.endurance <= 239) gainHealth = healthFromEndurancePointsLevel[11];
        if (Player.Instance.endurance >= 240 && Player.Instance.endurance <= 259) gainHealth = healthFromEndurancePointsLevel[12];
        if (Player.Instance.endurance >= 260 && Player.Instance.endurance <= 279) gainHealth = healthFromEndurancePointsLevel[13];
        if (Player.Instance.endurance >= 280 && Player.Instance.endurance <= 299) gainHealth = healthFromEndurancePointsLevel[14];
        if (Player.Instance.endurance >= 300 && Player.Instance.endurance <= 319) gainHealth = healthFromEndurancePointsLevel[15];
        if (Player.Instance.endurance >= 320 && Player.Instance.endurance <= 339) gainHealth = healthFromEndurancePointsLevel[16];
        if (Player.Instance.endurance >= 340 && Player.Instance.endurance <= 359) gainHealth = healthFromEndurancePointsLevel[17];
        if (Player.Instance.endurance >= 360 && Player.Instance.endurance <= 379) gainHealth = healthFromEndurancePointsLevel[18];
        if (Player.Instance.endurance >= 380 && Player.Instance.endurance <= 399) gainHealth = healthFromEndurancePointsLevel[19];
        if (Player.Instance.endurance >= 400 && Player.Instance.endurance <= 419) gainHealth = healthFromEndurancePointsLevel[20];

        // Intelect Points Gain
        int gainManaFromIntelect = 0;
        if (Player.Instance.intelect <= 29) gainManaFromIntelect = manaFromIntelectPointsLevel[0];
        if (Player.Instance.intelect >= 30 && Player.Instance.intelect <= 59) gainManaFromIntelect = manaFromIntelectPointsLevel[1];
        if (Player.Instance.intelect >= 60 && Player.Instance.intelect <= 89) gainManaFromIntelect = manaFromIntelectPointsLevel[2];
        if (Player.Instance.intelect >= 90 && Player.Instance.intelect <= 119) gainManaFromIntelect = manaFromIntelectPointsLevel[3];
        if (Player.Instance.intelect >= 120 && Player.Instance.intelect <= 149) gainManaFromIntelect = manaFromIntelectPointsLevel[4];
        if (Player.Instance.intelect >= 150 && Player.Instance.intelect <= 179) gainManaFromIntelect = manaFromIntelectPointsLevel[5];
        if (Player.Instance.intelect >= 180 && Player.Instance.intelect <= 209) gainManaFromIntelect = manaFromIntelectPointsLevel[6];
        if (Player.Instance.intelect >= 210 && Player.Instance.intelect <= 239) gainManaFromIntelect = manaFromIntelectPointsLevel[7];
        if (Player.Instance.intelect >= 240 && Player.Instance.intelect <= 269) gainManaFromIntelect = manaFromIntelectPointsLevel[8];
        if (Player.Instance.intelect >= 270 && Player.Instance.intelect <= 299) gainManaFromIntelect = manaFromIntelectPointsLevel[9];
        if (Player.Instance.intelect >= 300 && Player.Instance.intelect <= 329) gainManaFromIntelect = manaFromIntelectPointsLevel[10];
        if (Player.Instance.intelect >= 330 && Player.Instance.intelect <= 359) gainManaFromIntelect = manaFromIntelectPointsLevel[11];
        if (Player.Instance.intelect >= 360 && Player.Instance.intelect <= 389) gainManaFromIntelect = manaFromIntelectPointsLevel[12];
        if (Player.Instance.intelect >= 390 && Player.Instance.intelect <= 419) gainManaFromIntelect = manaFromIntelectPointsLevel[13];
        if (Player.Instance.intelect >= 420 && Player.Instance.intelect <= 449) gainManaFromIntelect = manaFromIntelectPointsLevel[14];
        if (Player.Instance.intelect >= 450 && Player.Instance.intelect <= 479) gainManaFromIntelect = manaFromIntelectPointsLevel[15];
        if (Player.Instance.intelect >= 480 && Player.Instance.intelect <= 509) gainManaFromIntelect = manaFromIntelectPointsLevel[16];
        if (Player.Instance.intelect >= 510 && Player.Instance.intelect <= 539) gainManaFromIntelect = manaFromIntelectPointsLevel[17];
        if (Player.Instance.intelect >= 540 && Player.Instance.intelect <= 569) gainManaFromIntelect = manaFromIntelectPointsLevel[18];
        if (Player.Instance.intelect >= 570 && Player.Instance.intelect <= 599) gainManaFromIntelect = manaFromIntelectPointsLevel[19];

        // Wisdom Points Gain
        int gainManaFromWisdom = 0;
        if (Player.Instance.wisdom <= 59) gainManaFromWisdom = manaFromWisdomPointsLevel[0];
        if (Player.Instance.wisdom >= 60 && Player.Instance.wisdom <= 119) gainManaFromWisdom = manaFromWisdomPointsLevel[1];
        if (Player.Instance.wisdom >= 120 && Player.Instance.wisdom <= 179) gainManaFromWisdom = manaFromWisdomPointsLevel[2];
        if (Player.Instance.wisdom >= 180 && Player.Instance.wisdom <= 239) gainManaFromWisdom = manaFromWisdomPointsLevel[3];
        if (Player.Instance.wisdom >= 240 && Player.Instance.wisdom <= 299) gainManaFromWisdom = manaFromWisdomPointsLevel[4];
        if (Player.Instance.wisdom >= 300 && Player.Instance.wisdom <= 359) gainManaFromWisdom = manaFromWisdomPointsLevel[5];
        if (Player.Instance.wisdom >= 360 && Player.Instance.wisdom <= 419) gainManaFromWisdom = manaFromWisdomPointsLevel[6];
        if (Player.Instance.wisdom >= 420 && Player.Instance.wisdom <= 479) gainManaFromWisdom = manaFromWisdomPointsLevel[7];
        if (Player.Instance.wisdom >= 480 && Player.Instance.wisdom <= 539) gainManaFromWisdom = manaFromWisdomPointsLevel[8];

        // Add Health Gain
        Player.Instance.health += gainHealth;

        // Add Mana Gain
        Player.Instance.mana += gainManaFromIntelect + gainManaFromWisdom;
    }

    IEnumerator LevelUpMessage()
    {
        GameObject ui = GameObject.Find("UI").gameObject;

        if (ui != null)
        {
            GameObject canvas = ui.transform.Find("Canvas").gameObject;
            if (canvas != null)
            {
                GameObject levelUp = canvas.transform.Find("Level Up").gameObject;
                if (levelUp != null)
                {
                    levelUp.SetActive(true);
                    yield return new WaitForSeconds(5);
                    levelUp.SetActive(false);
                }
                else yield return null;
            }
        }
    }
}