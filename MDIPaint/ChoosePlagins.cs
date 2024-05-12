using PluginInterface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MDIPaint
{
    public partial class ChoosePlagins : Form
    {
        public ChoosePlagins()
        {
            InitializeComponent();
            LoadElements();
        }

        private void ChoosePlagins_Load(object sender, EventArgs e)
        {
        }

        private void LoadElements()
        {
            string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
            {
                Assembly assembly = Assembly.LoadFrom(file);

                foreach (Type type in assembly.GetTypes())
                {
                    Type iface = type.GetInterface("PluginInterface.IPlugin");

                    if (iface != null)
                    {
                        IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                        AssemblyFileVersionAttribute fileVersionAttribute = 
                            (AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyFileVersionAttribute));
                        string fileVersion = fileVersionAttribute != null ? fileVersionAttribute.Version : "";
                        CheckBox checkbox = new CheckBox();
                        checkbox.Text = $"{plugin.Name}\nАвтор: {plugin.Author}\nv.{fileVersion}";
                        checkbox.Tag = new FileAddressAttribute(file);
                        checkbox.AutoSize = true;
                        checkbox.Left = 20;
                        checkbox.Top = 10 + panel1.Controls.Count * 50;
                        
                        if (MainForm.plugins.ContainsKey(plugin.Name))
                            checkbox.Checked = true;

                        panel1.Controls.Add(checkbox);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string configFilePath = "Plugins.config";
            File.WriteAllText(configFilePath, string.Empty);

            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Plugins");
            xmlDoc.AppendChild(rootNode);

            foreach (CheckBox checkbox in panel1.Controls)
            {
                if (checkbox.Checked)
                {
                    FileAddressAttribute address = checkbox.Tag as FileAddressAttribute;

                    XmlNode pluginNode = xmlDoc.CreateElement("Plugin");
                    pluginNode.InnerText = address.FileAddress;
                    rootNode.AppendChild(pluginNode);
                }
            }
            xmlDoc.Save(configFilePath);
            MainForm frm = this.Owner as MainForm;
            frm.FindPlugins();
            Close();
        }
    }

    public class FileAddressAttribute : Attribute
    {
        public string FileAddress { get; set; }

        public FileAddressAttribute(string fileAddress)
        {
            FileAddress = fileAddress;
        }
    }
}
