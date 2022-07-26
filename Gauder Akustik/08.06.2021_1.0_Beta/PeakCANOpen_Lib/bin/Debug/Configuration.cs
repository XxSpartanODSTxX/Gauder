using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using MessageHandlerLib;

namespace Robot_Lib
{
    public partial class Configuration : UserControl
    {

        Dictionary<string, IRobotPlugin> plugins = new Dictionary<string,IRobotPlugin>();


        public Configuration()
        {
            InitializeComponent();
            

        }

        private class ResItem
        {
            public string Name;
            public string Value;
            public ResItem(string name, string value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }

        private void comboBoxPropertyselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.DisplayMember = "Key";

            try
            {
                string selKey = (string)comboBox.SelectedItem.GetType().GetProperty("Key").GetValue(comboBox.SelectedItem, null);
                object selValue = (object)comboBox.SelectedItem.GetType().GetProperty("Value").GetValue(comboBox.SelectedItem, null);
                //     KeyValuePair<string, object> selItem = (KeyValuePair<string, object>)comboBox.SelectedItem;
                propertyGrid1.SelectedObject = null;
                if (selValue is CDrive) { 
                    propertyGrid1.SelectedObject = CSystemRessources.GetDrive(selKey);
                labelControlls.Text = "Drive selectet";
            }
                else if (selValue is CRobot)
                {
                    propertyGrid1.SelectedObject = CSystemRessources.GetRobot(selKey);
                    labelControlls.Text = "Robot Selected";
                }
                else if (selValue is IController)
                    {
                        propertyGrid1.SelectedObject = CSystemRessources.GetController(selKey);
                    labelControlls.Text = "Controller selected";
                }
                else if (selValue is IFormInterface)
                        {
                            propertyGrid1.SelectedObject = CSystemRessources.GetForm(selKey);
                    labelControlls.Text = "Form Selected";
                }

                if (selValue is IRobotPlugin /*&& propertyGrid1.SelectedObject == null*/)
                            {
                                propertyGrid1.SelectedObject = plugins[selKey];
                    labelControlls.Text = "Plugin Selected";
                }
                //else
                //    label1.Text = "...";

            }
            catch (Exception ex)
            {
                MessageHandler.Error(this, 1360, "Kann Propertys nicht darstellen", ex);
                //  throw;
            }
        }



        /// <summary>
        /// liest alle DLLs im angegebenen Verzeichnis
        /// </summary>
        /// <param name="path"></param>
        public void HandlePlugins(string path)
        {
            string[] dllFileNames = null;
            path = path.Replace("%20", " ");
            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");

            }
            dllFileNames = Directory.GetFiles(path, "*.dll");

            ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);  // hier werden alle dll assemlis eingetragen
            

            foreach (string dllFile in dllFileNames)
            {
                try
                {
                    AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }
                catch (Exception ex ) {
                    Debug.WriteLine("Error in Assemblies: "+ ex);

                }
            }

            

            Type pluginType = typeof(IRobotPlugin);
            Type commandType = typeof(IBasicCommand);
            ICollection<Type> pluginTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly != null)
                {
                    try
                    {
                        Type[] types = assembly.GetTypes();
                        foreach (Type type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                                if (type.GetInterface(commandType.FullName) != null)
                                {
                                    //todo irgendwo listen                                       pluginTypes.Add(type);
                                    ((IBasicCommand)Activator.CreateInstance(type)).SubscribeCommands();
                                   
                                    //foreach (MethodInfo mi in type.GetMethods())
                                    //{
                                    //    Console.WriteLine(mi.ToString());
                                    //}
                                }
                            }
                        }
                    }
                    catch (Exception ex) { Debug.WriteLine("Error subcribing Commands: "+ ex); }
                }
            }

            foreach (Type type in pluginTypes)
            {
                try
                {
                    IRobotPlugin plugin = (IRobotPlugin)Activator.CreateInstance(type);
                    KeyValuePair<string, IRobotPlugin> myPlugin = new KeyValuePair<string, IRobotPlugin>(plugin.PluginName, plugin);
                    comboBoxPluggins.Items.Add(myPlugin);
                    plugins.Add(plugin.PluginName, plugin);
                }
                    catch (Exception ex )
                {
                    Debug.WriteLine("Error:" +ex);
                }
        }

        }

        /// <summary>
        /// Eintragen der Plugins in die Combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Configuration_Enter(object sender, EventArgs e)
        {
            try
            {
                comboBoxControls.Items.Clear();
                foreach (KeyValuePair<string, CDrive> drive in CSystemRessources.Drives)
                    comboBoxControls.Items.Add(drive);
                foreach (KeyValuePair<string, CRobot> robot in CSystemRessources.Robots)
                    comboBoxControls.Items.Add(robot);
                foreach (KeyValuePair<string, IController> control in CSystemRessources.Controls)
                    comboBoxControls.Items.Add(control);
                foreach (KeyValuePair<string, IFormInterface> control in CSystemRessources.Forms)
                    comboBoxControls.Items.Add(control);
            }
            catch (Exception) { }
        }

        public static string GetExecutingDirectoryName()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath).Directory.FullName;
        }


        private void buttonPlugin_Click(object sender, EventArgs e)
        {
           // https://code.msdn.microsoft.com/windowsdesktop/Creating-a-simple-plugin-b6174b62
            HandlePlugins(GetExecutingDirectoryName());
        }

        private void buttonRes_Click(object sender, EventArgs e)
        {
            Configuration_Enter(sender, e);
        }

      

    
    }
}
