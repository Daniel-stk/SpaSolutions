   M S B A M L   `   `   `  ����U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fD2clr-namespace:MahApps.Metro;assembly=MahApps.MetroMahApps.Metro  5       SpaSolutions$   SpaSolutions.AccentStyleWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ;Controls,http://metro.mahapps.com/winfx/xaml/controls   5      ;
Converters*http://metro.mahapps.com/winfx/xaml/shared   5      >metro2clr-namespace:MahApps.Metro;assembly=MahApps.Metro   5        �� ClearTypeHint$  Enabled=�5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�5	       � Title$ Change App Style��5
       � SizeToContent$ WidthAndHeight=�5         AccentColorBrush(   "MahApps.Metro.Controls.MetroWindow   	GlowBrush# �   5       � 
ResizeMode$ CanResizeWithGrip=�5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   %    5      	   TitleTemplate	 5      �� 5   
   .R�5      �� $��CharacterEllipsis=�5      $��Center=�5      $��8 -1 0 0q�5        WindowTitleFontSize#���  5        DefaultFont#���  5      ��5      �� 
 �� RelativeSource
 �� *TemplatedParent+ �� Path$ Content� �� 	Converter /   )MahApps.Metro.Converters.ToUpperConverter  5      5       .��5#      �  5%   
    �� 
TargetType  MetroButton �� BasedOn �� Property �� Value��%�   '��       7  �� 5&      " ��6   8   .�5(      ��  � ��$ 23��5)      �� 5*       � ��$ 2q�5+      5,      5-       � RowDefinitions 5/   
   �� 50      $
��Auto �6   �� 51      $
��Auto �6   .+�54   
   �� $��0��6   $��5q�55      $��This Window��56      .��57      A�  A� Columns$ 5��6   .+�58      �� -   5        $
��Dark��58      59      �� 5:      -   5        $��Light��5:      5;      �� 5<      -   5        $
��Blue��5<      5=      �� 5>      -   5        $	��Red��5>      5?      �� 5@      -   5        $��Green��5@      5A      5B      5C      �� 5E   
   $��1��6   $��5q�5F      $��The Application��5G      .��5H      �  5I      �� 5J      $
��Auto �6$   �� 5K      $
��Auto �6$   �� 5L      $
��Auto �6$   .+�5O      A� $��0��6   $ 5��5P      .+�5Q      �� -   5        $
��Dark��5Q      5R      �� 5S      -   5        $��Light��5S      5T      �� 5U      -   5        $
��Blue��5U      5V      �� 5W      -	   5        $	��Red��5W      5X      �� 5Y      -
   5        $��Green��5Y      5Z      5[      A� 5]      $��1��6   $ 5��5^      .+�5_      �� -   5        $��Custom Theme��5_      5`      �� 5a      -   5        $��Custom Accent 1��5a      5b      �� 5c      -   5        $��Custom Accent 2��5c      5d      5e      A� 5g      $��2��6   $��5q�6+   $ 2��66    A� Rows$ 2��6B   .+�5h      �� $��Accents��6    $��0 0 2 0q�6/   $��Center=�6@   �� 5i      $��Colors��6    $��2 0 0 0q�6.   $��Center=�6?   �� 5k      -   5         ��Name$ AccentSelector��5k      $��0 0 2 0q�5l      $��Center=�5m          MahApps.Metro.ThemeManager   Accents#��Z 5o      ��5p      �� 5q      .R�5r   "   �� ��6,   �� *Name+5s      5u      �� 5w      -   5        $ ColorsSelector��5w      $��2 0 0 0q�5x      $��Center=�5y      ��5{      �� *Colors+��5|      �� 5}      .R�5~   "   �� ��6,   �� *Key+5      5�      5�      5�      5�      5�      5�      