﻿#pragma checksum "..\..\..\WindowsLeague\TeamStatsUX.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BCECE0976A914CFA98EF5F0866EA13D4FE98BB13B64D2B8AD2BEA770CA628AE5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SpectatorFootball.BindingConverters;
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
    /// TeamStatusUX
    /// </summary>
    public partial class TeamStatusUX : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\WindowsLeague\TeamStatsUX.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnStandings;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\WindowsLeague\TeamStatsUX.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblHeader;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\WindowsLeague\TeamStatsUX.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button help_btn;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\WindowsLeague\TeamStatsUX.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstTeamStats;
        
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
            System.Uri resourceLocater = new System.Uri("/SpectatorFootball;component/windowsleague/teamstatsux.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WindowsLeague\TeamStatsUX.xaml"
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
            this.btnStandings = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\WindowsLeague\TeamStatsUX.xaml"
            this.btnStandings.Click += new System.Windows.RoutedEventHandler(this.btnStandings_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lblHeader = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.help_btn = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\WindowsLeague\TeamStatsUX.xaml"
            this.help_btn.Click += new System.Windows.RoutedEventHandler(this.help_btn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lstTeamStats = ((System.Windows.Controls.ListView)(target));
            
            #line 33 "..\..\..\WindowsLeague\TeamStatsUX.xaml"
            this.lstTeamStats.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.ListViewHeader_Click));
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

