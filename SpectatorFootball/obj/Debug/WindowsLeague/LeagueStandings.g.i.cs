﻿#pragma checksum "..\..\..\WindowsLeague\LeagueStandings.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4A86C6321F3C4B2EADA408B64FB70018D25C908AF2D9FF53DDB550C848EBDF11"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SpectatorFootball.WindowsLeague;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SpectatorFootball.WindowsLeague {
    
    
    /// <summary>
    /// LeagueStandings
    /// </summary>
    public partial class LeagueStandings : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\WindowsLeague\LeagueStandings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox standingsSeasonsdb;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\WindowsLeague\LeagueStandings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GoCurrentSeason;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\WindowsLeague\LeagueStandings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label League_Logo_lbl;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\WindowsLeague\LeagueStandings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image League_image;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\WindowsLeague\LeagueStandings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button next_btn;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\WindowsLeague\LeagueStandings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button help_btn;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\WindowsLeague\LeagueStandings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button standBack;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\WindowsLeague\LeagueStandings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel sp1;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SpectatorFootball;component/windowsleague/leaguestandings.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WindowsLeague\LeagueStandings.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.standingsSeasonsdb = ((System.Windows.Controls.ComboBox)(target));
            
            #line 18 "..\..\..\WindowsLeague\LeagueStandings.xaml"
            this.standingsSeasonsdb.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.standingsSeasonsdb_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.GoCurrentSeason = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\WindowsLeague\LeagueStandings.xaml"
            this.GoCurrentSeason.Click += new System.Windows.RoutedEventHandler(this.GoCurrentSeason_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.League_Logo_lbl = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.League_image = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.next_btn = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\WindowsLeague\LeagueStandings.xaml"
            this.next_btn.Click += new System.Windows.RoutedEventHandler(this.standings_NextStepbtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.help_btn = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\WindowsLeague\LeagueStandings.xaml"
            this.help_btn.Click += new System.Windows.RoutedEventHandler(this.help_btn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.standBack = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\WindowsLeague\LeagueStandings.xaml"
            this.standBack.Click += new System.Windows.RoutedEventHandler(this.standBack_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.sp1 = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

