﻿#pragma checksum "..\..\..\WindowsLeague\TrainingCampUX.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "89B8A2DEB635E791BA562CE1587F427618CA3105258D317F9E610AC1234BA4E7"
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
    /// TrainingCampUX
    /// </summary>
    public partial class TrainingCampUX : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\WindowsLeague\TrainingCampUX.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnStandings;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\WindowsLeague\TrainingCampUX.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTrainingCampHeader;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\WindowsLeague\TrainingCampUX.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button help_btn;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\WindowsLeague\TrainingCampUX.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTrainingCamp;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\WindowsLeague\TrainingCampUX.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstTrainingCamp;
        
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
            System.Uri resourceLocater = new System.Uri("/SpectatorFootball;component/windowsleague/trainingcampux.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WindowsLeague\TrainingCampUX.xaml"
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
            
            #line 21 "..\..\..\WindowsLeague\TrainingCampUX.xaml"
            this.btnStandings.Click += new System.Windows.RoutedEventHandler(this.btnStandings_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lblTrainingCampHeader = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.help_btn = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\WindowsLeague\TrainingCampUX.xaml"
            this.help_btn.Click += new System.Windows.RoutedEventHandler(this.help_btn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnTrainingCamp = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\WindowsLeague\TrainingCampUX.xaml"
            this.btnTrainingCamp.Click += new System.Windows.RoutedEventHandler(this.btnTrainingCamp_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lstTrainingCamp = ((System.Windows.Controls.ListView)(target));
            
            #line 36 "..\..\..\WindowsLeague\TrainingCampUX.xaml"
            this.lstTrainingCamp.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lstTrainingCamp_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

