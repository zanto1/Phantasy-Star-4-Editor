using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS4Editor
{
    public partial class Form1 : Form
    {
        private Rom romData;
        private string selectedRom;
        private bool justLoadedRom;            // prevents the program to do some things when it loads rom data

        private List<System.Windows.Forms.Label> levelLabels = new List<System.Windows.Forms.Label>();
        private List<System.Windows.Forms.NumericUpDown> statFields = new List<System.Windows.Forms.NumericUpDown>();
        private List<System.Windows.Forms.ComboBox> comboFields = new List<System.Windows.Forms.ComboBox>();

        private int previousCharacterIndex;
        private int previousMonsterIndex;
        private int previousMonsterSkillIndex;
        private int previousPlayerTechIndex;
        private int previousPlayerSkillIndex;
        private bool displayGrowth;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setFieldsState(false);
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.DialogResult result = this.openFileOpenRom.ShowDialog(this);
                if (result.Equals(System.Windows.Forms.DialogResult.OK))
                {
                    selectedRom = this.openFileOpenRom.FileName;
                    this.Text = "PS4 Editor - " + selectedRom;
                    //File.Copy(selectedRom, "PS4Aux.sfc");
                    this.romData = new Rom(selectedRom);
                    this.Text = "PS4 Editor - " + selectedRom;
                    justLoadedRom = true;
                    setFieldsState(true);

                    loadCharComboBoxes();
                    loadCharacterStartTechLists();
                    loadCharacterStartSkillLists();
                    loadElementComboBoxes();
                    loadAIConditionComboBoxes();
                    loadMonsterSkillEffects();
                    loadEquipmentLists();
                    loadMonsterList();
                    loadMonsterSkillList();
                    loadMonsterSkillStats();
                    loadMonsterSkillTarget();

                    loadPlayerTechList();

                    cbCharacterList.SelectedIndex = 0;
                    cbMonsterList.SelectedIndex = 0;
                    cbMonsterSkillList.SelectedIndex = 0;
                    cbPlayerTechList.SelectedIndex = 0;
                    cbPlayerSkillList.SelectedIndex = 0;

                    previousCharacterIndex = 0;
                    previousMonsterIndex = 0;
                    previousMonsterSkillIndex = 0;
                    previousPlayerTechIndex = 0;
                    previousPlayerSkillIndex = 0;

                    justLoadedRom = false;
                    displayGrowth = false;

                    tabPage3.AutoSize = true;

                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error loading rom: " + x.ToString());
            }
        }





        /*****************************************************
         * 
         * setFieldsState
         * 
        *****************************************************/

        private void setFieldsState(bool value)
        {
            this.cbCharacterList.Enabled = value;
            this.numCharAgility.Enabled = value;
            this.numCharAntiEvilDef.Enabled = value;
            this.numCharBiologicalDef.Enabled = value;
            this.numCharBroseDef.Enabled = value;
            this.numCharDestroyDef.Enabled = value;
            this.numCharDexterity.Enabled = value;
            this.numCharEfessDef.Enabled = value;
            this.numCharElectricDef.Enabled = value;
            this.numCharEnergyDef.Enabled = value;
            this.numCharStartExp.Enabled = value;
            this.numCharFireDef.Enabled = value;
            this.numCharGravityDef.Enabled = value;
            this.numCharHolywordDef.Enabled = value;
            this.numCharHp.Enabled = value;
            this.numCharTp.Enabled = value;
            this.cbCharLeftHandEq.Enabled = value;
            this.cbCharRightHandEq.Enabled = value;
            this.cbCharBodyEq.Enabled = value;
            this.cbCharHeadEq.Enabled = value;
            this.cbCharProfession.Enabled = value;
            this.numCharMechanicalDef.Enabled = value;
            this.numCharMental.Enabled = value;
            this.numCharPhysDef.Enabled = value;
            this.numCharPsychicDef.Enabled = value;
            this.numCharStrength.Enabled = value;
            this.numCharWaterDef.Enabled = value;
            this.btnShowCharGrowth.Enabled = value;
            this.tabControl1.Enabled = value;

            this.cbCharacterList.Enabled = value;
        }




        /*****************************************************
         * 
         * loadElementComboBoxes
         * 
        *****************************************************/

        private void loadElementComboBoxes()
        {
            List<System.Windows.Forms.ComboBox> combos = new List<System.Windows.Forms.ComboBox>();

            combos.Add(cbMonsterAtkElement); combos.Add(cbMonsterSkillElement); combos.Add(cbPlayerTechElement); combos.Add(cbPlayerSkillElement);
            foreach (System.Windows.Forms.ComboBox cb in combos)
            {
                cb.Items.Add("None");
                cb.Items.Add("Physical");
                cb.Items.Add("Energy");
                cb.Items.Add("Fire");
                cb.Items.Add("Gravity");
                cb.Items.Add("Water");
                cb.Items.Add("Anti-Evil");
                cb.Items.Add("Electric");
                cb.Items.Add("Holyword");
                cb.Items.Add("Brose");
                cb.Items.Add("Biological");
                cb.Items.Add("Psychic");
                cb.Items.Add("Mechanical");
                cb.Items.Add("Efess");
                cb.Items.Add("Destroy");
            }
            cbPlayerSkillElement.Items.Add("Same as equipped weapon");
        }




        /*****************************************************
         * 
         * loadCharComboBoxes
         * 
        *****************************************************/

        private void loadCharComboBoxes()
        {
            List<System.Windows.Forms.ComboBox> combos = new List<System.Windows.Forms.ComboBox>();

            combos.Add(cbCharacterList);
            foreach (System.Windows.Forms.ComboBox cb in combos)
            {
                cb.Items.Add("Chaz");
                cb.Items.Add("Alys");
                cb.Items.Add("Hahn");
                cb.Items.Add("Rune");
                cb.Items.Add("Gryz");
                cb.Items.Add("Rika");
                cb.Items.Add("Demi");
                cb.Items.Add("Wren");
                cb.Items.Add("Raja");
                cb.Items.Add("Kyra");
                cb.Items.Add("Seth");
            }
        }



        /*****************************************************
         * 
         * loadEquipmentLists
         * 
        *****************************************************/

        private void loadEquipmentLists()
        {
            List<System.Windows.Forms.ComboBox> itemcombos = new List<System.Windows.Forms.ComboBox>();
            itemcombos.Add(cbCharLeftHandEq); itemcombos.Add(cbCharRightHandEq); itemcombos.Add(cbCharHeadEq); itemcombos.Add(cbCharBodyEq);

            foreach (System.Windows.Forms.ComboBox cb in itemcombos)
            {
                cb.Items.Clear();
                for (var i = 0; i < Rom.maxItems; i++)
                {
                    cb.Items.Insert(i, this.romData.itemName[i]);

                }
                cb.Items.Insert(0, "* Nothing *");
            }
        }




        /**********************************************************
         * ********************************************************
         * ********************************************************
         * 
         * CHARACTER TAB
         * 
         * ********************************************************
         * ********************************************************
         * ********************************************************/



        /*****************************************************
        * 
        * createCharacterFields
        * 
       *****************************************************/

        private void createCharacterFields()
        {
            tabPage3.AutoScroll = true;
            int charIndex = this.cbCharacterList.SelectedIndex;
            int startLevel = this.romData.charStartLevel[charIndex];
            for (var i = startLevel+1; i < 100; i++)
            {
                addStatFields(i, startLevel);
            }
        }



        /*****************************************************
        * 
        * addStatFields
        * 
       *****************************************************/

        private void addStatFields(int level, int startLevel)
        {
            int fieldNumber = level - startLevel - 1;
            int charIndex = this.cbCharacterList.SelectedIndex;

            int xpos = 23;
            int ypos = 120 + 420 + 100 + (fieldNumber * 26);

            Label newLabel = new Label();

            newLabel.AutoSize = true;
            newLabel.Location = new System.Drawing.Point(xpos, ypos);
            newLabel.Name = "lbLevel" + level.ToString();
            newLabel.Size = new System.Drawing.Size(48, 13);
            newLabel.TabIndex = 0;
            newLabel.Text = "Level " + level.ToString();

            levelLabels.Add(newLabel);

            tabPage3.Controls.Add(newLabel);

            
            xpos = 85;
            ypos = 120 + 420 + 98 + (fieldNumber * 26);

            NumericUpDown numStat;

            // HP field
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numHP" + level.ToString();
            numStat.Size = new System.Drawing.Size(54, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, cbCharacterList.Text + "'s HP on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthHp[charIndex][fieldNumber];

            // TP field
            xpos = 85 + 65;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numTP" + level.ToString();
            numStat.Size = new System.Drawing.Size(54, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, cbCharacterList.Text + "'s TP on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthTp[charIndex][fieldNumber];

            // STR field
            xpos = 85 + 130;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numStr" + level.ToString();
            numStat.Size = new System.Drawing.Size(54, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, cbCharacterList.Text + "'s strength on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthStr[charIndex][fieldNumber];

            // MEN field
            xpos = 85 + 195;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numMen" + level.ToString();
            numStat.Size = new System.Drawing.Size(54, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, cbCharacterList.Text + "'s mental on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthMen[charIndex][fieldNumber];

            // AGI field
            xpos = 85 + 260;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numAgi" + level.ToString();
            numStat.Size = new System.Drawing.Size(54, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, cbCharacterList.Text + "'s agility on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthAgi[charIndex][fieldNumber];

            // DEX field
            xpos = 85 + 325;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numDex" + level.ToString();
            numStat.Size = new System.Drawing.Size(54, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, cbCharacterList.Text + "'s dexterity on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthDex[charIndex][fieldNumber];

            // EXP field
            xpos = 1141;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { -1, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numExp" + level.ToString();
            numStat.Size = new System.Drawing.Size(83, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, cbCharacterList.Text + " needs this much exp to reach level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthExp[charIndex][fieldNumber];



            // Skill uses
            xpos = 812;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numSk1" + level.ToString();
            numStat.Size = new System.Drawing.Size(41, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, "Number of times " + cbCharacterList.Text + " can use his first skill on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthSk1[charIndex][fieldNumber];

            xpos = 859;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numSk2" + level.ToString();
            numStat.Size = new System.Drawing.Size(41, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, "Number of times " + cbCharacterList.Text + " can use his second skill on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthSk2[charIndex][fieldNumber];

            xpos = 906;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numSk3" + level.ToString();
            numStat.Size = new System.Drawing.Size(41, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, "Number of times " + cbCharacterList.Text + " can use his third skill on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthSk3[charIndex][fieldNumber];

            xpos = 953;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numSk4" + level.ToString();
            numStat.Size = new System.Drawing.Size(41, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, "Number of times " + cbCharacterList.Text + " can use his fourth skill on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthSk4[charIndex][fieldNumber];

            xpos = 1000;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numSk5" + level.ToString();
            numStat.Size = new System.Drawing.Size(41, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, "Number of times " + cbCharacterList.Text + " can use his fifth skill on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthSk5[charIndex][fieldNumber];

            xpos = 1047;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numSk6" + level.ToString();
            numStat.Size = new System.Drawing.Size(41, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, "Number of times " + cbCharacterList.Text + " can use his sixth skill on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthSk6[charIndex][fieldNumber];

            xpos = 1094;
            numStat = new NumericUpDown();
            numStat.Location = new System.Drawing.Point(xpos, ypos);
            numStat.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numStat.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numStat.Name = "numSk7" + level.ToString();
            numStat.Size = new System.Drawing.Size(41, 20);
            numStat.TabIndex = 0;
            this.statFieldTooltips.SetToolTip(numStat, "Number of times " + cbCharacterList.Text + " can use his seventh skill on level " + level.ToString());
            statFields.Add(numStat);
            tabPage3.Controls.Add(numStat);
            numStat.Value = this.romData.charGrowthSk7[charIndex][fieldNumber];





            System.Windows.Forms.ComboBox comboBox;

            // tech learned
            xpos = 475;
            comboBox = new System.Windows.Forms.ComboBox();
            comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox.Location = new System.Drawing.Point(xpos, ypos);
            comboBox.Name = "cbCharGrowthTech" + level.ToString();
            comboBox.Size = new System.Drawing.Size(159, 21);
            comboBox.Items.Add("");
            for (var i = 0; i < Rom.maxPlayerTechs; i++)
            {
                comboBox.Items.Add(this.romData.playerTechName[i]);
            }
            tabPage3.Controls.Add(comboBox);
            comboFields.Add(comboBox);
            comboBox.SelectedIndex = this.romData.charGrowthTech[charIndex][fieldNumber];
            this.statFieldTooltips.SetToolTip(comboBox, cbCharacterList.Text + " learns this tech on level " + level.ToString());

            // skill learned
            xpos = 645;
            comboBox = new System.Windows.Forms.ComboBox();
            comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox.Location = new System.Drawing.Point(xpos, ypos);
            comboBox.Name = "cbCharGrowthSkill" + level.ToString();
            comboBox.Size = new System.Drawing.Size(159, 21);
            comboBox.Items.Add("");
            for (var i = 0; i < Rom.maxPlayerSkills; i++)
            {
                comboBox.Items.Add(this.romData.playerTechName[i + Rom.maxPlayerTechs]);
            }
            tabPage3.Controls.Add(comboBox);
            comboFields.Add(comboBox);
            this.statFieldTooltips.SetToolTip(comboBox, cbCharacterList.Text + " learns this skill on level " + level.ToString());
            comboBox.SelectedIndex = this.romData.charGrowthSkill[charIndex][fieldNumber];
            
        }


        /*****************************************************
        * 
        * destroyAllStatFields
        * 
       *****************************************************/

        private void destroyAllStatFields()
        {
            this.statFieldTooltips.RemoveAll();

            Label label;
            while (levelLabels.Count > 0)
            {
                label = levelLabels[0];
                levelLabels.RemoveAt(0);
                tabPage3.Controls.Remove(label);
            }

            NumericUpDown numStat;
            while (statFields.Count > 0)
            {
                numStat = statFields[0];
                statFields.RemoveAt(0);
                tabPage3.Controls.Remove(numStat);
            }

            ComboBox comboBox;
            while (comboFields.Count > 0)
            {
                comboBox = comboFields[0];
                comboFields.RemoveAt(0);
                tabPage3.Controls.Remove(comboBox);
            }
        }



        /*****************************************************
        * 
        * btnShowCharGrowth_Click
        * 
       *****************************************************/

        private void btnShowCharGrowth_Click(object sender, EventArgs e)
        {
            if (displayGrowth)
            {
                hideCharacterGrowths();
            }
            else
            {
                showCharacterGrowths();
            }
        }


        private void hideCharacterGrowths()
        {
            destroyAllStatFields();
            lbGrowthAgi.Visible = false;
            lbGrowthDex.Visible = false;
            lbGrowthHp.Visible = false;
            lbGrowthMen.Visible = false;
            lbGrowthStr.Visible = false;
            lbGrowthTp.Visible = false;
            lbGrowthTech.Visible = false;
            lbGrowthSkill.Visible = false;
            lbGrowthSk1.Visible = false;
            lbGrowthSk2.Visible = false;
            lbGrowthSk3.Visible = false;
            lbGrowthSk4.Visible = false;
            lbGrowthSk5.Visible = false;
            lbGrowthSk6.Visible = false;
            lbGrowthSk7.Visible = false;
            lbGrowthExp.Visible = false;
            btnShowCharGrowth.Text = "Show character growth table";
            displayGrowth = false;
        }

        private void showCharacterGrowths()
        {
            createCharacterFields();
            lbGrowthAgi.Visible = true;
            lbGrowthDex.Visible = true;
            lbGrowthHp.Visible = true;
            lbGrowthMen.Visible = true;
            lbGrowthStr.Visible = true;
            lbGrowthTp.Visible = true;
            lbGrowthTech.Visible = true;
            lbGrowthSkill.Visible = true;
            lbGrowthSk1.Visible = true;
            lbGrowthSk2.Visible = true;
            lbGrowthSk3.Visible = true;
            lbGrowthSk4.Visible = true;
            lbGrowthSk5.Visible = true;
            lbGrowthSk6.Visible = true;
            lbGrowthSk7.Visible = true;
            lbGrowthExp.Visible = true;
            btnShowCharGrowth.Text = "Hide character growth table";
            displayGrowth = true;
        }


        /*****************************************************
         * 
         * loadCharacterStartTechLists
         * 
        *****************************************************/

        private void loadCharacterStartTechLists()
        {
            List<System.Windows.Forms.ComboBox> skillcombos = new List<System.Windows.Forms.ComboBox>();

            skillcombos.Add(cbCharStartTech1); skillcombos.Add(cbCharStartTech2); skillcombos.Add(cbCharStartTech3); skillcombos.Add(cbCharStartTech4);
            skillcombos.Add(cbCharStartTech5); skillcombos.Add(cbCharStartTech6); skillcombos.Add(cbCharStartTech7); skillcombos.Add(cbCharStartTech8);
            skillcombos.Add(cbCharStartTech9); skillcombos.Add(cbCharStartTech10); skillcombos.Add(cbCharStartTech11); skillcombos.Add(cbCharStartTech12);
            skillcombos.Add(cbCharStartTech13); skillcombos.Add(cbCharStartTech14); skillcombos.Add(cbCharStartTech15); skillcombos.Add(cbCharStartTech16);
            foreach (System.Windows.Forms.ComboBox cb in skillcombos)
            {
                cb.Items.Clear();
                cb.Items.Add("");
                for (var i = 0; i < Rom.maxPlayerTechs; i++)
                {
                    cb.Items.Add(this.romData.playerTechName[i]);
                }
            }
        }


        /*****************************************************
         * 
         * loadCharacterStartSkillLists
         * 
        *****************************************************/

        private void loadCharacterStartSkillLists()
        {
            List<System.Windows.Forms.ComboBox> skillcombos = new List<System.Windows.Forms.ComboBox>();

            skillcombos.Add(cbCharStartSkill1); skillcombos.Add(cbCharStartSkill2); skillcombos.Add(cbCharStartSkill3); skillcombos.Add(cbCharStartSkill4);
            skillcombos.Add(cbCharStartSkill5); skillcombos.Add(cbCharStartSkill6); skillcombos.Add(cbCharStartSkill7); skillcombos.Add(cbCharStartSkill8); 
            foreach (System.Windows.Forms.ComboBox cb in skillcombos)
            {
                cb.Items.Clear();
                cb.Items.Add("");
                for (var i = 0; i < Rom.maxPlayerSkills; i++)
                {
                    cb.Items.Add(this.romData.playerTechName[i + Rom.maxPlayerTechs]);
                }
            }
        }


        /*****************************************************
        * 
        * loadCharacterFields
        * 
       *****************************************************/

        private void loadCharacterFields()
        {
            int charIndex = this.cbCharacterList.SelectedIndex;
            lbCharStartLevel.Text = "Starting level: " + this.romData.charStartLevel[charIndex].ToString();
            cbCharProfession.SelectedIndex = this.romData.charProfession[charIndex];
            numCharHp.Value = this.romData.charStartHp[charIndex];
            numCharTp.Value = this.romData.charStartTp[charIndex];
            numCharStartExp.Value = this.romData.charStartExp[charIndex];
            numCharStrength.Value = this.romData.charStartStr[charIndex];
            numCharMental.Value = this.romData.charStartMen[charIndex];
            numCharAgility.Value = this.romData.charStartAgi[charIndex];
            numCharDexterity.Value = this.romData.charStartDex[charIndex];

            numCharPhysDef.Value = this.romData.charPhysDef[charIndex];
            numCharEnergyDef.Value = this.romData.charEnergyDef[charIndex];
            numCharFireDef.Value = this.romData.charFireDef[charIndex];
            numCharGravityDef.Value = this.romData.charGravityDef[charIndex];
            numCharWaterDef.Value = this.romData.charWaterDef[charIndex];
            numCharAntiEvilDef.Value = this.romData.charAntiEvilDef[charIndex];
            numCharElectricDef.Value = this.romData.charElectricDef[charIndex];
            numCharHolywordDef.Value = this.romData.charHolywordDef[charIndex];
            numCharBroseDef.Value = this.romData.charBroseDef[charIndex];
            numCharBiologicalDef.Value = this.romData.charBiologicalDef[charIndex];
            numCharPsychicDef.Value = this.romData.charPsychicDef[charIndex];
            numCharMechanicalDef.Value = this.romData.charMechanicalDef[charIndex];
            numCharEfessDef.Value = this.romData.charEfessDef[charIndex];
            numCharDestroyDef.Value = this.romData.charDestroyDef[charIndex];

            cbCharLeftHandEq.SelectedIndex = this.romData.charLeftHandEq[charIndex];
            cbCharRightHandEq.SelectedIndex = this.romData.charRightHandEq[charIndex];
            cbCharHeadEq.SelectedIndex = this.romData.charHeadEq[charIndex];
            cbCharBodyEq.SelectedIndex = this.romData.charBodyEq[charIndex];

            cbCharStartTech1.SelectedIndex = this.romData.charStartTech[charIndex][0];    cbCharStartTech2.SelectedIndex = this.romData.charStartTech[charIndex][1];
            cbCharStartTech3.SelectedIndex = this.romData.charStartTech[charIndex][2];    cbCharStartTech4.SelectedIndex = this.romData.charStartTech[charIndex][3];
            cbCharStartTech5.SelectedIndex = this.romData.charStartTech[charIndex][4];    cbCharStartTech6.SelectedIndex = this.romData.charStartTech[charIndex][5];
            cbCharStartTech7.SelectedIndex = this.romData.charStartTech[charIndex][6];    cbCharStartTech8.SelectedIndex = this.romData.charStartTech[charIndex][7];
            cbCharStartTech9.SelectedIndex = this.romData.charStartTech[charIndex][8];    cbCharStartTech10.SelectedIndex = this.romData.charStartTech[charIndex][9];
            cbCharStartTech11.SelectedIndex = this.romData.charStartTech[charIndex][10];    cbCharStartTech12.SelectedIndex = this.romData.charStartTech[charIndex][11];
            cbCharStartTech13.SelectedIndex = this.romData.charStartTech[charIndex][12];    cbCharStartTech14.SelectedIndex = this.romData.charStartTech[charIndex][13];
            cbCharStartTech15.SelectedIndex = this.romData.charStartTech[charIndex][14];    cbCharStartTech16.SelectedIndex = this.romData.charStartTech[charIndex][15];

            cbCharStartSkill1.SelectedIndex = this.romData.charStartSkill[charIndex][0]; cbCharStartSkill2.SelectedIndex = this.romData.charStartSkill[charIndex][1];
            cbCharStartSkill3.SelectedIndex = this.romData.charStartSkill[charIndex][2]; cbCharStartSkill4.SelectedIndex = this.romData.charStartSkill[charIndex][3];
            cbCharStartSkill5.SelectedIndex = this.romData.charStartSkill[charIndex][4]; cbCharStartSkill6.SelectedIndex = this.romData.charStartSkill[charIndex][5];
            cbCharStartSkill7.SelectedIndex = this.romData.charStartSkill[charIndex][6]; cbCharStartSkill8.SelectedIndex = this.romData.charStartSkill[charIndex][7];

            numCharStartSkillCount1.Value = this.romData.charStartSkillUse[charIndex][0]; numCharStartSkillCount2.Value = this.romData.charStartSkillUse[charIndex][1];
            numCharStartSkillCount3.Value = this.romData.charStartSkillUse[charIndex][2]; numCharStartSkillCount4.Value = this.romData.charStartSkillUse[charIndex][3];
            numCharStartSkillCount5.Value = this.romData.charStartSkillUse[charIndex][4]; numCharStartSkillCount6.Value = this.romData.charStartSkillUse[charIndex][5];
            numCharStartSkillCount7.Value = this.romData.charStartSkillUse[charIndex][6]; numCharStartSkillCount8.Value = this.romData.charStartSkillUse[charIndex][7];
        }


        /*****************************************************
        * 
        * cbCharacterList_SelectedIndexChanged
        * 
       *****************************************************/

        private void cbCharacterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int characterIndex = this.cbCharacterList.SelectedIndex;
            if (!justLoadedRom)
            {
                //saveMonsterFields();
            }
            hideCharacterGrowths();

            loadCharacterFields();

            previousCharacterIndex = this.cbCharacterList.SelectedIndex;
        }






        /**********************************************************
         * ********************************************************
         * ********************************************************
         * 
         * MONSTER TAB
         * 
         * ********************************************************
         * ********************************************************
         * ********************************************************/




        /*****************************************************
         * 
         * loadMonsterList
         * 
        *****************************************************/

        private void loadMonsterList()
        {
            cbMonsterList.Items.Clear();
            for (var i = 0; i < Rom.maxMonsters; i++)
            {
                cbMonsterList.Items.Insert(i, this.romData.monsterName[i]);

            }
        }


        /*****************************************************
         * 
         * loadMonsterSkillList
         * 
        *****************************************************/

        private void loadMonsterSkillList()
        {
            List<System.Windows.Forms.ComboBox> skillcombos = new List<System.Windows.Forms.ComboBox>();

            skillcombos.Add(cbMonsterSkill1); skillcombos.Add(cbMonsterSkill2); skillcombos.Add(cbMonsterSkill3); skillcombos.Add(cbMonsterSkill4); 
            skillcombos.Add(cbMonsterSkill5); skillcombos.Add(cbMonsterSkill6); skillcombos.Add(cbMonsterSkill7); skillcombos.Add(cbMonsterSkill8);
            skillcombos.Add(cbMonsterAISkill1); skillcombos.Add(cbMonsterAISkill2); skillcombos.Add(cbMonsterAISkill3); skillcombos.Add(cbMonsterAISkill4);
            skillcombos.Add(cbMonsterSkillList);
            foreach(System.Windows.Forms.ComboBox cb in skillcombos) {
                cb.Items.Clear();
                cb.Items.Add("Regular Attack");
                cb.Items.Add("???");
                for (var i = 0; i < Rom.maxMonsterSkills; i++)
                {
                    cb.Items.Add(this.romData.monsterSkillName[i]);
                }
            }
            cbMonsterSkillList.Items.RemoveAt(0);
            cbMonsterSkillList.Items.RemoveAt(0);
        }



        /*****************************************************
         * 
         * cbMonsterList_SelectedIndexChanged
         * 
        *****************************************************/

        private void cbMonsterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int monsterIndex = this.cbMonsterList.SelectedIndex;
            if (!justLoadedRom)
            {
                saveMonsterFields();
            }
            loadMonsterFields();

            previousMonsterIndex = this.cbMonsterList.SelectedIndex;
        }





        /*****************************************************
         * 
         * loadMonsterFields
         * 
        *****************************************************/

        private void loadMonsterFields()
        {
            int monsterIndex = this.cbMonsterList.SelectedIndex;
            
            numMonsterHp.Value = this.romData.monsterMaxHP[monsterIndex];
            numMonsterAgi.Value = this.romData.monsterAgi[monsterIndex];
            numMonsterAtk.Value = this.romData.monsterAtk[monsterIndex];
            numMonsterDef.Value = this.romData.monsterDef[monsterIndex];
            numMonsterDex.Value = this.romData.monsterDex[monsterIndex];
            numMonsterMagDef.Value = this.romData.monsterMagDef[monsterIndex];
            numMonsterMen.Value = this.romData.monsterMen[monsterIndex];
            numMonsterStr.Value = this.romData.monsterStr[monsterIndex];

            switch (this.romData.monsterAtkStat[monsterIndex])
            {
                case 0:
                    rbMonsterAtkStatNone.Checked = true;
                    break;
                case 27:
                    rbMonsterAtkStatPoison.Checked = true;
                    break;
                case 28:
                    rbMonsterAtkStatParalysis.Checked = true;
                    break;

            }

            numMonsterExp.Value = this.romData.monsterExp[monsterIndex];
            numMonsterMeseta.Value = this.romData.monsterMeseta[monsterIndex];

            cbMonsterAtkElement.SelectedIndex = this.romData.monsterAtkElem[monsterIndex];

            numMonsterPhysDef.Value = this.romData.monsterPhysDef[monsterIndex];
            numMonsterEnergyDef.Value = this.romData.monsterEnergyDef[monsterIndex];
            numMonsterFireDef.Value = this.romData.monsterFireDef[monsterIndex];
            numMonsterGravityDef.Value = this.romData.monsterGravityDef[monsterIndex];
            numMonsterWaterDef.Value = this.romData.monsterWaterDef[monsterIndex];
            numMonsterAntiEvilDef.Value = this.romData.monsterAntiEvilDef[monsterIndex];
            numMonsterElectricDef.Value = this.romData.monsterElectricDef[monsterIndex];
            numMonsterHolywordDef.Value = this.romData.monsterHolywordDef[monsterIndex];
            numMonsterBroseDef.Value = this.romData.monsterBroseDef[monsterIndex];
            numMonsterBiologicalDef.Value = this.romData.monsterBiologicalDef[monsterIndex];
            numMonsterPsychicDef.Value = this.romData.monsterPsychicDef[monsterIndex];
            numMonsterMechanicalDef.Value = this.romData.monsterMechanicalDef[monsterIndex];
            numMonsterEfessDef.Value = this.romData.monsterEfessDef[monsterIndex];
            numMonsterDestroyDef.Value = this.romData.monsterDestroyDef[monsterIndex];


            cbMonsterSkill1.SelectedIndex = this.romData.monsterRegSkill1[monsterIndex];
            cbMonsterSkill2.SelectedIndex = this.romData.monsterRegSkill2[monsterIndex];
            cbMonsterSkill3.SelectedIndex = this.romData.monsterRegSkill3[monsterIndex];
            cbMonsterSkill4.SelectedIndex = this.romData.monsterRegSkill4[monsterIndex];
            cbMonsterSkill5.SelectedIndex = this.romData.monsterRegSkill5[monsterIndex];
            cbMonsterSkill6.SelectedIndex = this.romData.monsterRegSkill6[monsterIndex];
            cbMonsterSkill7.SelectedIndex = this.romData.monsterRegSkill7[monsterIndex];
            cbMonsterSkill8.SelectedIndex = this.romData.monsterRegSkill8[monsterIndex];

            cbMonsterAICond1.SelectedIndex = this.romData.monsterAICond1[monsterIndex];
            cbMonsterAICond2.SelectedIndex = this.romData.monsterAICond2[monsterIndex];
            cbMonsterAICond3.SelectedIndex = this.romData.monsterAICond3[monsterIndex];
            cbMonsterAICond4.SelectedIndex = this.romData.monsterAICond4[monsterIndex];
            cbMonsterAISkill1.SelectedIndex = this.romData.monsterAISkill1[monsterIndex];
            cbMonsterAISkill2.SelectedIndex = this.romData.monsterAISkill2[monsterIndex];
            cbMonsterAISkill3.SelectedIndex = this.romData.monsterAISkill3[monsterIndex];
            cbMonsterAISkill4.SelectedIndex = this.romData.monsterAISkill4[monsterIndex];
        }



        /*****************************************************
         * 
         * saveMonsterFields
         * 
        *****************************************************/


        private void saveMonsterFields()
        {
            int monsterIndex = previousMonsterIndex;

            this.romData.monsterMaxHP[monsterIndex] = Convert.ToInt32(numMonsterHp.Value);
            this.romData.monsterAgi[monsterIndex] = Convert.ToInt32(numMonsterAgi.Value);
            this.romData.monsterAtk[monsterIndex] = Convert.ToInt32(numMonsterAtk.Value);
            this.romData.monsterDef[monsterIndex] = Convert.ToInt32(numMonsterDef.Value);
            this.romData.monsterDex[monsterIndex] = Convert.ToInt32(numMonsterDex.Value);
            this.romData.monsterMagDef[monsterIndex] = Convert.ToInt32(numMonsterMagDef.Value);
            this.romData.monsterMen[monsterIndex] = Convert.ToInt32(numMonsterMen.Value);
            this.romData.monsterStr[monsterIndex] = Convert.ToInt32(numMonsterStr.Value);


            if (rbMonsterAtkStatNone.Checked) {
                this.romData.monsterAtkStat[monsterIndex] = 0;
            } else if (rbMonsterAtkStatPoison.Checked) {
                this.romData.monsterAtkStat[monsterIndex] = 27;
            } else if (rbMonsterAtkStatParalysis.Checked) {
                this.romData.monsterAtkStat[monsterIndex] = 28;
            }

            this.romData.monsterExp[monsterIndex] = Convert.ToInt32(numMonsterExp.Value);
            this.romData.monsterMeseta[monsterIndex] = Convert.ToInt32(numMonsterMeseta.Value);

            this.romData.monsterAtkElem[monsterIndex] = Convert.ToInt32(cbMonsterAtkElement.SelectedIndex);

            this.romData.monsterPhysDef[monsterIndex] =             Convert.ToInt32(numMonsterPhysDef.Value);
            this.romData.monsterEnergyDef[monsterIndex] = Convert.ToInt32(numMonsterEnergyDef.Value);
            this.romData.monsterFireDef[monsterIndex] = Convert.ToInt32(numMonsterFireDef.Value);
            this.romData.monsterGravityDef[monsterIndex] = Convert.ToInt32(numMonsterGravityDef.Value);
            this.romData.monsterWaterDef[monsterIndex] = Convert.ToInt32(numMonsterWaterDef.Value);
            this.romData.monsterAntiEvilDef[monsterIndex] = Convert.ToInt32(numMonsterAntiEvilDef.Value);
            this.romData.monsterElectricDef[monsterIndex] = Convert.ToInt32(numMonsterElectricDef.Value);
            this.romData.monsterHolywordDef[monsterIndex] = Convert.ToInt32(numMonsterHolywordDef.Value);
            this.romData.monsterBroseDef[monsterIndex] = Convert.ToInt32(numMonsterBroseDef.Value);
            this.romData.monsterBiologicalDef[monsterIndex] = Convert.ToInt32(numMonsterBiologicalDef.Value);
            this.romData.monsterPsychicDef[monsterIndex] = Convert.ToInt32(numMonsterPsychicDef.Value);
            this.romData.monsterMechanicalDef[monsterIndex] = Convert.ToInt32(numMonsterMechanicalDef.Value);
            this.romData.monsterEfessDef[monsterIndex] = Convert.ToInt32(numMonsterEfessDef.Value);
            this.romData.monsterDestroyDef[monsterIndex] = Convert.ToInt32(numMonsterDestroyDef.Value);


            this.romData.monsterRegSkill1[monsterIndex] = Convert.ToInt32(cbMonsterSkill1.SelectedIndex);
            this.romData.monsterRegSkill2[monsterIndex] =             Convert.ToInt32(cbMonsterSkill2.SelectedIndex);
            this.romData.monsterRegSkill3[monsterIndex] =             Convert.ToInt32(cbMonsterSkill3.SelectedIndex);
            this.romData.monsterRegSkill4[monsterIndex] =             Convert.ToInt32(cbMonsterSkill4.SelectedIndex);
            this.romData.monsterRegSkill5[monsterIndex] =             Convert.ToInt32(cbMonsterSkill5.SelectedIndex);
            this.romData.monsterRegSkill6[monsterIndex] =             Convert.ToInt32(cbMonsterSkill6.SelectedIndex);
            this.romData.monsterRegSkill7[monsterIndex] =             Convert.ToInt32(cbMonsterSkill7.SelectedIndex);
            this.romData.monsterRegSkill8[monsterIndex] =             Convert.ToInt32(cbMonsterSkill8.SelectedIndex);

            this.romData.monsterAICond1[monsterIndex] =             Convert.ToInt32(cbMonsterAICond1.SelectedIndex);
            this.romData.monsterAICond2[monsterIndex] =             Convert.ToInt32(cbMonsterAICond2.SelectedIndex);
            this.romData.monsterAICond3[monsterIndex] =             Convert.ToInt32(cbMonsterAICond3.SelectedIndex);
            this.romData.monsterAICond4[monsterIndex] =             Convert.ToInt32(cbMonsterAICond4.SelectedIndex);
            this.romData.monsterAISkill1[monsterIndex] =             Convert.ToInt32(cbMonsterAISkill1.SelectedIndex);
            this.romData.monsterAISkill2[monsterIndex] =             Convert.ToInt32(cbMonsterAISkill2.SelectedIndex);
            this.romData.monsterAISkill3[monsterIndex] =Convert.ToInt32(cbMonsterAISkill3.SelectedIndex);
            this.romData.monsterAISkill4[monsterIndex] = Convert.ToInt32(cbMonsterAISkill4.SelectedIndex);

            //this.romData.monsterMaxHP[monsterIndex] = Convert.ToInt32(numMonsterHp.Value);

        }






        /*****************************************************
         * 
         * loadMonsterSkillEffects
         * 
        *****************************************************/

        private void loadMonsterSkillEffects()
        {
            List<System.Windows.Forms.ComboBox> combos = new List<System.Windows.Forms.ComboBox>();
            String hexstr;
            combos.Add(cbMonsterSkillEffect); combos.Add(cbPlayerTechEffect); combos.Add(cbPlayerSkillEffect);
            foreach (System.Windows.Forms.ComboBox cb in combos)
            {
                cb.Items.Add("None");
                cb.Items.Add("Damage");
                cb.Items.Add("Death");
                cb.Items.Add("Attack down");
                for (var i = 4; i < 6; i++)
                {
                    hexstr = i.ToString("X" + (2).ToString());
                    cb.Items.Add("Byte " + hexstr);
                }
                cb.Items.Add("Agility down");
                cb.Items.Add("Sleep");
                cb.Items.Add("Silence (as the player SEALS technique)");
                cb.Items.Add("Attack up");
                cb.Items.Add("Defense up");
                cb.Items.Add("Magic defense up");
                cb.Items.Add("Agility up");
                for (var i = 13; i < 18; i++)
                {
                    hexstr = i.ToString("X" + (2).ToString());
                    cb.Items.Add("Byte " + hexstr);
                }
                cb.Items.Add("Heal HP");

                for (var i = 19; i < 27; i++)
                {
                    hexstr = i.ToString("X" + (2).ToString());
                    cb.Items.Add("Byte " + hexstr);
                }

                cb.Items.Add("Poison");
                cb.Items.Add("Paralyze");

                for (var i = 29; i < 130; i++)
                {
                    hexstr = i.ToString("X" + (2).ToString());
                    cb.Items.Add("Byte " + hexstr);
                }

                cb.Items.Add("Silence (as the enemy SEALS technique)");
            }
        }





        /*****************************************************
         * 
         * loadAIConditionComboBoxes
         * 
        *****************************************************/

        private void loadAIConditionComboBoxes()
        {
            List<System.Windows.Forms.ComboBox> aicombos = new List<System.Windows.Forms.ComboBox>();

            aicombos.Add(cbMonsterAICond1); aicombos.Add(cbMonsterAICond2); aicombos.Add(cbMonsterAICond3); aicombos.Add(cbMonsterAICond4);
            foreach (System.Windows.Forms.ComboBox cb in aicombos)
            {
                cb.Items.Add("* No condition *");
                cb.Items.Add("If adjacent to empty space");
                cb.Items.Add("If HP <= 50%");
                cb.Items.Add("If adjacent to " + this.romData.monsterName[25]);
                cb.Items.Add("If adjacent to " + this.romData.monsterName[23]);
                cb.Items.Add("If " + this.romData.monsterName[40] + " adjacent to " + this.romData.monsterName[42]);
                cb.Items.Add("If adjacent to " + this.romData.monsterName[34]);
                cb.Items.Add("If attacked physically");
                cb.Items.Add("If attacked magically");
                cb.Items.Add("If alone");
                cb.Items.Add("If attacked with techs");
                cb.Items.Add("If surprise attack on player");
                cb.Items.Add("If tech sealed");
                cb.Items.Add("If adjacent to " + this.romData.monsterName[86]);
                cb.Items.Add("If adjacent to " + this.romData.monsterName[84]);
                cb.Items.Add("If anyone is damaged");
                cb.Items.Add("unused...");
                cb.Items.Add("If HP <= 25%");
                cb.Items.Add("If there are three " + this.romData.monsterName[123]);
                cb.Items.Add("If Psycho Wand was used on Zio");
            }
        }





        /*****************************************************
         * 
         * loadMonsterSkillStats
         * 
        *****************************************************/

        private void loadMonsterSkillStats()
        {
            List<System.Windows.Forms.ComboBox> combos = new List<System.Windows.Forms.ComboBox>();

            combos.Add(cbMonsterSkillOffense); combos.Add(cbMonsterSkillDefense); combos.Add(cbPlayerTechDefense); combos.Add(cbPlayerSkillOffense); combos.Add(cbPlayerSkillDefense);
            foreach (System.Windows.Forms.ComboBox cb in combos)
            {
                cb.Items.Add("None");
                cb.Items.Add("Strength");
                cb.Items.Add("Mental");
                cb.Items.Add("Agility");
                cb.Items.Add("Dexterity");
                cb.Items.Add("Attack");
                cb.Items.Add("Defense");
                cb.Items.Add("Magic defense");
                cb.Items.Add("Can't be used if tech sealed");
            }
            cbPlayerSkillOffense.Items.RemoveAt(8);
            cbPlayerSkillOffense.Items.Add("Strength, requires weapon");
            cbPlayerSkillOffense.Items.Add("Mental, requires weapon");
            cbPlayerSkillOffense.Items.Add("Agility, requires weapon");
            cbPlayerSkillOffense.Items.Add("Dexterity, requires weapon");
            cbPlayerSkillOffense.Items.Add("Attack, requires weapon");
        }





        /*****************************************************
         * 
         * loadMonsterSkillTarget
         * 
        *****************************************************/

        private void loadMonsterSkillTarget()
        {
            String hexstr;
            List<System.Windows.Forms.ComboBox> combos = new List<System.Windows.Forms.ComboBox>();

            combos.Add(cbMonsterSkillTarget);
            combos.Add(cbPlayerTechTarget);
            combos.Add(cbPlayerSkillTarget);
            foreach (System.Windows.Forms.ComboBox cb in combos)
            {
                cb.Items.Add("None");
                cb.Items.Add("One enemy");
                cb.Items.Add("All enemies");
                cb.Items.Add("Self");
                for (var i = 4; i < 8; i++)
                {
                    hexstr = i.ToString("X" + (2).ToString());
                    cb.Items.Add("Byte " + hexstr);
                }
                cb.Items.Add("One player");
                cb.Items.Add("All players");
                for (var i = 10; i < 17; i++)
                {
                    hexstr = i.ToString("X" + (2).ToString());
                    cb.Items.Add("Byte " + hexstr);
                }
                cb.Items.Add("One enemy (in battle)");
                cb.Items.Add("All enemies (in battle)");

                hexstr = (19).ToString("X" + (2).ToString());
                cb.Items.Add("Byte " + hexstr);

                cb.Items.Add("One biological player");
                cb.Items.Add("All biological players");

                for (var i = 22; i < 24; i++)
                {
                    hexstr = i.ToString("X" + (2).ToString());
                    cb.Items.Add("Byte " + hexstr);
                }

                cb.Items.Add("One player (in battle)");
                cb.Items.Add("All players (in battle)");

                for (var i = 26; i < 32; i++)
                {
                    hexstr = i.ToString("X" + (2).ToString());
                    cb.Items.Add("Byte " + hexstr);
                }

                cb.Items.Add("Non-combat teleport");

                for (var i = 33; i < 51; i++)
                {
                    hexstr = i.ToString("X" + (2).ToString());
                    cb.Items.Add("Byte " + hexstr);
                }

                cb.Items.Add("Self (in battle)");
                cb.Items.Add("One biological player (in battle)");
                cb.Items.Add("All biological players (in battle)");

                for (var i = 54; i < 56; i++)
                {
                    hexstr = i.ToString("X" + (2).ToString());
                    cb.Items.Add("Byte " + hexstr);
                }

                cb.Items.Add("One player (in/out battles)");
                cb.Items.Add("All players (in/out battles)");

                for (var i = 58; i < 97; i++)
                {
                    hexstr = i.ToString("X" + (2).ToString());
                    cb.Items.Add("Byte " + hexstr);
                }
            }
        }








        /*****************************************************
         * 
         * loadMonsterSkillFields
         * 
        *****************************************************/

        private void loadMonsterSkillFields()
        {
            int monsterSkillIndex = this.cbMonsterSkillList.SelectedIndex;

            numMonsterSkillPower.Value = this.romData.monsterSkillPower[monsterSkillIndex];
            cbMonsterSkillTarget.SelectedIndex = this.romData.monsterSkillTarget[monsterSkillIndex];
            cbMonsterSkillElement.SelectedIndex = this.romData.monsterSkillElement[monsterSkillIndex];
            cbMonsterSkillEffect.SelectedIndex = this.romData.monsterSkillEffect[monsterSkillIndex];

            if (this.romData.monsterSkillOffense[monsterSkillIndex] == 130)
            {
                cbMonsterSkillOffense.SelectedIndex = 8;
            }
            else
            {
                cbMonsterSkillOffense.SelectedIndex = this.romData.monsterSkillOffense[monsterSkillIndex];
            }

            if (this.romData.monsterSkillDefense[monsterSkillIndex] == 130)
            {
                cbMonsterSkillDefense.SelectedIndex = 8;
            }
            else
            {
                cbMonsterSkillDefense.SelectedIndex = this.romData.monsterSkillDefense[monsterSkillIndex];
            }
            //cbMonsterSkillDefense.SelectedIndex = this.romData.monsterSkillDefense[monsterSkillIndex];

        }








        /*****************************************************
         * 
         * saveMonsterSkillFields
         * 
        *****************************************************/

        private void saveMonsterSkillFields()
        {
            int monsterSkillIndex = previousMonsterSkillIndex;

            this.romData.monsterSkillPower[monsterSkillIndex] = Convert.ToInt32(numMonsterSkillPower.Value);
            this.romData.monsterSkillTarget[monsterSkillIndex] = cbMonsterSkillTarget.SelectedIndex;
            this.romData.monsterSkillElement[monsterSkillIndex] = cbMonsterSkillElement.SelectedIndex;
            this.romData.monsterSkillEffect[monsterSkillIndex] = cbMonsterSkillEffect.SelectedIndex;

            if (cbMonsterSkillOffense.SelectedIndex == 8)
            {
                this.romData.monsterSkillOffense[monsterSkillIndex] = 130;
            }
            else
            {
                this.romData.monsterSkillOffense[monsterSkillIndex] = cbMonsterSkillOffense.SelectedIndex;
            }


            if (cbMonsterSkillDefense.SelectedIndex == 8)
            {
                this.romData.monsterSkillDefense[monsterSkillIndex] = 130;
            }
            else
            {
                this.romData.monsterSkillDefense[monsterSkillIndex] = cbMonsterSkillDefense.SelectedIndex;
            }

            //this.romData.monsterSkillDefense[monsterSkillIndex] = cbMonsterSkillDefense.SelectedIndex;

        }





        /*****************************************************
         * 
         * loadPlayerTechList
         * 
        *****************************************************/

        private void loadPlayerTechList()
        {

            //load tech combos
            List<System.Windows.Forms.ComboBox> combos = new List<System.Windows.Forms.ComboBox>();

            combos.Add(cbPlayerTechList);
            foreach(System.Windows.Forms.ComboBox cb in combos) {
                cb.Items.Clear();
                for (var i = 0; i < Rom.maxPlayerTechs; i++)
                {
                    cb.Items.Add(this.romData.playerTechName[i]);
                }
            }

            combos.Clear();
            // load skill combos
            combos = new List<System.Windows.Forms.ComboBox>();

            combos.Add(cbPlayerSkillList);
            foreach (System.Windows.Forms.ComboBox cb in combos)
            {
                cb.Items.Clear();
                for (var i = 0; i < Rom.maxPlayerSkills; i++)
                {
                    cb.Items.Add(this.romData.playerTechName[i + Rom.maxPlayerTechs]);
                }
            }
        }








        /*****************************************************
         * 
         * loadPlayerTechFields
         * 
        *****************************************************/

        private void loadPlayerTechFields()
        {
            int playerTechIndex = this.cbPlayerTechList.SelectedIndex;

            numPlayerTechPower.Value = this.romData.playerTechPower[playerTechIndex];
            cbPlayerTechTarget.SelectedIndex = this.romData.playerTechTarget[playerTechIndex];
            cbPlayerTechElement.SelectedIndex = this.romData.playerTechElement[playerTechIndex];
            cbPlayerTechEffect.SelectedIndex = this.romData.playerTechEffect[playerTechIndex];
            numPlayerTechTPCost.Value = this.romData.playerTechTPCost[playerTechIndex];

            if (this.romData.playerTechDefense[playerTechIndex] == 130)
            {
                cbPlayerTechDefense.SelectedIndex = 8;
            }
            else
            {
                cbPlayerTechDefense.SelectedIndex = this.romData.playerTechDefense[playerTechIndex];
            }
            //cbMonsterSkillDefense.SelectedIndex = this.romData.monsterSkillDefense[monsterSkillIndex];

        }






        /*****************************************************
         * 
         * loadPlayerSkillFields
         * 
        *****************************************************/

        private void loadPlayerSkillFields()
        {
            int playerSkillIndex = this.cbPlayerSkillList.SelectedIndex + Rom.maxPlayerTechs;

            numPlayerSkillPower.Value = this.romData.playerTechPower[playerSkillIndex];
            cbPlayerSkillTarget.SelectedIndex = this.romData.playerTechTarget[playerSkillIndex];
            //cbPlayerSkillElement.SelectedIndex = this.romData.playerTechElement[playerSkillIndex];
            cbPlayerSkillEffect.SelectedIndex = this.romData.playerTechEffect[playerSkillIndex];
            
            //numPlayerTechTPCost.Value = this.romData.playerTechTPCost[playerSkillIndex];

            if (this.romData.playerTechElement[playerSkillIndex] == 16)
            {
                cbPlayerSkillElement.SelectedIndex = 15;
            }
            else
            {
                cbPlayerSkillElement.SelectedIndex = this.romData.playerTechElement[playerSkillIndex];
            }

            if (this.romData.playerTechDefense[playerSkillIndex] == 130)
            {
                cbPlayerSkillDefense.SelectedIndex = 8;
            }
            else
            {
                cbPlayerSkillDefense.SelectedIndex = this.romData.playerTechDefense[playerSkillIndex];
            }


            if (this.romData.playerTechTPCost[playerSkillIndex] >= 129)
            {
                cbPlayerSkillOffense.SelectedIndex = 8 + (this.romData.playerTechTPCost[playerSkillIndex] - 129);
            }
            else
            {
                cbPlayerSkillOffense.SelectedIndex = this.romData.playerTechTPCost[playerSkillIndex];
            }
            
            //cbMonsterSkillDefense.SelectedIndex = this.romData.monsterSkillDefense[monsterSkillIndex];

        }








        /*****************************************************
         * 
         * savePlayerTechFields
         * 
        *****************************************************/

        private void savePlayerTechFields()
        {
            int playerTechIndex = previousPlayerTechIndex;

            this.romData.playerTechPower[playerTechIndex] = Convert.ToInt32(numPlayerTechPower.Value);
            this.romData.playerTechTarget[playerTechIndex] = cbPlayerTechTarget.SelectedIndex;
            this.romData.playerTechElement[playerTechIndex] = cbPlayerTechElement.SelectedIndex;
            this.romData.playerTechEffect[playerTechIndex] = cbPlayerTechEffect.SelectedIndex;

            this.romData.playerTechTPCost[playerTechIndex] = Convert.ToInt32(numPlayerTechTPCost.Value);


            if (cbPlayerTechDefense.SelectedIndex == 8)
            {
                this.romData.playerTechDefense[playerTechIndex] = 130;
            }
            else
            {
                this.romData.playerTechDefense[playerTechIndex] = cbPlayerTechDefense.SelectedIndex;
            }

            //this.romData.monsterSkillDefense[monsterSkillIndex] = cbMonsterSkillDefense.SelectedIndex;

        }





        /*****************************************************
         * 
         * savePlayerSkillFields
         * 
        *****************************************************/

        private void savePlayerSkillFields()
        {
            int playerSkillIndex = previousPlayerSkillIndex + Rom.maxPlayerTechs;

            this.romData.playerTechPower[playerSkillIndex] = Convert.ToInt32(numPlayerSkillPower.Value);
            this.romData.playerTechTarget[playerSkillIndex] = cbPlayerSkillTarget.SelectedIndex;
            //cbPlayerSkillElement.SelectedIndex = this.romData.playerTechElement[playerSkillIndex];
            this.romData.playerTechEffect[playerSkillIndex] = cbPlayerSkillEffect.SelectedIndex;

            //numPlayerTechTPCost.Value = this.romData.playerTechTPCost[playerSkillIndex];

            if (cbPlayerSkillElement.SelectedIndex == 15)
            {
                this.romData.playerTechElement[playerSkillIndex] = 16;
            }
            else
            {
                this.romData.playerTechElement[playerSkillIndex] = cbPlayerSkillElement.SelectedIndex;
            }

            if (cbPlayerSkillDefense.SelectedIndex == 8)
            {
                this.romData.playerTechDefense[playerSkillIndex] = 130;
            }
            else
            {
                this.romData.playerTechDefense[playerSkillIndex] = cbPlayerSkillDefense.SelectedIndex;
            }


            if (cbPlayerSkillOffense.SelectedIndex >= 8)
            {
                this.romData.playerTechTPCost[playerSkillIndex] = 129 + (cbPlayerSkillOffense.SelectedIndex - 8);
            }
            else
            {
                this.romData.playerTechTPCost[playerSkillIndex] = cbPlayerSkillOffense.SelectedIndex;
            }

            //cbMonsterSkillDefense.SelectedIndex = this.romData.monsterSkillDefense[monsterSkillIndex];

        }




        




        /*****************************************************
         * 
         * cbMonsterSkillList_SelectedIndexChanged
         * 
        *****************************************************/

        private void cbMonsterSkillList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int monsterSkillIndex = this.cbMonsterSkillList.SelectedIndex;
            if (!justLoadedRom)
            {
                saveMonsterSkillFields();
            }
            loadMonsterSkillFields();

            previousMonsterSkillIndex = this.cbMonsterSkillList.SelectedIndex;
        }



        private void cbPlayerTechList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int playerTechIndex = this.cbPlayerTechList.SelectedIndex;
            if (!justLoadedRom)
            {
                savePlayerTechFields();
            }
            loadPlayerTechFields();

            previousPlayerTechIndex = this.cbPlayerTechList.SelectedIndex;

        }

        private void cbPlayerSkillList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int playerSkillIndex = this.cbPlayerSkillList.SelectedIndex;
            if (!justLoadedRom)
            {
                savePlayerSkillFields();
            }
            loadPlayerSkillFields();

            previousPlayerSkillIndex = this.cbPlayerSkillList.SelectedIndex;

        }



        


        /*****************************************************
         * 
         * saveToolStripMenuItem_Click
         * 
        *****************************************************/

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveMonsterFields();
            savePlayerTechFields();
            saveMonsterSkillFields();
            savePlayerSkillFields();
            this.romData.SaveData(selectedRom);
            MessageBox.Show("Saved!");
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutWindow = new AboutBox1();
            aboutWindow.Show();
        }
    }
}
