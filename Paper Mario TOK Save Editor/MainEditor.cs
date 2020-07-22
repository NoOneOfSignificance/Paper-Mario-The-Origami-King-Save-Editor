using DamienG.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Paper_Mario_TOK_Save_Editor
{
    public partial class MainEditor : Form
    {
        public MainEditor()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string appver = "v1.0";
            XmlDocument appinfor = new XmlDocument();

            appinfor.Load("https://raw.githubusercontent.com/zSupremoz/Paper-Mario-The-Origami-King-Save-Editor/master/appinfo.xml");
            XmlElement root = appinfor.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("//appinfo");

            foreach (XmlNode node in nodes)
            {
                string appver_xml = node["appver"].InnerText;
                string tag = node["apptag"].InnerText;

                if (appver != appver_xml)
                {
                    DialogResult Update = MessageBox.Show("The application is out of date! Would you like to go to the latest release on GitHub?", "Application Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (Update == DialogResult.Yes)
                    {
                        string ReleaseLink = "https://github.com/zSupremoz/Paper-Mario-The-Origami-King-Save-Editor/releases/tag/" + tag;
                        Process.Start(ReleaseLink);
                    }
                }
            }

            ItemSelectBox.SelectedIndex = 0;
            SaveLoaded = false;
            SaveCheck();
            if (Properties.Settings.Default.BackupReminder == true)
            {
                MessageBox.Show("Please be sure to always make backups of your save files. You can enable a setting to automatically create backups when opening a save in the Settings tab under File -> Settings.", "Paper Mario: The Origami King Save Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #region Init Variables
        public string SaveFilePath;
        public string SaveFileContents;
        public string OldHash;
        public string SaveFileContentsNoHash;
        public string JsonRead;
        public bool SaveLoaded;

        //Bibliofold Status Inits
        public string EarthBook;
        public string WaterBook;
        public string FireBook;
        public string IceBook;

        //Items
        public int ItemSlot;

        //Partners
        public string Partner0ID;

        OpenFileDialog OpenSaveFile = new OpenFileDialog();
        SaveFileDialog SaveFileEdits = new SaveFileDialog();
        #endregion

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region File Reading
            OpenSaveFile.Filter = "data00.bin (*.bin)|*bin";

            if (OpenSaveFile.ShowDialog() == DialogResult.OK)
            {
                SaveFilePath = OpenSaveFile.FileName;
                string BackupPath = SaveFilePath.Replace("data00", "data00.bak");
                SaveLoaded = true;

                var FileStream = OpenSaveFile.OpenFile();

                if (Properties.Settings.Default.AutoBackups == true)
                {
                    if (Properties.Settings.Default.BackupPath.Length == 0)
                    {
                        if (File.Exists(BackupPath))
                        {
                            File.Delete(BackupPath);
                            File.Copy(SaveFilePath, BackupPath);
                        }
                        else
                        {
                            File.Copy(SaveFilePath, BackupPath);
                        }
                    }
                    if (Properties.Settings.Default.BackupPath.Length > 0)
                    {
                        BackupPath = Properties.Settings.Default.BackupPath + @"\data00.bak.bin";
                        if (File.Exists(BackupPath))
                        {
                            try
                            {
                                File.Delete(BackupPath);
                                File.Copy(SaveFilePath, BackupPath);
                            } catch (DirectoryNotFoundException)
                            {
                                MessageBox.Show("The backup could not be created because your selected backup directory has been deleted or doesn't exist. Please change this in Settings if you wish to continue using Automatic Backups.", "Could not Create Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            try
                            {
                                File.Copy(SaveFilePath, BackupPath);
                            }
                            catch (DirectoryNotFoundException)
                            {
                                MessageBox.Show("The backup could not be created because your selected backup directory has been deleted or doesn't exist. Please change this in Settings if you wish to continue using Automatic Backups.", "Could not Create Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    
                }

                using (StreamReader Reader = new StreamReader(FileStream))
                {
                    //Gets File Contents then removes the old hash
                    SaveFileContents = Reader.ReadToEnd();
                    OldHash = SaveFileContents.Substring(SaveFileContents.Length - 8);
                    SaveFileContentsNoHash = SaveFileContents.Replace(OldHash, "");

                    JsonRead = $@"{SaveFileContentsNoHash}";

                    var stats = JsonConvert.DeserializeObject<dynamic>(JsonRead);

                    HPCounter.Value = Convert.ToInt32(stats.Pouch.hp);
                    MaxHPCounter.Value = Convert.ToInt32(stats.Pouch.hp_max);
                    CoinCounter.Value = Convert.ToInt32(stats.Pouch.coin);
                    CoinsSpentCounter.Value = Convert.ToInt32(stats.Pouch.use_coin);
                    CurrentConfettiCounter.Value = stats.Pouch.confetti_paper;
                    BagCapacityCounter.Value = Convert.ToInt32(stats.Pouch.confetti_paper_max);
                    SaveCheck();

                    //This gets used to read Bibliofold unlocks.
                    JObject obj = JObject.Parse(JsonRead);
                    var val = obj["Pouch"];
                    EarthBook = val["equipment"]["magic"]["0"]["type"].ToString();
                    WaterBook = val["equipment"]["magic"]["1"]["type"].ToString();
                    FireBook = val["equipment"]["magic"]["2"]["type"].ToString();
                    IceBook = val["equipment"]["magic"]["3"]["type"].ToString();

                    EarthBookBox.Checked = (EarthBook == "3");

                    WaterBookBox.Checked = (WaterBook == "3");

                    FireBookBox.Checked = (FireBook == "3");

                    IceBookBox.Checked = (IceBook == "3");

                    ReadInventory();

                    PlaytimeHourCounter.Value = Convert.ToInt32(val["play_time"]["play_hour"]);
                    PlaytimeMinuteCounter.Value = Convert.ToInt32(val["play_time"]["play_min"]);
                    PlaytimeSecondCounter.Value = Convert.ToInt32(val["play_time"]["play_sec"]);
                    GameOverCounter.Value = Convert.ToInt32(val["record"]["game_over_count"]);
                    ToadPointCounter.Value = Convert.ToInt32(val["kinopio_point"]);

                    string PartnerStatus = val["party_infor"]["partyMemberName"].ToString();

                    if (PartnerStatus == "{}")
                    {
                        PartnerSelectBox.SelectedIndex = 0;
                    }
                    else
                    {
                        Partner0ID = val["party_infor"]["partyMemberName"]["0"].ToString();
                        PartnerSelectBox.SelectedIndex = PartnerSelectBox.Items.IndexOf(ToPartnerName(Partner0ID));
                    }
                }
            }
            #endregion
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region File Saving
            if (SaveLoaded == false)
            {
                MessageBox.Show("Unable to Save");
                return;
            }
            JObject obj = JObject.Parse(JsonRead);
            
            
            var val = obj["Pouch"];

            val["hp"] = Convert.ToInt32(HPCounter.Value);
            val["hp_max"] = Convert.ToInt32(MaxHPCounter.Value);
            val["coin"] = Convert.ToInt32(CoinCounter.Value);
            val["use_coin"] = Convert.ToInt32(CoinsSpentCounter.Value);
            val["confetti_paper"] = Convert.ToDouble(CurrentConfettiCounter.Value);
            val["confetti_paper_max"] = Convert.ToInt32(BagCapacityCounter.Value);
            val["record"]["game_over_count"] = Convert.ToInt32(GameOverCounter.Value);
            val["kinopio_point"] = Convert.ToInt32(ToadPointCounter.Value);

            if (EarthBookBox.Checked)
            {
                val["equipment"]["magic"]["0"]["itemId"] = "O_BOOK_EARTH";
                val["equipment"]["magic"]["0"]["type"] = 3;
            }
            else
            {
                val["equipment"]["magic"]["0"]["itemId"] = "";
                val["equipment"]["magic"]["0"]["type"] = -1;
            }
            if (WaterBookBox.Checked)
            {
                val["equipment"]["magic"]["1"]["itemId"] = "O_BOOK_WATER";
                val["equipment"]["magic"]["1"]["type"] = 3;
            }
            else
            {
                val["equipment"]["magic"]["1"]["itemId"] = "";
                val["equipment"]["magic"]["1"]["type"] = -1;
            }
            if (FireBookBox.Checked)
            {
                val["equipment"]["magic"]["2"]["itemId"] = "O_BOOK_FIRE";
                val["equipment"]["magic"]["2"]["type"] = 3;
            }
            else
            {
                val["equipment"]["magic"]["2"]["itemId"] = "";
                val["equipment"]["magic"]["2"]["type"] = -1;
            }
            if (IceBookBox.Checked)
            {
                val["equipment"]["magic"]["3"]["itemId"] = "O_BOOK_ICE";
                val["equipment"]["magic"]["3"]["type"] = 3;
            }
            else
            {
                val["equipment"]["magic"]["3"]["itemId"] = "";
                val["equipment"]["magic"]["3"]["type"] = 1;
            }

            string result = obj.ToString() + OldHash;

            result = result.Replace("/", "\\/");
            result = result.Replace(":", " :");
            result = result.Replace("\"misc\" : {}", "\"misc\" : {\n  }");
            result = result.Replace("\"partyMemberName\" : \"{}\"", "\"partyMemberName\" : {\n      }");
            result = result.Replace(": {}", ": {\n    }");
            #endregion

            SaveFileEdits.Filter = "data00.bin|*.bin";
            if (SaveFileEdits.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SaveFileEdits.FileName, result);

                #region Crc32Recalculation
                byte[] Hash = new byte[4];
                byte[] Buffer;
                using (BinaryReader Reader = new BinaryReader(new FileStream(SaveFileEdits.FileName, FileMode.Open, FileAccess.Read)))
                {
                    Buffer = Reader.ReadBytes((int)Reader.BaseStream.Length - 8);
                    Crc32 crc32 = new Crc32();
                    Hash = crc32.ComputeHash(Buffer);
                }
                using (StreamWriter Writer = new StreamWriter(new FileStream(SaveFileEdits.FileName, FileMode.Open, FileAccess.Write)))
                {
                    Writer.BaseStream.Position = (Writer.BaseStream.Length - 8);
                    Writer.Write(BitConverter.ToString(Hash).Replace("-", "").ToLower());
                }
                #endregion
                MessageBox.Show("data00.bin has been saved.", "Paper Mario: The Origami King Save Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region Function for checking if the file is loaded
        public void SaveCheck()
        {
            if (SaveLoaded == false)
            {
                HPCounter.Enabled = false;
                MaxHPCounter.Enabled = false;
                CoinCounter.Enabled = false;
                CoinsSpentCounter.Enabled = false;
                CurrentConfettiCounter.Enabled = false;
                BagCapacityCounter.Enabled = false;
                EarthBookBox.Enabled = false;
                WaterBookBox.Enabled = false;
                FireBookBox.Enabled = false;
                IceBookBox.Enabled = false;
                ItemListBox.Enabled = false;
                ItemSelectBox.Enabled = false;
                UsedEnduranceCounter.Enabled = false;
                UsedBreakRateCounter.Enabled = false;
                ItemChangesApply.Enabled = false;
                PlaytimeHourCounter.Enabled = false;
                PlaytimeMinuteCounter.Enabled = false;
                PlaytimeSecondCounter.Enabled = false;
                PartnerSelectBox.Enabled = false;
                ToadPointCounter.Enabled = false;
            }
            if (SaveLoaded == true)
            {
                HPCounter.Enabled = true;
                MaxHPCounter.Enabled = true;
                CoinCounter.Enabled = true;
                CoinsSpentCounter.Enabled = true;
                CurrentConfettiCounter.Enabled = true;
                BagCapacityCounter.Enabled = true;
                EarthBookBox.Enabled = true;
                WaterBookBox.Enabled = true;
                FireBookBox.Enabled = true;
                IceBookBox.Enabled = true;
                ItemListBox.Enabled = true;
                ItemSelectBox.Enabled = true;
                UsedEnduranceCounter.Enabled = true;
                UsedBreakRateCounter.Enabled = true;
                ItemChangesApply.Enabled = true;
                PlaytimeHourCounter.Enabled = true;
                PlaytimeMinuteCounter.Enabled = true;
                PlaytimeSecondCounter.Enabled = true;
                PartnerSelectBox.Enabled = true;
                ToadPointCounter.Enabled = true;
            }
        }
        #endregion
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationInfo form2 = new ApplicationInfo();
            form2.Show();
        }



        InventoryItem[] items = new InventoryItem[151];

        public void ReadInventory()
        {
            JObject obj = JObject.Parse(JsonRead);
            var val = obj["Pouch"];

            for (int ItemSlot = 0; ItemSlot < items.Length; ItemSlot++)
            {
                string IStoString = ItemSlot.ToString();
                items[ItemSlot] = new InventoryItem
                {
                    itemId = val["equipment"]["bag"][IStoString]["itemId"].ToString(),
                    usedEndurance = Convert.ToInt32(val["equipment"]["bag"][IStoString]["usedEndurance"].ToString()),
                    usedBreakRate = Convert.ToInt32(val["equipment"]["bag"][IStoString]["usedBreakRate"].ToString()),
                    
                };
                items[ItemSlot].type = Convert.ToInt32(val["equipment"]["bag"][IStoString]["type"]);
                items[ItemSlot].stackCount = Convert.ToInt32(val["equipment"]["bag"][IStoString]["stackCount"].ToString());
            }

            for (int ItemListIndex = 0; ItemListIndex < items.Length; ItemListIndex++)
            {
                items[ItemListIndex].itemName = ToItemName(items[ItemListIndex].itemId);
                if (items[ItemListIndex].itemName == "Error")
                {
                    items[ItemListIndex].itemName = items[ItemListIndex].itemId;
                }
                if (items[ItemListIndex].itemName == "")
                {
                    items[ItemListIndex].itemName = "Unused Slot";
                    items[ItemListIndex].itemId = "";
                    items[ItemListIndex].usedEndurance = 0;
                    items[ItemListIndex].usedBreakRate = 0;
                    items[ItemListIndex].type = -1;
                    items[ItemListIndex].stackCount = 0;
                }
                if (items[ItemListIndex].type == 2)
                {
                    items[ItemListIndex].itemName = items[ItemListIndex].itemName + " - Accessory";
                }
                string result = $"[{ItemListIndex}] " + items[ItemListIndex].itemName;

                ItemListBox.Items.Add(result);
            }

        }


        private void ItemListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ItemCheck = ItemListBox.SelectedItem.ToString();
            if (ItemCheck.Contains("Unused"))
            {
                ItemNameLabel.Text = "Unused Slot";
            }
            else
            {
                ItemNameLabel.Text = ToItemName(items[ItemListBox.SelectedIndex].itemId);
            }
            ItemSelectBox.SelectedIndex = ItemSelectBox.Items.IndexOf(ToItemName(items[ItemListBox.SelectedIndex].itemId));
            UsedEnduranceCounter.Value = items[ItemListBox.SelectedIndex].usedEndurance;
            UsedBreakRateCounter.Value = items[ItemListBox.SelectedIndex].usedBreakRate;

            if (GetTypeFromName(items[ItemListBox.SelectedIndex].itemName) == 1 || GetTypeFromName(items[ItemListBox.SelectedIndex].itemName) == 0)
            {
                ItemSelectBox.Enabled = true;
                UsedEnduranceCounter.Enabled = true;
                UsedBreakRateCounter.Enabled = true;
                ItemCounter.Enabled = false;
                ItemCounter.Value = 1;
            }
            if (items[ItemListBox.SelectedIndex].type == 2)
            {
                ItemSelectBox.SelectedIndex = 29;
                ItemSelectBox.Enabled = false;
                UsedEnduranceCounter.Enabled = false;
                UsedBreakRateCounter.Enabled = false;
                ItemCounter.Enabled = false;
                ItemCounter.Value = 1; 
            }
            if (items[ItemListBox.SelectedIndex].type == 4)
            {
                UsedEnduranceCounter.Enabled = false;
                UsedBreakRateCounter.Enabled = false;
                ItemCounter.Enabled = true;
                ItemCounter.Value = items[ItemListBox.SelectedIndex].stackCount;
            }

        }

        private void ItemChangesApply_Click(object sender, EventArgs e)
        {
            JObject obj = JObject.Parse(JsonRead);
            var val = obj["Pouch"];
            int index = ItemListBox.SelectedIndex;

            if (ItemListBox.SelectedIndex >= 0)
            {
                val["equipment"]["bag"][Convert.ToString(ItemListBox.SelectedIndex)]["itemId"] = ToItemID(ItemSelectBox.GetItemText(ItemSelectBox.SelectedItem));
                val["equipment"]["bag"][Convert.ToString(ItemListBox.SelectedIndex)]["usedEndurance"] = UsedEnduranceCounter.Value;
                val["equipment"]["bag"][Convert.ToString(ItemListBox.SelectedIndex)]["usedBreakRate"] = UsedBreakRateCounter.Value;
                val["equipment"]["bag"][Convert.ToString(ItemListBox.SelectedIndex)]["type"] = GetTypeFromName(ItemSelectBox.GetItemText(ItemSelectBox.SelectedItem));

                string result = obj.ToString();
                JsonRead = result;

                ItemListBox.Items.Clear();
                ReadInventory();
                ItemListBox.SelectedIndex = index;
            }
            else
            {
                MessageBox.Show("You must select one of your items first");
            }
        }

        private void PlaytimeHourCounter_ValueChanged(object sender, EventArgs e)
        {
            JObject obj = JObject.Parse(JsonRead);
            var val = obj["Pouch"];

            val["play_time"]["play_hour"] = Convert.ToInt32(PlaytimeHourCounter.Value);
            JsonRead = obj.ToString();
        }

        private void PlaytimeMinuteCounter_ValueChanged(object sender, EventArgs e)
        {
            JObject obj = JObject.Parse(JsonRead);
            var val = obj["Pouch"];

            val["play_time"]["play_min"] = Convert.ToInt32(PlaytimeMinuteCounter.Value);
            JsonRead = obj.ToString();
        }

        private void PlaytimeSecondCounter_ValueChanged(object sender, EventArgs e)
        {
            JObject obj = JObject.Parse(JsonRead);
            var val = obj["Pouch"];

            val["play_time"]["play_sec"] = Convert.ToInt32(PlaytimeSecondCounter.Value);
            JsonRead = obj.ToString();
        }

        private void PartnerSelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            JObject obj = JObject.Parse(JsonRead);
            var val = obj["Pouch"];

            switch (PartnerSelectBox.SelectedIndex)
            {
                case 0:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "";
                    JsonRead = obj.ToString();
                    break;
                case 1:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "P_KPAO";
                    JsonRead = obj.ToString();
                    break;
                case 2:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "P_BOME";
                    JsonRead = obj.ToString();
                    break;
                case 3:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "P_KURB";
                    JsonRead = obj.ToString();
                    break;
                case 4:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "P_HEISOM";
                    JsonRead = obj.ToString();
                    break;
                case 5:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "P_KNPP";
                    JsonRead = obj.ToString();
                    break;
                case 6:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "P_KNP";
                    JsonRead = obj.ToString();
                    break;
                case 7:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "P_LUG";
                    JsonRead = obj.ToString();
                    break;
                case 8:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "P_GBN";
                    JsonRead = obj.ToString();
                    break;
                case 9:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "P_KPAJ_Crown";
                    JsonRead = obj.ToString();
                    break;
                case 10:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "P_KMP";
                    JsonRead = obj.ToString();
                    break;
                case 11:
                    val["party_infor"]["partyMemberNum"] = 1;
                    val["party_infor"]["partyMemberName"]["0"] = "P_KPA";
                    JsonRead = obj.ToString();
                    break;
            }
        }

        public string ToItemName(string itemId)
        {
            string result;
            switch (itemId)
            {
                case "BOOTS":
                    result = "Boots";
                    return result;
                case "HAMMER":
                    result = "Hammer";
                    return result;
                case "SILVER_BOOTS":
                    result = "Shiny Boots";
                    return result;
                case "SUPER_BOOTS":
                    result = "Flashy Boots";
                    return result;
                case "GREAT_BOOTS":
                    result = "Legendary Boots";
                    return result;
                case "IRON_BOOTS":
                    result = "Iron Boots";
                    return result;
                case "STEEL_BOOTS":
                    result = "Shiny Iron Boots";
                    return result;
                case "SUPER_IRON_BOOTS":
                    result = "Flashy Iron Boots";
                    return result;
                case "SUPER_STEEL_BOOTS":
                    result = "Legendary Iron Boots";
                    return result;
                case "GOLD_BOOTS":
                    result = "Gold Boots";
                    return result;
                case "SILVER_HAMMER":
                    result = "Shiny Hammer";
                    return result;
                case "SUPER_HAMMER":
                    result = "Flashy Hammer";
                    return result;
                case "GREAT_HAMMER":
                    result = "Legendary Hammer";
                    return result;
                case "FIRE_HAMMER":
                    result = "Fire Hammer";
                    return result;
                case "ICE_HAMMER":
                    result = "Ice Hammer";
                    return result;
                case "THROW_HAMMER":
                    result = "Hurlhammer";
                    return result;
                case "SUPER_THROW_HAMMER":
                    result = "Flashy Hurlhammer";
                    return result;
                case "GOLDEN_HAMMER":
                    result = "Gold Hammer";
                    return result;
                case "KINOKO":
                    result = "Mushroom";
                    return result;
                case "SUPER_KINOKO":
                    result = "Shiny Mushroom";
                    return result;
                case "GREAT_KINOKO":
                    result = "Flashy Mushroom";
                    return result;
                case "FIRE_FLOWER":
                    result = "Fire Flower";
                    return result;
                case "ICE_FLOWER":
                    result = "Ice Flower";
                    return result;
                case "SUPER_FIRE_FLOWER":
                    result = "Shiny Fire FLower";
                    return result;
                case "SUPER_ICE_FLOWER":
                    result = "Shiny Ice Flower";
                    return result;
                case "HEEL_KINOKO":
                    result = "1-Up Mushroom";
                    return result;
                case "POW_BLOCK":
                    result = "Pow Block";
                    return result;
                case "TAIL":
                    result = "Tail";
                    return result;
                case "SUPER_TAIL":
                    result = "Shiny Tail";
                    return result;
                case "PSV_Battle_DAMAGE1":
                    result = "Guard Plus";
                    return result;
                case "PSV_Battle_DAMAGE2":
                    result = "Silver Guard Plus";
                    return result;
                case "PSV_Battle_DAMAGE3":
                    result = "Gold Guard Plus";
                    return result;
                case "PSV_Battle_HP1":
                    result = "Heart Plus";
                    return result;
                case "PSV_Battle_HP2":
                    result = "Silver Heart Plus";
                    return result;
                case "PSV_Battle_HP3":
                    result = "Gold Heart Plus";
                    return result;
                case "PSV_Puzzle_TIME1":
                    result = "Time Plus";
                    return result;
                case "PSV_Puzzle_TIME2":
                    result = "Silver Time Plus";
                    return result;
                case "PSV_Puzzle_TIME3":
                    result = "Gold Time Plus";
                    return result;
                case "IC_WHISTLE":
                    result = "Desert Whistle";
                    return result;
                case "PSV_Sub_PaperVacuum":
                    result = "Confetti Vacuum";
                    return result;
                case "PSV_Sub_MembersCard1":
                    result = "Bronze Membership Card";
                    return result;
                case "PSV_Sub_MembersCard2":
                    result = "Silver Membership Card";
                    return result;
                case "PSV_Sub_MembersCard3":
                    result = "Gold Membership Card";
                    return result;
                case "PSV_Sub_FlowerShower":
                    result = "Petal Bag";
                    return result;
                case "PSV_Sub_BattleParty":
                    result = "Ally Tambourine";
                    return result;
                case "PSV_Sub_HideBlockAlarm":
                    result = "Hidden Block Alert";
                    return result;
                case "PSV_Sub_Pedometer":
                    result = "Coin Step Counter";
                    return result;
                case "PSV_Sub_KinopioAlarm":
                    result = "Toad Alert";
                    return result;
                case "PSV_Sub_TreasureAlarm":
                    result = "Treasure Alert";
                    return result;
                case "IC_SECRET_BLOCK_RS":
                    result = "Hidden Block Unhider";
                    return result;
                case "IC_KINOPIO_RS":
                    result = "Toad Radar";
                    return result;
                //case "IC_LATENCY":
                //    result = "Lamination Suit?";
                //    return result;
            }
            return "Error";
        }

        public string ToItemID(string ItemName)
        {
            string result;
            switch (ItemName)
            {
                case "Boots":
                    result = "BOOTS";
                    return result;
                case "Hammer":
                    result = "HAMMER";
                    return result;
                case "Shiny Boots":
                    result = "SILVER_BOOTS";
                    return result;
                case "Flashy Boots":
                    result = "SUPER_BOOTS";
                    return result;
                case "Legendary Boots":
                    result = "GREAT_BOOTS";
                    return result;
                case "Iron Boots":
                    result = "IRON_BOOTS";
                    return result;
                case "Shiny Iron Boots":
                    result = "STEEL_BOOTS";
                    return result;
                case "Flashy Iron Boots":
                    result = "SUPER_IRON_BOOTS";
                    return result;
                case "Legendary Iron Boots":
                    result = "SUPER_STEEL_BOOTS";
                    return result;
                case "Gold Boots":
                    result = "GOLD_BOOTS";
                    return result;
                case "Shiny Hammer":
                    result = "SILVER_HAMMER";
                    return result;
                case "Flashy Hammer":
                    result = "SUPER_HAMMER";
                    return result;
                case "Legendary Hammer":
                    result = "GREAT_HAMMER";
                    return result;
                case "Fire Hammer":
                    result = "FIRE_HAMMER";
                    return result;
                case "Ice Hammer":
                    result = "ICE_HAMMER";
                    return result;
                case "Hurlhammer":
                    result = "THROW_HAMMER";
                    return result;
                case "Flashy Hurlhammer":
                    result = "SUPER_THROW_HAMMER";
                    return result;
                case "Gold Hammer":
                    result = "GOLDEN_HAMMER";
                    return result;
                case "Mushroom":
                    result = "KINOKO";
                    return result;
                case "Shiny Mushroom":
                    result = "SUPER_KINOKO";
                    return result;
                case "Flashy Mushroom":
                    result = "GREAT_KINOKO";
                    return result;
                case "Fire Flower":
                    result = "FIRE_FLOWER";
                    return result;
                case "Ice FLower":
                    result = "ICE_FLOWER";
                    return result;
                case "Shiny Fire Flower":
                    result = "SUPER_FIRE_FLOWER";
                    return result;
                case "Shiny Ice Flower":
                    result = "SUPER_ICE_FLOWER";
                    return result;
                case "1-Up Mushroom":
                    result = "HEEL_KINOKO";
                    return result;
                case "Pow Block":
                    result = "POW_BLOCK";
                    return result;
                case "Tail":
                    result = "TAIL";
                    return result;
                case "Shiny Tail":
                    result = "SUPER_TAIL";
                    return result;
            }
            return "Error";
        }

        public int GetTypeFromName(string itemName)
        {
            switch (itemName)
            {
                case "Boots":
                    return 1;
                case "Hammer":
                    return 0;
                case "Shiny Boots":
                    return 1;
                case "Flashy Boots":
                    return 1;
                case "Legendary Boots":
                    return 1;
                case "Iron Boots":
                    return 1;
                case "Shiny Iron Boots":
                    return 1;
                case "Flashy Iron Boots":
                    return 1;
                case "Legendary Iron Boots":
                    return 1;
                case "Gold Boots":
                    return 1;
                case "Shiny Hammer":
                    return 0;
                case "Flashy Hammer":
                    return 0;
                case "Legendary Hammer":
                    return 0;
                case "Fire Hammer":
                    return 0;
                case "Ice Hammer":
                    return 0;
                case "Hurlhammer":
                    return 0;
                case "Flashy Hurlhammer":
                    return 0;
                case "Gold Hammer":
                    return 0;
                case "Mushroom":
                    return 4;
                case "Shiny Mushroom":
                    return 4;
                case "Flashy Mushroom":
                    return 4;
                case "Fire Flower":
                    return 4;
                case "Ice FLower":
                    return 4;
                case "Shiny Fire Flower":
                    return 4;
                case "Shiny Ice Flower":
                    return 4;
                case "1-Up Mushroom":
                    return 4;
                case "Pow Block":
                    return 4;
                case "Tail":
                    return 4;
                case "Shiny Tail":
                    return 4;
            }
            return 0;
        }

        public string ToPartnerName(string partnerID)
        {
            switch (partnerID)
            {
                case "P_KPAO":
                    return "Bowser (Folded)";
                case "P_BOME":
                    return "Bobby";
                case "P_KURB":
                    return "Bone Goomba";
                case "P_HEISOM":
                    return "Sombrero Guy";
                case "P_KNPP":
                    return "Prof. Toad";
                case "P_KNP":
                    return "Green Toad";
                case "P_LUG":
                    return "Luigi";
                case "P_GBN":
                    return "Spike";
                case "P_KPAJ_Crown":
                    return "Bowser Jr.";
                case "P_KMP":
                    return "Kamek";
                case "P_KPA":
                    return "Bowser";
                case "":
                    return "None";
            }
            return "Error";

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void MainEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.ToString() == "O")
            {
                MessageBox.Show("Open File Placeholder");
            }
            if (e.Control && e.Shift && e.KeyCode.ToString() == "S")
            {
                MessageBox.Show("Save File Placeholder");
            }
        }
    }
}
