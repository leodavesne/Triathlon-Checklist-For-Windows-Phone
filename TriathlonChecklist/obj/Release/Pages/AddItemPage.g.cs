﻿#pragma checksum "E:\Documents\Visual Studio 2010\Projects\TriathlonChecklist\TriathlonChecklist\Pages\AddItemPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A30DB791A132879871BCF16443C82797"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using TriathlonChecklist;


namespace TriathlonChecklist {
    
    
    public partial class AddItemPage : TriathlonChecklist.PageBase {
        
        internal System.Windows.DataTemplate lpkCategoryTemplate;
        
        internal System.Windows.DataTemplate lpkFullCategoryTemplate;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox NameTextBox;
        
        internal Microsoft.Phone.Controls.ListPicker lpkCategory;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton SaveBarIconButton;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem AboutAndHelpBarMenuItem;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Triathlon%20Checklist;component/Pages/AddItemPage.xaml", System.UriKind.Relative));
            this.lpkCategoryTemplate = ((System.Windows.DataTemplate)(this.FindName("lpkCategoryTemplate")));
            this.lpkFullCategoryTemplate = ((System.Windows.DataTemplate)(this.FindName("lpkFullCategoryTemplate")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.NameTextBox = ((System.Windows.Controls.TextBox)(this.FindName("NameTextBox")));
            this.lpkCategory = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("lpkCategory")));
            this.SaveBarIconButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("SaveBarIconButton")));
            this.AboutAndHelpBarMenuItem = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("AboutAndHelpBarMenuItem")));
        }
    }
}

