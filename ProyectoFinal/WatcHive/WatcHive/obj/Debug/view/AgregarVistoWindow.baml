   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   C ?WatcHive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WatcHive.ViewWatcHive.View 5   	   &    WatcHive.View.AgregarVistoWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -localclr-namespace:WatcHive.View     5   	   -   5          � Title$  AgregarVistoWindow��5	   	   $	��320��6$   $	��400��61    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency . .��5      �� ���	#FFF9F9F9 �� CornerRadius$ 30��6$      GradientBorderStyle#��[  66   $��0,0,0,0q�6c   ._�5   
   � $��20q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��0��6   $
��0,10q�6&   .+�5      �� $#��¿Cuándo viste el contenido?��6   $��0,0,0,5q�6?     FormLabelStyle#��[ 6P   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ 
fechaVista��5      
 �� Now	   SelectedDate#	 Z 61     DatePickerStyle#��[ 6\   5      �� 5!      $��1��6   $
��0,10q�6&   .+�5"      �� $#��¿Cómo te sentías ese día?��6   $��0,0,0,5q�6=   #��[ 6N   �� 5#      -   5        
 ��Name$
 comboEmocion��5#       �� SelectedIndex$ 0��61   .@�5$      �� $��Feliz��6#   �� 5%      $��Triste��6#   �� 5&      $��
Enfadado/a��6#   �� 5'      $��	Ansioso/a��6#   �� 5(      $��
Relajado/a��6#   �� 5)      $��
Motivado/a��6#   �� 5*      $��
Aburrido/a��6#   5+      5,      �� 5/      $��2��6   $
��0,15q�6&   .+�50      �� -   5         ��Name$ txtPuntuacion��50      $��Puntuación:��63   $��14(�6F   $��0,0,0,5q�6T   #��[ 6e   �� 51       �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�52      	� -   5         	� 	GroupName$ 
Puntuacion��52   "   $��1��69   $��5q�6E   6P   	� 53      -   5        $ 
Puntuacion��53   "   $��2��69   $��5q�6E   6P   	� 54      -   5        $ 
Puntuacion��54   "   $��3��69   $��5q�6E    d� 	IsChecked$
 TrueY�6P   6a   	� 55      -   5        $ 
Puntuacion��55   "   $��4��69   $��5q�6E   6P   	� 56      -	   5        $ 
Puntuacion��56   "   $��5��69   $��5q�6E   6P   57      58      �� 5;      $��4��6   $ 
Horizontal=�6&   $��Right=�6?   $��0,20,0,0q�6[   .+�5<      �� -
   5        $��Cancelar��5<      $��80��6,   $��0,0,10,0q�67     LoginButtonStyle#��[ 6`   �� 5=      -   5        $��Aceptar��5=      $��80��6+   #��[ 6L   5>      5?      5@      5A      