using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EcoTalk_Lib;
using Robot_Lib;

namespace PeakCANOpen_Lib
{
    public partial class SettingsPanel : UserControl
    {
        public SettingsPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ablegen der Einstellungen im Projektfile
        /// </summary>
        private void GetSettingsfromDialog()
        {
            CDrive set= CSystemRessources.GetDrive("A1");
            set.incskal = numericA1.Value;
            set.driveSpeed = numericUpDownSpeedA1.Value;
            set.driveAcceleration   = numericUpDownAcc.Value;
            set.driveId = (uint)numericUpDownA1id.Value;
//            set.driveCurrent = numericUpDownCurrent.Value;
//            set.refDir = Convert.ToDecimal(checkBoxRefA1.Checked);
            set.minPos = numericUpDownA1min.Value;
            set.maxPos = numericUpDownA1max.Value;

        }
        /// <summary>
        /// Lesen der Daten aus den Einstellungen
        /// </summary>
        private void LoadSettings()
        {
            CDrive set = CSystemRessources.GetDrive("A1");
           // labelError.Text = set.errorString;
            try
            {
                //Kinematik
                numericA1.Value = set.incskal;

                numericUpDownSpeedA1.Value = set.driveSpeed;

                numericUpDownA1id.Value = set.driveId;


                checkBoxRefA1.Checked = Convert.ToBoolean(set.refDir);

                numericUpDownA1min.Value = set.minPos;
                numericUpDownA1max.Value = set.maxPos;
              //  checkBoxCNCAktiv.Checked = set.KinematikAktiv;
                checkBoxIgnoreLimits.Checked = set.ignoreLimits;
               // checkBoxIgnorValidMove.Checked = set.ignoreValidMove;
                numericUpDownA1min.Enabled = !set.ignoreLimits;
                numericUpDownA1max.Enabled = !set.ignoreLimits;
 

            }
            catch (Exception e)
            {
                MessageHandler.Error(this, 751, "Load Kinematik Settings Setup ", e);
                labelError.Text = "Fehler beim Laden der Kinematik Einstellungen";
            }
 


        }


    }
}
