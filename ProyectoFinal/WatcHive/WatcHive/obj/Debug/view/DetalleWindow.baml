   M S B A M L   `   `   `  ����C  ?WatcHive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WatcHive.ViewWatcHive.View  5   	   !    WatcHive.View.DetalleWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WatcHive.View     5   	   -   5          � Title$  DetalleWindow��5   	   $	��450��6   $	��925��6,    � WindowStyle$
 None=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency . .��5      �� ���	#FFF9F9F9 �� CornerRadius$ 30��6$      GradientBorderStyle#��[  66   $��0,0,0,0q�6c   ._�5   
   � $��20q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��10q�6%   
�������$��1q�6D   $ 10��6X   ._�5      �� -   5         ��Name$ 	imgPoster��5      $	��300��5      $	��450��5      $��UniformToFill=�5      $��10q�5      5      � 5!      $��1��6   $��30,10,10,10q�6#    � RowDefinitions 5"      �� 5#      $
��Auto �6$   �� 5$      $��* �6$   �� 5%      $
��Auto �6$   .+�5(      �� -   5        	 ��Name$	 	btnCerrar��5(        CloseButtonStyle#��[ 5)      $��X��5*      $��Right=�5,      $	��Top=�5-      $��0,0,0,5q�5.      �� 50      $��1��6   $	��Top=�6*   .+�51      �� -   5        
 ��Name$
 	txtTitulo��51       $��30(�52       $
��Bold"�53       $
��Wrap=�54       $��0,0,0,10q�55       �� 57      -   5        $
 
txtGeneros��57       $��14(�58       
�������$
��Wrap=�5:       $��0,0,0,10q�5;       �� 5=      -   5        $
 txtNotaOrTemporadas��5=       $��14(�5>       
�������$
��Wrap=�5@       $��0,0,0,10q�5A       �� 5C      $
��Auto=�6#   $	��220��6F   .��5D      �� -   5        $
 txtDescripcion��5D   $   $��16(�5E   $   $
��Wrap=�5F   $   $��CharacterEllipsis=�5G   $   5H      5I      �� 5L      $��2��6    �� Orientation$ 
Horizontal=�6*   $��Center=�6C   $��0,20,0,0q�6`   .+�5M      �� -   5        $��btnAgreagarVistos��5O      $��Agregar a vistos��5M      $	��150��5N      $��0,0,10,0q�5P        LoginButtonStyle#��[ 5Q       � Cursor$
 Hand��5R      ���#1D18545S      �� 5U      -	   5        $��btnAgregarPendiente��5X      $��Agregar a pendientes��5U      $	��200��5V      $��10q�5W      #��[ 5Y      $
 Hand��5Z      6+   5]      5^      5_      5`      5a      