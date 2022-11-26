﻿using System;
using System.Windows;
using System.Windows.Controls;


namespace KompasRingPlugin.Controls;

/// <summary>
/// Interaction logic for AdvancedTextbox.xaml
/// </summary>
public partial class AdvancedTextbox : UserControl
{
    public AdvancedTextbox()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty InputDataProperty = DependencyProperty.Register(
        nameof(InputData), typeof(string), typeof(AdvancedTextbox), new PropertyMetadata(default(string)));

    
    //public static readonly DependencyProperty InputDataProperty = DependencyProperty.Register(
    //    nameof(InputData), typeof(string), typeof(AdvancedTextbox),
    //    new FrameworkPropertyMetadata(null,
    //        FrameworkPropertyMetadataOptions.AffectsRender,
    //        new PropertyChangedCallback(OnContainElementChanged)));

    //private static void OnContainElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //{
    //    ((AdvancedTextbox)d).textBox.Text = (string)e.NewValue;
    //    //if (e.NewValue != null) StartAnimation(((AdvancedTextbox)d));
    //}

    public string InputData
    {
        get { return (string)GetValue(InputDataProperty); }
        set
        {
            SetValue(InputDataProperty, value);
        }
    }

    private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        InputData = textBox.Text;
    }
}