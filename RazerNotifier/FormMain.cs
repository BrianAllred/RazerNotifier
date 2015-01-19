#region Using

using System;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;

#endregion

namespace RazerNotifier
{
    public partial class FormMain : Form
    {

        #region Fields
        
        /// <summary>
        /// Stores selected countyry
        /// </summary>
        private string _selectedCountry;

        /// <summary>
        /// Stores product ID
        /// </summary>
        private string _productId;

        /// <summary>
        /// Stores whether or not to check out of stock items
        /// </summary>
        private bool _checkOutOfStock;

        /// <summary>
        /// Tray icon object
        /// </summary>
        readonly NotifyIcon _icon = new NotifyIcon { Icon = Properties.Resources.Razer_Icon };

        /// <summary>
        /// Link to launch browser to Razer store page
        /// </summary>
        private string _buyLink;

        #endregion

        #region Initialization Methods
        
        /// <summary>
        /// Form constructor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            _icon.Click += IconClick;
            _icon.BalloonTipClicked += BallonClicked;
        }

        #endregion

        #region Events

        /// <summary>
        /// Form load event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        /// <summary>
        /// Tray icon click event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void IconClick(object sender, EventArgs e)
        {
            _icon.Visible = false;
            CheckTimer.Enabled = false;
            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
        }

        /// <summary>
        /// Interval text box changed event. Handles fixing the default text
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void IntervalTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IntervalTextBox.Text))
            {
                IntervalTextBox.Text = "hh:mm:ss";
            }
            if (IntervalTextBox.Text.Length > 0 && IntervalTextBox.Text.Substring(1).Equals("hh:mm:ss"))
            {
                IntervalTextBox.Text = IntervalTextBox.Text.Substring(0, 1);
                IntervalTextBox.SelectionStart = 1;
            }
        }

        /// <summary>
        /// Country selection event. Make sure user can't pick a region instead of a country
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void CountryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountryComboBox.Text.Contains("North America") || CountryComboBox.Text.Contains("Asia Pacific") ||
                CountryComboBox.Text.Contains("Europe"))
            {
                CountryComboBox.SelectedIndex += 1;
            }
        }

        /// <summary>
        /// Start button click event. Enable the timer and hide the window and stuff.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            int milliseconds;
            if (!TryParseInterval(out milliseconds)) return;
            if (!SetCountryAndProduct()) return;
            _checkOutOfStock = OutOfStockCheckBox.Checked;
            WindowState = FormWindowState.Minimized;
            CheckTimer.Interval = milliseconds;
            CheckTimer.Enabled = true;
            CheckTimer_Tick(null, null);
        }

        /// <summary>
        /// Timer tick event. Check the store and report back.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var json = webClient.DownloadString("http://www.razerzone.com/" + _selectedCountry + "store/dr_json/" + _productId);
                    var productList = JsonConvert.DeserializeObject<RazerProductInfoList>(json);
                    int quantity;
                    if (int.TryParse(productList.productInfo.product.availableQuantity, out quantity) && quantity > 0)
                    {
                        _icon.BalloonTipTitle = "In stock!";
                        _icon.BalloonTipIcon = ToolTipIcon.Info;
                        _icon.BalloonTipText = productList.productInfo.product.displayName +
                                               " is in stock with a quantity of " +
                                               quantity + ".\n\n"+
                                               "Click to buy.";
                        _buyLink = productList.productInfo.product.buyLink;
                        _icon.ShowBalloonTip(10000);
                    }
                    else if (_checkOutOfStock)
                    {
                        _icon.BalloonTipTitle = "Out of stock...";
                        _icon.BalloonTipIcon = ToolTipIcon.Info;
                        _icon.BalloonTipText = productList.productInfo.product.displayName +
                                              " is out of stock. :(\n" +
                                              "Click to try to buy anyway.";
                        _buyLink = productList.productInfo.product.buyLink;
                        _icon.ShowBalloonTip(10000);
                    }
                }
            }
            catch (Exception)
            {
                _icon.BalloonTipTitle = "Error!";
                _icon.BalloonTipIcon = ToolTipIcon.Error;
                _icon.BalloonTipText = "Error retrieving product " + _productId + " from Razer store " + _selectedCountry + ".";
                _icon.ShowBalloonTip(10000);
            }
        }

        /// <summary>
        /// Ballon click event. Open browser to the purchase link.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BallonClicked(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo(_buyLink));
        }

        /// <summary>
        /// Form closing event. Remove tray icon and save user settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _icon.Visible = false;
            SaveSettings();
        }

        /// <summary>
        /// Form resize event. Handles hiding the form when minimizing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            _icon.Visible = true;
            Hide();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Set country code for Razer store and store country and product IDs in global variables.
        /// </summary>
        /// <returns>True if user picked a valid country. Should never return false unless I messed up.</returns>
        private bool SetCountryAndProduct()
        {
            switch (CountryComboBox.Text)
            {
                case "-- Canada":
                    _selectedCountry = "ca-en/";
                    break;
                case "-- United States":
                    _selectedCountry = "";
                    break;
                case "-- Australia":
                    _selectedCountry = "au-en/";
                    break;
                case "-- Brunei":
                case "-- Japan":
                case "-- Malaysia":
                case "-- New Zealand":
                    _selectedCountry = "ap-en/";
                    break;
                case "-- Singapore":
                    _selectedCountry = "sg-en/";
                    break;
                case "-- Hong Kong":
                    _selectedCountry = "hk-zh/";
                    break;
                case "-- Taiwan":
                    _selectedCountry = "tw-zh/";
                    break;
                case "-- Belgium":
                case "-- Croatia":
                case "-- Cyprus":
                case "-- Czech Republic":
                case "-- Denmark":
                case "-- Estonia":
                case "-- Finland":
                case "-- Greece":
                case "-- Hungary":
                case "-- Ireland":
                case "-- Latvia":
                case "-- Luxembourg":
                case "-- Malta":
                case "-- Netherlands":
                case "-- Norway":
                case "-- Poland":
                case "-- Portugal":
                case "-- Slovakia":
                case "-- Slovenia":
                case "-- Sweden":
                case "-- Switzerland":
                    _selectedCountry = "eu-en/";
                    break;
                case "-- Germany":
                case "-- Austria":
                    _selectedCountry = "de-de/";
                    break;
                case "-- Spain":
                    _selectedCountry = "es-es/";
                    break;
                case "-- France":
                    _selectedCountry = "fr-fr/";
                    break;
                case "-- Italy":
                    _selectedCountry = "it-it/";
                    break;
                case "-- United Kingdom":
                    _selectedCountry = "gb-en/";
                    break;
                default:
                    return false;
            }
            _productId = ProductIdTextBox.Text;
            return true;
        }

        /// <summary>
        /// Try to parse out user supplied interval.
        /// </summary>
        /// <param name="millis">Return variable. 0 if method returns false.</param>
        /// <returns>True if parsed correctly, false otherwise. False if millis = 0</returns>
        private bool TryParseInterval(out int millis)
        {
            int milliseconds = 0;
            if (string.IsNullOrEmpty(CountryComboBox.Text))
            {
                millis = 0;
                return false;
            }
            var regexHms = new Regex(@"^\d+:\d+:\d+$");
            var regexMs = new Regex(@"^\d+:\d+$");
            var regexS = new Regex(@"^:\d+$");
            var regexMilliseconds = new Regex(@"^\d+$");
            if (regexHms.Match(IntervalTextBox.Text).Success)
            {
                string[] splitStrings = IntervalTextBox.Text.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                int hours = int.Parse(splitStrings[0]);
                int minutes = int.Parse(splitStrings[1]);
                int seconds = int.Parse(splitStrings[2]);
                milliseconds = (hours*60*60*1000) + (minutes*60*1000) + (seconds*1000);
            }
            else if (regexMs.Match(IntervalTextBox.Text).Success)
            {
                string[] splitStrings = IntervalTextBox.Text.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                int minutes = int.Parse(splitStrings[0]);
                int seconds = int.Parse(splitStrings[1]);
                milliseconds = (minutes*60*1000) + (seconds*1000);
            }
            else if (regexS.Match(IntervalTextBox.Text).Success)
            {
                int seconds = int.Parse(IntervalTextBox.Text.Substring(IntervalTextBox.Text.LastIndexOf(':') + 1));
                milliseconds = (seconds*1000);
            }
            else if (regexMilliseconds.Match(IntervalTextBox.Text).Success)
            {
                milliseconds = int.Parse(IntervalTextBox.Text);
            }
            else
            {
                MessageBox.Show("Please enter a time in either hh:mm:ss, mm:ss, :ss, or milliseconds.", "Invalid Interval");
                millis = milliseconds;
                return false;
            }
            millis = milliseconds;
            return milliseconds != 0;
        }

        /// <summary>
        /// Load user settings from AppData
        /// </summary>
        private void LoadSettings()
        {
            CountryComboBox.SelectedItem = Properties.Settings.Default.Country;
            ProductIdTextBox.Text = Properties.Settings.Default.ProductID;
            IntervalTextBox.Text = Properties.Settings.Default.Interval;
            OutOfStockCheckBox.Checked = Properties.Settings.Default.CheckOOS;
        }

        /// <summary>
        /// Save user settings to AppData
        /// </summary>
        public void SaveSettings()
        {
            try
            {
                Properties.Settings.Default.Country = CountryComboBox.SelectedText;
                Properties.Settings.Default.ProductID = ProductIdTextBox.Text;
                Properties.Settings.Default.Interval = IntervalTextBox.Text;
                Properties.Settings.Default.CheckOOS = OutOfStockCheckBox.Checked;
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                
            }
        }

        #endregion
    }
}
