   M S B A M L   `   `   `  ����C  ?WatcHive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WatcHive.ViewWatcHive.View  5      )    #WatcHive.View.GestionContenidosView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WatcHive.View     5      .��5	      � 
�������$��20q�6   .+�5
   
   � .+�5      �� $��0��6   $��10,10,10,60q�6)   $��2��6>   .+�5      �� -   5        $��search��5      $	��250��6(   $��30��64   $��0,0,0,10q�6@   5      &   System.Windows.Controls.DataGrid  5      -   5        $��tabla��5          AutoGenerateColumns  .     
IsReadOnly .    CanUserResizeRows .     CanUserResizeColumns .     HeadersVisibility$ Column=�5      $	��400��5      $
��Auto=�5      $
��Auto=�5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *id+6P     5      $ Nombre��6-   $
 auto 6X    6=   �� *nombre+6X     5      $ Estreno��6-   $
 auto 6_    6>   �� *fechaEstreno+6_     5      $ Descripción��6-   $
 auto 6b    6B   �� *descripcion+6b   5      5       5!      �� 5$   
   	 �� Orientation$	 
Horizontal=�6   $��Center=�5%      $��Bottom=�5&      $��0,0,0,10q�5'      .+�5(      �� -   5        
 ��Name$
 btnEliminar��5(      $��Eliminar��6*   $	��100��6=   $��31��6c      LoginButtonStyle#��[  6o   5)      5*      5+      