   M S B A M L   `   `   `  ����C  ?WatcHive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WatcHive.ViewWatcHive.View  5      '    !WatcHive.View.RecomendacionesView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WatcHive.View     5      .��5	      � ���#F2F2F2  � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��
50,20,0,20q�6.   $	��Top=�6B   .+�5      �� $��¿Cómo te sientes hoy?: ��6   $��0,0,5,0q�67      FormLabelStyle#��[  6H   $��Center=�6p   $
��Bold"�6�   �� 5      -   5         ��Name$ comboEmociones��5      $	��150��6/   $��Center=�6;   .@�5      �� $��Feliz��6   �� 5      $��Triste��6   �� 5      $��
Enfadado/a��6   �� 5      $��	Ansioso/a��6   �� 5      $��
Relajado/a��6   �� 5      $��
Motivado/a��6   �� 5      $��
Aburrido/a��6   5      �� 5      $ 
Horizontal=�6   $��20,0,0,0q�62   $��Center=�6D   .+�5      	� -   5         	�Name$ radioPeliculas��5      $��
Películas��66    	� 	GroupName$ TipoContenido��6J    d� 	IsChecked$
 TrueY�6d   $��0,0,10,0q�6u   $
��Bold"�6�   $��
Montserrat)�6�   	� 5      -   5        $ radioSeries��5      $��Series��63   $ TipoContenido��6D   $
��Bold"�6^   $��
Montserrat)�6p   5      �� 5       -   5        $��
Recomendar��5         LoginButtonStyle#��[ 5!      $
��10,5q�5"      $
��30,0q�5#      5$      5%      �� 5&   
   $��1��6   $
��Auto=�6$   $��Disabled=�6H   $��0,20,0,0q�6q   .��5'      �� $ Vertical=�6   $��0,10,0,0q�60   .+�5(      �� $��Hoy te recomendamos...��6   $��20(�6:   $
��Bold"�6H   $��0,0,0,10q�6Z   $��Center=�6l   � 5*      -   5         �Name$ wrapRecomendaciones��5*      $��Center=�69   5+      5,      5-      5.      