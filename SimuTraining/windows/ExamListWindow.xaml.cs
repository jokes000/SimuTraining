﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SimuTraining.util;

namespace SimuTraining.windows
{
    /// <summary>
    /// ExamListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExamListWindow : Window
    {
        public ExamListWindow()
        {
            InitializeComponent();

            DirectoryInfo folder = new DirectoryInfo("exam");
            //FileInfo[] files = folder.GetFiles();
            //Array.Sort(files, new FileSortHelper(SortOption.FileName));

            foreach (FileInfo file in folder.GetFiles())
            {
                Border border = new Border() { SnapsToDevicePixels = true, BorderBrush = Brushes.Transparent, BorderThickness = new Thickness(4), CornerRadius = new CornerRadius(5), HorizontalAlignment = HorizontalAlignment.Center };
                border.Effect = new DropShadowEffect() { Color = Colors.Black, BlurRadius = 16, ShadowDepth = 0, Opacity = 1 };

                Image img = new Image() { Stretch = Stretch.None };
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri("res/exam.png", UriKind.Relative);
                bi.EndInit();
                bi.Freeze();
                img.Source = bi;

                TextBlock tb = new TextBlock() { Text = System.IO.Path.GetFileNameWithoutExtension(file.Name), Margin = new Thickness(0, 10, 0, 0), TextWrapping = TextWrapping.Wrap, TextAlignment = TextAlignment.Center};
                StackPanel panel = new StackPanel() { Orientation = Orientation.Vertical, HorizontalAlignment = HorizontalAlignment.Center };
                panel.Children.Add(img);
                panel.Children.Add(tb);

                border.Child = panel;

                ListBoxItem item = new ListBoxItem() { Margin = new Thickness(50, 10, 50, 10) };
                item.Content = border;
                item.Tag = tb;

                item.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(onExamClick);

                //item.Width = SystemParameters.WorkArea.Width / 4;

                ExamListBox.Items.Add(item);
            }
        }

        private void mainPage_Click(object sender, RoutedEventArgs e)
        {
            Window main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("您确定要退出本程序吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void onExamClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem item = (sender as ListBoxItem);
            //ListBoxItem item = ExamListBox.SelectedItem as ListBoxItem;
            String name = (item.Tag as TextBlock).Text;

            try {
                String fullpath = System.IO.Path.GetFullPath("exam/" + name + ".xls");
                File.Move(fullpath, fullpath);
            } catch (Exception) {
                MessageBox.Show("文件正被其他程序占用，请关闭占用程序后打开");
                return;
            }

            Window exam = new ExamWindow(name);
            exam.Show();
            this.Close();
        }

        private void ScrollViewer_PreviewMouseWheel_1(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void ExamListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e) {
            ListBoxItem item = ExamListBox.SelectedItem as ListBoxItem;
            String name = (item.Tag as TextBlock).Text;

            try {
                String fullpath = System.IO.Path.GetFullPath("exam/" + name + ".xls");
                File.Move(fullpath, fullpath);
            } catch (Exception) {
                MessageBox.Show("文件正被其他程序占用，请关闭占用程序后打开");
                return;
            }

            Window exam = new ExamWindow(name);
            exam.Show();
            this.Close();
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                if (MessageBox.Show("您确定要退出本次考试回到试题选择吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                    Window examList = new ExamListWindow();
                    examList.Show();
                    this.Close();
                }
            }
            if (e.Key == Key.Escape) {
                if (MessageBox.Show("您确定要退出本程序吗？", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                    this.Close();
                }
            }
        }

    }
}
