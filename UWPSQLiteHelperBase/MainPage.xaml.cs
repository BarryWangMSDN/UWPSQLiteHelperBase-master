using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UWPSQLiteHelperBase.Helper;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPSQLiteHelperBase
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //SqliteHelper1 helper1 = new SqliteHelper1();
            // helper.sqlite_createtable("managementtable");
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(UWPSQLiteHelperBase.View.ManagementPage));
            //SqliteHelper2 helper2 = new SqliteHelper2();
            //helper2.readtable("PeopleModel");

        }
    }
}
