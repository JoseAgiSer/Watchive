   M S B A M L   `   `   `  ����C  ?WatcHive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WatcHive.ViewWatcHive.View  5      '    !WatcHive.View.GestionUsuariosView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WatcHive.View     5      .��5
      � 
�������$��20q�6   .+�5   
   �   � ColumnDefinitions  5      �� 5      $
��101* �6#   �� 5      $	��59* �6#   .+�5      �� $��0��6   $��10,10,10,60q�6)   .+�5      �� -   5        $��search��5      $	��250��6(   $��30��64   $��0,0,0,10q�6@   5      &   System.Windows.Controls.DataGrid  5      -   5        $��tabla��5         AutoGenerateColumns .     
IsReadOnly .    CanUserResizeRows .     CanUserResizeColumns .     HeadersVisibility$ Column=�5      $	��400��5      $
��Auto=�5      $
��Auto=�5          Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ Nombre de usuario��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6e   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6H   �� *
username+6e     5$      $ Nombre��6-   $
 auto 6X   	 6=   �� *nombre+6X     5%      $ 	Apellidos��6-   $
 auto 6^   	 6@   �� *	apellidos+6^     5&      $ 
Nacimiento��6-   $
 auto 6e   	 6A   �� *fechaNacimiento+6e     5'      $ Hijos��6-   $
 auto 6Y   	 6<   �� *
numHijos+6Y     5(      $ Correo electronico��6-   $
 auto 6c   	 6I   �� *email+6c     5)      $ Contraseña��6-   $
 auto 6^   	 6A   �� *
password+6^   5+      5,      �� 5/      $��1��6   $
��Auto=�6+   $��10,10,10,60q�6N   .��50      �� $	��320��6   $
��Left=�6)   .+�52      �� $��Nombre��6       FormLabelStyle#��[  6.   �� 53      -   5        $��	nombrebox��53        IconTextBoxStyle#��[ 6/   $	��300��6Y   �� 55      $��	Apellidos��6    #��[  61   �� 56      -   5        $��apellidosbox��56      #��[ 62   $	��300��6\   �� 58      $��Fecha de nacimiento��6    #��[  6;   (  "System.Windows.Controls.DatePicker  59      -   5        $��birthdaybox��59   !     DatePickerStyle#��[ 64   $	��300��6]   �� 5;      $��Número de hijos��6    #��[  67   �� 5<      -   5        $��numhijosbox��5<      #��[ 61   $	��300��5=      
 �� 	MaxLength$
 2��6*   $��0��68   5>      �� 5@      $��Correo electrónico��6    #��[  6:   �� 5A      -   5        $��emailbox��5A      #��[ 6.   $	��300��6X   �� 5C      $��Nombre de usuario��6    #��[  69   �� 5D      -   5        $��usernamebox��5D      #��[ 61   $	��300��6[   �� 5F      $��Contraseña��6    #��[  62   G� 5G      -	   5        $��passbox��5G   "     IconPasswordBoxStyle#��[ 61   $	��300��6_   5H      5I      5J      �� 5M   
    �� Orientation$ 
Horizontal=�6   $��Center=�5N      $��Bottom=�5O      $��0,0,0,10q�5P      .+�5Q      �� -
   5        $��	btnAnadir��5Q      $��   Añadir   ��6&   $
��10,0q�6=   $
��auto��6K     LoginButtonStyle#��[ 5R      6?   �� 5S      -   5        $��btnEliminar��5S      $��Eliminar��6(   $
��10,0q�6;   $	��100��6I   #��[ 5T      6?   �� 5U      -   5        $��btnModificar��5U      $��	Modificar��6)   $
��10,0q�6=   $	��100��6K   #��[ 5V      6?   5W      5X      5Y      