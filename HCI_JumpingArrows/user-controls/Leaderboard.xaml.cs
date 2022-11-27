using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_JumpingArrows.user_controls
{
    /// <summary>
    /// Interaction logic for Leaderboard.xaml
    /// </summary>
    public partial class Leaderboard : UserControl
    {
        public Leaderboard()
        {
            List<string> list = new List<string>();
            InitializeComponent();
            StreamReader sr = new StreamReader("results.txt");
            try
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                    list.Add(line);
                list.Sort((string str1, string str2) =>
                {
                    return Int32.Parse(str2.Split('#')[1]) - Int32.Parse(str1.Split('#')[1]);
                });
                for(int i=0; i<10; i++)
                {
                    Leader.Text +=(i+1)+". "+ list[i].Replace("#", "  ") + " pts"+Environment.NewLine;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
